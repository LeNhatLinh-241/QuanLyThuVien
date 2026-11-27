
using QuanLyThuVien.DAO;
using QuanLyThuVien.UI.UserControls;

namespace QuanLyThuVien;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var db = new DatabaseConnect();
        db.InitializeDatabase();

        while (true)
        {
            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                break;
            }

            using var mainForm = new FormMain(loginForm.LoggedInUser);
            Application.Run(mainForm);

            if (!mainForm.DaDangXuat)
            {
                break;
            }
        }
    }
}