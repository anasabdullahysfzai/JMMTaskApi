using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.Controllers.Errors
{

    /// <summary>
    ///  Interface for returning error code and error description for specific api request
    ///  DEFAULT USAGE: Implement this interface in a class and set error code and description according to your need
    /// </summary>

    
    interface IError
    {
        public Int32 error_code { get; }
        public string description { get; }
    }
}