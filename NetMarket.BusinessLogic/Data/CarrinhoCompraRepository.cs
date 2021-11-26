using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;

using StackExchange.Redis;

namespace NetMarket.BusinessLogic.Data
{
    public class CarrinhoCompraRepository : ICarrinhoCompraRepository
    {
        private readonly IDatabase database;

        public CarrinhoCompraRepository(IConnectionMultiplexer redis)
        {
            this.database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCarrinhoCompraAsync(string carrinhoId)
        {
            return await database.KeyDeleteAsync(carrinhoId);
            
        }

        public async Task<CarrinhoCompra> GetCarrinhoCompraAsync(string carrinhoId)
        {
            var data = await database.StringGetAsync(carrinhoId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CarrinhoCompra>(data);
        }

        public async Task<CarrinhoCompra> UpdateCarrinhoCompraAsync(CarrinhoCompra carrinhoCompra)
        {
            var status = await database.StringSetAsync(carrinhoCompra.Id, JsonSerializer.Serialize(carrinhoCompra), TimeSpan.FromDays(30));
            if (!status) return null;

            return await GetCarrinhoCompraAsync(carrinhoCompra.Id);
        }
    }
}
