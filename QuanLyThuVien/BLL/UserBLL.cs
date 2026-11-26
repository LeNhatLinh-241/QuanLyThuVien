using System.Data;
using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BLL;

public class UserBLL
{
    private UserDAO userDao = new UserDAO();

    public List<UserDTO> LayDanhSachDocGia()
    {
        return userDao.GetAllUsers();
    }

    public string ThemDocGia(string hoTen, string soDienThoai)
    {
        if (string.IsNullOrWhiteSpace(hoTen))
            return "Họ tên không được để trống!";
        UserDTO newUser = new UserDTO
        {
            HoTen = hoTen.Trim(),
            SoDienThoai = soDienThoai?.Trim()
        };
        if (userDao.InsertUser(newUser))
            return "Thêm độc giả thành công!";
        return "Lỗi khi thêm độc giả.";
    }

    public string SuaDocGia(int id, string hoTen, string soDienThoai)
    {
        if (string.IsNullOrWhiteSpace(hoTen))
            return "Họ tên không được để trống!";
        UserDTO editUser = new UserDTO
        {
            ID = id,
            HoTen = hoTen.Trim(),
            SoDienThoai = soDienThoai?.Trim()

        };
        if (userDao.UpdateUser(editUser))
            return "Cập nhật thành công!";
        return "Lỗi khi cập nhật thông tin.";

    }

    public bool XoaDocGia(int id)
    {
        return userDao.DeleteUser(id);
    }

    public DataTable LayBaoCaoQuaHan()
    {
        return userDao.GetOverdueUsers();
    }

    public DataTable LayBaoCaoDangMuon()
    {
        return userDao.GetBorrowedBooks();
    }

    public UserDTO KiemTraDangNhap(string username, string password)
    {
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;
        return userDao.CheckLogin(username, password);
    }
    public List<UserDTO> LayTatCaNguoiDung()
    {
        return userDao.GetAllUsersForAdmin();
    }

    public string ThemNguoiDung(UserDTO user)
    {
        if (string.IsNullOrWhiteSpace(user.HoTen))
            return "Họ và tên không được để trống!";

        if ((user.Role == "Admin" || user.Role == "ThuThu") &&
            (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password)))
            return "Tài khoản Admin / Thủ thư bắt buộc phải có Username và Mật khẩu!";

        return userDao.InsertFullUser(user) ? "Thêm mới thành công!" : "Lỗi khi thêm người dùng.";
    }

    public string SuaNguoiDung(UserDTO user)
    {
        if (user.ID <= 0)
            return "Vui lòng chọn một độc giả trong danh sách để sửa!";

        if (string.IsNullOrWhiteSpace(user.HoTen))
            return "Họ và tên không được để trống!";

        return userDao.UpdateFullUser(user) ? "Cập nhật thông tin thành công!" : "Lỗi khi cập nhật thông tin.";
    }

    public bool DoiTrangThaiThe(int id, int isActive)
    {
        return userDao.SetUserActive(id, isActive);
    }
}