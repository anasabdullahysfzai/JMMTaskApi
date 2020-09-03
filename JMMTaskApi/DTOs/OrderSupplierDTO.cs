using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.DTOs
{
    /// <summary>
    ///  Data Transfer Object class used as an object to fullfill Purchase Invoice Request from Supplier
    /// </summary>

    public class OrderSupplierDTO
    {
        public int OId { get; set; }

        public int? SId { get; set; }

        public string SName { get; set; }

        public string SAddress { get; set; }

        public string SPhone { get; set; }

        public string SIban { get; set; }

        public DateTime ODate { get; set; }

        public string OType { get; set; }

        public IList<ProductDTO> products { get; set; }


    }

    
    
}
