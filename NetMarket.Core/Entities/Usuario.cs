using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace NetMarket.Core.Entities
{
    public class Usuario: IdentityUser
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public Endereco Endereco { get; set; }

        public string Imagem { get; set; }
    }
}
