using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;

namespace NetMarket.WebApi.Controllers
{
   
    public class MarcaController : BaseApiController
    {
        private readonly IGenericRepository<Marca> marcaRepository;
        public MarcaController(IGenericRepository<Marca> marcaRepository)
        {
            this.marcaRepository = marcaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Categoria>>> Get()
        {
            return Ok(await marcaRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Categoria>>> Get(int id)
        {
            return Ok(await marcaRepository.GetByIdAsync(id));
        }
    }
}
