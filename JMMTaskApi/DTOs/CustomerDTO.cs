using System;
using System.Collections.Generic;

namespace JMMTaskApi
{
    public partial class CustomerDTO
    {
        public CustomerDTO()
        {

        }

        public int CId { get; set; }
        public string CName { get; set; }
        public string CAddress { get; set; }

        public string CPhone { get; set; }
    }
}
