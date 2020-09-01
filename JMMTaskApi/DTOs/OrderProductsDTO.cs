using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.DTOs
{
    public class OrderProductsDTO
    {
        public string PCode { get; set; }
        public string PName { get; set; }
        public int PQuantity { get; set; }
        public int PPrice { get; set; }
    }
}
