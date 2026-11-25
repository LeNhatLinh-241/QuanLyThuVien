namespace QuanLyThuVien;

// Điểm bắt đầu của ứng dụng WinForms.
static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Khởi tạo cấu hình giao diện WinForms, gồm hỗ trợ DPI.
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        // Tạo lớp kết nối và tự tạo các bảng SQLite lần đầu khi chạy.
        DAO.DatabaseConnect db = new DAO.DatabaseConnect();
        db.InitializeDatabase();
        // Mở cửa sổ chính và bắt đầu vòng lặp xử lý sự kiện của ứng dụng.
        Application.Run(new FormMain());
    }
}