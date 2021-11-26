using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities.OrdemCompra
{
    public class TipoEnvio: BaseEntity
    {
        public string Nome { get; set; }
        public string DeliveryTime { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
