using Microsoft.Reporting.WinForms;
using PlayerUI.Models;
using PlayerUI.Reportings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerUI
{
    public partial class ThongKeHDVangLai : Form
    {
        QLHHTShop db = new QLHHTShop();
        public ThongKeHDVangLai()
        {
            InitializeComponent();
        }

        private void ThongKeHDVangLai_Load(object sender, EventArgs e)
        {
            BindGridReport();
        }
        private void BindGridReport()
        {
            string truyVanSQL = "SELECT o.AdminID, ToTalPrice, CreateDate " +
                                "FROM [OrderHaunt] o, Administrator a WHERE a.AdminID = o.AdminID";
            List<ThongKeHoaDon> list = db.Database.SqlQuery<ThongKeHoaDon>(truyVanSQL).ToList();
            if (dtp1.Value.Date != null && dtp2.Value.Date != null)
            {
                list = list.Where(x => x.CreateDate >= dtp1.Value.Date && x.CreateDate < dtp2.Value.Date).ToList();
            }
            this.rptvTKHDVL.LocalReport.ReportPath = "ReportTKHDVL.rdlc";
            var reportDataSource = new ReportDataSource("TKHDVLDataSet", list);
            this.rptvTKHDVL.LocalReport.DataSources.Clear();
            this.rptvTKHDVL.LocalReport.DataSources.Add(reportDataSource);
            this.rptvTKHDVL.RefreshReport();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            BindGridReport();
        }
    }
}
