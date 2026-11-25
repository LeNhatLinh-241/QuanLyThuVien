namespace QuanLyThuVien.DTO;

// DTO lưu dữ liệu phiếu mượn/trả trong bảng BorrowRecords.
public class BorrowRecordDTO
{
    // Mã phiếu mượn.
    public int RecordID { get; set; }
    // Mã người dùng hoặc độc giả thực hiện mượn.
    public int UserID { get; set; }
    // Mã cuốn sách vật lý được mượn.
    public string CopyID { get; set; }
    // Ngày mượn được lưu dưới dạng chuỗi văn bản.
    public string NgayMuon { get; set; }
    // Ngày dự kiến trả sách.
    public string NgayHanTra { get; set; }
    // Ngày thực tế trả; có thể rỗng khi sách chưa được trả.
    public string NgayTraThucTe { get; set; } 
    // Trạng thái phiếu mượn.
    public int TrangThai { get; set; }
    // Tổng tiền phạt phát sinh.
    public double TienPhat { get; set; }
    // Trạng thái thu tiền phạt.
    public int TrangThaiThuTien { get; set; }
}