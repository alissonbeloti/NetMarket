using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;

namespace NetMarket.Core.Specification
{
    public class ProdutoWithCategoriaAndMarcaSpecification : BaseSpecification<Produto>
    {
        public ProdutoWithCategoriaAndMarcaSpecification(ProdutoSpecificationParams produtoParams)
            : base (x => 
            (string.IsNullOrEmpty(produtoParams.Search) || x.Nome.Contains(produtoParams.Search)) &&
            (!produtoParams.Marca.HasValue || x.MarcaId == produtoParams.Marca) && 
            (!produtoParams.Categoria.HasValue || x.CategoriaId == produtoParams.Categoria))
        {
            Relacoes();
            //ApplyPaging(0, 5);
            ApplyPaging(produtoParams.PageSize * (produtoParams.PageIndex - 1), produtoParams.PageSize);

            if (!string.IsNullOrEmpty(produtoParams.Sort))
            {
                switch (produtoParams.Sort)
                {
                    case "precoAsc":
                        AddOrderBy(p => p.Preco);
                        break;
                    case "precoDesc":
                        AddOrderByDescending(p => p.Preco);
                        break;
                    case "descricaoAsc":
                        AddOrderBy(p => p.Descricao);
                        break;
                    case "descricaoDesc":
                        AddOrderByDescending(p => p.Descricao);
                        break;
                    case "nomeoAsc":
                        AddOrderBy(p => p.Nome);
                        break;
                    case "nomeDesc":
                        AddOrderByDescending(p => p.Nome);
                        break;
                    default:
                        AddOrderBy(p => p.Nome);
                        break;
                }
            }
        }


        public ProdutoWithCategoriaAndMarcaSpecification(int id) : base(x => x.Id == id)
        {
            Relacoes();
        }
        private void Relacoes()
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);
        }

    }
}
