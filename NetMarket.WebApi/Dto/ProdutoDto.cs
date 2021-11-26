using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMarket.WebApi.Dto
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public int MarcaId { get; set; }
        public string MarcaNome { get; set; }

        public decimal Preco { get; set; }
        public string Imagem { get; set; }
    }
}
