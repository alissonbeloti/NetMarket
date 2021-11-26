using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMarket.WebApi.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }
        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "O Request enviado possuí erros.",
                401 => "Não tem autorização para esse recurso.",
                404 => "Recurso não encontrado.",
                500 => "Erro no servidor.",
            };
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
