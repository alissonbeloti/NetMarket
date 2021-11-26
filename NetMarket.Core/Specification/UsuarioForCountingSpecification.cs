using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;

namespace NetMarket.Core.Specification
{
    public class UsuarioForCountingSpecification : BaseSpecification<Usuario>
    {
        public UsuarioForCountingSpecification(UsuarioSpecificationParams usuarioParams)
            : base(x =>
               (string.IsNullOrEmpty(usuarioParams.Search) || (x.Nome.Contains(usuarioParams.Search))) &&
               (string.IsNullOrEmpty(usuarioParams.Nome) || (x.Nome.Contains(usuarioParams.Nome))) &&
               (string.IsNullOrEmpty(usuarioParams.SobreNome) || (x.SobreNome.Contains(usuarioParams.SobreNome)))
            )
        {

        }
    }
}
