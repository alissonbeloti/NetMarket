using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using NetMarket.Core.Entities;

namespace NetMarket.BusinessLogic.Data
{
    public class SegurancaDbContextData
    {
        public static async Task SeedUserAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nome = "Alisson",
                    SobreNome = "Beloti",
                    UserName = "alissonbeloti",
                    Email = "alisson@beloti.com",
                    EmailConfirmed = true,
                    Endereco = new Endereco
                    {
                        Rua = "Rua ministro 346",
                        Cidade = "Campinas",
                        CodigoPostal = "28985",
                    }
                };

                await userManager.CreateAsync(usuario, "Alisson123!@");
            }
            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole
                {
                    Name = "ADMIN"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
