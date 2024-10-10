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
    public partial class Lab02_Bai02 : System.Windows.Forms.Form
    {
        public Lab02_Bai02()
        {
            InitializeComponent();
        }

        private void btn_Read_file_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn file
            OpenFileDialog ofd = new OpenFileDialog();

            // Kiểm tra nếu người dùng chọn một file
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy tên file và đường dẫn
                    string fileName = ofd.SafeFileName.ToString();
                    string filePath = ofd.FileName.ToString();

                    // Đọc nội dung file
                    string fileContent = File.ReadAllText(filePath);
                    rtxt_Hien_thi.Text = fileContent; // Hiển thị nội dung trong RichTextBox

                    // Đếm số dòng, số từ, số ký tự
                    int lineCount = fileContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                    int wordCount = fileContent.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                    int charCount = fileContent.Length;

                    // Lấy kích thước file
                    long fileSize = new FileInfo(filePath).Length;

                    // Hiển thị thông tin file
                    txt_File_name.Text = fileName;
                    txt_Size.Text = fileSize + " bytes";
                    txt_URL.Text = filePath;
                    txt_Line_count.Text = lineCount.ToString();
                    txt_Words_count.Text = wordCount.ToString();
                    txt_Char_count.Text = charCount.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
