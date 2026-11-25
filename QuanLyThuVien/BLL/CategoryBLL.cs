using QuanLyThuVien.DAO;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BLL;

// BLL kiểm tra dữ liệu thể loại trước khi gọi DAO lưu vào cơ sở dữ liệu.
public class CategoryBLL
{
    private readonly CategoryDAO _categoryDAO = new();

    public List<CategoryDTO> GetAll()
    {
        // Lấy danh sách thể loại đang hoạt động để hiển thị cây và ComboBox.
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
        // Kiểm tra tên và quan hệ cha trước khi thêm thể loại mới.
        ValidateCategory(category);
        ValidateParent(category.ParentID);
        return _categoryDAO.Insert(category);
    }

    public bool Update(CategoryDTO category)
    {
        // Kiểm tra dữ liệu, vòng lặp cha/con rồi gọi DAO cập nhật.
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
        // Chỉ chấp nhận mã hợp lệ; điều kiện còn dữ liệu liên quan do DAO kiểm tra.
        if (categoryId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(categoryId), "Mã thể loại không hợp lệ.");
        }

        return _categoryDAO.Delete(categoryId);
    }

    private static void ValidateCategory(CategoryDTO category)
    {
        // Ngăn tham số null, tên rỗng và chuẩn hóa khoảng trắng ở hai đầu.
        ArgumentNullException.ThrowIfNull(category);

        if (string.IsNullOrWhiteSpace(category.TenTheLoai))
        {
            throw new ArgumentException("Tên thể loại không được để trống.", nameof(category));
        }

        category.TenTheLoai = category.TenTheLoai.Trim();
    }

    private void ValidateParent(int? parentId, int? categoryId = null)
    {
        // Theo dãy thể loại cha để ngăn tự trỏ về chính nó hoặc tạo vòng lặp.
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
