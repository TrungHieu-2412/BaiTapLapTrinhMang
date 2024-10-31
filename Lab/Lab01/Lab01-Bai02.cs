using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Lab01
{
    public partial class Lab01_Bai02 : Form
    {
        public Lab01_Bai02()
        {
            InitializeComponent();
            btnTinh.Click += new EventHandler(btnTinh_Click);
            btnXoa.Click += new EventHandler(btnXoa_Click);
            btnThoat.Click += new EventHandler(btnThoat_Click);
        }

        // Hàm tính giai thừa
        private long Factorial(int n)
        {
            if (n <= 1)
                return 1;
            return n * Factorial(n - 1);
        }

        // Hàm tính tổng A^1 + A^2 + ... + A^B
        private long SumPowers(int A, int B)
        {
            long sum = 0;
            for (int i = 1; i <= B; i++)
            {
                sum += (long)Math.Pow(A, i);
            }
            return sum;
        }


        // Hàm hiển thị bảng cửu chương của (B - A)
        private string MultiplicationTable(int A, int B)
        {
            int diff = Math.Abs(B - A); // Lấy chênh lệch tuyệt đối giữa A và B
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Bảng cửu chương của {diff}:");
            for (int j = 1; j <= 10; j++)
            {
                result.AppendLine($"{diff} x {j} = {diff * j}"); // Xuống hàng sau mỗi dòng
            }
            return result.ToString();
        }

        // Sự kiện nút "Tính các giá trị"
        private void btnTinh_Click(object sender, EventArgs e)
        {
            try
            {
                int A = int.Parse(txtA.Text);
                int B = int.Parse(txtB.Text);

                if (comboBox1.SelectedIndex == 0) // Lựa chọn xuất bảng cửu chương
                {
                    txtKetQua.Text = MultiplicationTable(A, B).ToString();
                }
                else if (comboBox1.SelectedIndex == 1) // Lựa chọn tính toán các giá trị
                {
                    int diff = A - B;

                    // Kiểm tra nếu A - B âm thì không tính giai thừa
                    string factorialResult;
                    if (diff < 0)
                    {
                        factorialResult = "Không tính giai thừa của số âm";
                    }
                    else
                    {
                        factorialResult = $"(A - B)! = {Factorial(diff)}";
                    }

                    // Tính tổng A^1 + A^2 + ... + A^B
                    long sumPowersResult = SumPowers(A, B);

                    // Hiển thị kết quả trên 2 dòng khác nhau với xuống hàng
                    StringBuilder result = new StringBuilder();
                    result.AppendLine(factorialResult); // Xuống hàng sau giai thừa
                    result.AppendLine($"Tổng = {sumPowersResult}"); // In tổng và xuống hàng

                    // Cập nhật kết quả vào textbox
                    txtKetQua.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một chức năng trong danh sách.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        // Sự kiện nút "Xóa"
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtA.Clear();
            txtB.Clear();
            txtKetQua.Clear();
            comboBox1.SelectedIndex = -1;
        }

        // Sự kiện nút "Thoát"
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lab01_Bai02_Load(object sender, EventArgs e)
        {

        }
    }
}
