using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{
    /// <summary>
    ///  This class would be used when user tries to insert a record which already exists in the database
    ///  Default Usage : Create Object of this class and pass it to BadRequest() function;
    ///  E.g : BadRequest(new ErrorRecordRepeat());
    /// </summary>
    public class ErrorRecordRepeat : IError
    {
        public int error_code => 1000;
        public string description => "The Record you are trying to insert is already present in database";
    }
}
