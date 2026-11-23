using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.UI.UserControls
{
    public partial class ucBook : UserControl
    {
        private readonly BookTitleBLL _bookTitleBLL = new();
        private readonly BookCopyBLL _bookCopyBLL = new();
        private readonly CategoryBLL _categoryBLL = new();
        private bool _isLoadingBookTitles;

        public ucBook()
        {
            InitializeComponent();
            dgvDanhSachSach.SelectionChanged += DisplaySelectedCopies;

            try
            {
                LoadCategories();
                LoadBookTitles();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(
                    this,
                    $"Không thể tải dữ liệu sách từ cơ sở dữ liệu: {ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            var selectedCategoryId = cboTheLoai.SelectedValue is int currentCategoryId
                ? currentCategoryId
                : (int?)null;
            var categories = _categoryBLL.GetAll();
            cboTheLoai.DisplayMember = nameof(CategoryDTO.TenTheLoai);
            cboTheLoai.ValueMember = nameof(CategoryDTO.CategoryID);
            cboTheLoai.DataSource = categories;
            cboTheLoai.Enabled = categories.Count > 0;

            if (selectedCategoryId.HasValue &&
                categories.Any(category => category.CategoryID == selectedCategoryId.Value))
            {
                cboTheLoai.SelectedValue = selectedCategoryId.Value;
            }

            tvTheLoai.BeginUpdate();
            try
            {
                tvTheLoai.Nodes.Clear();
                var nodes = categories.ToDictionary(
                    category => category.CategoryID,
                    category => new TreeNode(category.TenTheLoai)
                    {
                        Tag = category.CategoryID
                    });

                foreach (var category in categories)
                {
                    var node = nodes[category.CategoryID];
                    if (category.ParentID.HasValue &&
                        nodes.TryGetValue(category.ParentID.Value, out var parentNode))
                    {
                        parentNode.Nodes.Add(node);
                    }
                    else
                    {
                        tvTheLoai.Nodes.Add(node);
                    }
                }

                tvTheLoai.ExpandAll();
                lblCategoryCount.Text = $"{categories.Count} nhóm";
            }
            finally
            {
                tvTheLoai.EndUpdate();
            }
        }

        private void AddBookCategory(object? sender, EventArgs e)
        {
            var categoryName = PromptCategoryName();
            if (categoryName == null)
            {
                return;
            }

            var parentId = tvTheLoai.SelectedNode?.Tag is int selectedCategoryId
                ? selectedCategoryId
                : (int?)null;

            try
            {
                var newCategoryId = _categoryBLL.Add(new CategoryDTO
                {
                    TenTheLoai = categoryName,
                    ParentID = parentId
                });

                LoadCategories();
                var newNode = FindCategoryNode(tvTheLoai.Nodes, newCategoryId);
                if (newNode != null)
                {
                    tvTheLoai.SelectedNode = newNode;
                    newNode.EnsureVisible();
                }

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

        private string? PromptCategoryName()
        {
            var parentName = tvTheLoai.SelectedNode?.Text;
            using var dialog = new Form
            {
                Text = "Thêm thể loại sách",
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
                Text = parentName == null
                    ? "Nhập tên thể loại mới (thể loại gốc):"
                    : $"Nhập tên thể loại con của \"{parentName}\":",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);

            var nameTextBox = new TextBox
            {
                Dock = DockStyle.Fill
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
                Text = "Thêm",
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
            _isLoadingBookTitles = true;
            try
            {
                var categories = _categoryBLL.GetAll()
                    .ToDictionary(category => category.CategoryID, category => category.TenTheLoai);
                var bookTitles = _bookTitleBLL.GetAll();

                dgvDanhSachSach.Rows.Clear();
                dgvCuonSachVatLy.Rows.Clear();

                foreach (var bookTitle in bookTitles)
                {
                    var copies = _bookCopyBLL.GetByTitleId(bookTitle.TitleID);
                    var bookCode = $"SACH{bookTitle.TitleID:D2}";
                    var categoryName = categories.TryGetValue(bookTitle.CategoryID, out var name)
                        ? name
                        : "Không xác định";
                    var rowIndex = dgvDanhSachSach.Rows.Add(
                        bookCode,
                        bookTitle.TenSach,
                        bookTitle.TacGia,
                        categoryName,
                        bookTitle.GiaTien.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                        copies.Count.ToString(CultureInfo.CurrentCulture));
                    dgvDanhSachSach.Rows[rowIndex].Tag = bookTitle.TitleID;
                }

                if (dgvDanhSachSach.Rows.Count == 0)
                {
                    txtTenDauSach.Clear();
                    txtTacGia.Clear();
                    txtGiaTien.Clear();
                    cboTheLoai.SelectedIndex = -1;
                    lblPhysicalCopiesHeader.Text = "Chưa có đầu sách";
                    lblPhysicalCopiesSummary.Text = "Không có cuốn sách vật lý";
                    return;
                }

                var selectedRowIndex = 0;
                if (selectedTitleId.HasValue)
                {
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
                _isLoadingBookTitles = false;
            }
        }

        private void DisplaySelectedCopies(object? sender, EventArgs e)
        {
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
            var bookTitle = _bookTitleBLL.GetById(titleId);
            if (bookTitle == null)
            {
                LoadBookTitles();
                return;
            }

            txtTenDauSach.Text = bookTitle.TenSach;
            txtTacGia.Text = bookTitle.TacGia;
            txtGiaTien.Text = bookTitle.GiaTien.ToString(
                "0.##",
                CultureInfo.GetCultureInfo("vi-VN"));
            cboTheLoai.SelectedValue = bookTitle.CategoryID;
            DisplayCopies(titleId);
        }

        private void DisplayCopies(int titleId)
        {
            var bookCode = $"SACH{titleId:D2}";
            var copies = _bookCopyBLL.GetByTitleId(titleId);

            dgvCuonSachVatLy.Rows.Clear();
            foreach (var copy in copies)
            {
                var rowIndex = dgvCuonSachVatLy.Rows.Add(
                    copy.CopyID,
                    bookCode,
                    GetCopyStatusText(copy.TrangThai),
                    copy.TrangThai,
                    "-");
                if (copy.TrangThai == 0)
                {
                    dgvCuonSachVatLy.Rows[rowIndex].Cells[2].Style.ForeColor =
                        Color.FromArgb(22, 163, 74);
                }
            }

            lblPhysicalCopiesHeader.Text = $"Mã vạch sinh tự động cho {bookCode}";
            lblPhysicalCopiesSummary.Text = copies.Count == 0
                ? "Không có cuốn sách vật lý"
                : $"Hiển thị 1–{copies.Count} / {copies.Count} bản"
                    + $" | {copies.Count(copy => copy.TrangThai == 1)} đang mượn"
                    + $" | {copies.Count(copy => copy.TrangThai == 2)} mất/hỏng";
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
                TitleID = titleId,
                TenSach = txtTenDauSach.Text,
                TacGia = txtTacGia.Text,
                GiaTien = price,
                CategoryID = categoryId
            };

            try
            {
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
                TenSach = txtTenDauSach.Text,
                TacGia = txtTacGia.Text,
                GiaTien = price,
                CategoryID = categoryId
            };

            try
            {
                var result = _bookTitleBLL.AddWithCopies(bookTitle, quantity);
                LoadBookTitles(result.TitleId);
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
