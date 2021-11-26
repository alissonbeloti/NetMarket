using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities.OrdemCompra
{
    public enum OrdemStatus
    {
        [EnumMember(Value = "Pendente")]
        Pendente,
        [EnumMember(Value = "O Pagamento foi recebido.")]
        PagoRecebido,
        [EnumMember(Value = "O Pagamento teve erros.")]
        PagoFalhou,
    }
}
