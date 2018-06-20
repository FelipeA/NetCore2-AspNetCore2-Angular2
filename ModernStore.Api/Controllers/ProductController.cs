using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ModernStore.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("products")]
        [AllowAnonymous]
        //[Authorize(Policy = "Admin")]
        public IActionResult Get()
        {
            return Ok(_repository.Get());
        }

        [HttpGet]
        [Route("produtos/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok($"testou! {id}");
        }

        [HttpPost]
        [Route("produtos")]
        public IActionResult Post()
        {
            return Ok($"Criando novo produto!");
        }
    }
}