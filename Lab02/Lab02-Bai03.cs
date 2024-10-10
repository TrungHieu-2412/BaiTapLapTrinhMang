using System;
using System.IO;
using System.Windows.Forms;
using NCalc; // Thêm thư viện NCalc vào giúp thực hiện các thao tác tính toán phức tạp hơn 

namespace Lab02
{
    public partial class Lab02_Bai03 : Form
    {
        private string duongDanFile = "";// Biến lưu đường dẫn file được chọn
        public Lab02_Bai03()
        {
            InitializeComponent();
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog hopThoaiMoFile = new OpenFileDialog();
            hopThoaiMoFile.Filter = "File Văn Bản|*.txt|Tất Cả Các File|*.*";
            if (hopThoaiMoFile.ShowDialog() == DialogResult.OK)
            {
                duongDanFile = hopThoaiMoFile.FileName;
                // Sử dụng FileStream và StreamReader để đọc file
                using (FileStream fileStream = new FileStream(duongDanFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader docFile = new StreamReader(fileStream))
                    {
                        txtInput.Text = docFile.ReadToEnd();
                    }
                }
            }
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(duongDanFile))
            {
                MessageBox.Show("Vui lòng chọn file !");
                return;
            }

            // Đọc file từng dòng và lưu vào mảng
            string[] cacdong;
            using (FileStream fileStream = new FileStream(duongDanFile, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader docFile = new StreamReader(fileStream))
                {
                    var noiDungFile = docFile.ReadToEnd();
                    cacdong = noiDungFile.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                }
            }

            // Tạo chuỗi để lưu kết quả tính toán
            string chuoiKQ = "";

            // Thực hiện tính toán cho từng dòng
            foreach (string dong in cacdong)
            {
                try
                {
                    // Loại bỏ các khoảng trắng không cần thiết
                    string bieuThuc = dong.Trim();

                    // Kiểm tra xem dòng có trống hay không
                    if (string.IsNullOrWhiteSpace(bieuThuc))
                    {
                        continue; // Bỏ qua các dòng trống
                    }

                    // Thay dấu ',' bằng dấu '.' để đảm bảo tính đúng số thập phân
                    bieuThuc = bieuThuc.Replace(",", ".");

                    // Sử dụng NCalc để tính toán giá trị biểu thức
                    Expression expression = new Expression(bieuThuc);

                    // Tính kết quả của biểu thức
                    var ketQua = expression.Evaluate();

                    // Định dạng kết quả và thêm vào chuỗi kết quả
                    chuoiKQ += bieuThuc + " = " + ketQua + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi trong quá trình tính toán, hiển thị thông báo lỗi
                    chuoiKQ += dong + " = Lỗi: " + ex.Message + Environment.NewLine;
                }
            }

            // Hiển thị kết quả lên txtOutput
            txtOutput.Text = chuoiKQ;

            // Ghi kết quả vào file
            string thuMucKetQua = @"C:\path\to\output";
            string ketQuaFile = Path.Combine(thuMucKetQua, "KetQua.txt");

            if (!Directory.Exists(thuMucKetQua))
            {
                Directory.CreateDirectory(thuMucKetQua);
            }

            try
            {
                using (FileStream fileStream = new FileStream(ketQuaFile, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter ghiFile = new StreamWriter(fileStream))
                    {
                        ghiFile.Write(chuoiKQ); // Ghi kết quả vào file
                    }
                }

                MessageBox.Show("Kết quả đã được ghi vào file thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi file: " + ex.Message);
            }
        }

    }
}