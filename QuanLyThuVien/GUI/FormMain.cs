using QuanLyThuVien.UI.UserControls;

namespace QuanLyThuVien;

// Cửa sổ chính của ứng dụng, dùng để đặt giao diện quản lý sách làm nội dung.
public partial class FormMain : Form
{
    public FormMain()
    {
        // Tạo giao diện nền do Visual Studio Designer sinh ra.
        InitializeComponent();

        // Cấu hình tiêu đề, vị trí và cho phép cửa sổ phóng to toàn màn hình.
        Text = "Hệ thống quản lý thư viện";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        WindowState = FormWindowState.Maximized;

        var bookControl = new ucBook
        {
            // Fill giúp ucBook chiếm toàn bộ vùng hiển thị của cửa sổ.
            Dock = DockStyle.Fill
        };

        // Thêm màn hình quản lý sách vào cửa sổ chính.
        Controls.Add(bookControl);
    }
}