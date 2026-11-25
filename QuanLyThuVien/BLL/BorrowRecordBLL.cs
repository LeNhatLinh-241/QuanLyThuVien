namespace QuanLyThuVien.BLL;
using System;
using System.Collections.Generic;
using QuanLyThuVien.DTO;
using QuanLyThuVien.DAO;
public class BorrowRecordBLL
{
    // Khởi tạo lớp DAO để tầng BLL gọi xuống thao tác với cơ sở dữ liệu SQLite
    // Tầng giao diện (GUI) chỉ làm việc với lớp BLL này, không gọi thẳng xuống DAO
    private BorrowRecordDAO BorrowDao = new BorrowRecordDAO();
    
    // Lấy toàn bộ danh sách phiếu mượn từ DAO trả lên cho giao diện hiển thị
    public List<BorrowRecordDTO> LayDanhSachPhieuMuon()
    {
        return BorrowDao.GetAll();
    }
    
    // Xử lý nghiệp vụ cho mượn sách
    // Quy trình: kiểm tra đầu vào, kiểm tra 3 điều kiện của độc giả, kiểm tra tình trạng cuốn sách rồi mới tạo phiếu lưu xuống DB
    // Hàm trả về chuỗi thông báo để giao diện hiện thẳng lên MessageBox cho người dùng
    public string MuonSach(int userId, string copyId, DateTime ngayMuon, DateTime ngayHanTra)
    {   
        // Chặn trường hợp mã vạch bị rỗng hoặc chỉ toàn khoảng trắng
        if (string.IsNullOrWhiteSpace(copyId))
        {
            return "Vui lòng nhập hoặc quét mã vạch sách ";
        }
        
        // So sánh theo .Date để bỏ qua giờ phút giây, không cho hạn trả nhỏ hơn ngày mượn
        if (ngayHanTra.Date < ngayMuon.Date)
        {
            return "Ngày hạn trả không được nhỏ hơn ngày mượn";
        }
        
        // Điều kiện 1: kiểm tra độc giả có tồn tại và thẻ còn đang hoạt động hay đã bị khóa
        if (!BorrowDao.KiemTraDocGiaHoatDong(userId))
        {
            return "Độc giả không tồn tại hoặc đã bị khóa thẻ";
        }
        
        // Điều kiện 2: tính tổng tiền phạt chưa đóng của độc giả, nếu lớn hơn 0 thì chặn không cho mượn thêm
        double tienNo = BorrowDao.TinhTongNoPhat(userId);
        if (tienNo > 0)
        {
            return "Độc giả đang nợ tiền phạt (" + tienNo.ToString("N0")+ " đ) chưa đóng";
        }
        
        //Điều kiện 3: Độc giả có đang mượn quá 3 cuốn không?
        int soSachDangMuon = BorrowDao.DemSoSachDangMuon(userId);
        if (soSachDangMuon >= 3)
        {
            return "Độc giả đang mượn đủ 3 cuốn, không được mượn thêm";
        }
        
        // Kiểm tra tình trạng cuốn sách thực tế trong bảng BookCopies theo mã vạch, Trim() để cắt khoảng trắng thừa
        // Quy ước trạng thái bản sao sách: -1 là không tìm thấy, 0 là đang rảnh, 1 là đang có người mượn, 2 là đã mất hoặc hỏng
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
        
        // Qua hết các bước kiểm tra thì gom thông tin vào đối tượng BorrowRecordDTO để gửi xuống DAO lưu mới
        BorrowRecordDTO phieuMoi = new BorrowRecordDTO();
        phieuMoi.UserID = userId;
        phieuMoi.CopyID = copyId.Trim();
        
        // Lưu ngày theo chuẩn yyyy-MM-dd để đồng bộ trong SQLite và dễ sắp xếp, so sánh sau này
        phieuMoi.NgayMuon = ngayMuon.ToString("yyyy-MM-dd");
        phieuMoi.NgayHanTra = ngayHanTra.ToString("yyyy-MM-dd");
        phieuMoi.NgayTraThucTe = null;  // mới mượn nên chưa có ngày trả
        phieuMoi.TrangThai = 0;         // mã 0 tương ứng trạng thái Đang mượn
        phieuMoi.TienPhat = 0;          // mới mượn chưa phát sinh tiền phạt
        phieuMoi.TrangThaiThuTien = 0;  // chưa thu tiền phạt
        
        // Gọi DAO để vừa thêm phiếu mới vừa đổi trạng thái cuốn sách trong BookCopies sang 1 (đang mượn)
        bool ketqua = BorrowDao.InsertBorrowRecord(phieuMoi);
        if (ketqua)
        {
            return "Mượn sách thành công";
        }

        return "Lỗi khi lưu phiếu mượn ";
    }
    
    // Tính tổng tiền phạt dự kiến hoặc chốt sổ cho một phiếu mượn
    // mỗi ngày trễ hạn phạt 5.000 đ, nếu báo làm mất (mã 2) thì cộng thêm giá bìa cuốn sách lấy từ bảng BookTitles
    public double TinhTienPhat(string NgayHanTraStr, DateTime ngayTraThucTe, int trangThaiTra, string copyId)
    {
        double tienPhat = 0;
        DateTime ngayHanTra;
        
        // Đổi chuỗi ngày hạn trả từ DB sang kiểu DateTime an toàn
        if (DateTime.TryParse(NgayHanTraStr, out ngayHanTra))
        {
            // Trừ hai mốc ngày theo .Date để lấy đúng số ngày chênh lệch (không bị lệch bởi giờ phút)
            int soNgayTre = (ngayTraThucTe.Date - ngayHanTra.Date).Days;
            if (soNgayTre > 0)
            {
                tienPhat += soNgayTre * 5000; // 5000đ/ngày trễ hạn
            }
        }
        
        // Nếu hình thức xử lý là làm mất sách (mã 2) thì tra giá bìa của cuốn sách đó trong DB để cộng đền bù
        if (trangThaiTra == 2)
        {
            double giaSach = BorrowDao.LayGiaTienSach(copyId);
            tienPhat += giaSach;
        }

        return tienPhat;
    }
    
    // Xử lý khi độc giả mang sách đến trả (trangThaiTra bằng 1) hoặc báo làm mất sách (trangThaiTra bằng 2)
    // Tham số daDongPhat cho biết khách có đóng tiền phạt ngay tại quầy hay ghi nợ lại
    public string TraSachHoacBaoMat(int recordId, string copyId, string ngayHanTraStr, DateTime ngayTraThucTe,
        int trangThaiTra, bool daDongPhat)
    {
        
        // Gọi lại hàm TinhTienPhat để tính chốt số tiền phạt cuối cùng cần lưu vào phiếu
        double tienPhat = TinhTienPhat(ngayHanTraStr, ngayTraThucTe, trangThaiTra, copyId);
        
        //Nếu có phạt (tienPhat > 0): khách đóng tiền luôn thì TrangThaiThuTien = 1, nợ lại thì = 0
        //Nếu không bị phạt (tienPhat == 0): để mặc định = 0 (hoặc 1 nếu đã thu)
        int trangThaiThuTien = 0;
        if (tienPhat > 0 && daDongPhat)
        {
            trangThaiThuTien = 1;
        }
        // Đổi ngày trả sang chuỗi chuẩn yyyy-MM-dd trước khi lưu vào SQLite
        string ngayTraStr = ngayTraThucTe.ToString("yyyy-MM-dd");
        
        // Gọi DAO cập nhật phiếu mượn và đồng thời cập nhật lại trạng thái cuốn sách trong kho
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
    
    // Dùng cho trường hợp độc giả ghi nợ phạt trước đó rồi quay lại đóng tiền sau (đổi TrangThaiThuTien từ 0 sang 1)
    public bool DongTienPhat(int recordId)
    {
        return BorrowDao.UpdateTrangThaiThuTien(recordId);
    }
    
    //Lấy danh sách độc giả đang hoạt động
    public List<UserDTO> LayDanhSachDocGia()
    {
        return BorrowDao.LayDanhSachDocGiaHoatDong();
    }
    
    // Lấy danh sách mã vạch các cuốn sách đang rảnh (trạng thái bằng 0) để gợi ý gõ nhanh trên ô mã vạch
    public List<string> LayDanhSachMaVachSanSang()
    {
        return BorrowDao.LayDanhSachMaVachSanSang();
    }
    
    // Lấy bảng tra cứu từ mã độc giả (ID) sang họ tên để giao diện đổi số ID thành tên trên bảng
    public Dictionary<int, string> LayMapTenDocGia()
    {
        return BorrowDao.LayMapTenDocGia();
    }
    // Lấy bảng tra cứu từ mã vạch (CopyID) sang tên đầu sách để giao diện hiển thị tên sách và hỗ trợ tìm kiếm
    public Dictionary<string, string> LayMapTenSachTheoMaVach()
    {
        return BorrowDao.LayMapTenSachTheoMaVach();
    }
}