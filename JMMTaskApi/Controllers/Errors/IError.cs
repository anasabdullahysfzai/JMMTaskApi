using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{
    /**
     * Interface for returning error code and description for api
     * 
     * DEFAULT USAGE: Implement this interface in a class and set error code and description according to your need 
     * **/

    interface IError
    {
        public Int32 error_code { get; }
        public string description { get; }
    }
}
