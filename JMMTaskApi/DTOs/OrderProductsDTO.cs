using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMMTaskApi.DTOs
{
    /// <summary>
    ///  Data Transfer Object class used as an object to fullfill Request of Getting Products Against an Order
    /// </summary>
    public class OrderProductsDTO
    {
        public string PCode { get; set; }
        public string PName { get; set; }
        public int PQuantity { get; set; }
        public int PPrice { get; set; }
    }
}
