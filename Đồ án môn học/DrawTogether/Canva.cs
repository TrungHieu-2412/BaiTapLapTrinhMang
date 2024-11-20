using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using DrawTogether.Client.Networking;
using DrawTogether.Client.Model;
using DrawTogether.Client.Room;
using Server;

namespace DrawTogether
{
    public partial class Canva : Form
    {
        // Khởi tạo các giá trị cho bảng vẽ
        private Bitmap bitmap;
        private Graphics graphics;
        private Boolean cursorMoving = false;
        private Pen cursorPen;
        private int cursorX = -1;
        private int cursorY = -1;
        private Point p = new Point();
        private Color stateColor;
        private int shapeTag = 10;

        private List<Point> points_1 = new List<Point>();
        private List<Point> points_2 = new List<Point>();
        private bool isEraserMode = false;
        private Color currentColor = Color.Black;
        private int brushSize = 5;
        private Bitmap backupBitmap;
        private Stack<Bitmap> bitmapHistory = new Stack<Bitmap>();

        // Mạng
        private ClientNetworkManager networkManager;
        private ClientPacket Client_Information;
        private IPEndPoint serverEndPoint;

        private bool isOffline;
        private bool NewClient;

        // Quản lý phòng và người dùng
        private ClientRoomManager roomManager;

        // Ngữ cảnh đồng bộ hóa
        private SynchronizationContext uiContext = SynchronizationContext.Current ?? new SynchronizationContext();

        public Canva(bool IsOffline, int code, string Username, string Roomcode, string ServerIP)
        {
            InitializeComponent();

            if (Canvas == null)
            {
                throw new Exception("Canvas chưa được khởi tạo.");
            }

            // Tạo bảng vẽ và bút
            bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Canvas.Image = bitmap;
            stateColor = Color.Black;
            cursorPen = new Pen(stateColor, 2);
            PenOptimizer(cursorPen);
            this.ActiveControl = null;
            this.Resize += new EventHandler(Form_Canva_Resize);
            this.Load += new System.EventHandler(this.Form_Canva_Load);

            Client_Information = new ClientPacket()
            {
                Code = code,
                Username = Username,
                RoomID = Roomcode,
            };

            // Khởi tạo cờ trạng thái
            this.isOffline = IsOffline;
            if (!isOffline)
            {
                txtRoomCodeCanva.Visible = true;
                serverEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), 9999);
                networkManager = new ClientNetworkManager(serverEndPoint);

                roomManager = new ClientRoomManager(lisUserName, txtRoomCodeCanva, networkManager);
            }
            else
            {
                // Khởi tạo ClientRoomManager mà không truyền NetworkManager vào
                roomManager = new ClientRoomManager(lisUserName, txtRoomCodeCanva);
            }
        }

        private void Form_Canva_Load(object sender, EventArgs e)
        {
            uiContext = SynchronizationContext.Current;

            if (!isOffline)
            {
                // Kết nối với server
                if (networkManager.Connect())
                {
                    // Gửi thông tin client đến server
                    networkManager.Send(Client_Information);

                    // Bắt đầu luồng lắng nghe 
                    Thread listenThread = new Thread(Receive);
                    listenThread.IsBackground = true;
                    listenThread.Start();
                }
                else
                {
                    roomManager.ShowError("Can not connect to the server!");
                    this.Close();
                    return;
                }
            }

            Canvas.Dock = DockStyle.Fill;
            InitializeDrawingBitmap();
            cbBrushSize.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 17, 20, 30 });
            cbBrushSize.SelectedItem = 5;
            cbBrushSize.SelectedIndexChanged += new EventHandler(cbBrushSize_SelectedIndexChanged);
        }

        private void Receive()
        {
            try
            {
                while (true)
                {
                    // Nhận Packet từ server
                    ClientPacket response = networkManager.Receive();
                    if (response != null)
                    {
                        switch (response.Code)
                        {
                            case 0: // Trạng thái tạo phòng
                                HandleGenerateRoomStatus(response);
                                break;
                            case 1: // Trạng thái tham gia phòng
                                HandleJoinRoomStatus(response);
                                break;
                            case 2: // Đồng bộ Bitmap
                                HandleSyncBitmapStatus(response);
                                break;
                            case 3: // Vẽ Bitmap
                                HandleDrawBitmapStatus(response);
                                break;
                            case 4: // Nhận dữ liệu vẽ
                                HandleReceiveDrawingData(response, GetRoomManager());
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi kết nối
                MessageBox.Show("Lỗi kết nối đến server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Đóng kết nối
                networkManager.Close();
                // Đóng form
                this.Close();
            }
        }

        private void HandleGenerateRoomStatus(ClientPacket response)
        {
            Client_Information.RoomID = response.RoomID;
            roomManager.UpdateRoomID(Client_Information.RoomID);
            NewClient = false;
        }

        private void HandleJoinRoomStatus(ClientPacket response)
        {
            if (NewClient)
            {
                networkManager.Send(new ClientPacket
                {
                    Code = 2,
                    RoomID = response.RoomID,
                });
                NewClient = false;
            }

            if (response.Username == "err:thisroomdoesnotexist")
            {
                roomManager.ShowError("The room you requested does not exist");
                networkManager.Close();
                this.Close();
                return;
            }

            if (response.Username.Contains('!'))
            {
                roomManager.RemoveFromUserListView(response.Username.Substring(1));
            }
            else
            {
                List<string> list = response.Username.Split(',').ToList();
                foreach (string username in list)
                {
                    if (username == Client_Information.Username)
                    {
                        list.Remove(username);
                        break;
                    }
                }
                roomManager.ClearUserListView();
                foreach (string username in list)
                {
                    roomManager.AddToUserListView(username);
                }
            }
        }

        private void HandleSyncBitmapStatus(ClientPacket response)
        {
            // Cập nhật Bitmap từ server
            Bitmap _bitmap = roomManager.StringToBitmap(response.BitmapString);
            if (bitmap != null)
            {
                bitmap.Dispose(); // Giải phóng Bitmap cũ
            }
            bitmap = _bitmap;
            graphics = Graphics.FromImage(bitmap);
            Canvas.Image = bitmap;
            uiContext.Post(s =>
            {
                Canvas.Refresh();
                // Vẽ lại các nét vẽ đã lưu
                foreach (var line in drawnLines)
                {
                    graphics.DrawLine(line.Item3, line.Item1, line.Item2);
                }
                Canvas.Refresh();
            }, null);
        }


        private void HandleDrawBitmapStatus(ClientPacket response)
        {
            // Cập nhật Bitmap từ server
            Bitmap _bitmap = roomManager.StringToBitmap(response.BitmapString);
            if (bitmap != null)
            {
                bitmap.Dispose(); // Giải phóng Bitmap cũ
            }
            bitmap = _bitmap;
            graphics = Graphics.FromImage(bitmap);
            Canvas.Image = bitmap;
            uiContext.Post(s =>
            {
                Canvas.Refresh();
            }, null);
        }

        private ClientRoomManager GetRoomManager()
        {
            return roomManager;
        }


        private void HandleReceiveDrawingData(ClientPacket packet, ClientRoomManager roomManager)
        {
            // Lấy phòng hiện tại
            Room currentRoom = roomManager.GetRoom(packet.RoomID);
            if (currentRoom != null)
            {
                // Cập nhật Bitmap chung của phòng
                using (Graphics g = Graphics.FromImage(currentRoom.Bitmap))
                {
                    Pen p = new Pen(Color.FromName(packet.PenColor), packet.PenWidth);
                    if (packet.ShapeTag == 10) // Vẽ đường thẳng
                    {
                        for (int i = 0; i < packet.Points_1.Count; i++)
                        {
                            g.DrawLine(p, packet.Points_1[i], packet.Points_2[i]);
                        }
                    }
                    else
                    {
                        int cursorX = (int)packet.Position[0];
                        int cursorY = (int)packet.Position[1];
                        float w = packet.Position[2];
                        float h = packet.Position[3];

                        if (packet.ShapeTag == 11)
                        {
                            g.DrawLine(p, cursorX, cursorY, cursorX + w, cursorY + h);
                        }
                        else if (packet.ShapeTag == 12)
                        {
                            g.DrawRectangle(p, cursorX, cursorY, w, h);
                        }
                        else if (packet.ShapeTag == 13)
                        {
                            g.DrawEllipse(p, cursorX, cursorY, w, h);
                        }
                    }
                }
                ClientPacket syncPacket = new ClientPacket
                {
                    Code = 2,
                    RoomID = packet.RoomID,
                    BitmapString = currentRoom.BitmapToString(currentRoom.Bitmap)
                };
                networkManager.Send(syncPacket);
            }
        }
        private List<Tuple<Point, Point, Pen>> drawnLines = new List<Tuple<Point, Point, Pen>>();
        private string responseInJson;

        private void Form_Canva_Resize(object sender, EventArgs e)
        {
            if (Canvas.Width > 0 && Canvas.Height > 0 && (Canvas.Width != bitmap.Width || Canvas.Height != bitmap.Height))
            {
                backupBitmap = (Bitmap)bitmap.Clone();
                Bitmap newBitmap = new Bitmap(Canvas.Width, Canvas.Height);
                using (Graphics newGraphics = Graphics.FromImage(newBitmap))
                {
                    newGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    newGraphics.Clear(Color.White);
                    foreach (var line in drawnLines)
                    {
                        newGraphics.DrawLine(line.Item3, line.Item1, line.Item2);
                    }
                }
                bitmap = newBitmap;
                graphics = Graphics.FromImage(bitmap);
                Canvas.Image = bitmap;
                Canvas.Invalidate();
                bitmapHistory.Clear();
                bitmapHistory.Push((Bitmap)bitmap.Clone());
            }
        }

        private void PenOptimizer(Pen pen)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        // Khởi tạo vùng vẽ ban đầu
        private void InitializeDrawingBitmap()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            Canvas.Image = bitmap;
        }

        // Sự kiện khi nhấn chuột xuống Canvas
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            SaveBitmapState();
            cursorMoving = true;
            cursorX = e.X;
            cursorY = e.Y;

            // Lưu tọa độ điểm bắt đầu cho các hình dạng
            if (shapeTag == 11 || shapeTag == 12 || shapeTag == 13)
            {
                cursorX = e.X;
                cursorY = e.Y;
            }

            if (isEraserMode)
            {
                cursorPen = new Pen(Color.White, brushSize);
            }
            else
            {
                cursorPen = new Pen(currentColor, brushSize);
            }

            PenOptimizer(cursorPen);
        }

        // Sự kiện khi di chuyển chuột trên Canvas
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursorMoving && shapeTag == 10)
            {
                if (cursorX >= 0 && cursorY >= 0)
                {
                    p = e.Location;

                    // Lưu tọa độ của đường vẽ vào danh sách các điểm
                    points_1.Add(new Point(cursorX, cursorY));
                    points_2.Add(p);

                    // Vẽ đường từ điểm đầu tới điểm hiện tại
                    graphics.DrawLine(cursorPen, new Point(cursorX, cursorY), p);

                    // Lưu lại đường vẽ vào danh sách các đường đã vẽ
                    drawnLines.Add(new Tuple<Point, Point, Pen>(new Point(cursorX, cursorY), p, (Pen)cursorPen.Clone()));

                    // Cập nhật lại tọa độ chuột
                    cursorX = e.X;
                    cursorY = e.Y;

                    // Cập nhật lại hình ảnh hiển thị trên Canvas
                    Canvas.Image = bitmap;

                    // Gửi gói dữ liệu vẽ đến server
                    ClientPacket message = new ClientPacket
                    {
                        Code = 4,
                        Username = Client_Information.Username,
                        RoomID = Client_Information.RoomID,
                        PenColor = cursorPen.Color.Name,
                        PenWidth = cursorPen.Width,
                        ShapeTag = shapeTag,
                        Points_1 = points_1,
                        Points_2 = points_2,
                    };
                    sendToServer(message);
                }
            }
        }

        // Sự kiện khi thả chuột ra khỏi Canvas
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            cursorMoving = false;

            if (shapeTag == 11 || shapeTag == 12 || shapeTag == 13)
            {
                float w = e.X - cursorX;
                float h = e.Y - cursorY;

                ClientPacket message = new ClientPacket
                {
                    Code = 4,
                    Username = Client_Information.Username,
                    RoomID = Client_Information.RoomID,
                    PenColor = cursorPen.Color.Name,
                    PenWidth = cursorPen.Width,
                    ShapeTag = shapeTag,
                    Position = new float[] { cursorX, cursorY, w, h }
                };

                if (!isOffline)
                {
                    sendToServer(message);
                }
                uiContext.Send(s =>
                {
                    graphics.DrawLine(cursorPen, cursorX, cursorY, e.X, e.Y);
                    Canvas.Refresh();
                }, null);
            }
            else if (shapeTag == 12 || shapeTag == 13) // Hình chữ nhật và hình elip
            {
                float w = e.X - cursorX;
                float h = e.Y - cursorY;

                ClientPacket message = new ClientPacket
                {
                    Code = 4,
                    Username = Client_Information.Username,
                    RoomID = Client_Information.RoomID,
                    PenColor = cursorPen.Color.Name,
                    PenWidth = cursorPen.Width,
                    ShapeTag = shapeTag,
                    Position = new float[] { cursorX, cursorY, w, h }
                };

                if (!isOffline)
                {
                    sendToServer(message);
                }
                uiContext.Send(s =>
                {
                    if (shapeTag == 12)
                    {
                        graphics.DrawRectangle(cursorPen, cursorX, cursorY, w, h);
                    }
                    else if (shapeTag == 13)
                    {
                        graphics.DrawEllipse(cursorPen, cursorX, cursorY, w, h);
                    }
                    Canvas.Refresh();
                }, null);
            }
        }

        private void sendToServer(ClientPacket message)
        {
            if (networkManager != null)
            {
                try
                {
                    // Gọi hàm Send() của NetworkManager với đối tượng Packet
                    networkManager.Send(message);
                }
                catch (Exception ex)
                {
                    roomManager.ShowError("Failed to send data to server! " + ex.Message);
                }
            }
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
                picColor.BackColor = currentColor; // Hiển thị màu đã chọn trong PictureBox
            }
        }

        // Sự kiện khi thay đổi kích thước bút
        private void cbBrushSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cbBrushSize.SelectedItem.ToString(), out int size))
            {
                brushSize = size;
                cursorPen.Width = brushSize;
            }
        }

        // Sự kiện khi nhấn nút Eraser (Tẩy)
        private void btnEraser_Click(object sender, EventArgs e)
        {
            isEraserMode = true;
            cursorPen = new Pen(Color.White, brushSize);
            PenOptimizer(cursorPen);
            btnEraser.Enabled = false;
            btnDrawing.Enabled = true;
            btnLine.Enabled = true;
            btnEllipse.Enabled = true;
            btnRectangle.Enabled = true;
        }

        // Sự kiện khi nhấn nút Drawing (Vẽ)
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            shapeTag = 10;
            isEraserMode = false;
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = false;
            btnEraser.Enabled = true;
            btnLine.Enabled = true;
            btnEllipse.Enabled = true;
            btnRectangle.Enabled = true;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            shapeTag = 11;
            isEraserMode = false;
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = true;
            btnEraser.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = true;
            btnLine.Enabled = false;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            shapeTag = 12;
            isEraserMode = false;
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = true;
            btnEraser.Enabled = true;
            btnLine.Enabled = true;
            btnEllipse.Enabled = true;
            btnRectangle.Enabled = false;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            shapeTag = 13;
            isEraserMode = false;
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = true;
            btnEraser.Enabled = true;
            btnLine.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = false;
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
            bitmapHistory.Push((Bitmap)bitmap.Clone());
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (bitmapHistory.Count > 0)
            {
                bitmap.Dispose();
                bitmap = bitmapHistory.Pop();
                graphics = Graphics.FromImage(bitmap);
                Canvas.Image = bitmap;
                Canvas.Invalidate();
                drawnLines.Clear();
            }
            else
            {
                MessageBox.Show("Không có hành động nào để hoàn tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Canva_Load(object sender, EventArgs e)
        {

        }

        private void Canvas_Click(object sender, EventArgs e)
        {

        }
    }
}