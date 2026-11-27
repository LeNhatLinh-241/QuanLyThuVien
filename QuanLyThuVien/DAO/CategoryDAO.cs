using Dapper;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO;

// Truy cập trực tiếp dữ liệu thể loại trong bảng Categories.
public class CategoryDAO
{
    private readonly DatabaseConnect _databaseConnect = new();

    public List<CategoryDTO> GetAll()
    {
        // Query lấy toàn bộ thể loại chưa xóa và chuyển kết quả thành danh sách DTO.
        using var connection = _databaseConnect.GetConnection();
        return connection.Query<CategoryDTO>(
            @"SELECT CategoryID, TenTheLoai, ParentID, IsDeleted
              FROM Categories
              WHERE IsDeleted = 0
              ORDER BY TenTheLoai").ToList();
    }

    public CategoryDTO? GetById(int categoryId)
    {
        // Query lấy một thể loại theo mã; trả về null nếu không còn tồn tại.
        using var connection = _databaseConnect.GetConnection();
        return connection.QuerySingleOrDefault<CategoryDTO>(
            @"SELECT CategoryID, TenTheLoai, ParentID, IsDeleted
              FROM Categories
              WHERE CategoryID = @CategoryID AND IsDeleted = 0",
            new { CategoryID = categoryId });
    }

    public int Insert(CategoryDTO category)
    {
        // Thêm thể loại mới, sau đó lấy mã tự tăng để GUI chọn node vừa tạo.
        using var connection = _databaseConnect.GetConnection();
        connection.Open();
        connection.Execute(
            @"INSERT INTO Categories (TenTheLoai, ParentID)
              VALUES (@TenTheLoai, @ParentID)",
            new
            {
                category.TenTheLoai,
                category.ParentID
            });
        return connection.ExecuteScalar<int>("SELECT last_insert_rowid();");
    }

    public bool Update(CategoryDTO category)
    {
        // Cập nhật tên và thể loại cha nếu bản ghi chưa bị xóa.
        using var connection = _databaseConnect.GetConnection();
        return connection.Execute(
            @"UPDATE Categories
              SET TenTheLoai = @TenTheLoai, ParentID = @ParentID
              WHERE CategoryID = @CategoryID AND IsDeleted = 0",
            new
            {
                category.CategoryID,
                category.TenTheLoai,
                category.ParentID
            }) > 0;
    }

    public bool Delete(int categoryId)
    {
        // Xóa mềm chỉ khi không còn thể loại con và không còn đầu sách đang dùng.
        using var connection = _databaseConnect.GetConnection();
        return connection.Execute(
            @"UPDATE Categories
              SET IsDeleted = 1
              WHERE CategoryID = @CategoryID
                AND IsDeleted = 0
                AND NOT EXISTS (
                    SELECT 1 FROM Categories child
                    WHERE child.ParentID = Categories.CategoryID
                      AND child.IsDeleted = 0
                )
                AND NOT EXISTS (
                    SELECT 1 FROM BookTitles title
                    WHERE title.CategoryID = Categories.CategoryID
                      AND title.IsDeleted = 0
                )",
            new { CategoryID = categoryId }) > 0;
    }
}
