using QuanLyThuVien.DTO;
namespace QuanLyThuVien.DAO;

public class BorrowRecordDAO
{
    private DatabaseConnect db = new DatabaseConnect();
    
    //Lấy toàn b danh sách phiếu mượn trả theo đúng 9 cột của Bảng BorrowRecords
    public List<BorrowRecordDTO> GetAll()
    {
        List<BorrowRecordDTO> list = new List<BorrowRecordDTO>();
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                     SELECT RecordID, UserID, CopyID, NgayMuon, NgayHanTra, NgayTraThucTe, TrangThai, TienPhat, TrangThaiThuTien
                     From BorrowRecords
                     Order BY RecordID DESC";

                using (var reader = command.ExecuteReader())
                {
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
    
    //Điều kiện 1: kiểm tra bảng Users (ID tồn tại và IsActive = 1)
    public bool KiemTraDocGiaHoatDong(int userId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT IsActive From Users WHERE ID = @userId";
                command.Parameters.AddWithValue("@userId", userId);
                var result = command.ExecuteScalar();
                if (result != null && Convert.ToInt32(result) == 1)
                {
                    return true; // người dùng tồn tại và đang hoạt động
                }

                return false; // người dùng không tồn tại hoặc không hoạt động
            }
        }
    }
    
    // Điều kiện 2: kiểm tra bảng BorrowRecords (Có nợ TienPhat > 0 mà TrangThaiThuTien = 0 không)
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
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDouble(result); // trả về tổng số tiền phạt
                }

                return 0;
            }
        }
    }
    
    //Điều kiện 3: đếm số sách đang muonjwt trong BorrowRecords
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
    //Lấy cột GiaTien từ bảng BookTitles thông qua mã vạch CopyID ( dùng khi làm mất sách)
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
    
    //Mượn sách: Thêm vào BorrowRecords và đổi BookCopies.TrangThai = 1(Đang bị mượn)
    public bool InsertBorrowRecord(BorrowRecordDTO phieu)
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
                        //thêm phiếu mượn vào bảng BorrowRecords
                        //Thiết lập transaction cho command
                        command.Transaction = transaction;
                        command.CommandText = @"
                            INSERT INTO BorrowRecords (UserID, CopyID, NgayMuon, NgayHanTra, NgayTraThucTe, TrangThai, TienPhat, TrangThaiThuTien)
                            VALUES (@userId, @copyId, @ngayMuon, @ngayHanTra, NULL, 0, 0, 0)";
                        command.Parameters.AddWithValue("@userId", phieu.UserID);
                        command.Parameters.AddWithValue("@copyId", phieu.CopyID);
                        command.Parameters.AddWithValue("@ngayMuon", phieu.NgayMuon);
                        command.Parameters.AddWithValue("@ngayHanTra", phieu.NgayHanTra);
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                        command.CommandText = "UPDATE BookCopies SET TrangThai = 1 WHERE CopyID = @copyId";
                        command.Parameters.AddWithValue("@copyId", phieu.CopyID);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback(); //nếu có lỗi xảy ra, rollback transaction để đảm bảo dữ liệu không bị thay đổi
                    return false; // trả về false nếu có lỗi xảy ra
                }
            }
        }
    }
    
    //Tra sách / báo mất: cập nhật BorrowRecords và đổi BookCopies.TrangThai(0 = rảnh, 2 = mất/hỏng)
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

                        //Nếu chọn "ĐÃ TRẢ" (1) -> BookCopies.TrangThai = 0(Rảnh)
                        //Nếu chọn "Mất/ hỏng (2) -> BookCopies.TrangThai = 2(Mất/ hỏng)
                        int trangThaiCopyMoi = (trangThaiPhieu == 1) ? 0 : 2;

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
    
    //cập nhật TrangThaiThuTien = 1 khi độc giả đóng phạt
    public bool UpdateTrangThaiThuTien(int recordId)
    {
        using (var connection = db.GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE BorrowRecords SET TrangThaiThuTien = 1 WHERE RecordID = @recordId";
                command.Parameters.AddWithValue("@recordId", recordId);
                return command.ExecuteNonQuery() > 0; // trả về true nếu có ít nhất 1 hàng bị ảnh hưởng
            }
        }
    }
}