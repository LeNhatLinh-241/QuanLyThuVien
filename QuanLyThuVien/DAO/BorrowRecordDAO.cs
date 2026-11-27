using Dapper;
using QuanLyThuVien.DTO;
namespace QuanLyThuVien.DAO;

public class BorrowRecordDAO
{
    // Dùng lớp DatabaseConnect để tạo kết nối tới file SQLite dùng chung của cả dự án
    private DatabaseConnect db = new DatabaseConnect();

    /** Lấy toàn bộ danh sách phiếu mượn trả theo đúng 9 cột của bảng BorrowRecords
     * Sắp xếp theo RecordID giảm dần (DESC) để phiếu mới lập luôn nằm trên cùng bảng
     */
    public List<BorrowRecordDTO> GetAll()
    {
        List<BorrowRecordDTO> list = new List<BorrowRecordDTO>();
        
        // Khối using giúp tự động đóng kết nối và giải phóng tài nguyên sau khi chạy xong, tránh bị khóa file SQLite
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                     SELECT RecordID, UserID, CopyID, NgayMuon, NgayHanTra, NgayTraThucTe, TrangThai, TienPhat, TrangThaiThuTien
                     From BorrowRecords
                     Order BY RecordID DESC";
                
                // ExecuteReader dùng cho câu SELECT trả về nhiều dòng dữ liệu
                using (var reader = command.ExecuteReader())
                {
                    // Đọc lần lượt từng dòng trong kết quả, thứ tự số 0 đến 8 khớp đúng thứ tự 9 cột trong câu SELECT
                    while (reader.Read())
                    {
                        BorrowRecordDTO item = new BorrowRecordDTO();
                        item.RecordID = reader.GetInt32(0);
                        item.UserID = reader.GetInt32(1);
                        item.CopyID = reader.GetString(2);
                        item.NgayMuon = reader.GetString(3);
                        item.NgayHanTra = reader.GetString(4);
                        item.NgayTraThucTe = reader.IsDBNull(5) ? null : reader.GetString(5);
                        item.TrangThai = reader.GetInt32(6);
                        item.TienPhat = reader.GetDouble(7);
                        item.TrangThaiThuTien = reader.GetInt32(8);
                        list.Add(item);
                    }
                }
            }
        }

        return list;
    }

    // Điều kiện 1 khi mượn sách: kiểm tra độc giả có tồn tại trong bảng Users và cột IsActive có bằng 1 (đang hoạt động) hay không
    public bool KiemTraDocGiaHoatDong(int userId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // Truyền tham số @userId bằng Parameters thay vì cộng chuỗi trực tiếp để tránh lỗi SQL Injection
                command.CommandText = "SELECT IsActive From Users WHERE ID = @userId";
                command.Parameters.AddWithValue("@userId", userId);
                
                // ExecuteScalar dùng khi câu truy vấn chỉ trả về đúng 1 giá trị duy nhất (ô đầu tiên của dòng đầu tiên)
                var result = command.ExecuteScalar();
                if (result != null && Convert.ToInt32(result) == 1)
                {
                    return true; // người dùng tồn tại và đang hoạt động
                }

                return false; // không tìm thấy hoặc thẻ đã bị khóa
            }
        }
    }

    // Điều kiện 2 khi mượn sách: tính tổng tiền phạt chưa thu (TienPhat lớn hơn 0 và TrangThaiThuTien bằng 0) của một độc giả
    public double TinhTongNoPhat(int userId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT SUM(TienPhat)
                FROM BorrowRecords
                WHERE UserID = @userId AND TienPhat > 0 AND TrangThaiThuTien = 0";
                command.Parameters.AddWithValue("@userId", userId);
                
                var result = command.ExecuteScalar();
                // Nếu độc giả chưa có phiếu nợ nào thì hàm SUM của SQL trả về DBNull.Value nên phải kiểm tra trước khi ép kiểu
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDouble(result); // trả về tổng số tiền phạt
                }

                return 0;
            }
        }
    }

    // Điều kiện 3 khi mượn sách: đếm xem độc giả hiện đang giữ bao nhiêu cuốn sách
    public int DemSoSachDangMuon(int userId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT COUNT(*)
                    FROM BorrowRecords
                    WHERE UserID = @userId AND TrangThai = 0";
                command.Parameters.AddWithValue("@userId", userId);
                
                // COUNT(*) luôn trả về một con số nguyên (tối thiểu là 0)
                return Convert.ToInt32(command.ExecuteScalar()); // trả về số lượng sách đang mượn
            }
        }
    }

    //Kiểm tra mã vạch CopyID trong BookCopies (kết hợp kiểm tra IsDeleted = 0 ở BookTitles và Categories)
    //Trả về: -1 (không tồn tại hoặc đầu sách / thể loại đã xóa),0 (Rảnh), 1(Đang mượn), 2(Mất/Hỏng)
    public int LayTrangThaiCuonSach(string copyId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // Nối bảng BookCopies với BookTitles để kiểm tra IsDeleted của đầu sách
                // Dùng LEFT JOIN với Categories và IFNULL(c.IsDeleted, 0) phòng trường hợp đầu sách chưa gắn thể loại vẫn cho mượn được
                command.CommandText = @"
                    SELECT bc.TrangThai
                    FROM BookCopies bc 
                    JOIN BookTitles bt ON bc.TitleID = bt.TitleID
                    LEFT JOIN Categories c ON bt.CategoryID = c.CategoryID
                    WHERE bc.CopyID = @copyId AND bt.IsDeleted = 0 AND IFNULL(c.IsDeleted, 0) = 0";
                command.Parameters.AddWithValue("@copyId", copyId);
                var result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return -1; //không tồn tại hoặc dấu sách / thể loại đã xóa
                }

                return Convert.ToInt32(result); // trả về trạng thái của cuốn sách
            }
        }
    }

    // Tra cứu giá bìa (GiaTien) của đầu sách thông qua mã vạch CopyID để tính tiền đền bù khi độc giả làm mất sách
    public double LayGiaTienSach(string copyId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT bt.GiaTien
                    FROM BookCopies bc
                    JOIN BookTitles bt ON bc.TitleID = bt.TitleID
                    WHERE bc.CopyID = @copyId";
                command.Parameters.AddWithValue("@copyId", copyId);
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDouble(result); //trả về giá tiền của cuốn sách
                }

                return 0; //trả về 0 nếu không tìm thấy hoặc giá tiền không hợp lệ
            }
        }
    }

    // Lưu phiếu mượn mới vào bảng BorrowRecords đồng thời đổi trạng thái cuốn sách trong BookCopies sang 1 (đang bị mượn)
    // Bọc cả 2 lệnh SQL trong Transaction để đảm bảo tính đồng bộ: thành công hết thì lưu, lỗi giữa chừng thì hoàn tác sạch
    public bool InsertBorrowRecord(BorrowRecordDTO phieu)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Transaction: gom nhiều lệnh SQL làm 1 khối, chạy xong hết mới lưu (Commit), lỗi giữa chừng thì hủy sạch (Rollback)
                    // Gắn transaction cho command để mọi lệnh ExecuteNonQuery bên dưới nằm chung một giao dịch
                    using (var command = connection.CreateCommand())
                    {
                        //thêm phiếu mượn vào bảng BorrowRecords
                        command.Transaction = transaction;
                        command.CommandText = @"
                            INSERT INTO BorrowRecords (UserID, CopyID, NgayMuon, NgayHanTra, NgayTraThucTe, TrangThai, TienPhat, TrangThaiThuTien)
                            VALUES (@userId, @copyId, @ngayMuon, @ngayHanTra, NULL, 0, 0, 0)";
                        command.Parameters.AddWithValue("@userId", phieu.UserID);
                        command.Parameters.AddWithValue("@copyId", phieu.CopyID);
                        command.Parameters.AddWithValue("@ngayMuon", phieu.NgayMuon);
                        command.Parameters.AddWithValue("@ngayHanTra", phieu.NgayHanTra);
                        command.ExecuteNonQuery();
                        
                        // Bước 2: xóa bộ tham số cũ rồi cập nhật trạng thái bản sao sách thành 1 (đang có người mượn)
                        command.Parameters.Clear();
                        command.CommandText = "UPDATE BookCopies SET TrangThai = 1 WHERE CopyID = @copyId";
                        command.Parameters.AddWithValue("@copyId", phieu.CopyID);
                        command.ExecuteNonQuery();
                    }
                    
                    // Cả 2 bước đều chạy êm thì chốt lưu xuống SQLite
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    // Có bất kỳ lỗi nào thì hoàn tác lại toàn bộ để tránh cảnh có phiếu mượn nhưng sách vẫn báo rảnh
                    transaction.Rollback();
                    return false; 
                }
            }
        }
    }

    // Xử lý trả sách hoặc báo mất: cập nhật phiếu trong BorrowRecords và đồng thời đổi lại trạng thái sách trong BookCopies
    // Cũng dùng Transaction vì phải sửa cùng lúc 2 bảng
    public bool UpdateBorrowRecord(int recordId, string copyId, string ngayTraThucTe, int trangThaiPhieu,
        double tienPhat, int trangThaiThuTien)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        
                        // Bước 1: cập nhật ngày trả thực tế, trạng thái phiếu, tiền phạt và trạng thái thu tiền
                        command.CommandText = @"
                            UPDATE BorrowRecords
                            SET NgayTraThucTe = @ngayTra,
                                TrangThai = @trangThai,
                                TienPhat = @tienPhat,
                                TrangThaiThuTien = @thuTien
                            WHERE RecordID = @recordId";
                        command.Parameters.AddWithValue("@ngayTra", ngayTraThucTe);
                        command.Parameters.AddWithValue("@trangThai", trangThaiPhieu);
                        command.Parameters.AddWithValue("@tienPhat", tienPhat);
                        command.Parameters.AddWithValue("@thuTien", trangThaiThuTien);
                        command.Parameters.AddWithValue("@recordId", recordId);
                        command.ExecuteNonQuery();

                        /**
                         * Quy đổi sang trạng thái kho của bản sao sách:
                         * nếu phiếu là Đã trả (1) thì sách về rảnh (0), nếu phiếu là Làm mất (2) thì sách sang mất/hỏng (2)
                         */
                        int trangThaiCopyMoi = (trangThaiPhieu == 1) ? 0 : 2;
                        
                        // Bước 2: xóa bộ tham số cũ và cập nhật lại bảng BookCopies
                        command.Parameters.Clear();
                        command.CommandText = "UPDATE BookCopies SET TrangThai = @ttCopy WHERE CopyID = @copyId";
                        command.Parameters.AddWithValue("@ttCopy", trangThaiCopyMoi);
                        command.Parameters.AddWithValue("@copyId", copyId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }

    // Đánh dấu đã thu tiền phạt (đổi TrangThaiThuTien sang 1) cho trường hợp độc giả ghi nợ rồi quay lại đóng sau
    public bool UpdateTrangThaiThuTien(int recordId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE BorrowRecords SET TrangThaiThuTien = 1 WHERE RecordID = @recordId";
                command.Parameters.AddWithValue("@recordId", recordId);
                
                // ExecuteNonQuery trả về số dòng bị tác động; lớn hơn 0 nghĩa là đã cập nhật thành công
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    //Lấy danh sách đc giả hoạt động
    public List<UserDTO> LayDanhSachDocGiaHoatDong()
    {
        using (var conn = db.GetConnection())
        {
            string sql = "SELECT ID, HoTen FROM Users WHERE Role = 'DocGia' AND IsActive = 1 ORDER BY ID";
            return conn.Query<UserDTO>(sql).ToList();
        }
    }   

    //lấy mã vạch đang sẵn sàng để làm gợi ý
    public List<string> LayDanhSachMaVachSanSang()
    {
        List<string> list = new List<string>();
        using (var conn = db.GetConnection())
        {
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"
                    SELECT bc.CopyID
                    FROM BookCopies bc
                    JOIN BookTitles bt ON bc.TitleID = bt.TitleID
                    LEFT JOIN Categories c ON bt.CategoryID = c.CategoryID
                    WHERE bc.TrangThai = 0 AND bt.IsDeleted = 0 AND IFNULL(c.IsDeleted, 0) = 0
                    ORDER BY bc.CopyID";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
            }
        }

        return list;
    }

    // Tạo bảng tra cứu từ ID sang Họ tên cho toàn bộ tài khoản trong bảng Users
    public Dictionary<int, string> LayMapTenDocGia()
    {
        var map = new Dictionary<int, string>();
        using (var conn = db.GetConnection())
        {
            var list = conn.Query<UserDTO>("SELECT ID, HoTen FROM Users");
            foreach (var u in list)
            {
                map[u.ID] = u.HoTen ?? "Không rõ";
            }
        }

        return map;
    }

    // Tạo bảng tra cứu từ mã vạch CopyID sang Tên sách (không phân biệt chữ hoa hay thường)
    public Dictionary<string, string> LayMapTenSachTheoMaVach()
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (var conn = db.GetConnection())
        {
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                // Nối bảng BookCopies (viết tắt bc) với BookTitles (viết tắt bt) theo TitleID để lấy tên sách cho từng mã vạch
                // Dùng LEFT JOIN để luôn giữ lại mọi mã vạch bên BookCopies, kể cả khi đầu sách bên BookTitles không còn (lúc đó TenSach ra NULL)
                // Dùng IFNULL để bắt trường hợp NULL đó đổi thành "(Sách đã xóa)", rồi đặt tên cột kết quả là TenSach (AS TenSach)
                command.CommandText = @"
                    SELECT bc.CopyID, IFNULL(bt.TenSach, '(Sách đã xóa)') AS TenSach
                    FROM BookCopies bc
                    LEFT JOIN BookTitles bt ON bc.TitleID = bt.TitleID";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string copyId = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        string tenSach = reader.IsDBNull(1) ? "(Sách đã xóa)" : reader.GetString(1);
                        if (!string.IsNullOrEmpty(copyId))
                        {
                            map[copyId] = tenSach;
                        }
                    }
                }
            }
        }

        return map;
    }
}