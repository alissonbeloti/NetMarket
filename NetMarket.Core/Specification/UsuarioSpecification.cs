using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;

namespace NetMarket.Core.Specification
{
    public class UsuarioSpecification: BaseSpecification<Usuario>
    {
        public UsuarioSpecification(UsuarioSpecificationParams usuarioParams)
            : base(x => (string.IsNullOrEmpty(usuarioParams.Search) || (x.Nome.Contains(usuarioParams.Search))) &&
                (string.IsNullOrEmpty(usuarioParams.Nome) || (x.Nome.Contains(usuarioParams.Nome))) &&
                (string.IsNullOrEmpty(usuarioParams.SobreNome) || (x.SobreNome.Contains(usuarioParams.SobreNome)))
            )
        {
            ApplyPaging(usuarioParams.PageSize * (usuarioParams.PageIndex - 1), usuarioParams.PageSize);

            if (!string.IsNullOrEmpty(usuarioParams.Sort))
            {
                switch (usuarioParams.Sort)
                {
                    case "nomeAsc":
                        AddOrderBy(u => u.Nome);
                        break;
                    case "nomeDesc":
                        AddOrderByDescending(u => u.Nome);
                        break;
                    case "emailAsc":
                        AddOrderBy(u => u.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(u => u.Email);
                        break;
                    default:
                        AddOrderBy(u => u.Nome);
                        break;
                }
            }
        }
    }
}
