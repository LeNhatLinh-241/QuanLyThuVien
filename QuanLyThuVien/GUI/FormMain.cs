using QuanLyThuVien.DTO;
using QuanLyThuVien.GUI.UserControls;
using QuanLyThuVien.UI.UserControls;

namespace QuanLyThuVien;

public partial class FormMain : Form
{
    private readonly UserDTO? _currentUser;

    private ucUser? _ucUser;
    private ucBook? _ucBook;
    private ucBorrow? _ucBorrow;
    private ucReport? _ucReport;

    public bool DaDangXuat { get; private set; } = false;

    public FormMain() : this(null) { }

    public FormMain(UserDTO? currentUser)
    {
        InitializeComponent();
        _currentUser = currentUser;

        GanSuKienMenu();
        ThietLapThongTinNguoiDung();

        HienThiTrang(LayHoacTaoUcUser(), "QUẢN LÝ ĐỘC GIẢ", btnDocGia);
    }

    private void GanSuKienMenu()
    {
        btnDocGia.Click += (s, e) =>
        {
            var uc = LayHoacTaoUcUser();
            HienThiTrang(uc, "QUẢN LÝ ĐỘC GIẢ", btnDocGia);
            uc.TaiDanhSach();
        };

        btnKhoSach.Click += (s, e) =>
        {
            var uc = LayHoacTaoUcBook();
            HienThiTrang(uc, "QUẢN LÝ KHO SÁCH", btnKhoSach);
            uc.ReloadData();
        };

        btnMuonTra.Click += (s, e) =>
        {
            var uc = LayHoacTaoUcBorrow();
            HienThiTrang(uc, "MƯỢN - TRẢ SÁCH", btnMuonTra);
            uc.ReloadData();
        };

        btnBaoCao.Click += (s, e) =>
        {
            var uc = LayHoacTaoUcReport();
            HienThiTrang(uc, "BÁO CÁO - THỐNG KÊ", btnBaoCao);
            uc.TaiDuLieuBaoCao();
        };

        btnDangXuat.Click += BtnDangXuat_Click;
    }

    private void ThietLapThongTinNguoiDung()
    {
        string tenHienThi = _currentUser?.HoTen ?? "Admin";
        string vaiTro = _currentUser?.Role switch
        {
            "Admin" => "Quản trị viên",
            "ThuThu" => "Thủ thư",
            _ => "Thủ thư"
        };

        lblWelcome.Text = $"Xin chào: {tenHienThi} ({vaiTro})  |  Ngày: {DateTime.Now:dd/MM/yyyy}";
        lblUserName.Text = tenHienThi;
        lblUserRole.Text = vaiTro;
    }

    private ucUser LayHoacTaoUcUser() => _ucUser ??= new ucUser();
    private ucBook LayHoacTaoUcBook() => _ucBook ??= new ucBook();
    private ucBorrow LayHoacTaoUcBorrow() => _ucBorrow ??= new ucBorrow();
    private ucReport LayHoacTaoUcReport() => _ucReport ??= new ucReport();

    private void HienThiTrang(UserControl uc, string tieuDe, Button nutDangChon)
    {
        lblPageTitle.Text = tieuDe;
        CapNhatTrangThaiNutMenu(nutDangChon);

        pnlHienThi.SuspendLayout();
        pnlHienThi.Controls.Clear();
        uc.Dock = DockStyle.Fill;
        pnlHienThi.Controls.Add(uc);
        pnlHienThi.ResumeLayout();
    }

    private void CapNhatTrangThaiNutMenu(Button nutDangChon)
    {
        Button[] danhSachNut = { btnDocGia, btnKhoSach, btnMuonTra, btnBaoCao };

        foreach (Button btn in danhSachNut)
        {
            bool laNutChon = (btn == nutDangChon);
            btn.BackColor = laNutChon ? Color.FromArgb(37, 99, 235) : Color.Transparent;
            btn.ForeColor = laNutChon ? Color.White : Color.FromArgb(203, 213, 225);
            btn.Font = new Font("Segoe UI", 9.75F, laNutChon ? FontStyle.Bold : FontStyle.Regular);
        }
    }

    private void BtnDangXuat_Click(object? sender, EventArgs e)
    {
        var ketQua = MessageBox.Show(
            "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
            "Hệ thống quản lý thư viện",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (ketQua == DialogResult.Yes)
        {
            DaDangXuat = true;
            this.Close();
        }
    }
}