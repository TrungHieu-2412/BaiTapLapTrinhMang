using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab02
{
    public partial class Lab02_Bai05 : Form
    {
        public Lab02_Bai05()
        {
            InitializeComponent();
            LoadDrives(); // Load danh sách các ổ đĩa khi form khởi động
            treeView1.BeforeExpand += TreeView1_BeforeExpand; // Sự kiện mở thư mục
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick; // Sự kiện click vào file hoặc thư mục
        }

        // Tải danh sách các ổ đĩa
        private void LoadDrives()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    TreeNode node = new TreeNode(drive.Name);
                    node.Tag = drive.RootDirectory;
                    node.Nodes.Add(new TreeNode()); // Thêm node giả để hiển thị dấu + (lazy loading)
                    treeView1.Nodes.Add(node);
                }
            }
        }

        // Tải thư mục con khi node được mở
        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;

            // Nếu node có thư mục con giả (node rỗng), thì nạp thư mục thật
            if (node.Nodes.Count == 1 && node.Nodes[0].Tag == null)
            {
                node.Nodes.Clear(); // Xóa node giả
                LoadDirectories(node); // Nạp thư mục thật
            }
        }

        // Tải các thư mục con và tập tin của một thư mục
        private void LoadDirectories(TreeNode node)
        {
            DirectoryInfo directory = (DirectoryInfo)node.Tag;

            try
            {
                foreach (DirectoryInfo dir in directory.GetDirectories())
                {
                    TreeNode childNode = new TreeNode(dir.Name);
                    childNode.Tag = dir;
                    childNode.Nodes.Add(new TreeNode()); // Thêm node giả để lazy load
                    node.Nodes.Add(childNode);
                }

                foreach (var file in directory.GetFiles())
                {
                    TreeNode fileNode = new TreeNode(file.Name);
                    fileNode.Tag = file;
                    node.Nodes.Add(fileNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Không có quyền truy cập vào thư mục
            }
        }

        // Xử lý sự kiện click vào node
        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is FileInfo fileInfo)
            {
                DisplayFileContent(fileInfo); // Hiển thị nội dung file
            }
            else if (e.Node.Tag is DirectoryInfo)
            {
                // Chọn thư mục, xóa nội dung trước đó
                txtDocument.Clear();
                picImage.Image = null;
            }
        }

        // Hiển thị nội dung file văn bản hoặc hình ảnh
        private void DisplayFileContent(FileInfo fileInfo)
        {
            string extension = fileInfo.Extension.ToLower();

            if (extension == ".txt")
            {
                // Đọc nội dung file văn bản
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    txtDocument.Text = sr.ReadToEnd();
                }
                picImage.Image = null; // Xóa hình ảnh
            }
            else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
            {
                // Hiển thị hình ảnh và cho hình ảnh chiếm trọn picImage
                picImage.SizeMode = PictureBoxSizeMode.Zoom;
                picImage.Image = Image.FromFile(fileInfo.FullName);
                txtDocument.Clear(); // Xóa nội dung văn bản
            }
            else
            {
                txtDocument.Clear();
                picImage.Image = null; // Xóa hình ảnh
                MessageBox.Show("Chỉ hỗ trợ file văn bản và hình ảnh!");
            }
        }
    }
}
