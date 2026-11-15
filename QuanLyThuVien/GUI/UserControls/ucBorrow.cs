using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
namespace QuanLyThuVien.GUI.UserControls;

public partial class ucBorrow : UserControl
{
    private BorrowRecordBLL borrowBll = new BorrowRecordBLL();
    private string ngayHanTraTam = "";
    public ucBorrow()
    {
        InitializeComponent();
    }

    private void ucBorrow_Load(object sender, EventArgs e)
    {
        dtpNgayMuon.Value = DateTime.Now;
        dtpNgayHanTra.Value = DateTime.Now.AddDays(7);
        dtpNgayTra.Value = DateTime.Now;

        //tạo dữ liệu giả cho combobox để test UI
        List<UserDTO> listDocGiaTam = new List<UserDTO>();
        listDocGiaTam.Add(new UserDTO {ID = 1, HoTen = "DG001 Nguyễn Minh Anh"});

    listDocGiaTam.Add(new UserDTO{ID = 2, HoTen = "DG002 Trần Quốc Bảo"});
        listDocGiaTam.Add(new UserDTO{ID = 1, HoTen = "DG003 Lê Thu Hà"});

        cboDocGia.DataSource = listDocGiaTam;
        cboDocGia.DisplayMember = "HoTen";
        cboDocGia.ValueMember = "ID";

        cboTinhTrangSach.Items.Clear();
        cboTinhTrangSach.Items.Add("Trả sách");
        cboTinhTrangSach.Items.Add("Làm mất sách");
        cboTinhTrangSach.SelectedIndex = 0;

        cboLocTrangThai.Items.Clear();
        cboLocTrangThai.Items.Add("Tất cả trạng thái");
        cboLocTrangThai.Items.Add("Đang mượn");
        cboLocTrangThai.Items.Add("Đã trả");
        cboLocTrangThai.Items.Add("Làm mất");
        cboLocTrangThai.SelectedIndex = 0;

        dgvDanhSach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

        LoadDanhSachPhieuMuon();
        
    }
    
    //Đổi tên cột snag tiếng việt cho dễ nhìn
    private void DinhDangCotBang()
    {
        if (dgvDanhSach.Columns.Count > 0)
        {
            if (dgvDanhSach.Columns["RecordID"] != null) dgvDanhSach.Columns["RecordID"].HeaderText = "Mã Phiếu";
            if (dgvDanhSach.Columns["UserID"] != null) dgvDanhSach.Columns["UserID"].HeaderText = "Độc Giả";
            if (dgvDanhSach.Columns["CopyID"] != null) dgvDanhSach.Columns["CopyID"].HeaderText = "Sách Mượn";
            if (dgvDanhSach.Columns["NgayMuon"] != null) dgvDanhSach.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
            if (dgvDanhSach.Columns["NgayHanTra"] != null) dgvDanhSach.Columns["NgayHanTra"].HeaderText = "Hạn Trả";
            if (dgvDanhSach.Columns["NgayTraThucTe"] != null) dgvDanhSach.Columns["NgayTraThucTe"].HeaderText = "Ngày Trả";
            if (dgvDanhSach.Columns["TrangThai"] != null) dgvDanhSach.Columns["TrangThai"].HeaderText = "Trạng Thái";
            if (dgvDanhSach.Columns["TienPhat"] != null) dgvDanhSach.Columns["TienPhat"].HeaderText = "Tiền Phạt";
            if (dgvDanhSach.Columns["TrangThaiThuTien"] != null) dgvDanhSach.Columns["TrangThaiThuTien"].Visible = false;
        }
    }
    
    //Tải danh sách phiếu mượn và thống kế số lượng
    private void LoadDanhSachPhieuMuon()
    {
        try
        {
            List<BorrowRecordDTO> list = borrowBll.LayDanhSachPhieuMuon();
            dgvDanhSach.DataSource = null;
            dgvDanhSach.DataSource = list;
            DinhDangCotBang();
            // Đổi tên tiêu đề cột sang tiếng Việt giống thiết kế Figma
            if (dgvDanhSach.Columns.Count > 0)
            {
                if (dgvDanhSach.Columns["RecordID"] != null) dgvDanhSach.Columns["RecordID"].HeaderText = "Mã Phiếu";
                if (dgvDanhSach.Columns["UserID"] != null) dgvDanhSach.Columns["UserID"].HeaderText = "Độc Giả";
                if (dgvDanhSach.Columns["CopyID"] != null) dgvDanhSach.Columns["CopyID"].HeaderText = "Sách Mượn";
                if (dgvDanhSach.Columns["NgayMuon"] != null) dgvDanhSach.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
                if (dgvDanhSach.Columns["NgayHanTra"] != null) dgvDanhSach.Columns["NgayHanTra"].HeaderText = "Hạn Trả";
                if (dgvDanhSach.Columns["NgayTraThucTe"] != null) dgvDanhSach.Columns["NgayTraThucTe"].HeaderText = "Ngày Trả";
                if (dgvDanhSach.Columns["TienPhat"] != null) dgvDanhSach.Columns["TienPhat"].HeaderText = "Tiền Phạt";
                if (dgvDanhSach.Columns["TrangThai"] != null) dgvDanhSach.Columns["TrangThai"].HeaderText = "Trạng Thái";
                if (dgvDanhSach.Columns["TrangThaiThuTien"] != null) dgvDanhSach.Columns["TrangThaiThuTien"].Visible = false;
            }
            int dangMuon = 0;
            int quaHan = 0;
            foreach (BorrowRecordDTO item in list)
            {
                if (item.TrangThai == 0)
                {
                    dangMuon++;
                    DateTime hanTra;
                    if (DateTime.TryParse(item.NgayHanTra, out hanTra) && hanTra.Date < DateTime.Now.Date)
                    {
                        quaHan++;
                    }
                }
            }

            lblThongKeNho.Text = dangMuon + " phiếu đang mượn " + quaHan + "quá hạn";
            lblChanTrang.Text = "Hiển thị " + list.Count + "phiếu | đã chọn: " +
                                (string.IsNullOrEmpty(txtMaPhieu.Text) ? "Chưa chọn " : txtMaPhieu.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("không tải được danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
    
    //Đếm số sách đang mượn và tính nợ phạt của độc giả đang chọn
    private void cboDocGia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDocGia.SelectedValue == null || !(cboDocGia.SelectedValue is int))
        {
            return;
        }

        int userId = Convert.ToInt32(cboDocGia.SelectedValue);
        List<BorrowRecordDTO> list = borrowBll.LayDanhSachPhieuMuon();
        int soSachDangMuon = 0;
        double noPhat = 0;

        foreach (BorrowRecordDTO item in list)
        {
            
            //Chỉ đếm sách của user này
            if (item.UserID == userId)
            {
                if (item.TrangThai == 0)
                {
                    soSachDangMuon++;
                }

                if (item.TienPhat > 0 && item.TrangThaiThuTien == 0)
                {
                    noPhat += item.TienPhat;
                }
            }
        }

        lblTrangThaiMuon.Text = "Đang Mượn: " + soSachDangMuon + "/3 cuốn | Nợ Phạt: " + noPhat.ToString(
            "0" + "đ | Thẻ: Hợp lệ");
    }
    
    //Xử lý mượn sách
    private void btnChoMuon_Click(object sender, EventArgs e)
    {
        if (cboDocGia.SelectedValue == null)
        {
            MessageBox.Show("Vui lòng chọn độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int userId = Convert.ToInt32(cboDocGia.SelectedValue);
        string copyId = txtMaVach.Text.Trim();
        
        string thongBao = borrowBll.MuonSach(userId, copyId, dtpNgayMuon.Value, dtpNgayHanTra.Value);

        if (thongBao.Contains("Thành công"))
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMaVach.Clear();
            LoadDanhSachPhieuMuon();
            cboDocGia_SelectedIndexChanged(null, null);
        }
        else
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    
    //Click chọn dòng trên bảng để đổ sang khung trả sách bên trái
    private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvDanhSach.CurrentRow == null)
        {
            return;
        }

        DataGridViewRow row = dgvDanhSach.CurrentRow;
        txtMaPhieu.Text = row.Cells["RecordID"].Value?.ToString() ?? "";
        txtMaVachTra.Text = row.Cells["CopyID"].Value?.ToString() ?? "";
        ngayHanTraTam = row.Cells["NgayHanTra"].Value?.ToString() ?? "";

        lblChanTrang.Text = "Hiển thị " + dgvDanhSach.Rows.Count + " phiếu | Đã chọn: #" + txtMaPhieu.Text;
        CapNhatTienPhat(null, null);
    }
    
    //tính tiền phạt dự kiến khi chọn ngày trả hoặc đổi giữa Trả sách / làm mất
    private void CapNhatTienPhat(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMaPhieu.Text) || string.IsNullOrEmpty(ngayHanTraTam))
        {
            return;
        }

        int trangThaiTra = (cboTinhTrangSach.SelectedIndex == 0) ? 1 : 2;
        double tienPhat = borrowBll.TinhTienPhat(ngayHanTraTam, dtpNgayTra.Value, trangThaiTra, txtMaVachTra.Text);
        lblTongTienPhat.Text = "Tiền phạt: " + tienPhat.ToString("NO") + " đ";
    }
     
    //bấm nút nhận trả sách
    private void btnNhanTraSach_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMaPhieu.Text))
        {
            MessageBox.Show("Chọn một phiếu mượn bên bảng trước khi trả!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        int recordId = Convert.ToInt32(txtMaPhieu.Text);
        string copyId = txtMaVachTra.Text;
        int trangThaiTra = (cboTinhTrangSach.SelectedIndex == 0) ? 1 : 2;
        
        //mặc định khi nhận trả sách có thu tiền phạt tại quầy: daDongPhat = true
        string thongBao =
            borrowBll.TraSachHoacBaoMat(recordId, copyId, ngayHanTraTam, dtpNgayTra.Value, trangThaiTra, true);
        if (thongBao.Contains("Thành công"))
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachPhieuMuon();
            cboDocGia_SelectedIndexChanged(null, null);
        }
        else
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    //Tô màu nền và format lại chữ hiển thị trên DataGridView
    private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        string colName = dgvDanhSach.Columns[e.ColumnIndex].Name;

        if (colName == "RecordID" && e.Value != null)
        {
            if (int.TryParse(e.Value.ToString(), out int id))
            {
                e.Value = "PM" + id.ToString("D3");
                e.FormattingApplied = true;
            }

            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
        }

        if (colName == "NgayTraThucTe" && (e.Value == null || string.IsNullOrEmpty(e.Value.ToString())))
        {
            e.Value = "-";
            e.FormattingApplied = true;
        }

        if (colName == "TrangThai" && e.Value != null)
        {
            int trangThai = Convert.ToInt32(e.Value);
            string hanTraStr = dgvDanhSach.Rows[e.RowIndex].Cells["NgayHanTra"].Value?.ToString() ?? "";

            if (trangThai == 0)
            {
                if (DateTime.TryParse(hanTraStr, out DateTime hanTra) && hanTra.Date < DateTime.Now.Date)
                {
                    int soNgayTre = (DateTime.Now.Date - hanTra.Date).Days;
                    e.Value = "Quá hạn " + soNgayTre + " ngày";
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
                }
                else
                {
                    e.Value = "Đang mượn";
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(100, 83, 9);
                }
            }
            else if (trangThai == 1)
            {
                e.Value = "Đã Trả";
                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(220, 252, 231);
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            }
            else if(trangThai ==2)
            {
                e.Value = "Làm mất";
                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            }
            e.FormattingApplied = true;
        }
    }
    
    //bấm nút tìm kiếm và lọc danh sách
    private void btnTimKiem_Click(object sender, EventArgs e)
    {
        string tuKhoa = txtTimKiem.Text.Trim().ToLower();
        int trangThaiLoc = cboLocTrangThai.SelectedIndex - 1;

        List<BorrowRecordDTO> dsGoc = borrowBll.LayDanhSachPhieuMuon();
        List<BorrowRecordDTO> dsLoc = new List<BorrowRecordDTO>();

        foreach (BorrowRecordDTO item in dsGoc)
        {
            bool trungTuKhoa = string.IsNullOrEmpty(tuKhoa) || item.RecordID.ToString().Contains(tuKhoa) ||
                               item.UserID.ToString().Contains(tuKhoa) ||
                               (item.CopyID != null && item.CopyID.ToLower().Contains(tuKhoa));
        

            bool trungTrangThai = (trangThaiLoc == -1) || (item.TrangThai == trangThaiLoc);

            if (trungTuKhoa && trungTrangThai)
            {
                dsLoc.Add(item);
            }
        }
        dgvDanhSach.DataSource = null;
        dgvDanhSach.DataSource = dsLoc;
        DinhDangCotBang();
        lblChanTrang.Text = "Hiển thị " + dsLoc.Count + " / " + dsGoc.Count + " phiếu";
    }
}