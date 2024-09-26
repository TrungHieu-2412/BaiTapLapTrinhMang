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
        private string sourcePath = "";
        private string destinationPath = "";
        private bool isCut = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Copy function
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                sourcePath = listView1.SelectedItems[0].Tag.ToString();
                isCut = false;
            }
        }

        // Cut function
        private void btnCut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                sourcePath = listView1.SelectedItems[0].Tag.ToString();
                isCut = true;
            }
        }

        // Paste function (now asynchronous)
        private async void btnPaste_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sourcePath) && !string.IsNullOrEmpty(destinationPath))
            {
                string fileName = Path.GetFileName(sourcePath);
                string destFile = Path.Combine(destinationPath, fileName);

                if (isCut)
                {
                    File.Move(sourcePath, destFile);
                    isCut = false;
                }
                else
                {
                    // Use async copy
                    await CopyFileWithStreamAsync(sourcePath, destFile);
                }

                MessageBox.Show("Paste operation completed!");
            }
        }

        // Delete function
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].Tag.ToString();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }



        // Asynchronous function to copy large files using streams
        private async Task CopyFileWithStreamAsync(string source, string destination)
        {
            using (FileStream sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read))
            {
                using (FileStream destinationStream = new FileStream(destination, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
                    int bytesRead;
                    while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await destinationStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            }
        }

        // Function to load folders and files
        private void LoadFoldersAndFiles(string folderPath)
        {
            // Load folders and files into treeViewFolders and listViewFiles
        }

        // Event handler when a folder is selected in the TreeView
        private void treeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            destinationPath = e.Node.FullPath; // Set the destination path for paste operation
            LoadFoldersAndFiles(destinationPath);
        }
    }
}