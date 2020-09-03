using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMMTaskApi
{
    /// <summary>
    ///  Entity Framework Database Model file for Orders Table
    /// </summary>

    public partial class Orders
    {
        public Orders()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("o_id")]
        public int OId { get; set; }

        [Column("o_date")]
        [Required]
        public DateTime ODate { get; set; }

        [Column("o_type")]
        [Required]
        public string OType { get; set; }

        [Column("c_id")]
        public int? CId { get; set; }

        [Column("s_id")]
        public int? SId { get; set; }

        [Required]
        [Column("o_totalamount")]
        public int OTotalAmount { get; set; }

        public virtual Customer C { get; set; }
        public virtual Supplier S { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
