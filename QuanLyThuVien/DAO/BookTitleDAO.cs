using Dapper;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO;

public class BookTitleDAO
{
    private readonly DatabaseConnect _databaseConnect = new();

    public List<BookTitleDTO> GetAll()
    {
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

    public bool Update(BookTitleDTO bookTitle)
    {
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
