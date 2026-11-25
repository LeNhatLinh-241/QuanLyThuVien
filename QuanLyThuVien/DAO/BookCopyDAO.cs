using System.Data;
using Dapper;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAO;

// DAO truy xuất, sinh và thay đổi trạng thái cuốn sách vật lý.
public class BookCopyDAO
{
    private readonly DatabaseConnect _databaseConnect = new();

    public List<BookCopyDTO> GetByTitleId(int titleId)
    {
        // Lấy các cuốn vật lý thuộc một đầu sách theo mã CopyID tăng dần.
        using var connection = _databaseConnect.GetConnection();
        return connection.Query<BookCopyDTO>(
            @"SELECT CopyID, TitleID, TrangThai
              FROM BookCopies
              WHERE TitleID = @TitleID
              ORDER BY CopyID",
            new { TitleID = titleId }).ToList();
    }

    public List<BookCopyDTO> AddCopies(int titleId, int quantity)
    {
        // Kiểm tra đầu vào trước khi mở giao dịch thêm nhiều cuốn.
        if (titleId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(titleId), "Mã đầu sách không hợp lệ.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng sách phải lớn hơn 0.");
        }

        using var connection = _databaseConnect.GetConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        var titleExists = connection.ExecuteScalar<int>(
            @"SELECT COUNT(1)
              FROM BookTitles
              WHERE TitleID = @TitleID AND IsDeleted = 0",
            new { TitleID = titleId },
            transaction);

        if (titleExists == 0)
        {
            throw new ArgumentException("Đầu sách không tồn tại hoặc đã bị xóa.", nameof(titleId));
        }

        var addedCopies = AddCopiesInTransaction(connection, transaction, titleId, quantity);
        transaction.Commit();
        return addedCopies;
    }

    internal List<BookCopyDTO> AddCopiesInTransaction(
        IDbConnection connection,
        IDbTransaction transaction,
        int titleId,
        int quantity)
    {
        // Hàm nội bộ này chạy trong giao dịch để sinh mã và chèn từng cuốn vật lý.
        if (titleId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(titleId), "Mã đầu sách không hợp lệ.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng sách phải lớn hơn 0.");
        }

        var existingCopyIds = connection.Query<string>(
            "SELECT CopyID FROM BookCopies WHERE TitleID = @TitleID",
            new { TitleID = titleId },
            transaction).ToHashSet(StringComparer.Ordinal);

        var addedCopies = new List<BookCopyDTO>();
        var copyNumber = 1;

        for (var i = 0; i < quantity; i++)
        {
            string copyId;
            do
            {
                copyId = $"SACH{titleId:D2}_{copyNumber}";
                copyNumber++;
            }
            while (existingCopyIds.Contains(copyId));

            connection.Execute(
                @"INSERT INTO BookCopies (CopyID, TitleID, TrangThai)
                  VALUES (@CopyID, @TitleID, 0)",
                new
                {
                    CopyID = copyId,
                    TitleID = titleId
                },
                transaction);

            existingCopyIds.Add(copyId);
            addedCopies.Add(new BookCopyDTO
            {
                CopyID = copyId,
                TitleID = titleId,
                TrangThai = 0
            });
        }

        return addedCopies;
    }

    public bool UpdateStatus(string copyId, int status)
    {
        using var connection = _databaseConnect.GetConnection();
        return connection.Execute(
            @"UPDATE BookCopies
              SET TrangThai = @TrangThai
              WHERE CopyID = @CopyID",
            new
            {
                CopyID = copyId,
                TrangThai = status
            }) > 0;
    }
}
