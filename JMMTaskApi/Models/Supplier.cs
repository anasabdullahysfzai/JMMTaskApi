using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMMTaskApi
{
    public partial class Supplier
    {
        public Supplier()
        {
            Orders = new HashSet<Orders>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("s_id")]
        public int SId { get; set; }

        [Column("s_name")]
        [Required]
        public string SName { get; set; }

        [Column("s_address")]
        [Required]
        public string SAddress { get; set; }

        [Column("s_phone")]
        [Required]
        public string SPhone { get; set; }

        [Column("s_iban")]
        [Required]
        public string SIban { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
