using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BLL;

// BLL chứa nghiệp vụ và kiểm tra dữ liệu thể loại trước khi truy cập DAO.
public class CategoryBLL
{
    private readonly CategoryDAO _categoryDAO = new();

    public List<CategoryDTO> GetAll()
    {
        return _categoryDAO.GetAll();
    }

    public CategoryDTO? GetById(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(categoryId), "Mã thể loại không hợp lệ.");
        }

        return _categoryDAO.GetById(categoryId);
    }

    public int Add(CategoryDTO category)
    {
        // Kiểm tra tên và quan hệ cha-con trước khi thêm thể loại.
        ValidateCategory(category);
        ValidateParent(category.ParentID);
        return _categoryDAO.Insert(category);
    }

    public bool Update(CategoryDTO category)
    {
        // Kiểm tra dữ liệu và ngăn vòng lặp cha/con trước khi cập nhật.
        ValidateCategory(category);

        if (category.CategoryID <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(category.CategoryID), "Mã thể loại không hợp lệ.");
        }

        ValidateParent(category.ParentID, category.CategoryID);
        return _categoryDAO.Update(category);
    }

    public bool Delete(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(categoryId), "Mã thể loại không hợp lệ.");
        }

        return _categoryDAO.Delete(categoryId);
    }

    private static void ValidateCategory(CategoryDTO category)
    {
        // Kiểm tra tên bắt buộc và loại bỏ khoảng trắng thừa.
        ArgumentNullException.ThrowIfNull(category);

        if (string.IsNullOrWhiteSpace(category.TenTheLoai))
        {
            throw new ArgumentException("Tên thể loại không được để trống.", nameof(category));
        }

        category.TenTheLoai = category.TenTheLoai.Trim();
    }

    private void ValidateParent(int? parentId, int? categoryId = null)
    {
        // Duyệt chuỗi cha để phát hiện tự trỏ hoặc vòng lặp cây.
        var currentParentId = parentId;
        var visited = new HashSet<int>();

        while (currentParentId.HasValue)
        {
            if (categoryId == currentParentId.Value)
            {
                throw new ArgumentException("Thể loại không thể là cha của chính nó.");
            }

            if (!visited.Add(currentParentId.Value))
            {
                throw new ArgumentException("Cây thể loại đang có vòng lặp.");
            }

            var parent = _categoryDAO.GetById(currentParentId.Value);
            if (parent == null)
            {
                throw new ArgumentException("Thể loại cha không tồn tại hoặc đã bị xóa.");
            }

            currentParentId = parent.ParentID;
        }
    }
}
