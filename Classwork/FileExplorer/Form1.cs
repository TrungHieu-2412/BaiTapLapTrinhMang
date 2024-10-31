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
        private string copiedPath;
        private bool isCut;

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
            listView1.MouseDoubleClick += listView1_MouseDoubleClick;

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
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string selectedPath = Path.Combine(currentPath, selectedItem.Text);

                if (Directory.Exists(selectedPath))
                {
                    backStack.Push(currentPath);
                    currentPath = selectedPath;
                    LoadDirectory(currentPath);
                    txtPath.Text = currentPath;
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Process.Start(selectedPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở tệp: " + ex.Message);
                    }
                }
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
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = listView1.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    listView1.SelectedItems.Clear();
                    item.Selected = true;
                    cmnuMenu.Show(listView1, e.Location);
                }
            }
        }

       

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                copiedPath = Path.Combine(currentPath, selectedItem.Text);
                isCut = false;
                MessageBox.Show("Copied: " + copiedPath);
            }
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                copiedPath = Path.Combine(currentPath, selectedItem.Text);
                isCut = true;
                MessageBox.Show("Cut: " + copiedPath);
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(copiedPath))
            {
                string destinationPath = Path.Combine(currentPath, Path.GetFileName(copiedPath));

                try
                {
                    if (isCut)
                    {
                        File.Move(copiedPath, destinationPath);
                        MessageBox.Show("Moved: " + destinationPath);
                    }
                    else
                    {
                        File.Copy(copiedPath, destinationPath);
                        MessageBox.Show("Copied to: " + destinationPath);
                    }

                    LoadDirectory(currentPath);
                    copiedPath = "";
                    isCut = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error pasting file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No file or folder to paste.");
            }
        }
        private async Task CopyFileAsync(string sourceFilePath, string destinationFilePath)
        {
            using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                long totalBytes = sourceStream.Length;
                byte[] buffer = new byte[4096]; // Bộ đệm 4KB
                int bytesRead;
                long totalRead = 0;

                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await destinationStream.WriteAsync(buffer, 0, bytesRead);
                    totalRead += bytesRead;

                    // Tùy chọn: Hiển thị tiến trình (có thể sử dụng ProgressBar)
                    Console.WriteLine($"Tiến trình: {(totalRead / (float)totalBytes) * 100}%");
                }
            }
        }

        private async Task MoveFileAsync(string sourceFilePath, string destinationFilePath)
        {
            await CopyFileAsync(sourceFilePath, destinationFilePath);
            File.Delete(sourceFilePath);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string selectedPath = Path.Combine(currentPath, selectedItem.Text);

                try
                {
                    if (Directory.Exists(selectedPath))
                    {
                        Directory.Delete(selectedPath, true);
                    }
                    else if (File.Exists(selectedPath))
                    {
                        File.Delete(selectedPath);
                    }

                    LoadDirectory(currentPath);
                    MessageBox.Show("Deleted: " + selectedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a file or folder to delete.");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại để nhập tên thư mục mới
            string folderName = Microsoft.VisualBasic.Interaction.InputBox("Enter folder name:", "New Folder", "New Folder");

            if (!string.IsNullOrWhiteSpace(folderName))
            {
                // Tạo đường dẫn đầy đủ cho thư mục mới
                string newFolderPath = Path.Combine(currentPath, folderName);

                try
                {
                    // Kiểm tra xem thư mục đã tồn tại chưa
                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath); // Tạo thư mục mới
                        MessageBox.Show("Folder created: " + newFolderPath);
                        LoadDirectory(currentPath); // Tải lại thư mục để cập nhật danh sách
                    }
                    else
                    {
                        MessageBox.Show("Folder already exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Folder name cannot be empty.");
            }
        }
    }
}