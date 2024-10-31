using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Lab01_Bai01 : Form
    {
        public Lab01_Bai01()
        {
            InitializeComponent();
        }

        // sự kiện nút tìm 
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                // Convert.ToDouble(txtSo1.Text) chuyển giá trị nhập từ TextBox thành kiểu số double. Nếu người dùng nhập dữ liệu không hợp lệ (chữ hoặc ký tự đặc biệt), chương trình sẽ gặp lỗi và nhảy sang phần catch.
                double Sothu1 = Convert.ToDouble(txtSothu1.Text);
                double Sothu2 = Convert.ToDouble(txtSothu2.Text);
                double Sothu3 = Convert.ToDouble(txtSothu3.Text);
                // Sử dụng phương thức Math.Round(giá trị, 1) để làm tròn các số so1, so2, và so3 đến 1 chữ số thập phân.
                Sothu1 = Math.Round(Sothu1, 1);
                Sothu2 = Math.Round(Sothu2, 1);
                Sothu3 = Math.Round(Sothu3, 1);
                // Sử dụng hàm Math.Max() để tìm số lớn nhất và Math.Min() để tìm số nhỏ nhất trong ba số đã nhập.
                double Solonnhat = Math.Max(Sothu1, Math.Max(Sothu2, Sothu3));
                double Sonhonhat = Math.Min(Sothu1, Math.Min(Sothu2, Sothu3));
                // In ra màn hình
                txtSolonnhat.Text = Solonnhat.ToString();
                txtSonhonhat.Text = Sonhonhat.ToString();
            }
            catch (FormatException)
            {
                // Hiển thị thông báo lỗi 
                MessageBox.Show("Vui lòng nhập vào các số hợp lệ (không chứa ký tự hoặc chữ cái).",
                        "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // sự kiện nút xóa 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtSothu1.Clear();
            txtSothu2.Clear();
            txtSothu3.Clear();
            txtSolonnhat.Clear();
            txtSonhonhat.Clear();
        }
        // sự kiện nút thoát 
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lab01_Bai01_Load(object sender, EventArgs e)
        {

        }

        private void txtSothu1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



        

