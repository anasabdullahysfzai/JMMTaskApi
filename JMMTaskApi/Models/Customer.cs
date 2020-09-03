using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMMTaskApi
{
    /// <summary>
    ///  Entity Framework Database Model file for Customer Table
    /// </summary>
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("c_id")]
        public int CId { get; set; }

        [Column("c_name")]
        [Required]
        public string CName { get; set; }

        [Column("c_address")]
        [Required]
        public string CAddress { get; set; }

        [Column("c_phone")]
        [Required]
        public string CPhone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
