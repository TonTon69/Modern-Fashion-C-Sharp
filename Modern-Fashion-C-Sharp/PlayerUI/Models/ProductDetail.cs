namespace PlayerUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        [Key]
        public int STT { get; set; }

        [Required]
        [StringLength(10)]
        public string SizeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductID { get; set; }

        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductSize ProductSize { get; set; }
    }
}
