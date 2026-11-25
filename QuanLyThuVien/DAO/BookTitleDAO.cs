using Dapper;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO;

// Truy cập dữ liệu đầu sách và điều phối giao dịch cùng cuốn vật lý.
public class BookTitleDAO
{
    private readonly DatabaseConnect _databaseConnect = new();
    private readonly BookCopyDAO _bookCopyDAO = new();

    public List<BookTitleDTO> GetAll()
    {
        // Lấy các đầu sách còn hiệu lực để hiển thị trong DataGridView.
        using var connection = _databaseConnect.GetConnection();
        return connection.Query<BookTitleDTO>(
            @"SELECT TitleID, TenSach, TacGia, GiaTien, CategoryID, IsDeleted
              FROM BookTitles
              WHERE IsDeleted = 0
              ORDER BY TenSach").ToList();
    }

    public BookTitleDTO? GetById(int titleId)
    {
        using var connection = _databaseConnect.GetConnection();
        return connection.QuerySingleOrDefault<BookTitleDTO>(
            @"SELECT TitleID, TenSach, TacGia, GiaTien, CategoryID, IsDeleted
              FROM BookTitles
              WHERE TitleID = @TitleID AND IsDeleted = 0",
            new { TitleID = titleId });
    }

    public int Insert(BookTitleDTO bookTitle)
    {
        using var connection = _databaseConnect.GetConnection();
        connection.Open();
        connection.Execute(
            @"INSERT INTO BookTitles (TenSach, TacGia, GiaTien, CategoryID)
              VALUES (@TenSach, @TacGia, @GiaTien, @CategoryID)",
            new
            {
                bookTitle.TenSach,
                bookTitle.TacGia,
                bookTitle.GiaTien,
                bookTitle.CategoryID
            });
        return connection.ExecuteScalar<int>("SELECT last_insert_rowid();");
    }

    public (int TitleId, List<BookCopyDTO> Copies) InsertWithCopies(BookTitleDTO bookTitle, int quantity)
    {
        // Tạo đầu sách và nhiều cuốn trong cùng transaction để giữ dữ liệu nhất quán.
        using var connection = _databaseConnect.GetConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        connection.Execute(
            @"INSERT INTO BookTitles (TenSach, TacGia, GiaTien, CategoryID)
              VALUES (@TenSach, @TacGia, @GiaTien, @CategoryID)",
            new
            {
                bookTitle.TenSach,
                bookTitle.TacGia,
                bookTitle.GiaTien,
                bookTitle.CategoryID
            },
            transaction);

        var titleId = connection.ExecuteScalar<int>(
            "SELECT last_insert_rowid();",
            transaction: transaction);
        var copies = _bookCopyDAO.AddCopiesInTransaction(
            connection,
            transaction,
            titleId,
            quantity);

        transaction.Commit();
        return (titleId, copies);
    }

    public bool Update(BookTitleDTO bookTitle)
    {
        // Cập nhật các trường thông tin của đầu sách theo TitleID.
        using var connection = _databaseConnect.GetConnection();
        return connection.Execute(
            @"UPDATE BookTitles
              SET TenSach = @TenSach,
                  TacGia = @TacGia,
                  GiaTien = @GiaTien,
                  CategoryID = @CategoryID
              WHERE TitleID = @TitleID AND IsDeleted = 0",
            new
            {
                bookTitle.TitleID,
                bookTitle.TenSach,
                bookTitle.TacGia,
                bookTitle.GiaTien,
                bookTitle.CategoryID
            }) > 0;
    }

    public bool Delete(int titleId)
    {
        // Xóa mềm đầu sách nếu không còn cuốn đang mượn hoặc phiếu chưa trả.
        using var connection = _databaseConnect.GetConnection();
        return connection.Execute(
            @"UPDATE BookTitles
              SET IsDeleted = 1
              WHERE TitleID = @TitleID
                AND IsDeleted = 0
                AND NOT EXISTS (
                    SELECT 1
                    FROM BookCopies copy
                    WHERE copy.TitleID = BookTitles.TitleID
                      AND (
                          copy.TrangThai = 1
                          OR EXISTS (
                              SELECT 1
                              FROM BorrowRecords record
                              WHERE record.CopyID = copy.CopyID
                                AND record.TrangThai = 0
                          )
                      )
                )",
            new { TitleID = titleId }) > 0;
    }
}
