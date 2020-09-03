using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{
    /// <summary>
    ///  This class would be used when user wants to fetch a record which doesnt exist in database
    ///  Default Usage : Create Object of this class and pass it to NotFound() function; 
    ///  <example> <code> NotFound(new ErrorRecordDoesntExist()); </code> </example>
    /// </summary>
    public class ErrorRecordDoesntExist : IError
    {
        public int error_code => 1001;
        public string description => "The record against the details you provided doesnt exist";
    }
}
