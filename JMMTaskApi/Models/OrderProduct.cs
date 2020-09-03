using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMMTaskApi
{
    /// <summary>
    ///  Entity Framework Database Model file for OrderProduct Bridge Table
    /// </summary>
    public partial class OrderProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("op_id")]
        public int OpId { get; set; }

        [Column("op_quantity")]
        [Required]
        public int OpQuantity { get; set; }

        [Column("p_id")]
        [Required]
        public int PId { get; set; }

        [Column("o_id")]
        [Required]
        public int OId { get; set; }

        public virtual Orders O { get; set; }
        public virtual Product P { get; set; }
    }
}
