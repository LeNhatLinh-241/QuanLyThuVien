using Dapper;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO;

// DAO truy xuất và thay đổi dữ liệu thể loại trực tiếp trong SQLite.
public class CategoryDAO
{
    // Tạo kết nối dùng chung cho các truy vấn thể loại.
    private readonly DatabaseConnect _databaseConnect = new();

    // Lấy danh sách thể loại chưa bị xóa, sắp theo tên.
    public List<CategoryDTO> GetAll()
    {
        using var connection = _databaseConnect.GetConnection();
        return connection.Query<CategoryDTO>(
            @"SELECT CategoryID, TenTheLoai, ParentID, IsDeleted
              FROM Categories
              WHERE IsDeleted = 0
              ORDER BY TenTheLoai").ToList();
    }

    public CategoryDTO? GetById(int categoryId)
    {
        // Tìm một thể loại theo mã và chỉ trả về nếu chưa bị xóa.
        using var connection = _databaseConnect.GetConnection();
        return connection.QuerySingleOrDefault<CategoryDTO>(
            @"SELECT CategoryID, TenTheLoai, ParentID, IsDeleted
              FROM Categories
              WHERE CategoryID = @CategoryID AND IsDeleted = 0",
            new { CategoryID = categoryId });
    }

    public int Insert(CategoryDTO category)
    {
        // Thêm thể loại mới và lấy mã tự tăng vừa được tạo để đưa ngược lên cây.
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
        // Cập nhật tên và thể loại cha, không cập nhật bản ghi đã bị xóa.
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
        // Xóa mềm: đặt IsDeleted = 1 chỉ khi không còn thể loại con và không còn đầu sách đang dùng.
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
