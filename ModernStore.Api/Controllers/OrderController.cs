using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Infra.Transactions;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Commands.Inputs;
using Microsoft.AspNetCore.Authorization;

namespace ModernStore.Api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderCommandHandler _handler;

        public OrderController(IUow uow, OrderCommandHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("order")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]RegisterOrderCommand command)
        {
            var customer = User.Identity.Name;

            var result = _handler.Handle(command); ;
            return await Response(result, _handler.Notifications);
        }
    }
}