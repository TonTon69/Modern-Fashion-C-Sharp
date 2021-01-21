namespace PlayerUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HauntDetail")]
    public partial class HauntDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderHauntID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HauntDetailID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? Discount { get; set; }

        [StringLength(10)]
        public string SizeCode { get; set; }

        public virtual OrderHaunt OrderHaunt { get; set; }

        public virtual Product Product { get; set; }
    }
}
