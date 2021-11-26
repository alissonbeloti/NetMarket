using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;

namespace NetMarket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoCompraController : BaseApiController
    {
        private readonly ICarrinhoCompraRepository carrinhoCompraRepository;

        public CarrinhoCompraController(ICarrinhoCompraRepository carrinhoCompraRepository)
        {
            this.carrinhoCompraRepository = carrinhoCompraRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarrinhoCompra>> GetCarrinhoById(string id)
        {
            var carrinho = await this.carrinhoCompraRepository.GetCarrinhoCompraAsync(id);

            return Ok(carrinho ?? new CarrinhoCompra(id));
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoCompra>> UpdateCarrinhoCompra(CarrinhoCompra carrinhoParametro)
        {
            var carrinhoAtualizado = await this.carrinhoCompraRepository.UpdateCarrinhoCompraAsync(carrinhoParametro);
            return Ok(carrinhoAtualizado);
        }

        [HttpDelete]
        public async Task<ActionResult<CarrinhoCompra>> DeleteCarrinhoCompra(string id)
        {
            var ok = await this.carrinhoCompraRepository.DeleteCarrinhoCompraAsync(id);
            return Ok(ok);
        }
    }
}
