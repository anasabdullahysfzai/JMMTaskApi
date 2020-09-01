using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{
    public class ErrorRecordRepeat : IError
    {
        /**
         * Default Usage : Create Object of this class and pass it to BadRequest() function;
         * E.g : BadRequest(new ErrorRecordRepeat());
         * **/

        private Int32 ERROR_CODE = 1000;
        private string ERROR_DESC = "The Record you are trying to insert is already present in database";

        public ErrorRecordRepeat()
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
