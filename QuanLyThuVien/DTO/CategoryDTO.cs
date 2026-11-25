namespace QuanLyThuVien.DTO;

// DTO lưu dữ liệu một thể loại sách khi truyền giữa GUI, BLL và DAO.
public class CategoryDTO
{
    // Mã thể loại duy nhất trong bảng Categories.
    public int CategoryID { get; set; }
    // Tên thể loại hiển thị trên cây và ComboBox.
    public string TenTheLoai { get; set; }
    // Mã thể loại cha; null nghĩa là thể loại gốc không có cha.
    public int? ParentID { get; set; }
    // Cờ ẩn dữ liệu: 0 là đang dùng, 1 là đã xóa mềm.
    public int IsDeleted { get; set; }
}
