using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.DTOs
{
    public class OrderCustomerDTO
    {
        public int OId { get; set; }

        public int? CId { get; set; }

        public string CName { get; set; }

        public string CAddress { get; set; }

        public string CPhone { get; set; }

        public DateTime ODate { get; set; }

        public string OType { get; set; }

        public IList<ProductDTO> products { get; set; }
        
    }
}
