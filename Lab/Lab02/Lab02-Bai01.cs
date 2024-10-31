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
                // Tạo hộp thoại OpenFileDialog
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Chọn file để đọc";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                // Hiển thị hộp thoại và kiểm tra nếu người dùng chọn một file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Đường dẫn đến file do người dùng chọn
                    string filePath = openFileDialog.FileName;

                    // Đọc nội dung file và hiển thị trong TextBox
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string content = reader.ReadToEnd();
                        rtxt_Hien_Thi.Text = content; // Hiển thị nội dung trong TextBox
                    }
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
