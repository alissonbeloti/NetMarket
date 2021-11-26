using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;

namespace NetMarket.Core.Interfaces
{
    public interface ICarrinhoCompraRepository
    {
        Task<CarrinhoCompra> GetCarrinhoCompraAsync(string carrinhoId);
        Task<CarrinhoCompra> UpdateCarrinhoCompraAsync(CarrinhoCompra carrinhoCompra);

        Task<bool> DeleteCarrinhoCompraAsync(string carrinhoId);
    }
}
