using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using NetMarket.Core.Entities;

namespace NetMarket.BusinessLogic.Data
{
    public class MarketDbContextData
    {
        public static async Task CarregarDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Marcas.Any())
                {
                    var marcaData = File.ReadAllText("../NetMarket.BusinessLogic/Data/Mass/marca.json");
                    var marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData);

                    foreach (var m in marcas)
                    {
                        context.Marcas.Add(m);
                    }
                    await context.SaveChangesAsync();

                    var categoriaData = File.ReadAllText("../NetMarket.BusinessLogic/Data/Mass/categoria.json");
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(marcaData);

                    foreach (var c in categorias)
                    {
                        context.Categorias.Add(c);
                    }
                    await context.SaveChangesAsync();

                    var produtosData = File.ReadAllText("../NetMarket.BusinessLogic/Data/Mass/producto.json");
                    var produtos = JsonSerializer.Deserialize<List<Produto>>(produtosData);

                    foreach (var p in produtos)
                    {
                        context.Produtos.Add(p);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(e.Message);
            }
        }
        
    }
}
