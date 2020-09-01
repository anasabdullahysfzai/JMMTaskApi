using System;
using System.Collections.Generic;

namespace JMMTaskApi
{
    public partial class SupplierDTO
    {
        public SupplierDTO()
        {

        }

        public int SId { get; set; }
        public string SName { get; set; }
        public string SAddress { get; set; }
        public string SPhone { get; set; }
        public string SIban { get; set; }
    }
}
