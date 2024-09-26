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

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        private string currentPath = "";
        private Stack<string> backStack = new Stack<string>();
        private Stack<string> forwardStack = new Stack<string>();
        private List<string> _imageFiles;
        private int _currentIndex;
        private object pbMainImage;

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Date Modified", 150);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Size", 100);
            // Đặt đường dẫn mặc định
            currentPath = @"C:\";
            txtPath.Text = currentPath;

            // Tải thư mục mặc định
            LoadDirectory(currentPath);
            
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có mục nào được chọn trong ListView
            if (listView1.SelectedItems.Count > 0)
            {
                // Lấy item đầu tiên được chọn
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string selectedName = selectedItem.Text;  // Lấy tên file hoặc thư mục từ item
                string selectedPath = Path.Combine(currentPath, selectedName);  // Tạo đường dẫn đầy đủ

                // Kiểm tra nếu mục được chọn là thư mục
                if (Directory.Exists(selectedPath))
                {
                    backStack.Push(currentPath);  // Đẩy thư mục hiện tại vào stack để quay lại
                    currentPath = selectedPath;   // Cập nhật đường dẫn hiện tại
                    LoadDirectory(currentPath);   // Tải thư mục mới
                    txtPath.Text = currentPath;   // Cập nhật TextBox hiển thị đường dẫn
                }
                // Kiểm tra nếu mục được chọn là file
                else if (File.Exists(selectedPath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(selectedPath);  // Mở file bằng ứng dụng mặc định
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to open file: " + ex.Message);  // Thông báo lỗi
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a file or folder to open.");
            }
        }

        private void LoadDirectory(string path)
        {
            try
            {
                listView1.Items.Clear();  // Xóa các mục hiện có trong ListView

                // Lấy danh sách các thư mục và tệp trong thư mục hiện tại
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                foreach (string directory in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    ListViewItem item = new ListViewItem(dirInfo.Name);
                    item.SubItems.Add(dirInfo.LastWriteTime.ToString());
                    item.SubItems.Add("Folder");
                    item.SubItems.Add("");  // Thư mục không có kích thước
                    listView1.Items.Add(item);
                }

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(fileInfo.LastWriteTime.ToString());
                    item.SubItems.Add(fileInfo.Extension);
                    item.SubItems.Add(fileInfo.Length.ToString());
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading directory: " + ex.Message);  // Thông báo nếu có lỗi
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (backStack.Count > 0)
            {
                forwardStack.Push(currentPath);  // Lưu đường dẫn hiện tại vào forwardStack
                currentPath = backStack.Pop();   // Lấy đường dẫn trước đó từ backStack
                LoadDirectory(currentPath);      // Tải thư mục trước đó
                txtPath.Text = currentPath;      // Cập nhật TextBox
            }
            else
            {
                MessageBox.Show("No more history to go back to.");
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (forwardStack.Count > 0)
            {
                backStack.Push(currentPath);  // Lưu đường dẫn hiện tại vào backStack
                currentPath = forwardStack.Pop();  // Lấy đường dẫn tiếp theo từ forwardStack
                LoadDirectory(currentPath);   // Tải thư mục
                txtPath.Text = currentPath;   // Cập nhật TextBox
            }
            else
            {
                MessageBox.Show("No more forward history.");
            }
        }
        
    }
}