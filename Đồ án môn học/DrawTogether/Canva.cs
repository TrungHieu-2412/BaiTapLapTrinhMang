using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Net.Sockets;

namespace DrawTogether
{
    public partial class Canva : Form
    {
        private ClientManager clientManager;
        private string roomCode;
        private string userName;
        // Khởi tạo các giá trị cho bảng vẽ
        private Bitmap bitmap;                                   // Tạo vùng vẽ trên Canvas.
        private Graphics graphics;                               // Thao tác và vẽ lên Bitmap.
        private Boolean cursorMoving = false;                    // Kiểm tra xem chuột có đang di chuyển khi nhấn giữ không.
        private Pen cursorPen;                                   // Pen đại diện cho bút vẽ, định nghĩa các thuộc tính như màu sắc, độ rộng của nét vẽ.
        private int cursorX = -1;
        private int cursorY = -1;
        private Point p = new Point();                           // Point đại diện cho tọa độ x và y trong không gian 2D.
        private Color stateColor;                                // Lưu màu hiện tại của bút vẽ.
        private int shapeTag = 10;                               // Lưu trữ loại hình mà người dùng muốn vẽ.
        private List<Point> points_1 = new List<Point>();        // Lưu điểm đầu của mỗi đường vẽ.
        private List<Point> points_2 = new List<Point>();        // Lưu điểm cuối của mỗi đường vẽ.
        private bool isEraserMode = false;                       // Biến để kiểm tra chế độ xóa
        private Color currentColor = Color.Black;                // Màu mặc định của bút vẽ
        private int brushSize = 5;                               // Kích thước mặc định của bút vẽ
        private Bitmap backupBitmap;                             // Để giữ bitmap trước khi thay đổi kích thước
        private Stack<Bitmap> bitmapHistory = new Stack<Bitmap>();
        public Canva(ClientManager clientManager, string roomCode, string userName)
        {
            this.clientManager = clientManager;
            this.roomCode = roomCode;
            this.userName = userName;
            InitializeComponent();
            // Kiểm tra xem Canvas đã được khởi tạo chưa
            if (Canvas == null)
            {
                throw new Exception("Canvas chưa được khởi tạo.");
            }

            // Tạo bảng vẽ và bút
            bitmap = new Bitmap(Canvas.Width, Canvas.Height);  // Tạo một Bitmap với kích thước của Canvas.
            graphics = Graphics.FromImage(bitmap);             // Tạo đối tượng Graphics từ Bitmap để vẽ lên đó.
            graphics.Clear(Color.White);                       // Làm sạch vùng vẽ, đặt màu nền là trắng.
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Bật chế độ vẽ mượt mà để các nét vẽ trông mềm mại hơn.
            Canvas.Image = bitmap;                             // Hiển thị Bitmap lên Canvas.
            stateColor = Color.Black;                          // Đặt màu mặc định của bút vẽ là màu đen.
            cursorPen = new Pen(stateColor, 2);                // Tạo một đối tượng Pen với màu đen và độ rộng 2px.
            PenOptimizer(cursorPen);                           // Gọi hàm PenOptimizer để tối ưu hóa bút vẽ.
            this.ActiveControl = null;                         // Đảm bảo không có điều khiển nào được chọn mặc định khi form mở.
            this.Resize += new EventHandler(Form_Canva_Resize); // Lắng nghe sự kiện Resize
            this.Load += new System.EventHandler(this.Form_Canva_Load);
            txtRoomCodeCanva.Text = roomCode;
            txtPlayerName.Text = userName;
            clientManager.DataReceived += ClientManager_DataReceived;
        }
        private void ClientManager_DataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = e.Data;
            // Xử lý dữ liệu nhận được từ server
            if (data.StartsWith("RoomCode:"))
            {
                // Nhận room code từ server
                roomCode = data.Substring(9);
                // Hiển thị room code trên form DrawTogether
                this.Invoke((MethodInvoker)delegate
                {
                    txtRoomCodeCanva.Text = roomCode; // Sử dụng txtRoomCodeCanva
                });
            }
            else if (data.StartsWith("Players:"))
            {
                // Nhận danh sách người chơi từ server
                List<string> players = data.Substring(8).Split(',').ToList();
                // Hiển thị danh sách người chơi trên form DrawTogether
                this.Invoke((MethodInvoker)delegate
                {
                    txtPlayerName.Text = string.Join(", ", players); // Sử dụng txtPlayerName
                });
            }
            else if (data.StartsWith("Vẽ:"))
            {
                // Lấy thông tin từ data
                string[] parts = data.Substring(3).Split(',');
                int x1 = int.Parse(parts[0]);
                int y1 = int.Parse(parts[1]);
                Color color = Color.FromArgb(int.Parse(parts[2]));
                int width = int.Parse(parts[3]);

                // Vẽ nét vẽ trên Canvas
                graphics.DrawLine(new Pen(color, width), new Point(x1, y1), new Point(x1 + 1, y1 + 1)); // Thay đổi cách vẽ để đồng bộ với server
                Canvas.Invalidate();
            }
        }
        public Canva()
        {
        }

        private List<Tuple<Point, Point, Pen>> drawnLines = new List<Tuple<Point, Point, Pen>>(); // Lưu trữ các đường đã vẽ
        private void Form_Canva_Resize(object sender, EventArgs e)
        {
            if (Canvas.Width > 0 && Canvas.Height > 0 && (Canvas.Width != bitmap.Width || Canvas.Height != bitmap.Height))
            {
                // Giữ lại bitmap cũ để khôi phục sau khi thay đổi kích thước
                backupBitmap = (Bitmap)bitmap.Clone();
                // Tạo lại bitmap với kích thước mới của Canvas
                Bitmap newBitmap = new Bitmap(Canvas.Width, Canvas.Height);
                using (Graphics newGraphics = Graphics.FromImage(newBitmap))
                {
                    newGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Làm sạch và đặt nền trắng cho bitmap mới
                    newGraphics.Clear(Color.White);

                    // Vẽ lại chỉ những nét vẽ chưa bị hoàn tác
                    foreach (var line in drawnLines)
                    {
                        newGraphics.DrawLine(line.Item3, line.Item1, line.Item2);
                    }
                }

                // Cập nhật bitmap và Graphics sau khi thay đổi kích thước
                bitmap = newBitmap;
                graphics = Graphics.FromImage(bitmap); // Cập nhật lại Graphics để thao tác trên bitmap mới
                Canvas.Image = bitmap;
                Canvas.Invalidate();

                // Cập nhật lại bitmapHistory sau khi thay đổi kích thước
                bitmapHistory.Clear();
                bitmapHistory.Push((Bitmap)bitmap.Clone()); // Lưu trạng thái mới của bitmap vào stack
            }
        }
        private void PenOptimizer(Pen pen)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; // Đặt đầu bút tròn.
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;   // Đặt cuối bút tròn.
        }
        private void Form_Canva_Load(object sender, EventArgs e)
        {
            Canvas.Dock = DockStyle.Fill; // Panel sẽ tự động thay đổi kích thước theo Form
            InitializeDrawingBitmap();
            cbBrushSize.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 17, 20, 30 });
            cbBrushSize.SelectedItem = 5; // Thiết lập mặc định là 5
            cbBrushSize.SelectedIndexChanged += new EventHandler(cbBrushSize_SelectedIndexChanged); // Kết nối sự kiện thay đổi kích thước bút
        }
        // Khởi tạo vùng vẽ ban đầu
        private void InitializeDrawingBitmap()
        {
            if (bitmap != null)
            {
                bitmap.Dispose(); // Giải phóng bitmap cũ
            }
            bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White); // Đặt nền trắng
            Canvas.Image = bitmap;
        }
        // Sự kiện khi nhấn chuột xuống Canvas
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            SaveBitmapState();

            cursorMoving = true; // Bắt đầu vẽ.
            cursorX = e.X; // Lưu tọa độ X của chuột khi bắt đầu nhấn.
            cursorY = e.Y; // Lưu tọa độ Y của chuột khi bắt đầu nhấn.

            if (isEraserMode)
            {
                cursorPen = new Pen(Color.White, brushSize); // Nếu là tẩy thì dùng màu trắng
            }

            else
            {
                cursorPen = new Pen(currentColor, brushSize); // Cập nhật bút vẽ với màu và kích thước hiện tại
            }

            PenOptimizer(cursorPen); // Gọi hàm tối ưu hóa bút vẽ
        }

        // Sự kiện khi di chuyển chuột trên Canvas
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursorMoving && shapeTag == 10)
            {
                // Kiểm tra tọa độ chuột hiện tại có hợp lệ hay không
                if (cursorX >= 0 && cursorY >= 0)
                {
                    p = e.Location;

                    // Lưu tọa độ của đường vẽ vào danh sách các điểm
                    points_1.Add(new Point(cursorX, cursorY)); // Lưu điểm đầu.
                    points_2.Add(p); // Lưu điểm cuối.

                    // Vẽ đường từ điểm đầu tới điểm hiện tại
                    graphics.DrawLine(cursorPen, new Point(cursorX, cursorY), p);

                    // Lưu lại đường vẽ vào danh sách các đường đã vẽ
                    drawnLines.Add(new Tuple<Point, Point, Pen>(new Point(cursorX, cursorY), p, (Pen)cursorPen.Clone()));

                    // Cập nhật lại tọa độ chuột
                    cursorX = e.X;
                    cursorY = e.Y;

                    // Cập nhật lại hình ảnh hiển thị trên Canvas
                    Canvas.Image = bitmap;
                }
            }
        }


        // Sự kiện khi thả chuột ra khỏi Canvas
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            cursorMoving = false; // Ngừng vẽ.
            cursorX = -1;
            cursorY = -1;

            // Làm mới Canvas để hiển thị hình vẽ mới.
            Canvas.Invalidate();
        }

        // Sự kiện khi chọn màu vẽ
        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color; // Cập nhật màu hiện tại
                isEraserMode = false; // Đặt lại chế độ về vẽ khi chọn màu mới
                btnEraser.Enabled = true; // Kích hoạt lại nút tẩy
                btnDrawing.Enabled = false; // Vô hiệu hóa nút vẽ

                // Cập nhật màu sắc trong picColor
                picColor.BackColor = currentColor; // Hiển thị màu đã chọn trong PictureBox
            }
        }
        // Sự kiện khi thay đổi kích thước bút
        private void cbBrushSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbBrushSize.SelectedItem.ToString(), out int size))
            {
                brushSize = size; // Cập nhật kích thước bút vẽ
                cursorPen.Width = brushSize; // Thay đổi kích thước của bút hiện tại
            }
        }

        // Sự kiện khi nhấn nút Eraser (Tẩy)
        private void btnEraser_Click(object sender, EventArgs e)
        {
            isEraserMode = true; // Chuyển sang chế độ tẩy
            cursorPen = new Pen(Color.White, brushSize); // Đặt màu trắng cho tẩy và giữ kích thước bút
            PenOptimizer(cursorPen); // Tối ưu hóa bút vẽ
            btnEraser.Enabled = false; // Vô hiệu hóa nút tẩy khi đang chọn
            btnDrawing.Enabled = true; // Kích hoạt nút vẽ
        }

        // Sự kiện khi nhấn nút Drawing (Vẽ)
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            isEraserMode = false; // Quay lại chế độ vẽ
            cursorPen = new Pen(currentColor, brushSize); // Đặt lại màu của bút vẽ và kích thước bút
            PenOptimizer(cursorPen); // Tối ưu hóa bút vẽ
            btnDrawing.Enabled = false; // Vô hiệu hóa nút vẽ khi đang chọn
            btnEraser.Enabled = true; // Kích hoạt lại nút tẩy
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load hình ảnh từ tệp
                    Bitmap openedBitmap = new Bitmap(openFileDialog.FileName);

                    // Đảm bảo bitmap hiện tại đủ lớn để chứa hình ảnh mới
                    if (openedBitmap.Width > Canvas.Width || openedBitmap.Height > Canvas.Height)
                    {
                        MessageBox.Show("Kích thước ảnh quá lớn so với Canvas hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Làm sạch và vẽ hình ảnh mới lên canvas
                    graphics.Clear(Color.White);
                    graphics.DrawImage(openedBitmap, new Point(0, 0));
                    Canvas.Image = bitmap;
                    Canvas.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở ảnh: " + ex.Message);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Image (*.png)|*.png|All files (*.*)|*.*";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFile.FileName;

                // Kiểm tra xem tệp đã tồn tại
                if (System.IO.File.Exists(filePath))
                {
                    // Hiển thị thông báo cho người dùng
                    DialogResult result = MessageBox.Show("Tệp này đã tồn tại. Bạn có muốn ghi đè không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    // Nếu người dùng chọn "No", thoát hàm
                    if (result == DialogResult.No)
                    {
                        return; // Không làm gì, chỉ thoát
                    }
                }
                try
                {
                    Bitmap btm = bitmap.Clone(new Rectangle(0, 0, Canvas.Width, Canvas.Height), bitmap.PixelFormat);
                    btm.Save(filePath, ImageFormat.Png);
                    btm.Dispose(); // Giải phóng bitmap tạm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message);
                }
            }
        }
        private void SaveBitmapState()
        {
            // Lưu bitmap hiện tại vào stack trước khi thay đổi
            bitmapHistory.Push((Bitmap)bitmap.Clone());
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (bitmapHistory.Count > 0)
            {
                // Lấy trạng thái bitmap trước đó từ stack
                bitmap.Dispose(); // Giải phóng bitmap hiện tại
                bitmap = bitmapHistory.Pop(); // Khôi phục bitmap từ stack

                // Cập nhật lại đối tượng Graphics và hiển thị trên Canvas
                graphics = Graphics.FromImage(bitmap);
                Canvas.Image = bitmap;
                Canvas.Invalidate();

                // Xóa nét vẽ vừa bị hoàn tác khỏi drawnLines
                drawnLines.Clear(); // Xóa hết các nét vẽ 
            }
            else
            {
                MessageBox.Show("Không có hành động nào để hoàn tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Canva_Load(object sender, EventArgs e)
        {

        }
    }
}