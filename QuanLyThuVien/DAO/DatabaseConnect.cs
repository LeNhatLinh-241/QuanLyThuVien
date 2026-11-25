using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace QuanLyThuVien.DAO;

// Lớp chịu trách nhiệm tạo kết nối và khởi tạo cấu trúc cơ sở dữ liệu SQLite.
public class DatabaseConnect
{
        // Đường dẫn SQLite mặc định; tệp thuvien.db nằm trong thư mục chạy ứng dụng.
        private readonly string connectionString = "Data Source=thuvien.db;";

        // Tạo đối tượng kết nối mới để mỗi DAO sử dụng và đóng riêng.
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }

        // Tạo tất cả bảng nếu chưa tồn tại khi chương trình khởi động.
        public void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    //bảng Users
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

                    //bảng Categories
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Categories (
                            CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                            TenTheLoai TEXT NOT NULL,
                            ParentID INTEGER NULL,
                            IsDeleted INTEGER DEFAULT 0,
                            FOREIGN KEY (ParentID) REFERENCES Categories(CategoryID)
                        );";
                    command.ExecuteNonQuery();

                    //bảng BookTitles
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

                    //bảng BookCopies
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS BookCopies (
                            CopyID TEXT PRIMARY KEY,
                            TitleID INTEGER NOT NULL,
                            TrangThai INTEGER DEFAULT 0,
                            FOREIGN KEY (TitleID) REFERENCES BookTitles(TitleID)
                        );";
                    command.ExecuteNonQuery();

                    //bảngBorrowRecords
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
                }
            }
        }
    }
