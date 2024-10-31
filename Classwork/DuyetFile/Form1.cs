using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Duyet_Picture
{
    public partial class Form1 : Form
    {
        private List<string> _imageFiles;
        private int _currentIndex;

        public Form1()
        {
            InitializeComponent();
            _imageFiles = new List<string>();
            _currentIndex = -1;

            // Gán các sự kiện
            this.KeyDown += Form1_KeyDown;
            lbThumbnails.SelectedIndexChanged += lbThumbnails_SelectedIndexChanged;
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadImages(folderDialog.SelectedPath);
                }
            }
        }

        private void LoadImages(string path)
        {
            try
            {
                _imageFiles = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                s.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                s.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                s.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                lbThumbnails.Items.Clear();
                foreach (var file in _imageFiles)
                {
                    lbThumbnails.Items.Add(Path.GetFileName(file));
                }

                if (_imageFiles.Count > 0)
                {
                    _currentIndex = 0;
                    ShowImage(_imageFiles[_currentIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowImage(string filePath)
        {
            try
            {
                pbMainImage.Image?.Dispose(); // Giải phóng ảnh trước đó
                pbMainImage.Image = Image.FromFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi hiển thị ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbThumbnails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbThumbnails.SelectedIndex >= 0)
            {
                _currentIndex = lbThumbnails.SelectedIndex;
                ShowImage(_imageFiles[_currentIndex]);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_imageFiles.Count == 0) return;

            if (e.KeyCode == Keys.Right)
            {
                _currentIndex = (_currentIndex + 1) % _imageFiles.Count;
                lbThumbnails.SelectedIndex = _currentIndex;
                ShowImage(_imageFiles[_currentIndex]);
            }
            else if (e.KeyCode == Keys.Left)
            {
                _currentIndex = (_currentIndex - 1 + _imageFiles.Count) % _imageFiles.Count;
                lbThumbnails.SelectedIndex = _currentIndex;
                ShowImage(_imageFiles[_currentIndex]);
            }
        }

    }
}
