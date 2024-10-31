using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Lab01_Bai03 : Form
    {
        public Lab01_Bai03()
        {
            InitializeComponent();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtNhapso.Text;
                if (input.Length > 12)
                {
                    throw new Exception("Chỉ nhập tối đa 12 số ( vui lòng nhập lại )");
                }
                // Kiểm tra nếu chuỗi input có thể chuyển thành số (long). Nếu chuyển đổi thành công, nó gán giá trị số đó vào biến number và tiếp tục xử lý.
                //Nếu không thể chuyển đổi(ví dụ: chuỗi chứa ký tự không phải số), ném ngoại lệ với thông báo "Vui lòng nhập số hợp lệ!".
                if (long.TryParse(input, out long Nhapso))
                {
                    txtHienthichu.Text = ConvertNhapsoToWords(Nhapso);
                }
                else
                {
                    throw new Exception("Vui lòng nhập số hợp lệ!");
                }

            }
            catch (Exception ex) // Bắt lỗi và hiển thị thông báo
            {
                txtHienthichu.Text = ex.Message; // Hiển thị thông báo lỗi trong texbox
            }
        }

        private string ConvertNhapsoToWords(long Nhapso)
        {
            if (Nhapso == 0) return "Không";
            if (Nhapso < 0) return "Âm " + ConvertNhapsoToWords(Math.Abs(Nhapso));
            // units[]: Lưu trữ các từ biểu diễn chữ số từ 1 đến 9 bằng tiếng Việt.
            // scales[]: Lưu trữ các từ biểu diễn các bậc nghìn, triệu, tỷ bằng tiếng Việt
            string[] units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] scales = { "", "nghìn", "triệu", "tỷ" };

            string words = "";
            int scaleIndex = 0;

            while (Nhapso > 0)
            {
                long currentGroup = Nhapso % 1000;
                if (currentGroup > 0)
                {
                    words = ConvertGroupToWords(currentGroup, units) + " " + scales[scaleIndex] + " " + words;
                }

                Nhapso /= 1000;
                scaleIndex++;
            }

            return words.Trim();
        }

        // Hàm chuyển đổi nhóm 3 chữ số thành chữ
        private string ConvertGroupToWords(long number, string[] units)
        {
            string groupWords = "";
            int hundred = (int)(number / 100);
            int tenUnit = (int)(number % 100);
            int ten = tenUnit / 10;
            int unit = tenUnit % 10;

            if (hundred > 0)
            {
                groupWords += units[hundred] + " trăm ";
                if (ten == 0 && unit > 0) groupWords += "linh ";
            }

            if (ten > 1)
            {
                groupWords += units[ten] + " mươi ";
                if (unit > 0) groupWords += units[unit];
            }
            else if (ten == 1)
            {
                groupWords += "mười ";
                if (unit > 0) groupWords += units[unit];
            }
            else if (unit > 0)
            {
                groupWords += units[unit];
            }

            return groupWords.Trim();
        }
    }
}
