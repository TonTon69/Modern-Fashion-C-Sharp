using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerUI.Reportings
{
    class PrintOrder
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Discount { get; set; }
    }
}
