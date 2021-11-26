using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;
using NetMarket.Core.Specification;
using NetMarket.WebApi.Dto;
using NetMarket.WebApi.Errors;

namespace NetMarket.WebApi.Controllers
{

    public class ProdutoController : BaseApiController
    {
        private readonly IGenericRepository<Produto> produtoRepository;
        private readonly IMapper mapper;

        public ProdutoController(IGenericRepository<Produto> produtoRepository,
            IMapper mapper)
        {
            this.produtoRepository = produtoRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProdutoDto>>> Get([FromQuery] ProdutoSpecificationParams produtoParams)
        {
            var spec = new ProdutoWithCategoriaAndMarcaSpecification(produtoParams);
            var produtos = await produtoRepository.GetAllWithSpecAsync(spec);
            var specCount = new ProdutoForCountingSpecifications(produtoParams);
            var totalProdutos = await produtoRepository.CountAsync(specCount);
            var totalPage = Convert.ToInt32(Math.Ceiling((decimal)(totalProdutos / produtoParams.PageSize)));

            var data = mapper.Map<IReadOnlyList<Produto>, IReadOnlyList<ProdutoDto>>(produtos);

            return Ok(
                new Pagination<ProdutoDto>
                {
                    Data = data,
                    Count = totalProdutos,
                    PageCount = totalPage,
                    PageIndex = produtoParams.PageIndex,
                    PageSize = produtoParams.PageSize,
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            // spec = deve incluir a lógica da condição da consulta e também relação entre 
            // as entidades
            var spec = new ProdutoWithCategoriaAndMarcaSpecification(id);
            var produto = await produtoRepository.GetByIdWithSpecAsync(spec);
            if (produto == null) return NotFound(new CodeErrorResponse(404, "O produto não existe."));
            var produtoDto = mapper.Map<Produto, ProdutoDto>(produto);
            return Ok(produtoDto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            var resultado = await this.produtoRepository.Add(produto);
            if (resultado == 0)
            {
                throw new Exception("Não inseriu o produto");
            }

            return Ok(produto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Put(int id, Produto produto)
        {
            produto.Id = id;
            var resultado = await this.produtoRepository.Update(produto);
            if (resultado == 0)
            {
                throw new Exception("Não alterou o produto");
            }

            return Ok(produto);
        }

    }
}
