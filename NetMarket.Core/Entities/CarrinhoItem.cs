using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Imagem { get; set; }

        public string Marca { get; set; }
        public string Categoria { get; set; }
    }
}
