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
    public partial class Employee : Form
    {
        QLHHTShop db = new QLHHTShop();
        public Employee()
        {
            InitializeComponent();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            List<Administrator> listNV = db.Administrators.ToList();
            BindingDataToView(listNV);
        }

        private void BindingDataToView(List<Administrator> listNV)
        {
            int stt = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Tên tài khoản");
            dt.Columns.Add("CMND");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Chức vụ");
            for (int i = 0; i < listNV.Count; i++, stt++)// thêm khách hàng vào DataTable
            {
                Administrator nv = listNV[i];
                dt.Rows.Add(stt.ToString(), nv.AdminID, nv.FullName, nv.AccountName, nv.CMND, nv.Address, nv.Position);
            }
            dgvNV.DataSource = dt;// chắc là điền dữ liệu vào dgv
        }
        void clear()
        {
            accountNameTextBox.Text = "";
            fullNameTextBox.Text = "";
            passwordTextBox.Text = "";
            addressTextBox.Text = "";
            cMNDTextBox.Text = "";
            positionTextBox.Text = "";
            confirmpassTxt.Text = "";
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            List<Administrator> listNV;
            if (confirmpassTxt.Text != string.Empty || passwordTextBox.Text != string.Empty || accountNameTextBox.Text != string.Empty || addressTextBox.Text != string.Empty || cMNDTextBox.Text != string.Empty || fullNameTextBox.Text != string.Empty)
            {
                if (passwordTextBox.Text == confirmpassTxt.Text)
                {
                    var check = db.Administrators.FirstOrDefault(p => p.AccountName == accountNameTextBox.Text || p.CMND == cMNDTextBox.Text);
                    if (check != null)
                    {
                        MessageBox.Show("Tên tài khoản hoặc CMND đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Administrator ad = new Administrator();
                        ad.AccountName = accountNameTextBox.Text;
                        ad.FullName = fullNameTextBox.Text;
                        ad.Password = passwordTextBox.Text;
                        ad.Address = addressTextBox.Text;
                        ad.CMND = cMNDTextBox.Text;
                        ad.Position = positionTextBox.Text;
                        db.Administrators.Add(ad);
                        db.SaveChanges();
                        MessageBox.Show("Đăng ký thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();

                        listNV = db.Administrators.ToList();
                        BindingDataToView(listNV);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập cả hai mật khẩu giống nhau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin nhân viên không ?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string acc = accountNameTextBox.Text;
                    Administrator nv = db.Administrators.FirstOrDefault(p => p.AccountName == acc);
                    nv.FullName = fullNameTextBox.Text;
                    nv.Address = addressTextBox.Text;
                    nv.Position = positionTextBox.Text;
                    db.SaveChanges();
                    BindingDataToView(db.Administrators.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("bị lỗi ở phần sửa nhân viên: " + ex.Message);
            }
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedRow = e.RowIndex;
                fullNameTextBox.Text = dgvNV.Rows[selectedRow].Cells[2].Value.ToString();
                accountNameTextBox.Text = dgvNV.Rows[selectedRow].Cells[3].Value.ToString();
                cMNDTextBox.Text = dgvNV.Rows[selectedRow].Cells[4].Value.ToString();
                addressTextBox.Text = dgvNV.Rows[selectedRow].Cells[5].Value.ToString();
                positionTextBox.Text = dgvNV.Rows[selectedRow].Cells[6].Value.ToString();
                if (positionTextBox.Text == "Quản lý")
                {
                    btnXoa.Enabled = false;
                }
                else if (positionTextBox.Text != "Quản lý")
                {
                    btnXoa.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Administrator check = db.Administrators.FirstOrDefault(p => p.AccountName == accountNameTextBox.Text);
                if (check != null)
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn nhân viên này!!!", "Xác nhận", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in dgvNV.Rows)
                        {
                            if (item.Cells[3].Value.ToString() == accountNameTextBox.Text)
                            {
                                dgvNV.Rows.Remove(item);
                                break;
                            }
                        }
                        db.Administrators.Remove(check);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Administrator> listNV = new List<Administrator>();
                int maNV = int.Parse(txtTimKiem.Text);
                Administrator nv = db.Administrators.FirstOrDefault(p => p.AdminID == maNV || p.FullName.ToLower() == txtTimKiem.Text.ToLower());
                if (nv != null)
                {
                    listNV.Add(nv);
                    BindingDataToView(listNV);
                }
                else
                {
                    MessageBox.Show("không có nhân viên bạn muốn tìm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("bị lỗi ở phần tìm kiếm: " + ex.Message);
            }
        }
    }
}
