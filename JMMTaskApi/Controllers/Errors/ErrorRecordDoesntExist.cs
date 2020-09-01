using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{

    
    public class ErrorRecordDoesntExist : IError
    {
        /**
         * Default Usage : Create Object of this class and pass it to NotFound() function;
         * E.g : NotFound(new ErrorRecordDoesntExist());
         * **/

        private Int32 ERR_CODE = 1001;
        private string ERR_DESC = "The record against the details you provided doesnt exist";


        public int error_code => ERR_CODE;

        public string description => ERR_DESC;
    }
}
