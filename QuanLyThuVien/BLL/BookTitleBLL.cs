using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BLL;

// BLL kiểm tra dữ liệu đầu sách và điều phối thao tác với DAO.
public class BookTitleBLL
{
    private readonly BookTitleDAO _bookTitleDAO = new();
    private readonly CategoryDAO _categoryDAO = new();

    public List<BookTitleDTO> GetAll()
    {
        return _bookTitleDAO.GetAll();
    }

    public BookTitleDTO? GetById(int titleId)
    {
        if (titleId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(titleId), "Mã đầu sách không hợp lệ.");
        }

        return _bookTitleDAO.GetById(titleId);
    }

    public int Add(BookTitleDTO bookTitle)
    {
        ValidateBookTitle(bookTitle);
        return _bookTitleDAO.Insert(bookTitle);
    }

    public (int TitleId, List<BookCopyDTO> Copies) AddWithCopies(BookTitleDTO bookTitle, int quantity)
    {
        ValidateBookTitle(bookTitle);

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng sách phải lớn hơn 0.");
        }

        return _bookTitleDAO.InsertWithCopies(bookTitle, quantity);
    }

    public bool Update(BookTitleDTO bookTitle)
    {
        ValidateBookTitle(bookTitle);

        if (bookTitle.TitleID <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bookTitle.TitleID), "Mã đầu sách không hợp lệ.");
        }

        return _bookTitleDAO.Update(bookTitle);
    }

    public bool Delete(int titleId)
    {
        if (titleId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(titleId), "Mã đầu sách không hợp lệ.");
        }

        return _bookTitleDAO.Delete(titleId);
    }

    private void ValidateBookTitle(BookTitleDTO bookTitle)
    {
        // Kiểm tra tên, giá, thể loại và chuẩn hóa chuỗi trước khi lưu.
        ArgumentNullException.ThrowIfNull(bookTitle);

        if (string.IsNullOrWhiteSpace(bookTitle.TenSach))
        {
            throw new ArgumentException("Tên sách không được để trống.", nameof(bookTitle));
        }

        if (!double.IsFinite(bookTitle.GiaTien) || bookTitle.GiaTien < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bookTitle.GiaTien), "Giá sách không được âm.");
        }

        if (_categoryDAO.GetById(bookTitle.CategoryID) == null)
        {
            throw new ArgumentException("Thể loại không tồn tại hoặc đã bị xóa.", nameof(bookTitle));
        }

        bookTitle.TenSach = bookTitle.TenSach.Trim();
        bookTitle.TacGia = bookTitle.TacGia?.Trim() ?? string.Empty;
    }
}
