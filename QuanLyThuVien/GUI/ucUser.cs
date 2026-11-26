using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien;

public partial class ucUser : UserControl
{
   private readonly UserBLL _userBLL = new();

    private List<UserDTO> _allUsers = new();
    private List<UserDTO> _filteredUsers = new();
    private int _selectedUserId = -1;
    private int _selectedUserActive = 1;

    private int _currentPage = 1;
    private const int PageSize = 7;

    public ucUser()
    {
        InitializeComponent();

        cboVaiTro.SelectedIndex = 0;
        cboTrangThaiThe.SelectedIndex = 0;
        cboLocTrangThai.SelectedIndex = 0;

        
        btnThem.Click += BtnThem_Click;
        btnSua.Click += BtnSua_Click;
        btnXoa.Click += BtnXoa_Click;
        btnLamMoi.Click += BtnLamMoi_Click;
        btnTimKiem.Click += (s, e) => ApDungLocVaHienThi(resetPage: true);
        cboLocTrangThai.SelectedIndexChanged += (s, e) => ApDungLocVaHienThi(resetPage: true);
        txtTimKiem.KeyDown += TxtTimKiem_KeyDown;

       
        btnTrangTruoc.Click += (s, e) => { if (_currentPage > 1) { _currentPage--; HienThiTrangHienTai(); } };
        btnTrangSau.Click += (s, e) =>
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling(_filteredUsers.Count / (double)PageSize));
            if (_currentPage < totalPages) { _currentPage++; HienThiTrangHienTai(); }
        };

        dgvDanhSach.CellClick += DgvDanhSach_CellClick;
        dgvDanhSach.CellFormatting += DgvDanhSach_CellFormatting;
        this.Load += (s, e) => TaiDanhSach();
    }

    public void TaiDanhSach()
    {
        try
        {
            _allUsers = _userBLL.LayTatCaNguoiDung();

            int soDocGia = _allUsers.Count(u => u.Role == "DocGia");
            int soNhanVien = _allUsers.Count - soDocGia;
            lblTongSo.Text = $"{soDocGia} độc giả • {soNhanVien} nhân viên";

            ApDungLocVaHienThi(resetPage: false);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách độc giả: {ex.Message}", "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApDungLocVaHienThi(bool resetPage)
    {
        string tuKhoa = txtTimKiem.Text.Trim().ToLower();
        int locTrangThai = cboLocTrangThai.SelectedIndex; 

        _filteredUsers = _allUsers.Where(u =>
        {
            string maHienThi = TaoMaHienThi(u).ToLower();
            bool khopTuKhoa = string.IsNullOrEmpty(tuKhoa) ||
                              (u.HoTen?.ToLower().Contains(tuKhoa) ?? false) ||
                              (u.SoDienThoai?.ToLower().Contains(tuKhoa) ?? false) ||
                              (u.Username?.ToLower().Contains(tuKhoa) ?? false) ||
                              maHienThi.Contains(tuKhoa);

            bool khopTrangThai = locTrangThai switch
            {
                1 => u.IsActive == 1,
                2 => u.IsActive == 0,
                _ => true
            };

            return khopTuKhoa && khopTrangThai;
        }).ToList();

        if (resetPage) _currentPage = 1;
        HienThiTrangHienTai();
    }

    private void HienThiTrangHienTai()
    {
        int total = _filteredUsers.Count;
        int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
        if (_currentPage > totalPages) _currentPage = totalPages;

        var pageItems = _filteredUsers
            .Skip((_currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        dgvDanhSach.Rows.Clear();
        foreach (var u in pageItems)
        {
            int rowIdx = dgvDanhSach.Rows.Add(
                TaoMaHienThi(u),
                u.HoTen,
                u.SoDienThoai ?? "",
                ChuyenRoleSangHienThi(u.Role),
                u.Username ?? "",
                u.IsActive == 1 ? "Hoạt động" : "Đã khóa",
                "");
            dgvDanhSach.Rows[rowIdx].Tag = u;
        }

        int start = total == 0 ? 0 : ((_currentPage - 1) * PageSize) + 1;
        int end = Math.Min(_currentPage * PageSize, total);
        string dangChonText = _selectedUserId > 0 ? lblDangChon.Text.Replace("Đang chọn: ", "") : "Chưa chọn";

        lblHienThiBanGhi.Text = $"Hiển thị {start}–{end} / {total} bản ghi  |  Đã chọn: {dangChonText}";
        lblTrangHienTai.Text = _currentPage.ToString();
        btnTrangTruoc.Enabled = _currentPage > 1;
        btnTrangSau.Enabled = _currentPage < totalPages;
    }

    private void DgvDanhSach_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvDanhSach.Rows[e.RowIndex].Tag is not UserDTO user) return;

        _selectedUserId = user.ID;
        _selectedUserActive = user.IsActive;

        string ma = TaoMaHienThi(user);
        lblDangChon.Text = $"Đang chọn: {ma}";
        txtHoTen.Text = user.HoTen;
        txtSoDienThoai.Text = user.SoDienThoai ?? "";
        txtTaiKhoan.Text = user.Username ?? "";
        txtMatKhau.Text = user.Password ?? "";

        cboVaiTro.SelectedIndex = user.Role switch
        {
            "ThuThu" => 1,
            "Admin" => 2,
            _ => 0
        };
        cboTrangThaiThe.SelectedIndex = user.IsActive == 1 ? 0 : 1;

        btnXoa.Text = user.IsActive == 1 ? "🔒 Khóa thẻ (Xóa mềm)" : "🔓 Mở khóa thẻ";
        lblHienThiBanGhi.Text = lblHienThiBanGhi.Text.Split('|')[0].Trim() + $"  |  Đã chọn: {ma}";
    }

    private void DgvDanhSach_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;

        if (dgvDanhSach.Columns[e.ColumnIndex].Name == "colMaDG")
        {
            e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
            e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }
        else if (dgvDanhSach.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
        {
            bool hoatDong = e.Value.ToString() == "Hoạt động";
            e.CellStyle.ForeColor = hoatDong
                ? Color.FromArgb(22, 163, 74)
                : Color.FromArgb(220, 38, 38);
            e.CellStyle.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
        }
    }

    private void BtnThem_Click(object? sender, EventArgs e)
    {
        var newUser = DocDuLieuTuForm();
        string kq = _userBLL.ThemNguoiDung(newUser);

        bool thanhCong = kq.Contains("thành công", StringComparison.OrdinalIgnoreCase);
        MessageBox.Show(kq, "Hệ thống quản lý thư viện", MessageBoxButtons.OK,
            thanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

        if (thanhCong)
        {
            LamMoiForm();
            TaiDanhSach();
        }
    }

    private void BtnSua_Click(object? sender, EventArgs e)
    {
        if (_selectedUserId <= 0)
        {
            MessageBox.Show("Vui lòng chọn một dòng trong danh sách để sửa!", "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var editUser = DocDuLieuTuForm();
        editUser.ID = _selectedUserId;

        string kq = _userBLL.SuaNguoiDung(editUser);
        bool thanhCong = kq.Contains("thành công", StringComparison.OrdinalIgnoreCase);
        MessageBox.Show(kq, "Hệ thống quản lý thư viện", MessageBoxButtons.OK,
            thanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

        if (thanhCong)
        {
            TaiDanhSach();
        }
    }

    private void BtnXoa_Click(object? sender, EventArgs e)
    {
        if (_selectedUserId <= 0)
        {
            MessageBox.Show("Vui lòng chọn độc giả cần khóa / mở khóa thẻ!", "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int trangThaiMoi = _selectedUserActive == 1 ? 0 : 1;
        string hanhDong = trangThaiMoi == 0 ? "khóa thẻ" : "mở khóa thẻ";

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn {hanhDong} của \"{txtHoTen.Text.Trim()}\"?",
            "Hệ thống quản lý thư viện",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        if (_userBLL.DoiTrangThaiThe(_selectedUserId, trangThaiMoi))
        {
            MessageBox.Show($"Đã {hanhDong} thành công!", "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
            TaiDanhSach();
        }
        else
        {
            MessageBox.Show($"Lỗi khi {hanhDong}.", "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnLamMoi_Click(object? sender, EventArgs e)
    {
        LamMoiForm();
        txtTimKiem.Clear();
        cboLocTrangThai.SelectedIndex = 0;
        TaiDanhSach();
    }

    private void TxtTimKiem_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ApDungLocVaHienThi(resetPage: true);
        }
    }

    private void LamMoiForm()
    {
        _selectedUserId = -1;
        _selectedUserActive = 1;
        lblDangChon.Text = "Đang chọn: Chưa chọn";
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        txtTaiKhoan.Clear();
        txtMatKhau.Clear();
        cboVaiTro.SelectedIndex = 0;
        cboTrangThaiThe.SelectedIndex = 0;
        btnXoa.Text = "🔒 Khóa thẻ (Xóa mềm)";
        txtHoTen.Focus();
    }

    private UserDTO DocDuLieuTuForm()
    {
        string role = cboVaiTro.SelectedIndex switch
        {
            1 => "ThuThu",
            2 => "Admin",
            _ => "DocGia"
        };

        return new UserDTO
        {
            HoTen = txtHoTen.Text.Trim(),
            SoDienThoai = txtSoDienThoai.Text.Trim(),
            Username = txtTaiKhoan.Text.Trim(),
            Password = txtMatKhau.Text.Trim(),
            Role = role,
            IsActive = cboTrangThaiThe.SelectedIndex == 0 ? 1 : 0
        };
    }

    private static string TaoMaHienThi(UserDTO u)
    {
        string prefix = u.Role switch
        {
            "ThuThu" => "TT",
            "Admin" => "QT",
            _ => "DG"
        };
        return $"{prefix}{u.ID:D3}";
    }

    private static string ChuyenRoleSangHienThi(string role) => role switch
    {
        "ThuThu" => "Thủ thư",
        "Admin" => "Quản trị viên",
        _ => "Độc giả"
    };
}