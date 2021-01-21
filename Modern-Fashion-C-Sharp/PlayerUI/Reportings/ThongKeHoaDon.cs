using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerUI.Reportings
{
    class ThongKeHoaDon
    {
        public DateTime? CreateDate { get; set; }
        public int CustomerID { get; set; }
        public decimal? ToTalPrice { get; set; }
        public int AdminID { get; set; }
    }
}
