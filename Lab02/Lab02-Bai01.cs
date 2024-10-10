using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02
{
    public partial class Lab02_Bai01 : System.Windows.Forms.Form
    {
        public Lab02_Bai01()
        {
            InitializeComponent();
        }

        private void btn_Doc_File_Click(object sender, EventArgs e)
        {
            try
            {
                // Đường dẫn đến file input
                string inputFilePath = "input1.txt";

                // Kiểm tra xem file có tồn tại hay không
                if (File.Exists(inputFilePath))
                {
                    // Đọc toàn bộ nội dung file và đưa vào TextBox
                    using (StreamReader reader = new StreamReader(inputFilePath))
                    {
                        string content = reader.ReadToEnd();
                        rtxt_Hien_Thi.Text = content; // Hiển thị nội dung trong TextBox
                    }
                }
                else
                {
                    MessageBox.Show("File 'input1.txt' không tồn tại.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi đọc file: " + ex.Message, "Lỗi");
            }
        }

        private void btn_Ghi_File_Click(object sender, EventArgs e)
        {
            try
            {
                // Đường dẫn đến file output
                string outputFilePath = "output1.txt";

                // Lấy nội dung từ TextBox và chuyển thành in hoa
                string contentToWrite = rtxt_Hien_Thi.Text.ToUpper();

                // Ghi nội dung vào file output
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    writer.Write(contentToWrite);
                }

                MessageBox.Show("Nội dung đã được ghi xuống file 'output1.txt' thành công.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi ghi file: " + ex.Message, "Lỗi");
            }
        }
    }
}
