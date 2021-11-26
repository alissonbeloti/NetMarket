using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities
{
    public class Produto: BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        public decimal Preco { get; set; }
        public string  Imagem { get; set; }
    }
}
