using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Infra.Transactions;
using ModernStore.Domain.Commands.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace ModernStore.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerCommandHandler _handler;

        public CustomerController(IUow uow, CustomerCommandHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("customer")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerCommand command)
        {
            var result = _handler.Handle(command); ;
            return await Response(result, _handler.Notifications);
        }
    }
}