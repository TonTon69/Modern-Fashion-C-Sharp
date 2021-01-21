namespace PlayerUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderHaunt")]
    public partial class OrderHaunt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderHaunt()
        {
            HauntDetails = new HashSet<HauntDetail>();
        }

        public int OrderHauntID { get; set; }

        public DateTime? CreateDate { get; set; }

        public decimal? ToTalPrice { get; set; }

        public int AdminID { get; set; }

        public virtual Administrator Administrator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HauntDetail> HauntDetails { get; set; }
    }
}
