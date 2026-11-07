namespace QuanLyThuVien.BLL;
using System;
using System.Collections.Generic;
using QuanLyThuVien.DTO;
using QuanLyThuVien.DAO;
public class BorrowRecordBLL
{
    private BorrowRecordDAO BorrowDao = new BorrowRecordDAO();

    public List<BorrowRecordDTO> LayDanhSachPhieuMuon()
    {
        return BorrowDao.GetAll();
    }
    
    //Chức năng mượn sách: kiểm tra 3 điều kiện chặn + trạng thái mã vạch CopyID
    public string MuonSach(int userId, string copyId, DateTime ngayMuon, DateTime ngayHanTra)
    {
        if (string.IsNullOrWhiteSpace(copyId))
        {
            return "Vui lòng nhập hoặc quét mã vạch sách ";
        }

        if (ngayHanTra.Date < ngayMuon.Date)
        {
            return "Ngày hạn trả không được nhỏ hơn ngày mượn";
        }
        
        //Điều kiện 1: Độc giả có bị khóa thẻ không?
        if (!BorrowDao.KiemTraDocGiaHoatDong(userId))
        {
            return "Độc giả đã tồn tại hoặc đã bị khóa thẻ";
        }
        
        //Đều kiện 2: Độc gi có đang nợ tiền phạt không?
        double tienNo = BorrowDao.TinhTongNoPhat(userId);
        if (tienNo > 0)
        {
            return "Độc giả đang nợ tiền phạt (" + tienNo+ " đ) chưa đóng";
        }
        
        //Điều kiện 3: Độc giả có đang mượn quá 3 cuốn không?
        int soSachDangMuon = BorrowDao.DemSoSachDangMuon(userId);
        if (soSachDangMuon >= 3)
        {
            return "Độc giả đang mượn đủ 3 cuốn, không được mượn thêm";
        }
        
        //Kiểm tra mã vạch CopyID trong bảng BookCopies
        int trangThaiSach = BorrowDao.LayTrangThaiCuonSach(copyId.Trim());
        if (trangThaiSach == -1)
        {
            return "Mã vạch sách không tồn tại ";
        }

        if (trangThaiSach == 1)
        {
            return "Cuốn sách này hiện đang có người mượn";
        }

        if (trangThaiSach == 2)
        {
            return "Cuốn sách này đã bị mất hoặc hỏng";
        }

        BorrowRecordDTO phieuMoi = new BorrowRecordDTO();
        phieuMoi.UserID = userId;
        phieuMoi.CopyID = copyId.Trim();
        phieuMoi.NgayMuon = ngayMuon.ToString("yyyy-MM-dd");
        phieuMoi.NgayHanTra = ngayHanTra.ToString("yyyy-MM-dd");
        phieuMoi.NgayTraThucTe = null;
        phieuMoi.TrangThai = 0;
        phieuMoi.TienPhat = 0;
        phieuMoi.TrangThaiThuTien = 0;

        bool ketqua = BorrowDao.InsertBorrowRecord(phieuMoi);
        if (ketqua)
        {
            return "Mượn sách thành công";
        }

        return "Lỗi khi lưu phiếu mượn ";
    }
    
    //Tính tiền phạt : trễ hạn = 5000đ/ ngày + cộng GiaTien (bảng BookTitles) nếu làm mất (trạng thái = 2)
    public double TinhTienPhat(string NgayHanTraStr, DateTime ngayTraThucTe, int trangThaiTra, string copyId)
    {
        double tienPhat = 0;
        DateTime ngayHanTra;
        if (DateTime.TryParse(NgayHanTraStr, out ngayHanTra))
        {
            int soNgayTre = (ngayTraThucTe.Date - ngayHanTra.Date).Days;
            if (soNgayTre > 0)
            {
                tienPhat += soNgayTre * 5000; // 5000đ/ngày trễ hạn
            }
        }
        
        //Nếu làm mất -> cộng thêm GiaTien vào TienPhat
        if (trangThaiTra == 2)
        {
            double giaSach = BorrowDao.LayGiaTienSach(copyId);
            tienPhat += giaSach;
        }

        return tienPhat;
    }
    
    //Chức năng Trả Sách (trangThaiTra = 1) , làm mất (trangThaiTra = 2)
    public string TraSachHoacBaoMat(int recordId, string copyId, string ngayHanTraStr, DateTime ngayTraThucTe,
        int trangThaiTra, bool daDongPhat)
    {
        double tienPhat = TinhTienPhat(ngayHanTraStr, ngayTraThucTe, trangThaiTra, copyId);
        
        //Nếu có phạt (tienPhat > 0): khách đóng tiền luôn thì TrangThaiThuTien = 1, nợ lại thì = 0
        //Nếu không bị phạt (tienPhat == 0): để mặc định = 0 (hoặc 1 nếu đã thu)
        int trangThaiThuTien = 0;
        if (tienPhat > 0 && daDongPhat)
        {
            trangThaiThuTien = 1;
        }

        string ngayTraStr = ngayTraThucTe.ToString("yyyy-MM-dd");
        bool ketqua =
            BorrowDao.UpdateBorrowRecord(recordId, copyId, ngayTraStr, trangThaiTra, tienPhat, trangThaiThuTien);

        if (ketqua)
        {
            if (tienPhat > 0)
            {
                return "Cập nhật thành công! Tổng tiền phạt: " + tienPhat + " đ";
            }
            return "Trả sách thành công";
        }
        return "Lỗi khi cập nhật phiếu mượn";
    }
    
    //Thu tiền phạt sau cho các phiếu đang nợ (TrangThaiThuTien = 0 -> 1)
    public bool DongTienPhat(int recordId)
    {
        return BorrowDao.UpdateTrangThaiThuTien(recordId);
    }
}