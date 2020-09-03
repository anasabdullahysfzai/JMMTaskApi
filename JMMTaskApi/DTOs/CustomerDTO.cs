using System;
using System.Collections.Generic;

namespace JMMTaskApi
{
    /// <summary>
    ///  Data Transfer Object class to send Customer Related Details in Response 
    /// </summary>
    public partial class CustomerDTO
    {
        public int CId { get; set; }
        public string CName { get; set; }
        public string CAddress { get; set; }
        public string CPhone { get; set; }
    }
}
