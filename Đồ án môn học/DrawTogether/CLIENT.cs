using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;


namespace DrawTogether
{
    public partial class CLIENT : Form
    {
        // Khởi tạo các biến liên quan đến vẽ
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
        private Stack<Bitmap> bitmapHistory = new Stack<Bitmap>(); // Lịch sử cho chức năng hoàn tác

        private Bitmap backupBitmap;
        private List<Tuple<Point, Point, Pen>> drawnLines = new List<Tuple<Point, Point, Pen>>();


        // Khởi tạo các biến liên quan đến Mạng

        //private ClientNetworkManager networkManager; // . . . .


        private TcpClient client; //
        private StreamReader reader; //Tạo đối tượng để đọc thông tin trao đổi qua luồng
        private StreamWriter writer; //Đối tượng này để ghi thông tin gửi dữ liệu qua luồng
        private Packet Client_Information; //Thằng này để gửi thông tin lúc kết nối
        private IPEndPoint serverEndPoint; //Tạo một điểm kết nối để giao tiếp với server
        private bool isOffline; //Check Offline
        private bool NewClient;
        private RoomManager roomManager; // Quản lý phòng và danh sách người chơi trong phòng


        private List<Point> currentStroke = new List<Point>(); // Lưu trữ nét vẽ hiện tại


        private CancellationTokenSource cancellationTokenSource; // . . . . . .


        // Các biến khác
        private SynchronizationContext uiContext = SynchronizationContext.Current ?? new SynchronizationContext(); // Ngữ cảnh đồng bộ hóa. Tránh xung đột khi cập nhật từ các luồng khác nhau

        //-------------------------------------NETWORK---------------------------------------------        

        // Constructor khởi tạo Client
        public CLIENT(bool IsOffline, int code, string Username, string Roomcode, string ServerIP)
        {
            InitializeComponent();

            if (Canvas == null)
            {
                throw new Exception("Canvas chưa được khởi tạo.");
            }
            // Khởi tạo Bitmap và Graphics
            bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Canvas.Image = bitmap;

            //Thiết lập bút vẽ và sự kiện
            stateColor = Color.Black;
            cursorPen = new Pen(stateColor, 2);
            PenOptimizer(cursorPen);
            this.ActiveControl = null;
            this.Resize += new EventHandler(Form_Canva_Resize);
            this.Load += new System.EventHandler(this.Form_Canva_Load);
            
            //Mặc định (offline) các hiển thị không cần thiết thì bị tắt
            labRoom.Visible = false;
            txtRoomCodeCanva.Visible = false;
            lisUserName.Visible = false;
            labPlayerName.Visible = false;
            txtInputMess.Visible = false;
            txtOutputMess.Visible = false;
            btnSendMess.Visible = false;


            // Khởi tạo thông tin Client
            Client_Information = new Packet()
            {
                Code = code,
                Username = Username,
                RoomID = Roomcode,
            };


            // Khởi tạo cờ trạng thái
            NewClient = true;

            this.isOffline = IsOffline;
            if (!isOffline)
            {
                labRoom.Visible = true;
                txtRoomCodeCanva.Visible = true;
                lisUserName.Visible = true;
                labPlayerName.Visible = true;
                txtInputMess.Visible = true;
                txtOutputMess.Visible = true;
                btnSendMess.Visible = true;

                // Khởi tạo một điểm kết nối với Server thông qua địa chị IP của Server đã nhập trong Textbox bên HOME
                serverEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), 9999);
            }
            roomManager = new RoomManager(lisUserName, txtRoomCodeCanva);
        }





        // Load form & bắt đầu kết nối với Server
        private void Form_Canva_Load(object sender, EventArgs e)
        {
            btnSendMess.Click += new EventHandler(btnSendMess_Click);
            //uiContext = SynchronizationContext.Current;

            if (!isOffline)
            {
                if (Connect()) // Kết nối tới Server
                {
                    // Gửi thông tin client đến server. Bao gồm CODE, ROOM ID và Tên người chơi
                    Send(Client_Information);

                    // Cần cập nhật ID phòng lên giao diện người dùng
                    roomManager.UpdateRoomID(Client_Information.RoomID);

                    // Cần thêm Client đó vào List Player
                    roomManager.AddToUserListView(Client_Information.Username + " (you)");

                    // Bắt đầu lắng nghe
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
            // Một số cài đặt mặc định cho app
            //Canvas.Dock = DockStyle.Fill;
            //InitializeDrawingBitmap();
            cbBrushSize.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 17, 20, 30 });
            cbBrushSize.SelectedItem = 5;
            cbBrushSize.SelectedIndexChanged += new EventHandler(cbBrushSize_SelectedIndexChanged);
        }

        

        // Connect tới Server
        public bool Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverEndPoint);
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
                return false;
            }
        }

        // Hàm SEND chịu trách nhiệm gửi gói tin đến Server
        private void Send(Packet packet)
        {
            // Chuyển đổi tượng packet thành chuỗi
            string messageInJson = JsonConvert.SerializeObject(packet);
            try
            {
                // Thực hiện ghi vào luồng
                writer.WriteLine(messageInJson);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi dữ liệu: " + ex.Message);
            }
        }




        private void btnSendMess_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInputMess.Text))
            {
                // Tạo gói tin nhắn
                Packet message = new Packet
                {
                    Code = 5, // Mã tin nhắn
                    Username = Client_Information.Username,
                    RoomID = Client_Information.RoomID,
                    BitmapString = txtInputMess.Text // Dùng BitmapString làm nơi chứa tin nhắn
                };

                // Gửi tin nhắn tới Server
                Send(message);

                // Hiển thị tin nhắn trong `txtOutputMess`
                txtOutputMess.AppendText($"{Client_Information.Username}: {txtInputMess.Text}{Environment.NewLine}");
                txtInputMess.Clear();
            }
        }


        private void Receive()
        {
            try
            {
                string responseInJson = string.Empty;
                while (true)
                {
                    responseInJson = reader.ReadLine();

                    // Nhận Packet từ server
                    Packet response = JsonConvert.DeserializeObject<Packet>(responseInJson);
                    if (response != null)
                    {
                        if (response.Code == 0)
                        {
                            HandleGenerateRoomStatus(response);
                        }
                        else if (response.Code == 1)
                        {
                            HandleJoinRoomStatus(response);
                        }
                        else if (response.Code == 2)
                        {
                            HandleSyncBitmapStatus(response);
                        }
                        else if (response.Code == 3)
                        {
                            HandleSendBitmapStatus(response);
                        }
                        else if (response.Code == 4)
                        {
                            HandleSendGraphics(response);
                        }
                        else if (response.Code == 5)
                        {
                            // Hiển thị tin nhắn trong Textbox
                            uiContext.Send(_ =>
                            {
                                txtOutputMess.AppendText($"{response.Username}: {response.BitmapString}{Environment.NewLine}");
                            }, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi kết nối
                MessageBox.Show("Lỗi kết nối đến server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

//----------------------GIAO TIẾP CHÍNH----------------------------
        private void HandleGenerateRoomStatus(Packet response)
        {
            Client_Information.RoomID = response.RoomID;
            roomManager.UpdateRoomID(Client_Information.RoomID);
            NewClient = false;
        }

        void HandleJoinRoomStatus(Packet response)
        {
            if (NewClient)
            {
                Send(new Packet
                {
                    Code = 2,
                    RoomID = response.RoomID,
                });
                NewClient = false;
            }

            if (response.Username == "err:thisroomdoesnotexist")
            {
                roomManager.ShowError("The room you requested does not exist");
                client.Close();
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

        private void HandleSyncBitmapStatus(Packet respone)
        {
            Packet message = new Packet
            {
                Code = 3,
                RoomID = respone.RoomID,
                BitmapString = roomManager.BitmapToString(bitmap),
            };
            Send(message);
        }

        private void HandleSendBitmapStatus(Packet response)
        {
            Bitmap _bitmap = roomManager.StringToBitmap(response.BitmapString);
            bitmap = _bitmap;
            graphics = Graphics.FromImage(bitmap);
            Canvas.Image = bitmap;
            uiContext.Send(s =>
            {
                Canvas.Refresh();
            }, null);
        }

        void HandleSendGraphics(Packet response)
        {
            //Pen p = new Pen(Color.FromName(response.PenColor), response.PenWidth);
            //PenOptimizer(p);

            Color receivedColor = Color.FromName(response.PenColor);
            Pen p = new Pen(receivedColor, response.PenWidth);
            PenOptimizer(p);

            int cursorX = 0, cursorY = 0;
            float w = 0, h = 0;

            if (response.ShapeTag == 10)
            {
                int length = response.Points_1.ToArray().Length;

                for (int i = 0; i < length; i++)
                {
                    uiContext.Send(s =>
                    {
                        graphics.DrawLine(p, response.Points_1[i], response.Points_2[i]);
                    }, null);
                }
            }
            else
            {
                cursorX = (int)response.Position[0];
                cursorY = (int)response.Position[1];
                w = response.Position[2];
                h = response.Position[3];
            }
            if (response.ShapeTag == 11)
            {
                uiContext.Send(s =>
                {
                    graphics.DrawLine(p, cursorX, cursorY, w + cursorX, h + cursorY);
                }, null);
            }
            if (response.ShapeTag == 12)
            {
                uiContext.Send(s =>
                {
                    graphics.DrawRectangle(p, cursorX, cursorY, w, h);
                }, null);
            }
            if (response.ShapeTag == 13)
            {
                uiContext.Send(s =>
                {
                    graphics.DrawEllipse(p, cursorX, cursorY, w, h);
                }, null);
            }
            uiContext.Send(s =>
            {
                Canvas.Refresh();
            }, null);
        }

















//------------------------THAO TÁC CHUỘT TRÊN BITMAP-------------------------------
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            cursorMoving = true;
            cursorX = e.X;
            cursorY = e.Y;
        }
        

        //private void Canvas_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (cursorX != -1 && cursorY != -1 && cursorMoving == true && (shapeTag == 10))
        //    {
        //        p = e.Location;

        //        points_1.Add(new Point(cursorX, cursorY));
        //        points_2.Add(p);

        //        graphics.DrawLine(cursorPen, new Point(cursorX, cursorY), p);

        //        cursorX = e.X;
        //        cursorY = e.Y;
        //    }
        //    uiContext.Send(s =>
        //    {
        //        Canvas.Refresh();
        //    }, null);
        //}
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursorX != -1 && cursorY != -1 && cursorMoving == true && (shapeTag == 10))
            {
                p = e.Location;
                points_1.Add(new Point(cursorX, cursorY));
                points_2.Add(p);
                cursorPen = new Pen(currentColor, brushSize); // Update pen with currentColor
                                                              
                graphics.DrawLine(cursorPen, new Point(cursorX, cursorY), p);
                cursorX = e.X;
                cursorY = e.Y;
            }
            uiContext.Send(s => { Canvas.Refresh(); }, null);
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            float w = e.Location.X - cursorX;
            float h = e.Location.Y - cursorY;

            cursorPen = new Pen(currentColor, brushSize); // Update pen with currentColor

            if (shapeTag == 11)
            {
                graphics.DrawLine(cursorPen, cursorX, cursorY, w + cursorX, h + cursorY);
            }
            if (shapeTag == 12)
            {
                graphics.DrawRectangle(cursorPen, cursorX, cursorY, w, h);
            }
            if (shapeTag == 13)
            {
                graphics.DrawEllipse(cursorPen, cursorX, cursorY, w, h);
            }
            uiContext.Send(s =>
            {
                Canvas.Refresh();
            }, null);

            float[] pos = new float[] { cursorX, cursorY, w, h };

            Packet messsage = new Packet
            {
                Code = 4,
                Username = Client_Information.Username,
                RoomID = Client_Information.RoomID,
                PenColor = cursorPen.Color.Name,
                PenWidth = cursorPen.Width,
                ShapeTag = shapeTag,
                Points_1 = points_1,
                Points_2 = points_2,
                Position = pos
            };

            if (!isOffline)
            {
                Send(messsage);
            }

            cursorMoving = false;
            cursorX = -1;
            cursorY = -1;
            points_1.Clear();
            points_2.Clear();
        }

        //-------------------------------------MAIN EVEN---------------------------------------------        

        // Khởi tạo vùng vẽ ban đầu
        //private void InitializeDrawingBitmap()
        //{
        //    if (bitmap != null)
        //    {
        //        bitmap.Dispose();
        //    }
        //    bitmap = new Bitmap(Canvas.Width, Canvas.Height);
        //    graphics = Graphics.FromImage(bitmap);
        //    graphics.Clear(Color.White);
        //    Canvas.Image = bitmap;
        //}

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

//------------------------SỰ KIỆN CHÍNH------------------------------
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
            currentColor = Color.White; // Set currentColor to white for eraser
            cursorPen = new Pen(Color.White, brushSize);
            PenOptimizer(cursorPen);
            btnEraser.Enabled = false;
            btnDrawing.Enabled = true;
            btnLine1.Enabled = true;
            btnEllipse.Enabled = true;
            btnRectangle.Enabled = true;
        }

        // Sự kiện khi nhấn nút Drawing (Vẽ)
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            shapeTag = 10;
            isEraserMode = false;
            currentColor = picColor.BackColor; // Set currentColor from the color picker
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = false;
            btnEraser.Enabled = true;
            btnLine1.Enabled = true;
            btnEllipse.Enabled = true;
            btnRectangle.Enabled = true;
        }

        // Vẽ đường thẳng
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
            btnLine1.Enabled = false;
        }

        //Vẽ hình chữ nhật
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            shapeTag = 12;
            isEraserMode = false;
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = true;
            btnEraser.Enabled = true;
            btnLine1.Enabled = true;
            btnEllipse.Enabled = true;
            btnRectangle.Enabled = false;
        }

        // Vẽ hình Elif -> Hình tròn đó
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            shapeTag = 13;
            isEraserMode = false;
            cursorPen = new Pen(currentColor, brushSize);
            PenOptimizer(cursorPen);
            btnDrawing.Enabled = true;
            btnEraser.Enabled = true;
            btnLine1.Enabled = true;
            btnRectangle.Enabled = true;
            btnEllipse.Enabled = false;
        }

        // Hoàn tác về thao tác trước đó
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

        // Mở file đã lưu trước đó
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

        // Lưu bản vẽ hiện tại
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


//-------------------------------------FUNCTION---------------------------------------------        

        // Lưu bản sao của Bitmap vào Stack, phục vụ cho chức năng hoàn tác
        private void SaveBitmapState()
        {
            bitmapHistory.Push((Bitmap)bitmap.Clone());
        }


        // Thiết lập nét vẽ
        private void PenOptimizer(Pen pen)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }



        private void Canva_Load(object sender, EventArgs e)
        {

        }
    }

//-----------------------------ĐỊNH NGHĨA CÁC LỚP CẦN THIẾT---------------------------------------
    
    public class Packet
    {
        public int Code { get; set; }
        public string Username { get; set; }
        public string RoomID { get; set; }
        public string PenColor { get; set; }
        public float PenWidth { get; set; }
        public int ShapeTag { get; set; }
        public List<Point> Points_1 { get; set; }
        public List<Point> Points_2 { get; set; }
        public float[] Position { get; set; }
        public string BitmapString { get; set; }
    }


    public class RoomManager
    {
        private ListView lisUserName;
        private TextBox txtRoomCodeCanva;
        private string currentRoomID;


        public RoomManager(ListView lisUserName, TextBox txtRoomCodeCanva)
        {
            this.lisUserName = lisUserName;
            this.txtRoomCodeCanva = txtRoomCodeCanva;
        }


        //Cập nhật RoomID lên Textbox Client
        public void UpdateRoomID(string roomID)
        {
            if (txtRoomCodeCanva.InvokeRequired)
            {
                txtRoomCodeCanva.Invoke(new Action(() =>
                {
                    txtRoomCodeCanva.Text = "" + roomID;
                }));
            }
            else
            {
                txtRoomCodeCanva.Text = "" + roomID;
            }
        }


        //Cập nhật người chơi vào danh sách người chơi
        //public void AddToUserListView(string line)
        //{
        //    if (lisUserName.InvokeRequired)
        //    {
        //        lisUserName.Invoke(new Action(() =>
        //        {
        //            lisUserName.Items.Add(line);
        //        }));
        //    }
        //    else
        //    {
        //        lisUserName.Items.Add(line);
        //    }
        //}

        public void AddToUserListView(string line)
        {
            if (lisUserName.InvokeRequired)
            {
                lisUserName.Invoke(new Action(() =>
                {
                    lisUserName.Items.Add(new ListViewItem(line));
                }));
            }
            else
            {
                lisUserName.Items.Add(new ListViewItem(line));
            }
        }

        public void RemoveFromUserListView(string line)
        {
            Action action = () =>
            {
                foreach (ListViewItem item in lisUserName.Items)
                {
                    if (item.Text == line)
                    {
                        lisUserName.Items.Remove(item);
                        break;
                    }
                }
            };
            if (lisUserName.InvokeRequired)
            {
                lisUserName.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public void ClearUserListView()
        {
            Action action = () =>
            {
                ListViewItem firstLine = lisUserName.Items[0];
                lisUserName.Clear();
                lisUserName.Items.Add(firstLine);
            };
            if (lisUserName.InvokeRequired)
            {
                lisUserName.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public string BitmapToString(Bitmap bitmap)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = stream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }

        public Bitmap StringToBitmap(string base64string)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length);
            stream.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(stream, true);
            Bitmap bitmap = new Bitmap(image);

            return bitmap;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}