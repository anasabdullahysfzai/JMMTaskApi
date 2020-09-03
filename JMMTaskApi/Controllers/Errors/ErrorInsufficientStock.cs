using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{
    /// <summary>
    /// This class would be used when user wants to buy or sell more products than there available stock
    /// Default Usage : Create Object of this class and pass it to BadRequest() function;
    /// E.g : BadRequest(new ErrorInsufficientStock());
    /// </summary>
    public class ErrorInsufficientStock : IError
    {
        public int error_code => 1002;
        public string description => "Insufficient Product Stock to fulfill this request";
    }
}
