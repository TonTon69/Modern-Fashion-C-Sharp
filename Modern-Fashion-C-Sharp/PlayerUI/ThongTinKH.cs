using PlayerUI.Models;
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
    public partial class ThongTinKH : Form
    {
        QLHHTShop db = new QLHHTShop();
        public ThongTinKH()
        {
            InitializeComponent();
        }
        private void BindingDataToView(List<Customer> listCustomer)
        {
            int stt = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("Mã khách hàng");
            dt.Columns.Add("Tên Khách Hàng");
            dt.Columns.Add("Số Điện Thoại");
            dt.Columns.Add("Số Tiền Đã Mua");
            for (int i = 0; i < listCustomer.Count; i++, stt++)// thêm khách hàng vào DataTable
            {
                Customer ct = listCustomer[i];
                dt.Rows.Add(stt.ToString(), ct.CustomerID, ct.FullName, ct.SDT, ct.BuyTotal.ToString());
            }
            dgvDanhSachKH.DataSource = dt;// chắc là điền dữ liệu vào dgv
            for (int i = 0; i < dt.Columns.Count; i++)// chỗ này không biết nó dùng để làm gì luôn => chỗ này là đổ dữ liều từ datatable lên datagridview nhưng chưa hiểu rõ
            {
                if (i < dt.Columns.Count - 1)
                {
                    dgvDanhSachKH.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                else
                {
                    dgvDanhSachKH.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void ThongTinKH_Load(object sender, EventArgs e)
        {
            dgvDanhSachKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            BindingDataToView(db.Customers.ToList());
            dgvDanhSachKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            txtMaKH.ReadOnly=true;
        }

        private void DgvDanhSachKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dgvr = dgvDanhSachKH.SelectedRows[0];
                txtMaKH.Text = dgvr.Cells[1].Value.ToString();
                txtTenKH.Text = dgvr.Cells[2].Value.ToString();
                txtSDT.Text = dgvr.Cells[3].Value.ToString();
                txtTongTien.Text = dgvr.Cells[4].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("lỗi ở click datagridview: "+ ex.Message);
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Customer check = db.Customers.FirstOrDefault(c => c.SDT == txtSDT.Text);
                if (check == null)
                {
                    Customer ct = new Customer();
                    ct.FullName = txtTenKH.Text;
                    ct.SDT = txtSDT.Text;
                    ct.BuyTotal = decimal.Parse(txtTongTien.Text);
                    db.Customers.Add(ct); // thêm ct vào database
                    db.SaveChanges();
                    BindingDataToView(db.Customers.ToList());// hiển thị dgv
                }
                else
                {
                    MessageBox.Show("số điện thoại đã được đăng kí!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("lỗi ở thêm khách hàng: "+ ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Customer check = db.Customers.FirstOrDefault(p => p.SDT == txtSDT.Text);
                if (check != null)
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn khách hàng này", "Xác nhận", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in dgvDanhSachKH.Rows)
                        {
                            if (item.Cells[3].Value.ToString() == txtSDT.Text)
                            {
                                dgvDanhSachKH.Rows.Remove(item);
                                break;
                            }
                        }
                        db.Customers.Remove(check);
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("bị lỗi ở xóa khách hàng: "+ex.Message);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("bạn có chắc chán muốn sửa khách hàng không ?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    int maKH = int.Parse(txtMaKH.Text);
                    Customer ct = db.Customers.FirstOrDefault(p => p.CustomerID == maKH);// nhớ LINQ phải sử dụng biến phụ để so sánh khi so sánh có chuyển đổi cơ sở dữ liệu.
                    ct.FullName = txtTenKH.Text;
                    ct.SDT = txtSDT.Text;
                    ct.BuyTotal = decimal.Parse(txtTongTien.Text);
                    db.SaveChanges();
                    BindingDataToView(db.Customers.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("bị lỗi ở phần sửa khách hàng: "+ ex.Message);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Customer> listCT = new List<Customer>();
                int maKH = int.Parse(txtTimKiem.Text);
                Customer ct = db.Customers.FirstOrDefault(p => p.CustomerID == maKH || p.SDT == txtTimKiem.Text);
                if (ct != null)
                {
                    listCT.Add(ct);
                    BindingDataToView(listCT);
                }
                else
                {
                    MessageBox.Show("không có khách hàng bạn muốn tìm");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("bị lỗi ở phần tìm kiếm: "+ex.Message);
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaKH.Text = null;
                txtTenKH.Text = null;
                txtSDT.Text = null;
                txtTimKiem.Text = null;
                txtTongTien.Text = null;
                BindingDataToView(db.Customers.ToList());
            }
            catch(Exception ex)
            {
                MessageBox.Show("bị lỗi ở phần cập nhật: "+ex.Message);
            }
        }
    }
}
