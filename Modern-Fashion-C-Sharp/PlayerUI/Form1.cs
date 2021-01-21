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
    public partial class Form1 : Form
    {
        QLHHTShop db = new QLHHTShop();
        Administrator ad;
        public Form1()
        {
            InitializeComponent();
            hideSubMenu();
        }

        public Form1(Administrator admin)
        {
            InitializeComponent();
            hideSubMenu();
            this.ad = admin;
        }
        private void Login()
        {
            Form frmchild = this.MdiChildren.OfType<Login>().FirstOrDefault();
            if (frmchild == null)
            {
                panelChildForm.Hide();
                Login FrmLogin = new Login();
                FrmLogin.MdiParent = this;
                FrmLogin.StartPosition = FormStartPosition.CenterScreen;
                FrmLogin.Show();
                FrmLogin.authentication += frmauthentication;
                return;
            }
            frmchild.Activate();
        }
        private void frmauthentication(Administrator admin)
        {
            setFunction(admin.Position);
            this.ad = admin;

        }
        private void loadFunction()
        {
            btnBanHang.Visible = false;
            btnSanPham.Visible = false;
            btnThongKe.Visible = false;
            btnLogOut.Visible = false;
            btnRegister.Visible = false;
        }
        private void setFunction(string position)
        {
            btnLogin.Visible = false;

            if (position == "Nhân viên")
            {
                btnBanHang.Visible = true;
                btnSanPham.Visible = false;
                btnThongKe.Visible = false;
                btnRegister.Visible = false;
                btnLogOut.Visible = true;
                panelChildForm.Visible = true;

            }
            if (position == "Quản lý")
            {
                btnBanHang.Visible = true;
                btnSanPham.Visible = true;
                btnThongKe.Visible = true;
                btnRegister.Visible = true;
                btnLogOut.Visible = true;
                panelChildForm.Visible = true;
            }
        }

        private void hideSubMenu()
        {
            panelPlaylistSubMenu.Visible = false;
            panel3.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        //Bán hàng
        private void btnMedia_Click(object sender, EventArgs e) 
        {
            // chỗ nãy để kick vào sổ cái bản xuống
            //   showSubMenu(panelMediaSubMenu);
            panelPlaylistSubMenu.Show();
        }

        #region MediaSubMenu
        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new QLBanHang());
            //..
            //your codes
            //..
            hideSubMenu();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongTinKH());
            //..
            //your codes
            //..
            hideSubMenu();
        }
        //Sản phẩm
        #endregion

        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            openChildForm(new Products());
        }



        //Thống kê
        #region ToolsSubMenu


        #endregion

        private void BtnThongKe_Click_1(object sender, EventArgs e)
        {
            panel3.Show();
            
        }
        private void btnTKHD_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongKeHD());
            //..
            //your codes
            //..
            hideSubMenu();
        }
        private void btnTKHDVL_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongKeHDVangLai());
            //..
            //your codes
            //..
            hideSubMenu();
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "YES/NO", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadFunction();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            ad = null;
            foreach (var item in this.MdiChildren)
            {
                item.Close();
            }
            btnLogin.Visible = true;
            panelChildForm.Hide();
            Login();
            loadFunction();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongKeHD());
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void PanelSideMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnLogin_Click_1(object sender, EventArgs e)
        {
            Login();
        }

        private void BtnQuanLyBanHang_Click(object sender, EventArgs e)
        {
            openChildForm(new QLBanHang());
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void BtnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongTinKH());
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void BtnSanPham_Click(object sender, EventArgs e)
        {
            openChildForm(new Products());
        }

        private void Register()
        {
            Form frmchild = this.MdiChildren.OfType<Employee>().FirstOrDefault();
            if (frmchild == null)
            {
                panelChildForm.Hide();
                Employee FrmRegister = new Employee();
                FrmRegister.MdiParent = this;
                FrmRegister.StartPosition = FormStartPosition.CenterScreen;
                FrmRegister.Show();
                return;
            }
            frmchild.Activate();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            openChildForm(new Employee());
            //..
            //your codes
            //..
            hideSubMenu();
        }
    }
}
