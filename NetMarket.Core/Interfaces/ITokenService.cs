using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;

namespace NetMarket.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario, IList<string> roles);
    }
}
