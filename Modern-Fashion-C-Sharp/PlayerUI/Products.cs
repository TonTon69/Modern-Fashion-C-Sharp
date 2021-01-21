using PlayerUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PlayerUI
{
    public partial class Products : Form
    {
        QLHHTShop db = new QLHHTShop();
        Product pro = new Product();
        ProductSize proSize = new ProductSize();
        ProductDetail proDetail = new ProductDetail();
        productall proall = new productall();
        string filename;
        Image ConverBinaryToImage(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            try
            {
                List<productall> listProductAll = db.productalls.OrderBy(p => p.ProductID).ToList();
                List<ProductSize> listSizes = db.ProductSizes.ToList();
                BindGird(listProductAll);
                FillSizeCombox(listSizes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGird(List<productall> listProductAll)
        {
            dgvListProduct.Rows.Clear();
            foreach (var item in listProductAll)
            {
                int index = dgvListProduct.Rows.Add();
                dgvListProduct.Rows[index].Cells[0].Value = item.ProductID;
                dgvListProduct.Rows[index].Cells[1].Value = item.Name;
                dgvListProduct.Rows[index].Cells[2].Value = item.BuyPrice;
                dgvListProduct.Rows[index].Cells[3].Value = item.SellPrice;
                dgvListProduct.Rows[index].Cells[4].Value = item.SizeCode;
                dgvListProduct.Rows[index].Cells[5].Value = item.Quantity;
                dgvListProduct.Rows[index].Cells[6].Value = Convert.ToDateTime(item.DateCreate).ToString("dd/MM/yyyy");
            }
        }

        private void FillSizeCombox(List<ProductSize> listSizes)
        {
            this.cmbSize.DataSource = listSizes;
            this.cmbSize.DisplayMember = "SizeCode";
            this.cmbSize.ValueMember = "SizeCode";
        }
        private int GetSelectedRow(string productID)
        {
            for (int i = 0; i < dgvListProduct.Rows.Count; i++)
            {
                if (dgvListProduct.Rows[i].Cells[0].Value.ToString() == productID)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InsertUpdate(int selectedRow)
        {
            dgvListProduct.Rows[selectedRow].Cells[0].Value = txtProductID.Text;
            dgvListProduct.Rows[selectedRow].Cells[1].Value = txtProductName.Text;
            dgvListProduct.Rows[selectedRow].Cells[2].Value = txtPriceBuy.Text;
            dgvListProduct.Rows[selectedRow].Cells[3].Value = txtPriceSell.Text;
            dgvListProduct.Rows[selectedRow].Cells[4].Value = cmbSize.Text;
            dgvListProduct.Rows[selectedRow].Cells[5].Value = txtQuantity.Text;
            dgvListProduct.Rows[selectedRow].Cells[6].Value = Convert.ToDateTime(pro.DateCreate).ToString("dd/MM/yyyy");
        }

        void clear()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPriceBuy.Text = "";
            txtPriceSell.Text = "";
            cmbSize.Text = "";
            txtQuantity.Text = "";
        }
        private async void btnCreate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text == "" || txtProductName.Text == "" || txtPriceBuy.Text == " " || txtPriceSell.Text == " ")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sản phẩm!");
                int selectedRow = GetSelectedRow(txtProductID.Text);
                var check = db.ProductDetails.FirstOrDefault(p => p.SizeCode == cmbSize.Text && p.ProductID == txtProductID.Text);
                var checkSize = db.ProductDetails.FirstOrDefault(p => p.SizeCode == cmbSize.Text);
                if (selectedRow == -1)
                {
                    selectedRow = dgvListProduct.Rows.Add();

                    //Product
                    pro.ProductID = txtProductID.Text;
                    pro.Name = txtProductName.Text;
                    pro.Image = converImagesToBinary(pbxImage.Image);
                    pro.BuyPrice = decimal.Parse(txtPriceBuy.Text);
                    pro.SellPrice = decimal.Parse(txtPriceSell.Text);
                    pro.DateCreate = DateTime.Now;
                    db.Products.Add(pro);
                    await db.SaveChangesAsync();
                    proDetail = new ProductDetail();
                    //ProductDetail
                    proDetail.ProductID = txtProductID.Text;
                    proDetail.SizeCode = cmbSize.SelectedValue.ToString();
                    proDetail.Quantity = Int32.Parse(txtQuantity.Text);
                    db.ProductDetails.Add(proDetail);
                    await db.SaveChangesAsync();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                    List<productall> listProductAll = db.productalls.ToList();
                    BindGird(listProductAll);
                }
                else if (check == null)
                {
                    selectedRow = dgvListProduct.Rows.Add();
                    proDetail = new ProductDetail();
                    //ProductDetail
                    proDetail.ProductID = txtProductID.Text;
                    proDetail.SizeCode = cmbSize.SelectedValue.ToString();
                    proDetail.Quantity = Int32.Parse(txtQuantity.Text);
                    db.ProductDetails.Add(proDetail);
                    await db.SaveChangesAsync();
                    List<productall> listProductAll = db.productalls.ToList();
                    BindGird(listProductAll);
                    MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else if (check != null)
                {
                    MessageBox.Show("Sản phẩm tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtProductID.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã sản phẩm cần sửa!");
                }
                else
                {
                    Product pro = db.Products.FirstOrDefault(n => n.ProductID == txtProductID.Text);
                    ProductDetail proDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID == txtProductID.Text);
                    if (pro != null)
                    {
                        pro.Name = txtProductName.Text;
                        pro.BuyPrice = decimal.Parse(txtPriceBuy.Text);
                        pro.SellPrice = decimal.Parse(txtPriceSell.Text);
                        pro.Image = converImagesToBinary(pbxImage.Image);
                        pro.DateCreate = DateTime.Now;
                        proDetail.SizeCode = cmbSize.SelectedValue.ToString();
                        proDetail.Quantity = Int32.Parse(txtQuantity.Text);
                        db.SaveChanges();
                        InsertUpdate(selectedRow);
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtProductID.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã sản phẩm cần xóa!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvListProduct.Rows.RemoveAt(selectedRow);
                        ProductDetail pro = db.ProductDetails.FirstOrDefault(n => n.ProductID == txtProductID.Text);
                        if (pro != null)
                        {
                            db.ProductDetails.Remove(pro);
                            db.SaveChanges();
                            InsertUpdate(selectedRow);
                            MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dgvListProduct_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedRow = e.RowIndex;
                txtProductID.Text = dgvListProduct.Rows[selectedRow].Cells[0].Value.ToString();
                txtProductName.Text = dgvListProduct.Rows[selectedRow].Cells[1].Value.ToString();
                productall pro = db.productalls.FirstOrDefault(n => n.ProductID == txtProductID.Text);
                var data = (Byte[])(pro.Image);
                var stream = new MemoryStream(data);
                pbxImage.Image = Image.FromStream(stream);
                txtPriceBuy.Text = dgvListProduct.Rows[selectedRow].Cells[2].Value.ToString();
                txtPriceSell.Text = dgvListProduct.Rows[selectedRow].Cells[3].Value.ToString();
                cmbSize.Text = dgvListProduct.Rows[selectedRow].Cells[4].Value.ToString();
                txtQuantity.Text = dgvListProduct.Rows[selectedRow].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        byte[] converImagesToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG| *.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filename = ofd.FileName;
                    pbxImage.Image = Image.FromFile(filename);
                }
            }
        }

        private void txtProductID_TextChanged(object sender, EventArgs e)
        {
            txtProductID.MaxLength = 50;
        }
    }
}

