using Microsoft.Data.Sqlite;

namespace QuanLyThuVien.DAO;

// Quản lý chuỗi kết nối SQLite và khởi tạo cấu trúc cơ sở dữ liệu.
public class DatabaseConnect
{
        // Tệp cơ sở dữ liệu được lưu trong thư mục chạy ứng dụng.
        private readonly string connectionString = "Data Source=thuvien.db;";

        // Tạo kết nối mới cho mỗi thao tác truy vấn hoặc khởi tạo.
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }

        public void InitializeDatabase()
        {
            // Mở kết nối và chạy lần lượt các lệnh tạo bảng.
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    // Tạo bảng lưu tài khoản và thông tin người dùng.
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Users (
                            ID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NULL,
                            Password TEXT NULL,
                            Role TEXT NOT NULL,
                            HoTen TEXT NOT NULL,
                            SoDienThoai TEXT,
                            IsActive INTEGER DEFAULT 1
                        );";
                    command.ExecuteNonQuery();

                    // Tạo bảng thể loại có quan hệ cha-con.
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Categories (
                            CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                            TenTheLoai TEXT NOT NULL,
                            ParentID INTEGER NULL,
                            IsDeleted INTEGER DEFAULT 0,
                            FOREIGN KEY (ParentID) REFERENCES Categories(CategoryID)
                        );";
                    command.ExecuteNonQuery();

                    // Tạo bảng đầu sách.
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS BookTitles (
                            TitleID INTEGER PRIMARY KEY AUTOINCREMENT,
                            TenSach TEXT NOT NULL,
                            TacGia TEXT,
                            GiaTien REAL DEFAULT 0,
                            CategoryID INTEGER,
                            IsDeleted INTEGER DEFAULT 0,
                            FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
                        );";
                    command.ExecuteNonQuery();

                    // Tạo bảng cuốn vật lý và trạng thái.
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS BookCopies (
                            CopyID TEXT PRIMARY KEY,
                            TitleID INTEGER NOT NULL,
                            TrangThai INTEGER DEFAULT 0,
                            FOREIGN KEY (TitleID) REFERENCES BookTitles(TitleID)
                        );";
                    command.ExecuteNonQuery();

                    // Tạo bảng phiếu mượn/trả.
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS BorrowRecords (
                            RecordID INTEGER PRIMARY KEY AUTOINCREMENT,
                            UserID INTEGER NOT NULL,
                            CopyID TEXT NOT NULL,
                            NgayMuon TEXT NOT NULL,
                            NgayHanTra TEXT NOT NULL,
                            NgayTraThucTe TEXT NULL,
                            TrangThai INTEGER DEFAULT 0,
                            TienPhat REAL DEFAULT 0,
                            TrangThaiThuTien INTEGER DEFAULT 0,
                            FOREIGN KEY (UserID) REFERENCES Users(ID),
                            FOREIGN KEY (CopyID) REFERENCES BookCopies(CopyID)
                        );";
                    command.ExecuteNonQuery();
                    // tk test
                    command.CommandText = @"
                    INSERT INTO Users (Username, Password, Role, HoTen, SoDienThoai, IsActive)
                    SELECT 'admin', '123', 'Admin', 'Quản trị viên', '0900000001', 1
                    WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'admin');

                    INSERT INTO Users (Username, Password, Role, HoTen, SoDienThoai, IsActive)
                    SELECT 'thuthu', '123', 'ThuThu', 'Thủ thư', '0900000002', 1
                    WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'thuthu');";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
