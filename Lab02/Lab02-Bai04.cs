using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Lab02
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Phone { get; set; }
        public float Course1 { get; set; }
        public float Course2 { get; set; }
        public float Course3 { get; set; }
        public float Average { get; set; }
        public void CalculateAverage()
        {
            Average = (Course1 + Course2 + Course3) / 3;
        }
        public override string ToString()
        {
            return $"{Name}\n{ID}\n{Phone}\n{Course1}\n{Course2}\n{Course3}\n{Average}";
        }
    }


    public partial class Lab02_Bai04 : System.Windows.Forms.Form
    {
        private List<Student> students = new List<Student>();
        private int currentPage = 1;
        private int pageSize = 5; // Số sinh viên mỗi trang
        private string inputFilePath = "input4.txt";
        private string outputFilePath = "output4.txt";

        public Lab02_Bai04()
        {
            InitializeComponent();
            AddButton.Click += new EventHandler(AddButton_Click);
            WriteToFileButton.Click += new EventHandler(WriteToFileButton_Click);
            ReadFromFileButton.Click += new EventHandler(ReadFromFileButton_Click);
            NextPageButton.Click += new EventHandler(NextPageButton_Click);
            PreviousPageButton.Click += new EventHandler(PreviousPageButton_Click);
        }
        private void Lab02_Bai04_Load(object sender, EventArgs e)
        {

        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string name = nameTextBox.Text;
            int id;
            if (!int.TryParse(idTextBox.Text, out id) || id.ToString().Length != 8)
            {
                MessageBox.Show("Mã số sinh viên phải có 8 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string phone = phoneTextBox.Text;
            if (!Regex.IsMatch(phone, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            float course1, course2, course3;
            if (!float.TryParse(course1TextBox.Text, out course1) || course1 < 0 || course1 > 10 ||
                !float.TryParse(course2TextBox.Text, out course2) || course2 < 0 || course2 > 10 ||
                !float.TryParse(course3TextBox.Text, out course3) || course3 < 0 || course3 > 10)
            {
                MessageBox.Show("Điểm các môn học phải từ 0 đến 10.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Thêm sinh viên mới vào danh sách
            Student newStudent = new Student
            {
                Name = name,
                ID = id,
                Phone = phone,
                Course1 = course1,
                Course2 = course2,
                Course3 = course3
            };
            students.Add(newStudent);
            MessageBox.Show("Thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa thông tin trong các TextBox
            ClearInputFields();

            // Hiển thị danh sách sinh viên
            DisplayStudents();
        }

        private void ClearInputFields()
        {
            nameTextBox.Text = "";
            idTextBox.Text = "";
            phoneTextBox.Text = "";
            course1TextBox.Text = "";
            course2TextBox.Text = "";
            course3TextBox.Text = "";
        }

        private void WriteToFileButton_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(inputFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, students);
            }
            MessageBox.Show("Ghi vào file thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void ReadFromFileButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(inputFilePath))
            {
                MessageBox.Show("File không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (FileStream fs = new FileStream(inputFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                students = (List<Student>)formatter.Deserialize(fs);
            }

            // Tính điểm trung bình cho từng sinh viên
            foreach (var student in students)
            {
                student.CalculateAverage();
            }

            // Ghi thông tin sinh viên có điểm trung bình xuống file output4.txt
            WriteToOutputFile();
            DisplayStudents();

            // Hiển thị sinh viên đầu tiên lên phần bên phải
            if (students.Count > 0)
            {
                currentPage = 1; // Đặt trang hiện tại là 1
                DisplayStudentOnRight(0); // Hiển thị sinh viên đầu tiên
            }
        }

        private void DisplayStudentOnRight(int studentIndex)
        {
            if (studentIndex >= 0 && studentIndex < students.Count)
            {
                Student student = students[studentIndex];
                nameTextBoxRight.Text = student.Name;
                idTextBoxRight.Text = student.ID.ToString();
                phoneTextBoxRight.Text = student.Phone;
                course1TextBoxRight.Text = student.Course1.ToString();
                course2TextBoxRight.Text = student.Course2.ToString();
                course3TextBoxRight.Text = student.Course3.ToString();
                averageTextBoxRight.Text = student.Average.ToString("F2");
            }
        }

        private void WriteToOutputFile()
        {
            using (FileStream fs = new FileStream(outputFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, students);
            }
        }

        private void DisplayStudents()
        {
            int start = (currentPage - 1) * pageSize;
            int end = Math.Min(start + pageSize, students.Count);
            studentListBox.Items.Clear();

            for (int i = start; i < end; i++)
            {
                studentListBox.Items.Add(students[i]);
            }
            currentPageLabel.Text = $"{currentPage} / {Math.Ceiling((double)students.Count / pageSize)}";
        }

        private void NextPageButton_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu vẫn còn trang tiếp theo để chuyển đến
            if (currentPage * pageSize < students.Count)
            {
                currentPage++;
                DisplayStudents();
                DisplayStudentOnRight((currentPage - 1) * pageSize); // Hiển thị sinh viên tương ứng trên TextBox
            }
        }
        private void PreviousPageButton_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu vẫn còn trang trước đó để quay lại
            if (currentPage > 1)
            {
                currentPage--;
                DisplayStudents();
                DisplayStudentOnRight((currentPage - 1) * pageSize); // Hiển thị sinh viên tương ứng trên TextBox
            }
        }
    }
}
