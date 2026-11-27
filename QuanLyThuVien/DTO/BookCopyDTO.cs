namespace QuanLyThuVien.DTO;

// Đối tượng truyền dữ liệu một cuốn sách vật lý.
public class BookCopyDTO
{
    // Mã cuốn vật lý duy nhất, ví dụ SACH01_1.
    public string CopyID { get; set; }
    // Mã đầu sách mà cuốn này thuộc về.
    public int TitleID { get; set; }
    // Trạng thái: 0 rảnh, 1 đang mượn, 2 mất/hỏng.
    public int TrangThai { get; set; }
}