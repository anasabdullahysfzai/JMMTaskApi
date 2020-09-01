using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{
    public class ErrorInsufficientStock : IError
    {
        /**
        * Default Usage : Create Object of this class and pass it to BadRequest() function;
        * E.g : BadRequest(new ErrorInsufficientStock());
        * **/

        private Int32 ERROR_CODE = 1002;
        private string ERROR_DESC = "Insufficient Stock to fulfill this request";

        public ErrorInsufficientStock()
        {

        }

        public Int32 error_code
        {
            get
            {
                return ERROR_CODE;
            }
        }
        public string description
        {
            get
            {
                return ERROR_DESC;
            }
        }
    }
}
