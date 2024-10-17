using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02
{
    public partial class Lab02_Bai04 : Form
    {

        int index = 0;


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
            //public override string ToString()
            //{
            //    return $"{Name}\n{ID}\n{Phone}\n{Course1}\n{Course2}\n{Course3}\n{Average}";
            //}
        }

        private List<Student> students = new List<Student>();


        public Lab02_Bai04()
        {
            InitializeComponent();
            AddButton.Click += new EventHandler(AddButton_Click);
            WriteToFileButton.Click += new EventHandler(WriteToFileButton_Click);
            ReadFromFileButton.Click += new EventHandler(ReadFromFileButton_Click);
            NextButton.Click += new EventHandler(NextButton_Click);
            BackButton.Click += new EventHandler(BackButton_Click);
        }


        //ADD BUTTON
        private void AddButton_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox va check dieu kien
            string name = nameTextBox.Text;
            int id;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Nhập đầy đủ thông tin trước khi ghi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            //Hien thi thong tin tren cai Man hinh chinh giua
            richTextBoxFile.Text += (nameTextBox.Text + Environment.NewLine);
            richTextBoxFile.Text += (idTextBox.Text + Environment.NewLine);
            richTextBoxFile.Text += (phoneTextBox.Text + Environment.NewLine);
            richTextBoxFile.Text += (course1TextBox.Text + Environment.NewLine);
            richTextBoxFile.Text += (course2TextBox.Text + Environment.NewLine);
            richTextBoxFile.Text += (course3TextBox.Text + Environment.NewLine);
            richTextBoxFile.Text += Environment.NewLine;

            // Thêm sinh viên mới vào danh sách
            Student newStudent = new Student
            {
                Name = name,
                ID = id,
                Phone = phone,
                Course1 = course1,
                Course2 = course2,
                Course3 = course3,
                Average = (course1 + course2 + course3) / 3
            };
            students.Add(newStudent);
            // Xóa thông tin trong các TextBox
            ClearInputFields();
            MessageBox.Show("Thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        //WRITE TO A FILE 
        private void WriteToFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, richTextBoxFile.Text);
                }
                MessageBox.Show("Ghi thành công vào file: " + sfd.FileName.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ReadFromFileButton.Enabled = true;
        }



        //READ FROM A FILE 
        private void ReadFromFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    richTextBoxFile.Text = (string)formatter.Deserialize(fs);
                }

                //Chua hieu Index o dau ra
                DisplayStudentOnRight(index);

            }
            BackButton.Enabled = false;
            NextButton.Enabled = true;
            //richTextBoxFile.Clear();
        }




        private void DisplayStudentOnRight(int a)
        {
            nameTextBoxRight.Text = students[a].Name;
            idTextBoxRight.Text = students[a].ID.ToString();
            phoneTextBoxRight.Text = students[a].Phone.ToString();
            course1TextBoxRight.Text = students[a].Course1.ToString();
            course2TextBoxRight.Text = students[a].Course2.ToString();
            course3TextBoxRight.Text = students[a].Course3.ToString();
            averageTextBoxRight.Text = students[a].Average.ToString();

            studentIndexLabel.Text = $"{a + 1}";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                DisplayStudentOnRight(index);
            }
            if (index < students.Count() - 1)
            {
                NextButton.Enabled = true;
            }
            if (index > 0)
            {
                BackButton.Enabled = true;
            }
            if (index == 0)
            {
                BackButton.Enabled = false;
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (index < students.Count() - 1)
            {
                index++;
                DisplayStudentOnRight(index);
            }
            if (index > 0)
            {
                BackButton.Enabled = true;
            }
            if (index == students.Count() - 1)
            {
                NextButton.Enabled = false;
            }
        }


        private void Lab02_Bai04_Load(object sender, EventArgs e)
        {

        }
    }
}
