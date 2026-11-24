using QuanLyThuVien.UI.UserControls;

namespace QuanLyThuVien;

public partial class FormMain : Form
{
    public FormMain()
    {
        InitializeComponent();
        Text = "Hệ thống quản lý thư viện";
        StartPosition = FormStartPosition.WindowsDefaultLocation;
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        WindowState = FormWindowState.Maximized;
        var bookControl = new ucBook
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(bookControl);
    }
}