using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlayerUI.Models;

namespace PlayerUI
{
    public partial class QLBanHang : Form
    {
        DataTable tblCTHD = new DataTable();// datatable của chi tiết hóa đơn
        QLHHTShop db = new QLHHTShop();
        List<HauntDetail> listHauntDetail_Database = new List<HauntDetail>();
        List<OrderDetail> listOrderDetail_Database = new List<OrderDetail>();
        public QLBanHang()
        {
            InitializeComponent();
        }

        private void QLBanHang_Load(object sender, EventArgs e)
        {
            btnThemHoaDon.Enabled = true;
            btnLuuHD.Enabled = false;
            btnXoaHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = false;
            btnXuatHoaDon.Enabled = false;
            btnThemSP.Enabled = false;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTenSanPham.ReadOnly = true;
            txtMaSanPham.Enabled = false;
            txtSoLuong.Enabled = false;
            txtMaHoaDon.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTongTienDaMua.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            //txtTongTienHoaDon.ReadOnly = true;
            
        }
        private void addDataToView() // thêm dữ liệu vào trong chi tiết hóa đơn_Database 
        {
            try
            {
                btnLuuHD.Enabled = true;
                Product prd = new Product();
                ProductDetail prdt = new ProductDetail();
                HauntDetail hdt = new HauntDetail();// tạo 1 chi tiêt hóa đơn mới
                OrderDetail od = new OrderDetail();
                string check = cmbSize.Text;
                prd = db.Products.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text);
                prdt = db.ProductDetails.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text && p.SizeCode == check);
                if (txtMaKH.Text == "")
                {
                    if (prd == null)
                    {
                        MessageBox.Show("không tồn tại sản phẩm bạn cần tìm");
                    }
                    else
                    {
                        int soluongkho = int.Parse(prdt.Quantity.ToString());
                        if (txtSoLuong.Text != "")
                        {
                            if (soluongkho >= int.Parse(txtSoLuong.Text))
                            {
                                // int hauntDetailID = (db.HauntDetails.OrderByDescending(o => o.HauntDetailID).First().HauntDetailID) + 1; ;
                                txtTenSanPham.Text = prd.Name;
                                txtDonGia.Text = prd.BuyPrice.ToString();
                                hdt.OrderHauntID = int.Parse(txtMaHoaDon.Text);// gán mã hóa đơn
                                hdt.SizeCode = cmbSize.Text;
                                if (txtGiamGia.Text != "")
                                {
                                    hdt.Discount = int.Parse(txtGiamGia.Text);
                                }
                                //hdt.HauntDetailID = hauntDetailID;
                                hdt.ProductID = txtMaSanPham.Text;//gán mã sản phẩm
                                hdt.Name = txtTenSanPham.Text;//gán tên sản phẩm
                                hdt.Quantity = int.Parse(txtSoLuong.Text);// gán số lượng !!!nhớ trừ số lượng trong productdetail(chưa xử lý)
                                hdt.Price = decimal.Parse(txtDonGia.Text);// gán đơn giá
                                hdt.TotalPrice = decimal.Parse(txtThanhTien.Text); //gán thành tiền
                                //prdt.Quantity = prdt.Quantity - hdt.Quantity; //xử lý phần trừ được nhắc đến ở trên
                                if (listHauntDetail_Database.FirstOrDefault(h => h.ProductID == hdt.ProductID && h.SizeCode == hdt.SizeCode) != null)
                                {
                                    HauntDetail temp = listHauntDetail_Database.FirstOrDefault(h => h.ProductID == hdt.ProductID && h.SizeCode == hdt.SizeCode);
                                    temp.Quantity = temp.Quantity + hdt.Quantity;
                                    temp.TotalPrice = temp.TotalPrice + hdt.TotalPrice;
                                }
                                else
                                {
                                    listHauntDetail_Database.Add(hdt);
                                    txtTongTienHoaDon.Text = listHauntDetail_Database.Sum(s => s.TotalPrice).ToString();
                                }
                                LoadDataGridView_Haunt(hdt.OrderHauntID);
                                txtMaSanPham.Text = "";
                                txtSoLuong.Text = "";
                                if (IsNumeric(txtMaKH.Text))
                                {
                                    txtGiamGia.Text = "";
                                }
                            }
                        }
                    }
                }
                else if (txtMaKH.Text != "" && IsNumeric(txtMaKH.Text) && txtSDT.Text != "")
                {
                    if (prd == null)
                    {
                        MessageBox.Show("không tồn tại sản phẩm bạn cần tìm");
                    }
                    else
                    {
                        int soluongkho = int.Parse(prdt.Quantity.ToString());
                        if (txtSoLuong.Text != "")
                        {
                            if (soluongkho >= int.Parse(txtSoLuong.Text))
                            {

                                //int orderDetailID = (db.OrderDetails.OrderByDescending(o => o.OrderID).First().OrderDetailID) + 1; ;
                                txtTenSanPham.Text = prd.Name;
                                txtDonGia.Text = prd.BuyPrice.ToString();
                                od.OrderID = int.Parse(txtMaHoaDon.Text);// gán mã hóa đơn
                                if (txtGiamGia.Text != "" && IsNumeric(txtGiamGia.Text))
                                {
                                    od.DisCount = int.Parse(txtGiamGia.Text);
                                }
                                od.SizeCode = cmbSize.Text;
                                //od.OrderDetailID = orderDetailID;
                                od.ProductID = txtMaSanPham.Text;//gán mã sản phẩm
                                od.Name = txtTenSanPham.Text;//gán tên sản phẩm
                                od.Quantity = int.Parse(txtSoLuong.Text);// gán số lượng !!!nhớ trừ số lượng trong productdetail(chưa xử lý)
                                od.Price = decimal.Parse(txtDonGia.Text);// gán đơn giá
                                od.TotalPrice = decimal.Parse(txtThanhTien.Text); //gán thành tiền
                                                                                  //prdt.Quantity = prdt.Quantity - od.Quantity; //xử lý phần trừ được nhắc đến ở trên
                                if (listOrderDetail_Database.FirstOrDefault(h => h.ProductID == od.ProductID && h.SizeCode == od.SizeCode) != null)
                                {
                                    OrderDetail temp = listOrderDetail_Database.FirstOrDefault(h => h.ProductID == od.ProductID && h.SizeCode == od.SizeCode);
                                    temp.Quantity = temp.Quantity + od.Quantity;
                                    temp.TotalPrice = temp.TotalPrice + od.TotalPrice;
                                }
                                else
                                {
                                    listOrderDetail_Database.Add(od);
                                    txtTongTienHoaDon.Text = listOrderDetail_Database.Sum(s => s.TotalPrice).ToString();
                                }
                                LoadDataGridView_Order(od.OrderID);
                                txtMaSanPham.Text = "";
                                txtSoLuong.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("khách hàng không tồn tại");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("bị lỗi ở phần thêm sảm phẩm!");
            }

        }

        private bool checkStaff(int AdminID)
        {
            Administrator admin = db.Administrators.FirstOrDefault(a => a.AdminID == AdminID);
            if (admin != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool checkCustomer(int customerID)
        {
            Customer ct = db.Customers.FirstOrDefault(a => a.CustomerID == customerID);
            if (ct != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void FillValueComboBox(List<ProductSize> listProductSize)// method được tạo ra để điền dữ liệu
        {
            this.cmbSize.DataSource = listProductSize;
            this.cmbSize.DisplayMember = "SizeCode";
        }
        private void LoadDataGridView_Haunt(int MaHD)
        {
            int stt = 1;
            decimal sum = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("Mã sản phẩm");
            dt.Columns.Add("Tên sản phẩm");
            dt.Columns.Add("Size");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Đơn giá");
            dt.Columns.Add("khuyến mãi");
            dt.Columns.Add("Thành tiền");
            List<HauntDetail> listHauntDetail = listHauntDetail_Database;
            for (int i = 0; i < listHauntDetail.Count; i++, stt++)// thêm khách hàng vào DataTable
            {
                if (listHauntDetail[i].OrderHauntID == MaHD)
                {
                    HauntDetail hdt = listHauntDetail[i];
                    dt.Rows.Add(stt.ToString(), hdt.ProductID, hdt.Name, hdt.SizeCode, hdt.Quantity, hdt.Price, hdt.Discount + "%", hdt.TotalPrice);
                    sum = sum + (decimal)hdt.TotalPrice;
                }
            }
            txtTongTienHoaDon.Text = sum.ToString();
            dgvChiTietHoaDon.DataSource = dt;// chắc là điền dữ liệu vào dgv
            for (int i = 0; i < dt.Columns.Count; i++)// chỗ này không biết nó dùng để làm gì luôn => chỗ này là đổ dữ liều từ datatable lên datagridview nhưng chưa hiểu rõ
            {
                if (i < dt.Columns.Count - 1)
                {
                    dgvChiTietHoaDon.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                else
                {
                    dgvChiTietHoaDon.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }
            }
        }
        private void LoadDataGridView_Order(int MaHD)
        {
            int stt = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("Mã sản phẩm");
            dt.Columns.Add("Tên sản phẩm");
            dt.Columns.Add("Size");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Đơn giá");
            dt.Columns.Add("khuyến mãi");
            dt.Columns.Add("Thành tiền");
            List<OrderDetail> listOrderDetail = listOrderDetail_Database;
            for (int i = 0; i < listOrderDetail.Count; i++, stt++)// thêm khách hàng vào DataTable
            {
                if (listOrderDetail[i].OrderID == MaHD)
                {
                    OrderDetail od = listOrderDetail[i];
                    dt.Rows.Add(stt.ToString(), od.ProductID, od.Name, od.SizeCode, od.Quantity, od.Price, od.DisCount + "%", od.TotalPrice);
                }
            }
            dgvChiTietHoaDon.DataSource = dt;// chắc là điền dữ liệu vào dgv
            for (int i = 0; i < dt.Columns.Count; i++)// chỗ này không biết nó dùng để làm gì luôn => chỗ này là đổ dữ liều từ datatable lên datagridview nhưng chưa hiểu rõ
            {
                if (i < dt.Columns.Count - 1)
                {
                    dgvChiTietHoaDon.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                else
                {
                    dgvChiTietHoaDon.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }
        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private void BtnThemHoaDon_Click(object sender, EventArgs e)
        {
            listOrderDetail_Database.Clear();
            listHauntDetail_Database.Clear();
            dgvChiTietHoaDon.DataSource = null;
            txtTongTienHoaDon.Text = "";

            if (txtMaKH.Text == "")// xử lý khi không có mã khách hàng
            {
                if (txtMaNV.Text == "")
                {
                    MessageBox.Show("vui lòng nhập mã nhân viên");
                }
                else
                {
                    if (txtMaKH.Text == "" && checkStaff(int.Parse(txtMaNV.Text)))
                    {
                        txtMaKH.Enabled = false;
                    }
                    bool check = checkStaff(int.Parse(txtMaNV.Text));
                    if (check == false)
                    {
                        MessageBox.Show("mã nhân viên không tồn tại vui lòng nhập lại");
                    }
                    else
                    {
                        OrderHaunt odh = new OrderHaunt();
                        odh.CreateDate = DateTime.Now;
                        odh.AdminID = int.Parse(txtMaNV.Text);
                        odh.ToTalPrice = 0;
                        db.OrderHaunts.Add(odh);
                        db.SaveChanges();
                        //   dtpNgayban.Value = odh.CreateDate; chưa xử lý được phần ngày.
                        odh = db.OrderHaunts.OrderByDescending(o => o.OrderHauntID).First();//tìm ra hóa đơn có mã lớn nhất
                        txtMaHoaDon.Text = odh.OrderHauntID.ToString();// đưa mã hóa đơn lên textbox
                        txtMaNV.Text = odh.AdminID.ToString();
                        Administrator admin = db.Administrators.FirstOrDefault(a => a.AdminID.ToString() == txtMaNV.Text);
                        txtTenNhanVien.Text = admin.FullName;
                        btnHuyHoaDon.Enabled = true;
                        btnXuatHoaDon.Enabled = true;
                        btnThemSP.Enabled = true;
                        btnThemHoaDon.Enabled = false;
                        txtMaSanPham.Enabled = true;
                        MessageBox.Show("hóa đơn mới đã được tạo!");
                    }
                }
            }// kết thúc phần code không khách hàng
            else if (checkCustomer(int.Parse(txtMaKH.Text)) == false)
            {
                MessageBox.Show("không tồn tại khách hàng");
            }
            else
            {
                Order od = new Order();
                od.CreateDate = DateTime.Now;
                od.AdminID = int.Parse(txtMaNV.Text);
                od.ToTalPrice = 0;
                od.CustomerID = int.Parse(txtMaKH.Text);
                db.Orders.Add(od);
                db.SaveChanges();
                //   dtpNgayban.Value = odh.CreateDate; chưa xử lý được phần ngày.
                od = db.Orders.OrderByDescending(o => o.OrderID).First();//tìm ra hóa đơn có mã lớn nhất
                txtMaHoaDon.Text = od.OrderID.ToString();// đưa mã hóa đơn lên textbox
                txtMaNV.Text = od.AdminID.ToString();
                Administrator admin = db.Administrators.FirstOrDefault(a => a.AdminID.ToString() == txtMaNV.Text);
                txtTenNhanVien.Text = admin.FullName;
                btnHuyHoaDon.Enabled = true;
                btnXuatHoaDon.Enabled = true;
                btnThemSP.Enabled = true;
                btnThemHoaDon.Enabled = false;
                txtMaSanPham.Enabled = true;
                txtSoLuong.Enabled = true;
                MessageBox.Show("hóa đơn mới đã được tạo!");
            }
        }

        private void BtnHuyHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                btnThemHoaDon.Enabled = true;
                btnLuuHD.Enabled = false;
                btnXoaHoaDon.Enabled = false;
                btnHuyHoaDon.Enabled = false;
                btnXuatHoaDon.Enabled = false;
                btnThemSP.Enabled = false;
                txtMaKH.Enabled = true;
                txtMaHoaDon.Text = "";
                txtMaSanPham.Text = "";
                txtTenSanPham.Text = "";
                txtSoLuong.Text = "";
                txtDonGia.Text = "";
                txtThanhTien.Text = "";
                dgvChiTietHoaDon.DataSource = null;
                MessageBox.Show("Đã hủy hóa đơn");
                OrderHaunt odh = new OrderHaunt();
                odh = db.OrderHaunts.OrderByDescending(o => o.OrderHauntID).First();
                db.OrderHaunts.Remove(odh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnThemSP_Click(object sender, EventArgs e)
        {
            try
            {
                btnLuuHD.Enabled = true;
                Product prd = new Product();
                ProductDetail prdt = new ProductDetail();
                string check = cmbSize.Text;
                prd = db.Products.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text);
                prdt = db.ProductDetails.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text && p.SizeCode == check);
                if (txtMaSanPham.Text == "")
                {
                    MessageBox.Show("vui lòng nhập mã sản phẩm");
                }
                if (txtMaSanPham.Text != "" && txtSoLuong.Text == "")
                {
                    MessageBox.Show("vui lòng nhập số lượng!");
                }
                if (prd == null)
                {
                    throw new Exception("không tồn tại sản phẩm bạn cần tìm");
                }
                if (txtMaKH.Text == "" && txtSoLuong.Text != "" && txtMaSanPham.Text != "" && txtTenSanPham.Text != "")
                {
                    int soluongkho = int.Parse(prdt.Quantity.ToString());
                    if (soluongkho >= int.Parse(txtSoLuong.Text))
                    {
                        addDataToView();
                        LoadDataGridView_Haunt(int.Parse(txtMaHoaDon.Text));
                        txtMaSanPham.Text = "";
                        txtSoLuong.Text = "";
                        if (IsNumeric(txtMaKH.Text))
                        {
                            txtGiamGia.Text = "";
                        }
                    }
                }
                else if (txtMaKH.Text != "" && IsNumeric(txtMaKH.Text) && txtSDT.Text != "" && txtSoLuong.Text != "" && txtMaSanPham.Text != "")
                {
                    if (prd == null)
                    {
                        MessageBox.Show("không tồn tại sản phẩm bạn cần tìm");
                    }
                    else
                    {
                        int soluongkho = int.Parse(prdt.Quantity.ToString());
                        if (soluongkho >= int.Parse(txtSoLuong.Text))
                        {
                            addDataToView();
                            LoadDataGridView_Order(int.Parse(txtMaHoaDon.Text));
                            txtMaSanPham.Text = "";
                            txtSoLuong.Text = "";
                        }
                    }
                }
                else if (txtMaKH.Text != "" && txtSDT.Text == "")
                {
                    MessageBox.Show("khách hàng không tồn tại");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtMaSanPham_TextChanged(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                txtSoLuong.Enabled = false;
            }
            else if (txtMaSanPham.Text != "")
            {
                txtSoLuong.Enabled = true;
                List<ProductDetail> listdetail = db.ProductDetails.Where(p => p.ProductID == txtMaSanPham.Text).ToList();
                this.cmbSize.DataSource = listdetail;
                this.cmbSize.DisplayMember = "SizeCode";
            }

            Product prd = new Product();
            prd = db.Products.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text);
            if (prd != null)
            {
                txtTenSanPham.Text = prd.Name;
                txtDonGia.Text = prd.BuyPrice.ToString();
            }
            if (txtMaSanPham.Text == "" || prd == null)
            {
                txtTenSanPham.Text = "";
                txtDonGia.Text = "";
                txtThanhTien.Text = "";
            }
        }

        private void TxtSoLuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //ProductDetail check1 = db.ProductDetails.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text && p.SizeCode == cmbSize.Text);
                //if (check1 == null)
                //{
                //    throw new Exception("Sản phẩm này đã hết size");
                //}
                if (IsNumeric(txtSoLuong.Text))
                {
                    if (txtSoLuong.Text == "")
                    {
                        txtThanhTien.Text = "";
                    }
                    if (txtSoLuong.Text != "" && txtTenSanPham.Text != "")
                    {
                        ProductDetail prdt = new ProductDetail();
                        string check = cmbSize.Text;
                        prdt = db.ProductDetails.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text && p.SizeCode == check);
                        int soluongkho = int.Parse(prdt.Quantity.ToString());
                        int soluongmua = int.Parse(txtSoLuong.Text);
                        double thanhtien = double.Parse(txtDonGia.Text) * int.Parse(txtSoLuong.Text);
                        txtThanhTien.Text = thanhtien.ToString();
                        if (IsNumeric(txtGiamGia.Text) && txtGiamGia.Text != "")
                        {
                            thanhtien = double.Parse(txtDonGia.Text) * int.Parse(txtSoLuong.Text) * (1 - (double.Parse(txtGiamGia.Text) / 100));
                            txtThanhTien.Text = thanhtien.ToString();
                        }

                        if (soluongkho < soluongmua)
                        {
                            MessageBox.Show("Số hàng trong kho chỉ còn " + soluongkho + " sản phẩm vui lòng nhập lại");
                            txtSoLuong.Text = "0";
                        }
                    }
                }
                else
                {
                    throw new Exception("vui long nhap so");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtMaNV_TextChanged(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                txtTenNhanVien.Text = "";
            }
            if (txtMaNV.Text != "" && IsNumeric(txtMaNV.Text))
            {
                bool check = checkStaff(int.Parse(txtMaNV.Text));
                if (check)
                {
                    int ma = int.Parse(txtMaNV.Text);
                    Administrator ad = db.Administrators.FirstOrDefault(a => a.AdminID == ma);
                    txtTenNhanVien.Text = ad.FullName;
                }
            }
        }

        private void BtnLuuHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (listHauntDetail_Database.Count != 0)
                {
                    int hauntDetailID;
                    if (db.HauntDetails.ToList().Count == 0)
                    {
                        hauntDetailID = 1;
                    }
                    else
                    {
                        hauntDetailID = (db.HauntDetails.OrderByDescending(o => o.HauntDetailID).First().HauntDetailID) + 1;
                    }
                    int mahd = int.Parse(txtMaHoaDon.Text);
                    OrderHaunt odh = db.OrderHaunts.FirstOrDefault(o => o.OrderHauntID == mahd);
                    HauntDetail hd = new HauntDetail();
                    ProductDetail prdt = new ProductDetail();
                    int dem = hauntDetailID;
                    for (int i = 0; i < listHauntDetail_Database.Count; i++, dem++)
                    {

                        listHauntDetail_Database[i].HauntDetailID = dem;
                        hd = listHauntDetail_Database[i];
                        prdt = db.ProductDetails.FirstOrDefault(p => p.SizeCode == hd.SizeCode && p.ProductID == hd.ProductID);
                        prdt.Quantity = prdt.Quantity - hd.Quantity;
                        db.HauntDetails.Add(hd);
                    }
                    MessageBox.Show("lưu hóa đơn thành công");
                    btnLuuHD.Enabled = false;
                    btnThemHoaDon.Enabled = true;
                    odh.ToTalPrice = listHauntDetail_Database.Sum(s => s.TotalPrice);
                    db.SaveChanges();

                  

                }
                else if (listOrderDetail_Database.Count != 0)// nhớ là khi khởi tạo 1 new list nó sẽ được cấp 1 ô nhớ để lưu địa chỉ ô nhớ khác => nó không null
                {
                    int orderDetailID;
                    if (db.OrderDetails.ToList().Count == 0)
                    {
                        orderDetailID = 1;
                    }
                    else
                    {
                        orderDetailID = (db.OrderDetails.OrderByDescending(o => o.OrderDetailID).First().OrderDetailID) + 1;
                    }

                    int mahd = int.Parse(txtMaHoaDon.Text);
                    Order odr = db.Orders.FirstOrDefault(o => o.OrderID == mahd);
                    OrderDetail od = new OrderDetail();
                    ProductDetail prdt = new ProductDetail();
                    int dem = orderDetailID;
                    for (int i = 0; i < listOrderDetail_Database.Count; i++, dem++)
                    {

                        listOrderDetail_Database[i].OrderDetailID = dem;
                        od = listOrderDetail_Database[i];
                        prdt = db.ProductDetails.FirstOrDefault(p => p.SizeCode == od.SizeCode && p.ProductID == od.ProductID);
                        prdt.Quantity = prdt.Quantity - od.Quantity;
                        db.OrderDetails.Add(od);
                    }
                    odr.ToTalPrice = listOrderDetail_Database.Sum(s => s.TotalPrice);
                    Customer ct = db.Customers.FirstOrDefault(c => c.SDT == txtSDT.Text);
                    ct.BuyTotal = ct.BuyTotal + odr.ToTalPrice;
                    db.SaveChanges();
                    MessageBox.Show("lưu hóa đơn thành công");
                    Customer update = db.Customers.FirstOrDefault(x => x.SDT == txtSDT.Text);
                    if (update != null)
                    {
                        txtTongTienDaMua.Text = update.BuyTotal.ToString();
                    }
                    btnLuuHD.Enabled = false;
                    btnThemHoaDon.Enabled = true;
                }
                else
                {
                    MessageBox.Show("hóa đơn rỗng, vui lòng thêm sản phẩm.");
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex.GetBaseException();
                MessageBox.Show(exception.Message);
                MessageBox.Show("đã bị lỗi ở lưu hóa đơn");
            }
        }


        private void TxtGiamGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double thanhtien;
                Product prd = new Product();
                prd = db.Products.FirstOrDefault(p => p.ProductID == txtMaSanPham.Text);
                if (txtGiamGia.Text != "")
                {
                    if (IsNumeric(txtGiamGia.Text))
                    {

                        if (int.Parse(txtGiamGia.Text) >= 100 || int.Parse(txtGiamGia.Text) < 1)
                        {
                            MessageBox.Show("vui lòng nhập số từ 1->99");
                            txtGiamGia.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("vui lòng nhập vào số");
                        txtGiamGia.Text = "";
                    }
                }
                if (txtGiamGia.Text != "" && IsNumeric(txtGiamGia.Text))
                {
                    if (prd != null && IsNumeric(txtSoLuong.Text))
                    {

                        thanhtien = (double)(prd.BuyPrice) * int.Parse(txtSoLuong.Text) * (1 - (double.Parse(txtGiamGia.Text) / 100));
                        txtThanhTien.Text = thanhtien.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("bị lỗi ở nhập giảm giá");
            }
        }

        private void TxtMaKH_TextChanged(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtSDT.Text = "";
                txtTongTienDaMua.Text = "";
                txtGiamGia.Text = "";
            }
            if (txtMaKH.Text != "" && IsNumeric(txtMaKH.Text))
            {
                bool check = checkCustomer(int.Parse(txtMaKH.Text));
                if (check)
                {
                    int ma = int.Parse(txtMaKH.Text);
                    Customer ct = db.Customers.FirstOrDefault(a => a.CustomerID == ma);
                    txtTenKH.Text = ct.FullName;
                    txtSDT.Text = ct.SDT;
                    txtTongTienDaMua.Text = ct.BuyTotal.ToString();
                    if (ct.BuyTotal > 2000000)
                    {
                        txtGiamGia.Text = "15";
                    }
                    else if (ct.BuyTotal > 1000000)
                    {
                        txtGiamGia.Text = "10";
                    }
                    else if (ct.BuyTotal > 500000)
                    {
                        txtGiamGia.Text = "5";
                    }
                }
            }
        }

        private void DgvChiTietHoaDon_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) // nháy đúp vào để xóa nhưng chưa xử lý được
        {
            try
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này khỏi hóa đơn", "Xác nhận", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (txtMaKH.Text == "")
                    {
                        for (int i = 0; i < listHauntDetail_Database.Count; i++)
                        {
                            if (listHauntDetail_Database[i].OrderHauntID == int.Parse(txtMaHoaDon.Text) && listHauntDetail_Database[i].ProductID == dgvr.Cells[1].Value.ToString() && listHauntDetail_Database[i].SizeCode == dgvr.Cells[3].ToString()) ;
                            {
                                listHauntDetail_Database.Remove(listHauntDetail_Database[i]);
                                //LoadDataGridView_Haunt(int.Parse(txtMaHoaDon.Text));
                                MessageBox.Show("đã xóa thành công");
                            }
                        }
                    }
                    else if (txtMaKH.Text != "" && txtSDT.Text != "")
                    {
                        OrderDetail od = new OrderDetail();
                        od = listOrderDetail_Database.Where(o => o.ProductID == dgvr.Cells[1].Value.ToString() && o.SizeCode == dgvr.Cells[3].ToString()).Single();
                        if (od == null)
                        {
                            MessageBox.Show("không tìm thấy");
                        }
                        else
                        {
                            listOrderDetail_Database.Remove(od);
                            LoadDataGridView_Order(int.Parse(txtMaHoaDon.Text));
                            MessageBox.Show("đã xóa thành công");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            PrintOrder ReportForm = new PrintOrder(listHauntDetail_Database);
            this.TopMost = true;
            ReportForm.ShowDialog();
        }
    }
}
