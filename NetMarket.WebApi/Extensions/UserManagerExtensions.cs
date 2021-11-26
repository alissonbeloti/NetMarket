using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NetMarket.Core.Entities;

namespace NetMarket.WebApi.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<Usuario> BuscarUsuarioComEnderecoAsync(this UserManager<Usuario> input, ClaimsPrincipal usr)
        {
            var email = usr?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usuario = await input.Users.Include(u => u.Endereco).SingleOrDefaultAsync(u => u.Email == email);

            return usuario;
        }

        public static async Task<Usuario> BuscarUsuarioAsync(this UserManager<Usuario> input, ClaimsPrincipal usr)
        {
            var email = usr?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usuario = await input.Users.SingleOrDefaultAsync(u => u.Email == email);

            return usuario;
        }
    }
}
