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
    public partial class ThongKeHD : Form
    {
        QLHHTShop db = new QLHHTShop();
        public ThongKeHD()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            BindGridReport();
        }

        private void BindGridReport()
        {
            string truyVanSQL = "SELECT o.AdminID, o.CustomerID, ToTalPrice, CreateDate " +
                                "FROM [Order] o, Administrator a, Customer c WHERE a.AdminID = o.AdminID and o.CustomerID = c.CustomerID ";
            List<ThongKeHoaDon> list = db.Database.SqlQuery<ThongKeHoaDon>(truyVanSQL).ToList();
            if (dtp1.Value.Date != null && dtp2.Value.Date != null)
            {
                list = list.Where(x => x.CreateDate >= dtp1.Value.Date && x.CreateDate < dtp2.Value.Date).ToList();
            }
            this.rptvTK.LocalReport.ReportPath = "ReportTKHD.rdlc";
            var reportDataSource = new ReportDataSource("TKHDDataSet", list);
            this.rptvTK.LocalReport.DataSources.Clear();
            this.rptvTK.LocalReport.DataSources.Add(reportDataSource);
            this.rptvTK.RefreshReport();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            BindGridReport();
        }
    }
}
