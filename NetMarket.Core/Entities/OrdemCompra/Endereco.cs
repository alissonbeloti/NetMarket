using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMarket.Core.Entities.OrdemCompra
{
    public class Endereco
    {
        public Endereco()
        {
        }

        public Endereco(string departamento, string rua, string cidade, string codigoPostal)
        {
            Departamento = departamento;
            Rua = rua;
            Cidade = cidade;
            CodigoPostal = codigoPostal;
        }

        public string Departamento { get; set; }

        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string CodigoPostal { get; set; }
        
    }
}
