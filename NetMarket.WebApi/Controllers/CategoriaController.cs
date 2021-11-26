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
    
    public class CategoriaController : BaseApiController
    {
        private readonly IGenericRepository<Categoria> categoriaRepository;
        public CategoriaController(IGenericRepository<Categoria> categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Categoria>>> Get()
        {
            return Ok(await categoriaRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Categoria>>> Get(int id)
        {
            return Ok(await categoriaRepository.GetByIdAsync(id));
        }
    }
}
