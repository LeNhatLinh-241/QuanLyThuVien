using System.Data;
using QuanLyThuVien.DAO;

namespace QuanLyThuVien.BLL;

public class ReportBLL
{

    private readonly ReportDAO _reportDAO = new();

    public ReportKpiResult LayTongQuan(DateTime tuNgay, DateTime denNgay)
    {
        ValidateDateRange(tuNgay, denNgay);
        return _reportDAO.GetKpiSummary(tuNgay, denNgay);
    }

    public List<(string Nhan, int SoLuot)> LayBieuDoTheoTuan(DateTime tuNgay, DateTime denNgay)
    {
        ValidateDateRange(tuNgay, denNgay);
        return _reportDAO.GetBorrowTrendByWeek(tuNgay, denNgay);
    }

    public DataTable LayDuLieuBang(int loaiBaoCaoIndex, DateTime tuNgay, DateTime denNgay)
    {
        ValidateDateRange(tuNgay, denNgay);
        return loaiBaoCaoIndex switch
        {
            0 => _reportDAO.GetTopBorrowedBooks(tuNgay, denNgay),
            1 => _reportDAO.GetCurrentlyBorrowedBooks(),
            2 => _reportDAO.GetOverdueUsers(),
            _ => _reportDAO.GetTopBorrowedBooks(tuNgay, denNgay)
        };
    }

    private static void ValidateDateRange(DateTime tuNgay, DateTime denNgay)
    {
        if (tuNgay.Date > denNgay.Date)
            throw new ArgumentException("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
    }
}