using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMarket.WebApi.Dto.Login
{
    public class EnderecoDto
    {
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string CodigoPostal { get; set; }
        public string UsuarioId { get; set; }
    }
}
