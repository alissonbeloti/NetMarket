using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;

namespace NetMarket.Core.Specification
{
    public class ProdutoForCountingSpecifications : BaseSpecification<Produto>
    {
        public ProdutoForCountingSpecifications(ProdutoSpecificationParams produtoParams)
            : base(x =>
            (string.IsNullOrEmpty(produtoParams.Search) || x.Nome.Contains(produtoParams.Search)) &&
            (!produtoParams.Marca.HasValue || x.MarcaId == produtoParams.Marca) &&
                       (!produtoParams.Categoria.HasValue || x.CategoriaId == produtoParams.Categoria))
        { }

    }
}
