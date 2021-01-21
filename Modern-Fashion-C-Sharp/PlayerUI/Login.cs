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
    public partial class Login : Form
    {
        QLHHTShop db = new QLHHTShop();

        public delegate void Authentication(Administrator admin);
        public event Authentication authentication;
        public Login()
        {
            InitializeComponent();
        }

        private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            txtTaiKhoan.MaxLength = 20;

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.MaxLength = 20;
        }
        public string Position = " ";
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text != "" || txtPassword.Text != "")
            {
                Administrator admin = db.Administrators.Where(x => x.AccountName == txtTaiKhoan.Text && x.Password == txtPassword.Text).FirstOrDefault();
                if (admin != null)
                {
                    Position = admin.Position;
                    MessageBox.Show("Đăng nhập thành công");
                    this.Close();
                    if (authentication != null)
                    {
                        authentication(admin);
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
                }

            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "YES/NO", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Hide();
            }
        }
    }
}
