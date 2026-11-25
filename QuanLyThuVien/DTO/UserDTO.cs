namespace QuanLyThuVien.DTO;

// DTO chứa thông tin tài khoản người dùng trong bảng Users.
public class UserDTO
{
    // Mã tài khoản.
    public int ID { get; set; }
    // Tên đăng nhập.
    public string Username { get; set; }
    // Mật khẩu tài khoản.
    public string Password { get; set; }
    // Vai trò người dùng, ví dụ thủ thư hoặc độc giả.
    public string Role { get; set; }
    // Họ tên người dùng.
    public string HoTen { get; set; }
    // Số điện thoại liên hệ.
    public string SoDienThoai { get; set; }
    // Cờ tài khoản đang hoạt động: 1 là hoạt động, 0 là khóa.
    public int IsActive { get; set; } 
}
