using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nome { get; set; }
        //public virtual IEnumerable<Produto> Produtos { get; set; }
    }
}
