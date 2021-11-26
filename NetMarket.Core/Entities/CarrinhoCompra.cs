using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities
{
    public class CarrinhoCompra
    {
        public CarrinhoCompra() { }
        public CarrinhoCompra(string id)
        {
            Id = id;
        }
        public string  Id { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();

    }
}
