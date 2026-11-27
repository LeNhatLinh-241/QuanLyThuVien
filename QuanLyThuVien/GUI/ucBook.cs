using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.UI.UserControls
{
    // Lớp điều khiển màn hình quản lý sách, thể loại và cuốn sách vật lý
    public partial class ucBook : UserControl
    {
        // BLL dùng để truy cập và xử lý dữ liệu đầu sách
        private readonly BookTitleBLL _bookTitleBLL = new();
        // BLL dùng để truy cập và xử lý dữ liệu cuốn sách vật lý
        private readonly BookCopyBLL _bookCopyBLL = new();
        // BLL dùng để truy cập và xử lý dữ liệu thể loại.
        private readonly CategoryBLL _categoryBLL = new();
        // Cờ tránh xử lý sự kiện chọn dòng trong lúc bảng đang được nạp dữ liệu
        private bool _isLoadingBookTitles;
        // Số cuốn vật lý hiển thị trên mỗi trang.
        private const int PhysicalCopiesPageSize = 10;
        // Đầu sách đang hiển thị và trang cuốn vật lý hiện tại.
        private int _currentCopiesTitleId;
        private int _physicalCopiesPage = 1;

        // Hàm khởi tạo UserControl, được gọi khi màn hình được tạo
        public ucBook()
        {
            
            // Tạo các điều khiển giao diện trong ucBook.Designer.cs
            InitializeComponent();
            // Khi người dùng chọn dòng đầu sách, tự động tải cuốn vật lý tương ứng
            dgvDanhSachSach.SelectionChanged += DisplaySelectedCopies;

            try
            {
                // Nạp danh sách thể loại vào cây và ComboBox.
                LoadCategories();
                // Nạp danh sách đầu sách và chọn dòng đầu tiên.
                LoadBookTitles();
            }
            catch (SqliteException ex)
            {
                // Thông báo nếu không thể kết nối hoặc truy vấn cơ sở dữ liệu
                MessageBox.Show(
                    this,
                    $"Không thể tải dữ liệu sách từ cơ sở dữ liệu: {ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        public void ReloadData()
        {
            try
            {
                int? currentId = _currentCopiesTitleId > 0 ? _currentCopiesTitleId : null;
                LoadCategories();
                LoadBookTitles(currentId);
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(this, $"Không thể tải lại dữ liệu sách: {ex.Message}",
                    "Hệ thống quản lý thư viện", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            // Giữ lại thể loại đang chọn trước khi nạp lại ComboBox
            var selectedCategoryId = cboTheLoai.SelectedValue is int currentCategoryId
                ? currentCategoryId
                : (int?)null;
            // Lấy tất cả thể loại chưa bị xóa từ cơ sở dữ liệu
            var categories = _categoryBLL.GetAll();
            // Hiển thị tên thể loại trên ComboBox
            cboTheLoai.DisplayMember = nameof(CategoryDTO.TenTheLoai);
            // Lưu mã thể loại làm giá trị được chọn
            cboTheLoai.ValueMember = nameof(CategoryDTO.CategoryID);
            // Gán danh sách thể loại vào ComboBox
            cboTheLoai.DataSource = categories;
            // Chỉ cho chọn thể loại khi danh sách không rỗng
            cboTheLoai.Enabled = categories.Count > 0;

            // Khôi phục lựa chọn cũ nếu thể loại vẫn còn trong danh sách mới
            if (selectedCategoryId.HasValue &&
                categories.Any(category => category.CategoryID == selectedCategoryId.Value))
            {
                cboTheLoai.SelectedValue = selectedCategoryId.Value;
            }

            tvTheLoai.BeginUpdate();
            try
            {
                // Xóa toàn bộ node cũ trước khi dựng lại cây
                tvTheLoai.Nodes.Clear();
                // Tạo một node cây cho mỗi thể loại và lưu CategoryID trong Tag
                var nodes = categories.ToDictionary(
                    category => category.CategoryID,
                    category => new TreeNode(category.TenTheLoai)
                    {
                        Tag = category.CategoryID
                    });

                // Duyệt từng thể loại để xác định node cha/con
                foreach (var category in categories)
                {
                    var node = nodes[category.CategoryID];
                    // Nếu có cha hợp lệ thì thêm node vào dưới node cha
                    if (category.ParentID.HasValue &&
                        nodes.TryGetValue(category.ParentID.Value, out var parentNode))
                    {
                        parentNode.Nodes.Add(node);
                    }
                    else
                    {
                        // Không có cha thì node là thể loại gốc
                        tvTheLoai.Nodes.Add(node);
                    }
                }

                // Mở rộng tất cả node để người dùng thấy cây phân cấp
                tvTheLoai.ExpandAll();
                // Cập nhật tổng số thể loại đang hiển thị
                lblCategoryCount.Text = $"{categories.Count} nhóm";
            }
            finally
            {
                tvTheLoai.EndUpdate();
            }
        }

        private void AddBookCategory(object? sender, EventArgs e)
        {
            // Mở hộp thoại yêu cầu nhập tên thể loại mới
            var categoryName = PromptCategoryName();
            if (categoryName == null)
            {
                // Người dùng nhấn Hủy hoặc đóng hộp thoại
                return;
            }

            // Nếu có node đang chọn thì thêm thể loại con dưới node đó
            var parentId = tvTheLoai.SelectedNode?.Tag is int selectedCategoryId
                ? selectedCategoryId
                : (int?)null;

            try
            {
                // Gửi dữ liệu thể loại mới xuống BLL để kiểm tra và lưu
                var newCategoryId = _categoryBLL.Add(new CategoryDTO
                {
                    TenTheLoai = categoryName,
                    ParentID = parentId
                });

                // Tải lại cây và ComboBox sau khi thêm thành công
                LoadCategories();
                // Tìm node vừa được tạo để chọn và cuộn tới nó
                var newNode = FindCategoryNode(tvTheLoai.Nodes, newCategoryId);
                if (newNode != null)
                {
                    tvTheLoai.SelectedNode = newNode;
                    newNode.EnsureVisible();
                }

                // Hiển thị thông báo thành công
                MessageBox.Show(
                    this,
                    "Đã thêm thể loại sách.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Không thể thêm thể loại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(
                    this,
                    $"Không thể lưu thể loại sách: {ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateBookCategory(object? sender, EventArgs e)
        {
            // Chỉ tiếp tục khi người dùng đã chọn một node thể loại trong cây
            if (tvTheLoai.SelectedNode?.Tag is not int categoryId)
            {
                    MessageBox.Show(
                        this,
                        "Vui lòng chọn thể loại cần sửa trong cây thể loại.",
                        "Chưa chọn thể loại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
            }

            // Lấy dữ liệu thể loại hiện tại để biết tên và ParentID
            var category = _categoryBLL.GetById(categoryId);
            if (category == null)
            {
                    MessageBox.Show(
                        this,
                        "Không tìm thấy thể loại được chọn.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
            }

            // Mở hộp thoại sửa và điền tên thể loại hiện tại
            var categoryName = PromptCategoryName(category.TenTheLoai);
            if (categoryName == null)
            {
                    // Người dùng hủy sửa, không thay đổi dữ liệu
                    return;
            }

            // Cập nhật tên mới vào đối tượng DTO; giữ nguyên ParentID
            category.TenTheLoai = categoryName;

            try
            {
                    // Gọi BLL để lưu thay đổi tên thể loại
                    var updated = _categoryBLL.Update(category);
                    if (!updated)
                    {
                        MessageBox.Show(
                            this,
                            "Không thể cập nhật thể loại sách.",
                            "Thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    LoadCategories();
                    var updatedNode = FindCategoryNode(tvTheLoai.Nodes, categoryId);
                    if (updatedNode != null)
                    {
                        tvTheLoai.SelectedNode = updatedNode;
                        updatedNode.EnsureVisible();
                    }

                    MessageBox.Show(
                        this,
                        "Đã cập nhật tên thể loại sách.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                    MessageBox.Show(
                        this,
                        ex.Message,
                        "Không thể sửa thể loại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            catch (SqliteException ex)
            {
                    MessageBox.Show(
                        this,
                        $"Không thể cập nhật thể loại sách: {ex.Message}",
                        "Lỗi cơ sở dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void DeleteBookCategory(object? sender, EventArgs e)
        {
            // Chỉ cho phép xóa khi người dùng đã chọn một thể loại trong cây.
            if (tvTheLoai.SelectedNode?.Tag is not int categoryId)
            {
                MessageBox.Show(
                        this,
                        "Vui lòng chọn thể loại cần xóa trong cây thể loại.",
                        "Chưa chọn thể loại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin thể loại để hiển thị tên trong hộp xác nhận.
            var category = _categoryBLL.GetById(categoryId);
            if (category == null)
            {
                MessageBox.Show(
                        this,
                        "Không tìm thấy thể loại được chọn.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            // Yêu cầu người dùng xác nhận trước khi thực hiện xóa.
            var confirmation = MessageBox.Show(
                this,
                $"Bạn có chắc muốn xóa thể loại \"{category.TenTheLoai}\"?",
                "Xác nhận xóa thể loại",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Gọi BLL để thực hiện xóa; DAO chỉ xóa được khi không còn dữ liệu liên quan.
                if (!_categoryBLL.Delete(categoryId))
                {
                        MessageBox.Show(
                            this,
                            "Không thể xóa thể loại. Hãy kiểm tra xem thể loại còn thể loại con hoặc đầu sách đang sử dụng hay không.",
                            "Không thể xóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                }

                // Sau khi xóa thành công, nạp lại cây thể loại và danh sách đầu sách.
                LoadCategories();
                LoadBookTitles();
                MessageBox.Show(
                        this,
                        "Đã xóa thể loại sách.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                        this,
                        ex.Message,
                        "Không thể xóa thể loại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(
                        this,
                        $"Không thể xóa thể loại sách: {ex.Message}",
                        "Lỗi cơ sở dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private string? PromptCategoryName(string? currentName = null)
        {
            var parentName = tvTheLoai.SelectedNode?.Text;
            using var dialog = new Form
            {
                    Text = currentName == null ? "Thêm thể loại sách" : "Sửa thể loại sách",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ShowInTaskbar = false,
                ClientSize = new Size(380, 150)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layout.Controls.Add(new Label
            {
                Text = currentName == null
                    ? parentName == null
                        ? "Nhập tên thể loại mới (thể loại gốc):"
                        : $"Nhập tên thể loại con của \"{parentName}\":"
                    : "Nhập tên mới cho thể loại:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);

            var nameTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Text = currentName ?? string.Empty
            };
            layout.Controls.Add(nameTextBox, 0, 1);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };
            var addButton = new Button
            {
                Text = currentName == null ? "Thêm" : "Lưu",
                DialogResult = DialogResult.OK,
                AutoSize = true
            };
            var cancelButton = new Button
            {
                Text = "Hủy",
                DialogResult = DialogResult.Cancel,
                AutoSize = true
            };
            buttons.Controls.Add(cancelButton);
            buttons.Controls.Add(addButton);
            layout.Controls.Add(buttons, 0, 2);

            dialog.AcceptButton = addButton;
            dialog.CancelButton = cancelButton;
            dialog.Controls.Add(layout);

            return dialog.ShowDialog(this) == DialogResult.OK
                ? nameTextBox.Text
                : null;
        }

        private static TreeNode? FindCategoryNode(TreeNodeCollection nodes, int categoryId)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is int currentCategoryId && currentCategoryId == categoryId)
                {
                    return node;
                }

                var childNode = FindCategoryNode(node.Nodes, categoryId);
                if (childNode != null)
                {
                    return childNode;
                }
            }

            return null;
        }

        private void LoadBookTitles(int? selectedTitleId = null)
        {
            // Bật cờ để bỏ qua SelectionChanged trong lúc bảng đang được nạp lại
            _isLoadingBookTitles = true;
            try
            {
                // Tạo từ điển ánh xạ mã thể loại sang tên thể loại
                var categories = _categoryBLL.GetAll()
                    .ToDictionary(category => category.CategoryID, category => category.TenTheLoai);
                // Lấy tất cả đầu sách chưa bị xóa.
                var bookTitles = _bookTitleBLL.GetAll();

                // Xóa dữ liệu cũ trong bảng đầu sách và bảng cuốn vật lý
                dgvDanhSachSach.Rows.Clear();
                dgvCuonSachVatLy.Rows.Clear();

                // Duyệt từng đầu sách để tạo một dòng trên bảng
                foreach (var bookTitle in bookTitles)
                {
                    // Lấy số cuốn vật lý thuộc đầu sách này
                    var copies = _bookCopyBLL.GetByTitleId(bookTitle.TitleID);
                    // Tạo mã đầu sách hiển thị theo định dạng SACH01, SACH02...
                    var bookCode = $"SACH{bookTitle.TitleID:D2}";
                    // Tìm tên thể loại; dùng chuỗi mặc định nếu dữ liệu không tồn tại
                    var categoryName = categories.TryGetValue(bookTitle.CategoryID, out var name)
                        ? name
                        : "Không xác định";
                    // Thêm dòng đầu sách gồm mã, tên, tác giả, thể loại, giá và số bản
                    var rowIndex = dgvDanhSachSach.Rows.Add(
                        bookCode,
                        bookTitle.TenSach,
                        bookTitle.TacGia,
                        categoryName,
                        bookTitle.GiaTien.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                        copies.Count.ToString(CultureInfo.CurrentCulture));
                    // Lưu TitleID ngoài cột hiển thị để dùng khi chọn hoặc sửa/xóa
                    dgvDanhSachSach.Rows[rowIndex].Tag = bookTitle.TitleID;
                }

                if (dgvDanhSachSach.Rows.Count == 0)
                {
                    // Xóa các ô nhập vì không còn đầu sách để hiển thị
                    txtTenDauSach.Clear();
                    txtTacGia.Clear();
                    txtGiaTien.Clear();
                    cboTheLoai.SelectedIndex = -1;
                    // Cập nhật phần trạng thái bảng cuốn vật lý    
                    lblPhysicalCopiesHeader.Text = "Chưa có đầu sách";
                    lblPhysicalCopiesSummary.Text = "Không có cuốn sách vật lý";
                    ResetPhysicalPagination();
                    return;
                }

                // Mặc định chọn dòng đầu tiên
                var selectedRowIndex = 0;
                if (selectedTitleId.HasValue)
                {
                    // Tìm dòng có TitleID trùng ID được yêu cầu chọn lại
                    for (var index = 0; index < dgvDanhSachSach.Rows.Count; index++)
                    {
                        if (Equals(dgvDanhSachSach.Rows[index].Tag, selectedTitleId.Value))
                        {
                            selectedRowIndex = index;
                            break;
                        }
                    }
                }

                dgvDanhSachSach.ClearSelection();
                dgvDanhSachSach.Rows[selectedRowIndex].Selected = true;
                if (dgvDanhSachSach.Rows[selectedRowIndex].Tag is int titleId)
                {
                    DisplaySelectedBook(titleId);
                }
            }
            finally
            {
                // Tắt cờ sau khi nạp dữ liệu xong để sự kiện chọn dòng hoạt động lại
                _isLoadingBookTitles = false;
            }
        }

        private void DisplaySelectedCopies(object? sender, EventArgs e)
        {
            // Bỏ qua khi đang nạp bảng hoặc người dùng chưa chọn dòng
            if (_isLoadingBookTitles || dgvDanhSachSach.SelectedRows.Count == 0)
            {
                return;
            }

            if (dgvDanhSachSach.SelectedRows[0].Tag is int titleId)
            {
                try
                {
                    DisplaySelectedBook(titleId);
                }
                catch (SqliteException ex)
                {
                    MessageBox.Show(
                        this,
                        $"Không thể tải thông tin sách: {ex.Message}",
                        "Lỗi cơ sở dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void DisplaySelectedBook(int titleId)
        {
            // Lấy thông tin đầu sách theo mã được chọn
            var bookTitle = _bookTitleBLL.GetById(titleId);
            if (bookTitle == null)
            {
                // Nếu đầu sách không còn tồn tại thì nạp lại bảng
                LoadBookTitles();
                return;
            }

            // Đưa dữ liệu đầu sách vào các ô nhập để người dùng xem/sửa
            txtTenDauSach.Text = bookTitle.TenSach;
            txtTacGia.Text = bookTitle.TacGia;
            txtGiaTien.Text = bookTitle.GiaTien.ToString(
                "0.##",
                CultureInfo.GetCultureInfo("vi-VN"));
            // Chọn thể loại tương ứng trong ComboBox
            cboTheLoai.SelectedValue = bookTitle.CategoryID;
            // Tải danh sách cuốn vật lý của đầu sách
            DisplayCopies(titleId);
        }

        private void DisplayCopies(int titleId)
        {
            // Chọn đầu sách mới thì quay về trang đầu tiên.
            if (_currentCopiesTitleId != titleId)
            {
                _currentCopiesTitleId = titleId;
                _physicalCopiesPage = 1;
            }

            // Tạo mã đầu sách để hiển thị trong vùng cuốn vật lý
            var bookCode = $"SACH{titleId:D2}";
            // Lấy tất cả cuốn vật lý thuộc đầu sách
            var copies = _bookCopyBLL.GetByTitleId(titleId);

            // Xóa các dòng cuốn vật lý cũ trước khi nạp dữ liệu mới
            dgvCuonSachVatLy.Rows.Clear();

            // Tính tổng số trang và xác định tập cuốn thuộc trang hiện tại.
            var totalPages = Math.Max(1, (int)Math.Ceiling(copies.Count / (double)PhysicalCopiesPageSize));
            _physicalCopiesPage = Math.Min(_physicalCopiesPage, totalPages);
            var pageCopies = copies
                .Skip((_physicalCopiesPage - 1) * PhysicalCopiesPageSize)
                .Take(PhysicalCopiesPageSize)
                .ToList();

            foreach (var copy in pageCopies)
            {
                // Thêm mã vạch, đầu sách, trạng thái và ghi chú vào bảng
                var rowIndex = dgvCuonSachVatLy.Rows.Add(
                    copy.CopyID,
                    bookCode,
                    GetCopyStatusText(copy.TrangThai),
                    copy.TrangThai,
                    "-");
                // Tô xanh trạng thái "Sẵn sàng trên kệ"
                if (copy.TrangThai == 0)
                {
                    dgvCuonSachVatLy.Rows[rowIndex].Cells[2].Style.ForeColor =
                        Color.FromArgb(22, 163, 74);
                }
            }

            lblPhysicalCopiesHeader.Text = $"Mã vạch sinh tự động cho {bookCode}";
            if (copies.Count == 0)
            {
                lblPhysicalCopiesSummary.Text = "Không có cuốn sách vật lý";
            }
            else
            {
                var firstIndex = ((_physicalCopiesPage - 1) * PhysicalCopiesPageSize) + 1;
                var lastIndex = Math.Min(_physicalCopiesPage * PhysicalCopiesPageSize, copies.Count);
                lblPhysicalCopiesSummary.Text =
                    $"Hiển thị {firstIndex}–{lastIndex} / {copies.Count} bản"
                    + $" | {copies.Count(copy => copy.TrangThai == 1)} đang mượn"
                    + $" | {copies.Count(copy => copy.TrangThai == 2)} mất/hỏng";
            }

            lblPhysicalPage.Text = copies.Count == 0
                ? "0"
                : $"{_physicalCopiesPage}/{totalPages}";
            btnPhysicalPrevious.Enabled = copies.Count > 0 && _physicalCopiesPage > 1;
            btnPhysicalNext.Enabled = copies.Count > 0 && _physicalCopiesPage < totalPages;
        }

        private void PhysicalPreviousPage(object? sender, EventArgs e)
        {
            // Giảm trang và nạp lại bảng; nút đã tự khóa ở trang đầu.
            if (_physicalCopiesPage > 1)
            {
                _physicalCopiesPage--;
                DisplayCopies(_currentCopiesTitleId);
            }
        }

        private void PhysicalNextPage(object? sender, EventArgs e)
        {
            // Tăng trang và nạp lại bảng; nút đã tự khóa ở trang cuối.
            _physicalCopiesPage++;
            DisplayCopies(_currentCopiesTitleId);
        }

        private void ResetPhysicalPagination()
        {
            // Xóa trạng thái phân trang khi không còn đầu sách được chọn.
            _currentCopiesTitleId = 0;
            _physicalCopiesPage = 1;
            lblPhysicalPage.Text = "0";
            btnPhysicalPrevious.Enabled = false;
            btnPhysicalNext.Enabled = false;
        }

        private static string GetCopyStatusText(int status)
        {
            return status switch
            {
                0 => "Sẵn sàng trên kệ",
                1 => "Đang mượn",
                2 => "Mất/hỏng",
                _ => $"Không xác định ({status})"
            };
        }

        private void UpdateSelectedBookTitle(object? sender, EventArgs e)
        {
            // Kiểm tra đã chọn đầu sách và lấy TitleID từ dòng bảng
            if (dgvDanhSachSach.SelectedRows.Count == 0 ||
                dgvDanhSachSach.SelectedRows[0].Tag is not int titleId)
            {
                MessageBox.Show(
                    this,
                    "Hãy chọn đầu sách cần sửa.",
                    "Chưa chọn đầu sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(
                    txtGiaTien.Text,
                    NumberStyles.Number,
                    CultureInfo.GetCultureInfo("vi-VN"),
                    out var price))
            {
                // Dừng thao tác và yêu cầu nhập lại nếu giá tiền không hợp lệ
                MessageBox.Show(
                    this,
                    "Giá tiền không hợp lệ.",
                    "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtGiaTien.Focus();
                return;
            }

            if (cboTheLoai.SelectedValue is not int categoryId ||
                cboTheLoai.SelectedItem is not CategoryDTO)
            {
                // Yêu cầu chọn thể loại hợp lệ trước khi lưu
                MessageBox.Show(
                    this,
                    "Hãy chọn thể loại hợp lệ trước khi lưu.",
                    "Thiếu thể loại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboTheLoai.Focus();
                return;
            }

            var bookTitle = new BookTitleDTO
            {
                // Gắn TitleID để biết dòng nào cần cập nhật
                TitleID = titleId,
                TenSach = txtTenDauSach.Text,
                TacGia = txtTacGia.Text,
                GiaTien = price,
                CategoryID = categoryId
            };

            try
            {
                // Gọi BLL để cập nhật đầu sách trong cơ sở dữ liệu
                if (!_bookTitleBLL.Update(bookTitle))
                {
                    MessageBox.Show(
                        this,
                        "Đầu sách không còn tồn tại hoặc đã bị xóa.",
                        "Không thể cập nhật",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    LoadBookTitles();
                    return;
                }

                LoadBookTitles(titleId);
                MessageBox.Show(
                    this,
                    "Đã cập nhật thông tin đầu sách.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Không thể cập nhật đầu sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(
                    this,
                    $"Không thể cập nhật đầu sách: {ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedBookTitle(object? sender, EventArgs e)
        {
            // Kiểm tra người dùng đã chọn đầu sách cần xóa chưa
            if (dgvDanhSachSach.SelectedRows.Count == 0 ||
                dgvDanhSachSach.SelectedRows[0].Tag is not int titleId)
            {
                MessageBox.Show(
                    this,
                    "Hãy chọn đầu sách cần xóa.",
                    "Chưa chọn đầu sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvDanhSachSach.SelectedRows[0];
            // Lấy tên đầu sách từ cột thứ hai để hiển thị trong hộp xác nhận
            var titleName = Convert.ToString(selectedRow.Cells[1].Value) ?? "đầu sách đã chọn";
            var confirmation = MessageBox.Show(
                this,
                $"Bạn có chắc muốn xóa đầu sách \"{titleName}\"? Đầu sách sẽ được ẩn khỏi danh sách. "
                    + "Không thể xóa nếu còn cuốn đang được mượn.",
                "Xác nhận xóa đầu sách",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Gọi BLL để ẩn đầu sách khi đủ điều kiện xóa
                if (!_bookTitleBLL.Delete(titleId))
                {
                    LoadBookTitles(titleId);
                    MessageBox.Show(
                        this,
                        "Không thể xóa đầu sách. Có thể sách đang được mượn hoặc đầu sách đã bị xóa.",
                        "Không thể xóa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                LoadBookTitles();
                MessageBox.Show(
                    this,
                    "Đã xóa đầu sách khỏi danh sách.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Không thể xóa đầu sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(
                    this,
                    $"Không thể xóa đầu sách: {ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddBookTitleAndCopies(object? sender, EventArgs e)
        {
            // Kiểm tra số lượng nhập là số nguyên dương
            if (!int.TryParse(
                    txtSoLuongNhap.Text,
                    NumberStyles.Integer,
                    CultureInfo.CurrentCulture,
                    out var quantity) ||
                quantity <= 0)
            {
                MessageBox.Show(
                    this,
                    "Số lượng nhập phải là số nguyên lớn hơn 0.",
                    "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtSoLuongNhap.Focus();
                return;
            }

            if (!double.TryParse(
                    txtGiaTien.Text,
                    NumberStyles.Number,
                    CultureInfo.GetCultureInfo("vi-VN"),
                    out var price))
            {
                // Dừng thao tác nếu định dạng giá tiền không hợp lệ
                MessageBox.Show(
                    this,
                    "Giá tiền không hợp lệ.",
                    "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtGiaTien.Focus();
                return;
            }

            if (cboTheLoai.SelectedValue is not int categoryId ||
                cboTheLoai.SelectedItem is not CategoryDTO category)
            {
                MessageBox.Show(
                    this,
                    "Chưa có thể loại hợp lệ. Hãy thêm thể loại trước khi tạo đầu sách.",
                    "Thiếu thể loại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var bookTitle = new BookTitleDTO
            {
                // Gom thông tin người dùng nhập thành một đầu sách mới
                TenSach = txtTenDauSach.Text,
                TacGia = txtTacGia.Text,
                GiaTien = price,
                CategoryID = categoryId
            };

            try
            {
                // Tạo đầu sách và đồng thời sinh số cuốn vật lý theo số lượng nhập
                var result = _bookTitleBLL.AddWithCopies(bookTitle, quantity);
                // Nạp lại bảng và chọn đầu sách vừa được tạo
                LoadBookTitles(result.TitleId);
                MessageBox.Show(
                    this,
                    $"Đã thêm đầu sách và sinh {result.Copies.Count} mã vạch thành công!",
                    "Hệ thống quản lý thư viện",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Không thể thêm đầu sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(
                    this,
                    $"Không thể lưu đầu sách và cuốn vật lý: {ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
