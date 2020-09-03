using System;
using System.Collections.Generic;

namespace JMMTaskApi
{
    /// <summary>
    ///  Data Transfer Object class to send supplier related Details in Response
    /// </summary>
    public partial class SupplierDTO
    {

        public int SId { get; set; }
        public string SName { get; set; }
        public string SAddress { get; set; }
        public string SPhone { get; set; }
        public string SIban { get; set; }
    }
}
