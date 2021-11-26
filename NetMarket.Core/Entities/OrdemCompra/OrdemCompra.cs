using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities.OrdemCompra
{
    public class OrdemCompras : BaseEntity
    {
        public OrdemCompras()
        {
        }

        public OrdemCompras(string compradorEmail, Endereco enderecoEnvio, TipoEnvio tipoEnvio, IReadOnlyList<OrderItem> orderItems, decimal subtotal)
        {
            CompradorEmail = compradorEmail;
            EnderecoEnvio = enderecoEnvio;
            TipoEnvio = tipoEnvio;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string CompradorEmail { get; set; }
        public DateTimeOffset DataHoraCompra { get; set; } = DateTimeOffset.Now;
        public Endereco EnderecoEnvio { get; set; }
        public TipoEnvio TipoEnvio { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrdemStatus Status { get; set; } = OrdemStatus.Pendente;

        public string IntencaoPagamentoId { get; set; }
        public decimal GetTotal() { return Subtotal + TipoEnvio.Preco; }
    }
}
