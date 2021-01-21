using Microsoft.Reporting.WinForms;
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


    public partial class PrintOrder : Form
    {

        List<Models.HauntDetail> listHauntDetail_Database;
        public PrintOrder(List<Models.HauntDetail> listHauntDetail_Database)
        {
            InitializeComponent();
            this.listHauntDetail_Database = listHauntDetail_Database;
        }

        private void PrintOrder_Load(object sender, EventArgs e)
        {
            this.rptvPrintHD.RefreshReport();
            print();
        }

        public void print()
        {
            this.rptvPrintHD.LocalReport.ReportPath = "ReportPrintHD.rdlc";
            var reportDataSource = new ReportDataSource("PrintOrderDataSet", listHauntDetail_Database);
            this.rptvPrintHD.LocalReport.DataSources.Clear();
            this.rptvPrintHD.LocalReport.DataSources.Add(reportDataSource);
            this.rptvPrintHD.RefreshReport();
        }
    }
}
