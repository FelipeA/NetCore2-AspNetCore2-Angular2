using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernStore.Api.Security
{
    public class TokenOptions
    {
        // Quem está pedindo o token
        public string Issuer { get; set; }
        // O que está sendo pedido
        public string Subject { get; set; }
        // Quem está recebendo o token
        public string Audience { get; set; }
        // Pra verificar a data para não pedir um token antes de "agora"
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;
        //Data em que o token foi pedido
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(2);
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }
    }
}
