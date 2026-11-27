using System.Data;

namespace QuanLyThuVien.DAO;
public class ReportKpiResult
{
    public int TongBanSach { get; set; }
    public int TongDauSach { get; set; }
    public int BanSachSanCo { get; set; }
    public int TongDocGia { get; set; }
    public int TheHoatDong { get; set; }
    public int TheBiKhoa { get; set; }
    public int PhieuDangMuon { get; set; }
    public int PhieuQuaHan { get; set; }
    public int PhieuDaTraTrongKy { get; set; }
}

public class ReportDAO
{

    private readonly DatabaseConnect db = new DatabaseConnect();

    public ReportKpiResult GetKpiSummary(DateTime tuNgay, DateTime denNgay)
    {
        var kpi = new ReportKpiResult();
        string fromStr = tuNgay.ToString("yyyy-MM-dd");
        string toStr = denNgay.ToString("yyyy-MM-dd");

        using var conn = db.GetConnection();
        conn.Open();

        
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                SELECT 
                    (SELECT COUNT(*) FROM BookCopies bc INNER JOIN BookTitles bt ON bc.TitleID = bt.TitleID WHERE bt.IsDeleted = 0) AS TongBanSach,
                    (SELECT COUNT(*) FROM BookTitles WHERE IsDeleted = 0) AS TongDauSach,
                    (SELECT COUNT(*) FROM BookCopies bc INNER JOIN BookTitles bt ON bc.TitleID = bt.TitleID WHERE bt.IsDeleted = 0 AND bc.TrangThai = 0) AS BanSachSanCo;";
            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                kpi.TongBanSach = Convert.ToInt32(rd["TongBanSach"]);
                kpi.TongDauSach = Convert.ToInt32(rd["TongDauSach"]);
                kpi.BanSachSanCo = Convert.ToInt32(rd["BanSachSanCo"]);
            }
        }

        
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                SELECT 
                    COUNT(*) AS TongDocGia,
                    IFNULL(SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END), 0) AS TheHoatDong,
                    IFNULL(SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END), 0) AS TheBiKhoa
                FROM Users
                WHERE Role = 'DocGia';";
            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                kpi.TongDocGia = Convert.ToInt32(rd["TongDocGia"]);
                kpi.TheHoatDong = Convert.ToInt32(rd["TheHoatDong"]);
                kpi.TheBiKhoa = Convert.ToInt32(rd["TheBiKhoa"]);
            }
        }

        
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = @"
                SELECT 
                    IFNULL(SUM(CASE WHEN TrangThai = 0 THEN 1 ELSE 0 END), 0) AS DangMuon,
                    IFNULL(SUM(CASE WHEN TrangThai = 0 AND date(NgayHanTra) < date('now', 'localtime') THEN 1 ELSE 0 END), 0) AS QuaHan,
                    IFNULL(SUM(CASE WHEN TrangThai = 1 AND date(NgayMuon) BETWEEN @TuNgay AND @DenNgay THEN 1 ELSE 0 END), 0) AS DaTraKy
                FROM BorrowRecords;";
            cmd.Parameters.AddWithValue("@TuNgay", fromStr);
            cmd.Parameters.AddWithValue("@DenNgay", toStr);
            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                kpi.PhieuDangMuon = Convert.ToInt32(rd["DangMuon"]);
                kpi.PhieuQuaHan = Convert.ToInt32(rd["QuaHan"]);
                kpi.PhieuDaTraTrongKy = Convert.ToInt32(rd["DaTraKy"]);
            }
        }

        return kpi;
    }

    
    public List<(string Nhan, int SoLuot)> GetBorrowTrendByWeek(DateTime tuNgay, DateTime denNgay)
    {
        var result = new List<(string, int)>();
        using var conn = db.GetConnection();
        conn.Open();

        DateTime current = tuNgay.Date;
        while (current <= denNgay.Date)
        {
            DateTime weekEnd = current.AddDays(6);
            if (weekEnd > denNgay.Date) weekEnd = denNgay.Date;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*) FROM BorrowRecords 
                WHERE date(NgayMuon) BETWEEN @Start AND @End;";
            cmd.Parameters.AddWithValue("@Start", current.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@End", weekEnd.ToString("yyyy-MM-dd"));

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            result.Add(($"{current:dd/MM}-{weekEnd:dd/MM}", count));

            current = weekEnd.AddDays(1);
        }

        return result;
    }

    
    public DataTable GetTopBorrowedBooks(DateTime tuNgay, DateTime denNgay)
    {
        var dt = new DataTable();
        using var conn = db.GetConnection();
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT 
                bt.TitleID AS MaSach,
                bt.TenSach AS TenSach,
                COUNT(br.RecordID) AS LuotMuon,
                SUM(CASE WHEN br.TrangThai = 0 THEN 1 ELSE 0 END) AS ChuaTra
            FROM BorrowRecords br
            INNER JOIN BookCopies bc ON br.CopyID = bc.CopyID
            INNER JOIN BookTitles bt ON bc.TitleID = bt.TitleID
            WHERE date(br.NgayMuon) BETWEEN @TuNgay AND @DenNgay
            GROUP BY bt.TitleID, bt.TenSach
            ORDER BY LuotMuon DESC, ChuaTra DESC;";
        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@DenNgay", denNgay.ToString("yyyy-MM-dd"));

        using var reader = cmd.ExecuteReader();
        dt.Load(reader);
        return dt;
    }

    
    public DataTable GetCurrentlyBorrowedBooks()
    {
        var dt = new DataTable();
        using var conn = db.GetConnection();
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT 
                br.CopyID AS MaVach,
                bt.TenSach AS TenSach,
                u.HoTen AS DocGia,
                br.NgayHanTra AS HanTra
            FROM BorrowRecords br
            INNER JOIN BookCopies bc ON br.CopyID = bc.CopyID
            INNER JOIN BookTitles bt ON bc.TitleID = bt.TitleID
            INNER JOIN Users u ON br.UserID = u.ID
            WHERE br.TrangThai = 0
            ORDER BY br.NgayHanTra ASC;";

        using var reader = cmd.ExecuteReader();
        dt.Load(reader);
        return dt;
    }

    
    public DataTable GetOverdueUsers()
    {
        var dt = new DataTable();
        using var conn = db.GetConnection();
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT 
                u.HoTen AS DocGia,
                bt.TenSach AS TenSach,
                br.NgayHanTra AS HanTra,
                CAST(julianday(date('now', 'localtime')) - julianday(date(br.NgayHanTra)) AS INTEGER) AS SoNgayTre
            FROM BorrowRecords br
            INNER JOIN Users u ON br.UserID = u.ID
            INNER JOIN BookCopies bc ON br.CopyID = bc.CopyID
            INNER JOIN BookTitles bt ON bc.TitleID = bt.TitleID
            WHERE br.TrangThai = 0 AND date(br.NgayHanTra) < date('now', 'localtime')
            ORDER BY SoNgayTre DESC;";

        using var reader = cmd.ExecuteReader();
        dt.Load(reader);
        return dt;
    }
}