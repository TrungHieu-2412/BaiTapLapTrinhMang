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
    public partial class Lab01_Bai04 : Form
    {
        public Lab01_Bai04()
        {
            InitializeComponent();
            btnCheck.Click += new EventHandler(btnCheck_Click);
            btnXoa.Click += new EventHandler(btnXoa_Click);
        }

        private void Lab01_Bai04_Load(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            // Lấy giá trị ngày sinh từ txtInput
            DateTime birthDate;
            if (DateTime.TryParse(txtInput.Text, out birthDate))
            {
                string zodiacSign = GetZodiacSign(birthDate);
                txtKetQua.Text = zodiacSign; // Hiển thị kết quả trong txtKetQua
            }
            else
            {
                txtKetQua.Text = "Ngày sinh không hợp lệ!";
            }
        }

        // Hàm trả về cung hoàng đạo dựa vào ngày sinh
        private string GetZodiacSign(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            if ((day >= 21 && month == 3) || (day <= 20 && month == 4))
                return "Bạch Dương";
            else if ((day >= 21 && month == 4) || (day <= 21 && month == 5))
                return "Kim Ngưu";
            else if ((day >= 22 && month == 5) || (day <= 21 && month == 6))
                return "Song Tử";
            else if ((day >= 22 && month == 6) || (day <= 22 && month == 7))
                return "Cự Giải";
            else if ((day >= 23 && month == 7) || (day <= 22 && month == 8))
                return "Sư Tử";
            else if ((day >= 23 && month == 8) || (day <= 23 && month == 9))
                return "Xử Nữ";
            else if ((day >= 24 && month == 9) || (day <= 23 && month == 10))
                return "Thiên Bình";
            else if ((day >= 24 && month == 10) || (day <= 22 && month == 11))
                return "Thần Nông";
            else if ((day >= 23 && month == 11) || (day <= 21 && month == 12))
                return "Nhân Mã";
            else if ((day >= 22 && month == 12) || (day <= 20 && month == 1))
                return "Ma Kết";
            else if ((day >= 21 && month == 1) || (day <= 19 && month == 2))
                return "Bảo Bình";
            else if ((day >= 20 && month == 2) || (day <= 20 && month == 3))
                return "Song Ngư";

            return "Không xác định";
        }

        // Sự kiện nút "Xóa"
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtKetQua.Clear();
        }
    }
}
