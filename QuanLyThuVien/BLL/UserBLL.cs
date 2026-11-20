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
    
}