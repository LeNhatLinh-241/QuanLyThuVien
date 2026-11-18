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
        private readonly CategoryBLL _categoryBLL = new();

        public ucBook()
        {
            InitializeComponent();
            LoadCategories();
            LoadSampleData();
        }

        private void LoadCategories()
        {
            var categories = _categoryBLL.GetAll();
            if (categories.Count == 0)
            {
                foreach (var categoryName in new[]
                {
                    "Công nghệ thông tin / Lập trình C#",
                    "Kinh tế",
                    "Ngoại ngữ"
                })
                {
                    _categoryBLL.Add(new CategoryDTO { TenTheLoai = categoryName });
                }

                categories = _categoryBLL.GetAll();
            }

            cboTheLoai.DisplayMember = nameof(CategoryDTO.TenTheLoai);
            cboTheLoai.ValueMember = nameof(CategoryDTO.CategoryID);
            cboTheLoai.DataSource = categories;
        }

        private void LoadSampleData()
        {
            dgvDanhSachSach.Rows.Add(
                "SACH01", "Lập trình C# căn bản", "Phạm Hữu Khang",
                "Công nghệ", "120.000", "10"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH02", "Cơ sở dữ liệu", "Nguyễn Thị Ngọc Mai",
                "Công nghệ", "110.000", "8"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH03", "Cấu trúc dữ liệu", "Đỗ Xuân Lôi",
                "Công nghệ", "130.000", "12"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH04", "Kinh tế học vĩ mô", "Nguyễn Văn Dân",
                "Kinh tế", "95.000", "15"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH05", "Dế Mèn phiêu lưu ký", "Tô Hoài",
                "Văn học", "45.000", "20"
            );

            for (var copyNumber = 1; copyNumber <= 4; copyNumber++)
            {
                var rowIndex = dgvCuonSachVatLy.Rows.Add(
                    $"SACH01_{copyNumber}",
                    "SACH01",
                    "Sẵn sàng trên kệ",
                    "0",
                    "Kệ A1-01"
                );
                dgvCuonSachVatLy.Rows[rowIndex].Cells[2].Style.ForeColor =
                    Color.FromArgb(22, 163, 74);
            }

            dgvDanhSachSach.Rows[0].Selected = true;
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
                var bookCode = $"SACH{result.TitleId:D2}";
                var titleValues = new object[]
                {
                    bookCode,
                    bookTitle.TenSach,
                    bookTitle.TacGia,
                    category.TenTheLoai,
                    price.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                    quantity.ToString(CultureInfo.CurrentCulture)
                };

                var titleRow = -1;
                for (var index = 0; index < dgvDanhSachSach.Rows.Count; index++)
                {
                    if (Equals(dgvDanhSachSach.Rows[index].Cells[0].Value, bookCode))
                    {
                        titleRow = index;
                        break;
                    }
                }

                if (titleRow < 0)
                {
                    titleRow = dgvDanhSachSach.Rows.Add(titleValues);
                }
                else
                {
                    dgvDanhSachSach.Rows[titleRow].SetValues(titleValues);
                }

                dgvDanhSachSach.ClearSelection();
                dgvDanhSachSach.Rows[titleRow].Selected = true;
                dgvCuonSachVatLy.Rows.Clear();
                foreach (var copy in result.Copies)
                {
                    var copyRow = dgvCuonSachVatLy.Rows.Add(
                        copy.CopyID,
                        bookCode,
                        "Sẵn sàng trên kệ",
                        copy.TrangThai,
                        "-"
                    );
                    dgvCuonSachVatLy.Rows[copyRow].Cells[2].Style.ForeColor =
                        Color.FromArgb(22, 163, 74);
                }

                lblPhysicalCopiesHeader.Text = $"Mã vạch sinh tự động cho {bookCode}";
                lblPhysicalCopiesSummary.Text =
                    $"Hiển thị 1–{result.Copies.Count} / {result.Copies.Count} bản";
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
