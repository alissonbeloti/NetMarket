using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMarket.WebApi.Dto.Login
{
    public class RegistrarDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Password { get; set; }
        public string Imagem  { get; set; }
    }
}
