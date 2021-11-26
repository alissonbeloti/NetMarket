using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities.OrdemCompra
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(ProdutoItemOrdenado itemOrdenado, decimal preco, int quantidade)
        {
            ItemOrdenado = itemOrdenado;
            Preco = preco;
            Quantidade = quantidade;
        }

        public ProdutoItemOrdenado ItemOrdenado { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

    }
}
