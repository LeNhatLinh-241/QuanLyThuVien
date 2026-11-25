namespace QuanLyThuVien.DTO;

// DTO đại diện cho một cuốn sách vật lý cụ thể trong bảng BookCopies.
public class BookCopyDTO
{
    // Mã vạch duy nhất của cuốn vật lý, ví dụ SACH01_1.
    public string CopyID { get; set; }
    // Mã đầu sách mà cuốn vật lý này thuộc về.
    public int TitleID { get; set; }
    // Trạng thái: 0 đang rảnh, 1 đang mượn, 2 mất/hỏng.
    public int TrangThai { get; set; }
}