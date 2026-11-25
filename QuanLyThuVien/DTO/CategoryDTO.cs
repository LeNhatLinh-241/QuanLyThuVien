namespace QuanLyThuVien.DTO;

public class CategoryDTO
{
    
    public int CategoryID { get; set; }
    public string TenTheLoai { get; set; }
    public int? ParentID { get; set; }
    public int IsDeleted { get; set; }
}
