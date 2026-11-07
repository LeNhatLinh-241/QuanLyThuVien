using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BLL;

public class BookCopyBLL
{
    private readonly BookCopyDAO _bookCopyDAO = new();
    private readonly BookTitleDAO _bookTitleDAO = new();

    public List<BookCopyDTO> GetByTitleId(int titleId)
    {
        if (titleId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(titleId), "Mã đầu sách không hợp lệ.");
        }

        return _bookCopyDAO.GetByTitleId(titleId);
    }

    public List<BookCopyDTO> AddCopies(int titleId, int quantity)
    {
        if (titleId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(titleId), "Mã đầu sách không hợp lệ.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng sách phải lớn hơn 0.");
        }

        if (_bookTitleDAO.GetById(titleId) == null)
        {
            throw new ArgumentException("Đầu sách không tồn tại hoặc đã bị xóa.", nameof(titleId));
        }

        return _bookCopyDAO.AddCopies(titleId, quantity);
    }

    public bool UpdateStatus(string copyId, int status)
    {
        if (string.IsNullOrWhiteSpace(copyId))
        {
            throw new ArgumentException("Mã cuốn sách không được để trống.", nameof(copyId));
        }

        if (status is < 0 or > 2)
        {
            throw new ArgumentOutOfRangeException(nameof(status), "Trạng thái phải là 0, 1 hoặc 2.");
        }

        return _bookCopyDAO.UpdateStatus(copyId.Trim(), status);
    }
}
