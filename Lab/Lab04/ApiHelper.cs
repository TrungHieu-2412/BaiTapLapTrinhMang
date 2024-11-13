using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Lab04;
using Newtonsoft.Json;
using System.Net;
using System.Windows.Forms;

public class ApiHelper
{
    private const string BaseUrl = "https://nt106.uitiot.vn/docs";
    private readonly HttpClient _httpClient;

    public ApiHelper()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // Đặt header xác thực
    public void SetAuthorizationHeader(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    // Phương thức đăng nhập
    public async Task<Token> Login(string email, string password)
    {
        var data = new { email, password };
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("user/login", content);

            if (response.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
                return token;
            }
            else
            {
                // Xử lý lỗi dựa trên mã trạng thái lỗi
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Xử lý các trường hợp lỗi khác
                }
                return null;
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi khi đọc dữ liệu JSON
            MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // Phương thức đăng ký
    public async Task<UserCreate> Signup(string username, string email, string password,  string firstName, string lastName, string sex, DateTime birthday, string language, string phone)
    {
        
        var data = new UserCreate
        {
            Username = username,
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            Sex = sex,
            Birthday = birthday,
            Language = language,
            Phone = phone,
            EmailVerified = false, 
            Avatar = null,
            IsActive = true, 
            IsSuperuser = false,
        };

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("user/signup", content);
        if (response.IsSuccessStatusCode)
        {
            var userCreate = JsonConvert.DeserializeObject<UserCreate>(await response.Content.ReadAsStringAsync());
            return userCreate;
        }
        else
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // Đọc lỗi từ API (nếu có)
                string errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Xử lý các trường hợp lỗi khác
                MessageBox.Show("Đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
    }

    // Phương thức lấy danh sách món ăn
    public async Task<List<MonAn>> GetDishes(int page, int pageSize)
    {
        var response = await _httpClient.GetAsync($"monan/all?page={page}&pageSize={pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var dishes = JsonConvert.DeserializeObject<List<MonAn>>(await response.Content.ReadAsStringAsync());
            // Giả sử API trả về dữ liệu JSON có thêm trường "NguoiDongGop"
            foreach (var dish in dishes)
            {
                dish.NguoiDongGop = dish.NguoiDongGop; // Cập nhật thuộc tính NguoiDongGop
            }
            return dishes;
        }
        else
        {
            // Xử lý lỗi
            return null;
        }
    }

    // Phương thức lấy danh sách món ăn của người dùng
    public async Task<List<MonAn>> GetMyDishes(int page, int pageSize)
    {
        var response = await _httpClient.GetAsync($"monan/my-dishes?page={page}&pageSize={pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var dishes = JsonConvert.DeserializeObject<List<MonAn>>(await response.Content.ReadAsStringAsync());
            return dishes;
        }
        else
        {
            // Xử lý lỗi
            return null;
        }
    }

    // Phương thức thêm món ăn
    public async Task<MonAn> AddDish(string name, string gia, string diaChi, string hinhAnh, string mota)
    {
        var data = new { TenMonAn = name, Gia = gia, DiaChi = diaChi, HinhAnh = hinhAnh, MoTa = mota};
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("monan/add", content);
        if (response.IsSuccessStatusCode)
        {
            var monAn = JsonConvert.DeserializeObject<MonAn>(await response.Content.ReadAsStringAsync());
            return monAn;
        }
        else
        {
            // Xử lý lỗi
            return null;
        }
    }

    
}