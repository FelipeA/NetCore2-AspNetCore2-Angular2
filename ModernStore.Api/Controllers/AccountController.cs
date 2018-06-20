using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Infra.Transactions;
using ModernStore.Api.Security;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Entities;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using FluentValidator;
using System.IdentityModel.Tokens.Jwt;

namespace ModernStore.Api.Controllers
{
    public class AccountController : BaseController
    {
        private Customer _customer;
        private readonly ICustomerRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        public AccountController(IOptions<TokenOptions> jwtOptions, IUow uow, ICustomerRepository repository) : base(uow)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> Post([FromForm]AuthenticateUserCommand command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário e/ou senha inválida!") });

            var identity = await GetClaims(command);

            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário e/ou senha inválida!") });

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.username),
                new Claim(JwtRegisteredClaimNames.NameId, command.username),
                new Claim(JwtRegisteredClaimNames.Email, command.username),
                new Claim(JwtRegisteredClaimNames.Sub, command.username),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("ModernStore"),
                identity.FindFirst("Teste")
            };

            //User.Identity.Name
            //User.Claims

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _customer.Id,
                    name = $"{_customer.FirstName} {_customer.LastName}",
                    email = _customer.Email.Address,
                    username = _customer.User.Username
                }
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(response);
        }

        // Tratar erros do ModernStore.Api.Security.TokenOptions
        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(options.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(options.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(options.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            // Verificar Padrão DomainServices

            var customer = _repository.GetByUsername(command.username);

            if (customer == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if (!customer.User.Authenticate(command.username, command.password))
                return Task.FromResult<ClaimsIdentity>(null);

            _customer = customer;

            return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(customer.User.Username, "Token"),
                new[] {
                    new Claim("ModernStore", "User"),
                    new Claim("Teste", "123")
                }));
        }
    }
}