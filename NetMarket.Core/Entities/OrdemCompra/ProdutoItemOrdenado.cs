using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities.OrdemCompra
{
    public class ProdutoItemOrdenado
    {
        public ProdutoItemOrdenado()
        {
        }

        public ProdutoItemOrdenado(int produtoItemId, string nomeProduto, string imagem)
        {
            ProdutoItemId = produtoItemId;
            NomeProduto = nomeProduto;
            Imagem = imagem;
        }

        public int ProdutoItemId { get; set; }
        public string NomeProduto { get; set; }
        public string Imagem { get; set; }

    }
}
