using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ungdungve
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point lastPoint;
        private Bitmap drawingBitmap;
        private Graphics bitmapGraphics;
        private Color currentColor = Color.Black;
        private int brushSize = 5;
        private List<Bitmap> bitmapHistory = new List<Bitmap>(); // Lưu trữ trạng thái Bitmap

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load; // Thêm sự kiện Load
            drawingPanel.Paint += new PaintEventHandler(drawingPanel_Paint);
            drawingPanel.MouseDown += new MouseEventHandler(drawingPanel_MouseDown);
            drawingPanel.MouseMove += new MouseEventHandler(drawingPanel_MouseMove);
            drawingPanel.MouseUp += new MouseEventHandler(drawingPanel_MouseUp);
            btnChooseColor.Click += new EventHandler(btnChooseColor_Click);
            cbBrushSize.SelectedIndexChanged += new EventHandler(cbBrushSize_SelectedIndexChanged);
            btnClear.Click += new EventHandler(btnClear_Click);
            btnUndo.Click += new EventHandler(btnUndo_Click); // Đăng ký sự kiện hoàn tác
            drawingPanel.Resize += new EventHandler(drawingPanel_Resize); // Đăng ký sự kiện Resize

            // Bật double buffering
            this.DoubleBuffered = true;
            drawingPanel.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(drawingPanel, true, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            drawingPanel.Dock = DockStyle.Fill; // Panel sẽ tự động thay đổi kích thước theo Form
            InitializeDrawingBitmap();
            cbBrushSize.Items.AddRange(new object[] { 1, 3, 5, 7, 10 });
            cbBrushSize.SelectedItem = 5; // Thiết lập mặc định là 5
        }

        private void InitializeDrawingBitmap()
        {
            drawingBitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
            bitmapGraphics = Graphics.FromImage(drawingBitmap);
            bitmapGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bitmapGraphics.Clear(Color.White); // Clear the background to white
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (drawingBitmap != null)
            {
                e.Graphics.DrawImage(drawingBitmap, Point.Empty);
            }
        }

        private void drawingPanel_Resize(object sender, EventArgs e)
        {
            if (drawingBitmap != null)
            {
                Bitmap resizedBitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
                Graphics resizedGraphics = Graphics.FromImage(resizedBitmap);
                resizedGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                resizedGraphics.Clear(Color.White); // Ensure the new background is white
                resizedGraphics.DrawImage(drawingBitmap, 0, 0); // Copy the old drawing to the new resized bitmap

                drawingBitmap.Dispose();
                drawingBitmap = resizedBitmap;
                bitmapGraphics = resizedGraphics;

                drawingPanel.Invalidate();
            }
        }

        private void drawingPanel_MouseDown(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Lưu trạng thái hiện tại của Bitmap vào danh sách trước khi vẽ
                bitmapHistory.Add((Bitmap)drawingBitmap.Clone());
                isDrawing = true;
                lastPoint = e.Location;
            }
        }

        private void drawingPanel_MouseMove(Object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Tính toán vùng cần vẽ lại
                Rectangle invalidRect = new Rectangle(Math.Min(lastPoint.X, e.Location.X) - brushSize,
                                                      Math.Min(lastPoint.Y, e.Location.Y) - brushSize,
                                                      Math.Abs(lastPoint.X - e.Location.X) + brushSize * 2,
                                                      Math.Abs(lastPoint.Y - e.Location.Y) + brushSize * 2);

                // Chỉ vẽ nếu khoảng cách giữa hai điểm lớn hơn 1 pixel
                if (Math.Abs(lastPoint.X - e.Location.X) > 1 || Math.Abs(lastPoint.Y - e.Location.Y) > 1)
                {
                    using (Pen pen = new Pen(currentColor, brushSize))
                    {
                        pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round; // Đầu bút tròn
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; // Góc nối tròn
                        bitmapGraphics.DrawLine(pen, lastPoint, e.Location);
                    }

                    lastPoint = e.Location;
                    drawingPanel.Invalidate(invalidRect); // Chỉ vẽ lại vùng cần thiết
                }
            }
        }

        private void drawingPanel_MouseUp(Object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Lưu trạng thái hiện tại của Bitmap vào danh sách khi kết thúc vẽ
                bitmapHistory.Add((Bitmap)drawingBitmap.Clone());
                isDrawing = false;
            }
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }

        private void cbBrushSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBrushSize.SelectedItem != null)
            {
                brushSize = (int)cbBrushSize.SelectedItem;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            bitmapHistory.Add((Bitmap)drawingBitmap.Clone()); // Lưu trạng thái trước khi xóa
            bitmapGraphics.Clear(Color.White); // Xóa với màu trắng
            drawingPanel.Invalidate();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void Undo()
        {
            if (bitmapHistory.Count > 0)
            {
                drawingBitmap.Dispose();
                drawingBitmap = (Bitmap)bitmapHistory.Last().Clone();
                bitmapHistory.RemoveAt(bitmapHistory.Count - 1);
                bitmapGraphics = Graphics.FromImage(drawingBitmap);
                bitmapGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                drawingPanel.Invalidate();
            }
        }
    }
}
