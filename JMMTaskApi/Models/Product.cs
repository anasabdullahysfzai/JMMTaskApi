using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMMTaskApi
{
    public partial class Product : ICloneable
    {
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("p_id")]
        public int PId { get; set; }

        [Column("p_code")]
        [Required]
        public string PCode { get; set; }

        [Column("p_name")]
        [Required]
        public string PName { get; set; }

        [Column("p_price")]
        [Required]
        public int PPrice { get; set; }

        [Column("p_stock")]
        [Required]
        public int PStock { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }


        public object Clone()
        {
            return new Product()
            {
                PId = this.PId,
                PCode = this.PCode,
                PName = this.PName,
                PPrice = this.PPrice,
                PStock = this.PStock,
            };
        }
    }
}
