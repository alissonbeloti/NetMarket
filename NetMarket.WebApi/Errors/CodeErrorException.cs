using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMarket.WebApi.Errors
{
    public class CodeErrorException: CodeErrorResponse
    {
        public CodeErrorException(int statusCode, string message = null, string details = null) : base (statusCode, message)
        {
            this.Details = details;
        }
        public string Details { get; set; }
    }
}
