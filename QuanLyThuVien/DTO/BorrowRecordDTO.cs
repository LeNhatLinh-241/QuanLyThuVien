namespace QuanLyThuVien.DTO;

public class BorrowRecordDTO
{
    public int RecordID { get; set; }
    public int UserID { get; set; }
    public string CopyID { get; set; }
    public string NgayMuon { get; set; }
    public string NgayHanTra { get; set; }
    public string NgayTraThucTe { get; set; } 
    public int TrangThai { get; set; }
    public double TienPhat { get; set; }
    public int TrangThaiThuTien { get; set; }
}