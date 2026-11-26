using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien;

public partial class LoginForm : Form
{
    private readonly UserBLL _userBLL = new();

    public UserDTO? LoggedInUser { get; private set; }

    public LoginForm()
    {
        InitializeComponent();

        
        this.AcceptButton = btnDangNhap;

        btnDangNhap.Click += BtnDangNhap_Click;
        rdoAdmin.CheckedChanged += RoleRadio_CheckedChanged;
        rdoThuThu.CheckedChanged += RoleRadio_CheckedChanged;

        CapNhatMauVaiTro();
    }

    private void RoleRadio_CheckedChanged(object? sender, EventArgs e)
    {
        CapNhatMauVaiTro();
    }

    private void CapNhatMauVaiTro()
    {
        Color activeColor = Color.FromArgb(37, 99, 235);
        Color normalColor = Color.FromArgb(71, 85, 105);

        rdoAdmin.ForeColor = rdoAdmin.Checked ? activeColor : normalColor;
        rdoAdmin.Font = new Font("Segoe UI", 9.5F, rdoAdmin.Checked ? FontStyle.Bold : FontStyle.Regular);

        rdoThuThu.ForeColor = rdoThuThu.Checked ? activeColor : normalColor;
        rdoThuThu.Font = new Font("Segoe UI", 9.5F, rdoThuThu.Checked ? FontStyle.Bold : FontStyle.Regular);
    }

    private void BtnDangNhap_Click(object? sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show(
                "Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!",
                "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtUsername.Focus();
            return;
        }

        try
        {
            UserDTO? user = _userBLL.KiemTraDangNhap(username, password);
            if (user == null)
            {
                MessageBox.Show(
                    "Sai tên đăng nhập, mật khẩu hoặc tài khoản đã bị khóa!",
                    "Hệ thống quản lý thư viện",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            string vaiTroChon = rdoAdmin.Checked ? "Admin" : "ThuThu";
            if (!string.Equals(user.Role, vaiTroChon, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    $"Tài khoản này không thuộc vai trò {(rdoAdmin.Checked ? "Admin" : "Thủ thư")}!",
                    "Hệ thống quản lý thư viện",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            LoggedInUser = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi kết nối cơ sở dữ liệu: {ex.Message}",
                "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}