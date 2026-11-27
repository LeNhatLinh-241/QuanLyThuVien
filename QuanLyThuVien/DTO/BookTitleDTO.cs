namespace QuanLyThuVien.DTO;

public class BookTitleDTO
{
    public int TitleID { get; set; }
    public string TenSach { get; set; }
    public string TacGia { get; set; }
    public double GiaTien { get; set; }
    public int CategoryID { get; set; }
    public int IsDeleted { get; set; }
}