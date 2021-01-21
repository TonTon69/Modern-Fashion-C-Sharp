namespace PlayerUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("productall")]
    public partial class productall
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ProductID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public decimal? BuyPrice { get; set; }

        public decimal? SellPrice { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string SizeCode { get; set; }

        [StringLength(250)]
        public string Desciption { get; set; }

        public int? Quantity { get; set; }

        public DateTime? DateCreate { get; set; }
    }
}
