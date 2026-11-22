using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
namespace QuanLyThuVien.GUI.UserControls;

public partial class ucBorrow : UserControl
{
    private BorrowRecordBLL borrowBll = new BorrowRecordBLL();
    private Dictionary<int, string> mapTenDocGia = new Dictionary<int, string>();
    private Dictionary<string, string> mapTenSach = new  Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    
    private string ngayHanTraTam = "";
    private string ngayMuonTam = "";
    private int trangThaiPhieuDangChon = -1;
    private bool daTaiLanDau = false;
    public ucBorrow()
    {
        InitializeComponent();
        
        //Tự tải lại dữ liệu mỗi khi chuyển tab quay lại màn hình này
        this.VisibleChanged += ucBorrow_VisibleChanged;
        
        //Hỗ trợ bấm Enter ở ô quét mã vạch và ô tìm kiếm
        txtMaVach.KeyDown += txtMaVach_KeyDown;
        txtTimKiem.KeyDown += txtTimKiem_KeyDown;
    }

    private void ucBorrow_Load(object sender, EventArgs e)
    {
        dtpNgayMuon.Value = DateTime.Now;
        dtpNgayHanTra.Value = DateTime.Now.AddDays(7);
        dtpNgayTra.Value = DateTime.Now;

      //  //tạo dữ liệu giả cho combobox để test UI
      //  List<UserDTO> listDocGiaTam = new List<UserDTO>();
      //  listDocGiaTam.Add(new UserDTO {ID = 1, HoTen = "DG001 Nguyễn Minh Anh"});

       // listDocGiaTam.Add(new UserDTO{ID = 2, HoTen = "DG002 Trần Quốc Bảo"});
       // listDocGiaTam.Add(new UserDTO{ID = 1, HoTen = "DG003 Lê Thu Hà"});

       // cboDocGia.DataSource = listDocGiaTam;
       // cboDocGia.DisplayMember = "HoTen";
       // cboDocGia.ValueMember = "ID";

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
        
        cboDocGia.DropDownStyle = ComboBoxStyle.DropDown;
        cboDocGia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cboDocGia.AutoCompleteSource = AutoCompleteSource.ListItems;
        //bật gợi ý mã vạch tự động
        txtMaVach.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txtMaVach.AutoCompleteSource = AutoCompleteSource.CustomSource;
        
        //LoadDanhSachPhieuMuon();
        ReloadData();
        daTaiLanDau = true;
    }

    private void ucBorrow_VisibleChanged(object sender, EventArgs e)
    {
        if (daTaiLanDau && this.Visible && !this.DesignMode)
        {
            ReloadData();
        }
    }
    
    //Gom các hàm nạp dữ liệu để dùng chung lúc mở form và lúc chuyển tab
    public void ReloadData()
    {
        LoadMapTraCuu();
        LoadDanhSachDocGia();
        LoadGoiYMaVach();
        LoadDanhSachPhieuMuon();
    }
    
    //Nạp sẵn tên độc giả và tên sách vào Dictionary để hiện thị và tìm kiếm
    private void LoadMapTraCuu()
    {
        try
        {
            mapTenDocGia = borrowBll.LayMapTenDocGia();
            mapTenSach = borrowBll.LayMapTenSachTheoMaVach();
        }
        catch
        {
            //Bỏ qua nếu chưa tải được map
        }
    }
    
    //Nạp danh sách độc giả thật từ SQLite vào combobox
    private void LoadDanhSachDocGia()
    {
        try
        {
            object dangChon = cboDocGia.SelectedValue;
            List<UserDTO> dsDocGia = borrowBll.LayDanhSachDocGia();
            cboDocGia.DataSource = null;
            cboDocGia.DisplayMember = "HoTen";
            cboDocGia.ValueMember = "ID";
            cboDocGia.DataSource = dsDocGia;
            if (dangChon is int id && dsDocGia.Exists(u => u.ID == id))
            {
                cboDocGia.SelectedValue = id;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Không tải được danh sách độc giả: " + e.Message, "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
    
    //Nạp các mã vạch đang rảnh vào gợi ý AutoComplete
    private void LoadGoiYMaVach()
    {
        try
        {
            List<string> dsMaVach = borrowBll.LayDanhSachMaVachSanSang();
            AutoCompleteStringCollection nguonGoiY = new AutoCompleteStringCollection();
            nguonGoiY.AddRange(dsMaVach.ToArray());
            txtMaVach.AutoCompleteCustomSource = nguonGoiY;
        }
        catch
        {
            //Bỏ qua nếu kho chưa có sách
        }
    }

    private void ResetKhungTraSach()
    {
        txtMaPhieu.Clear();
        txtMaVachTra.Clear();
        ngayMuonTam = "";
        ngayHanTraTam = "";
        trangThaiPhieuDangChon = -1;
        cboTinhTrangSach.SelectedIndex = 0;
        dtpNgayTra.Value = DateTime.Now;
        lblTongTienPhat.Text = "Tiền phạt: 0 đ";
        btnNhanTraSach.Enabled = true;
        btnNhanTraSach.BackColor = Color.FromArgb(22, 163, 74);
    }
    private void txtMaVach_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            btnChoMuon_Click(sender, e);
        }
    }
    private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            btnTimKiem_Click(sender, e);
        }
    }

    //Đổi tên cột snag tiếng việt cho dễ nhìn
    private void DinhDangCotBang()
    {
        if (dgvDanhSach.Columns.Count > 0)
        {
            if (dgvDanhSach.Columns["RecordID"] != null)
            {
                dgvDanhSach.Columns["RecordID"].HeaderText = "Mã Phiếu";
                dgvDanhSach.Columns["RecordID"].FillWeight = 75;
            }
            if (dgvDanhSach.Columns["UserID"] != null)
            {
                dgvDanhSach.Columns["UserID"].HeaderText = "Độc Giả";
                dgvDanhSach.Columns["UserID"].FillWeight = 115;
            }
            if (dgvDanhSach.Columns["CopyID"] != null)
            {
                dgvDanhSach.Columns["CopyID"].HeaderText = "Sách Mượn";
                dgvDanhSach.Columns["CopyID"].FillWeight = 145;
            }
            if (dgvDanhSach.Columns["NgayMuon"] != null)
            {
                dgvDanhSach.Columns["NgayMuon"].HeaderText = "Ngày Mượn";
                dgvDanhSach.Columns["NgayMuon"].FillWeight = 90;
            }
            if (dgvDanhSach.Columns["NgayHanTra"] != null)
            {
                dgvDanhSach.Columns["NgayHanTra"].HeaderText = "Hạn Trả";
                dgvDanhSach.Columns["NgayHanTra"].FillWeight = 85;
            }
            if (dgvDanhSach.Columns["NgayTraThucTe"] != null)
            {
                dgvDanhSach.Columns["NgayTraThucTe"].HeaderText = "Ngày Trả";
                dgvDanhSach.Columns["NgayTraThucTe"].FillWeight = 85;
            }
            if (dgvDanhSach.Columns["TrangThai"] != null)
            {
                dgvDanhSach.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvDanhSach.Columns["TrangThai"].FillWeight = 100;
            }
            if (dgvDanhSach.Columns["TienPhat"] != null)
            {
                dgvDanhSach.Columns["TienPhat"].HeaderText = "Tiền Phạt";
                dgvDanhSach.Columns["TienPhat"].FillWeight = 85;
            }
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

            lblThongKeNho.Text = dangMuon + " phiếu đang mượn " + quaHan + " quá hạn";
            lblChanTrang.Text = "Hiển thị " + list.Count + "phiếu | đã chọn: " +
                                (string.IsNullOrEmpty(txtMaPhieu.Text) ? "Chưa chọn " : "PM" + txtMaPhieu.Text.PadLeft(3, '0'));
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
            "N0") + "đ | Thẻ: Hợp lệ";
    }
    
    //Xử lý mượn sách
    private void btnChoMuon_Click(object sender, EventArgs e)
    {
        if (cboDocGia.SelectedValue == null)
        {
            MessageBox.Show("Vui lòng chọn độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dtpNgayHanTra.Value.Date < dtpNgayMuon.Value.Date)
        {
            MessageBox.Show("Ngày hạn trả không được nhỏ hơn ngày mượn ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        int userId = Convert.ToInt32(cboDocGia.SelectedValue);
        string copyId = txtMaVach.Text.Trim();

        if (string.IsNullOrEmpty(copyId))
        {
            MessageBox.Show("Vui lòng nhập hoặc quét mã vạch sách", "THÔNG BÁO", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtMaVach.Focus();
            return;
        }

        string thongBao = borrowBll.MuonSach(userId, copyId, dtpNgayMuon.Value, dtpNgayHanTra.Value);

        if (thongBao.Contains("Thành công", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMaVach.Clear();
            LoadDanhSachPhieuMuon();
            LoadMapTraCuu();
            LoadGoiYMaVach();
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
        ngayMuonTam = row.Cells["NgayMuon"].Value?.ToString() ?? "";
        trangThaiPhieuDangChon = Convert.ToInt32(row.Cells["TrangThai"].Value ?? -1);
        
        //Khoá nút nếu phiếu đã trả hoặc báo mất
        bool dangMuon = (trangThaiPhieuDangChon == 0);
        btnNhanTraSach.Enabled = dangMuon;
        btnNhanTraSach.BackColor = dangMuon ? Color.FromArgb(22, 163, 74) : Color.FromArgb(148, 163, 184);
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

        if (trangThaiPhieuDangChon != 0)
        {
            lblTongTienPhat.Text = "Phiếu này đã xử lý xong";
            return;
        }
        int trangThaiTra = (cboTinhTrangSach.SelectedIndex == 0) ? 1 : 2;
        double tienPhat = borrowBll.TinhTienPhat(ngayHanTraTam, dtpNgayTra.Value, trangThaiTra, txtMaVachTra.Text);
        lblTongTienPhat.Text = "Tiền phạt: " + tienPhat.ToString("N0") + " đ";
    }
     
    //bấm nút nhận trả sách
    private void btnNhanTraSach_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(txtMaPhieu.Text.Trim(), out int recordId))
        {
            MessageBox.Show("Chọn một phiếu mượn bên bảng trước khi trả!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (trangThaiPhieuDangChon != 0)
        {
            MessageBox.Show("Phiếu này ã được trả hoặc báo amats rồi", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (DateTime.TryParse(ngayMuonTam, out DateTime ngayMuon) && dtpNgayTra.Value < ngayMuon.Date)
        {
            MessageBox.Show("Ngày trả không được nhỏ hơn ngày mượn", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }
        string copyId = txtMaVachTra.Text;
        int trangThaiTra = (cboTinhTrangSach.SelectedIndex == 0) ? 1 : 2;
        
        //mặc định khi nhận trả sách có thu tiền phạt tại quầy: daDongPhat = true
        string thongBao =
            borrowBll.TraSachHoacBaoMat(recordId, copyId, ngayHanTraTam, dtpNgayTra.Value, trangThaiTra, true);
        if (thongBao.Contains("Thành công", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetKhungTraSach();
            LoadGoiYMaVach();
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
        if (colName == "UserID" && e.Value != null)
        {
            if (int.TryParse(e.Value.ToString(), out int userId) && mapTenDocGia.TryGetValue(userId, out string tenDocGia))
            {
                e.Value = tenDocGia;
                e.FormattingApplied = true;
            }
        }
        if (colName == "CopyID" && e.Value != null)
        {
            string copyId = e.Value.ToString() ?? "";
            string tenSach = mapTenSach.TryGetValue(copyId, out string ten) ? ten : "(Sách đã xóa)";
            e.Value = copyId + " · " + tenSach;
            e.FormattingApplied = true;
        }
        if (colName == "TienPhat" && e.Value != null)
        {
            if (double.TryParse(e.Value.ToString(), out double tien))
            {
                e.Value = tien.ToString("N0") + " đ";
                e.FormattingApplied = true;
            }
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
            string maPhieuDayDu = "pm" + item.RecordID.ToString("D3");
            string tenDocGia = mapTenDocGia.TryGetValue(item.UserID, out string dg) ? dg.ToLower() : "";
            string tenSach = (item.CopyID != null && mapTenSach.TryGetValue(item.CopyID, out string ts)) ? ts.ToLower() : "";
            
            //trùng từ khóa
            bool trungTuKhoa = string.IsNullOrEmpty(tuKhoa) || item.RecordID.ToString().Contains(tuKhoa) ||
                               item.UserID.ToString().Contains(tuKhoa) || maPhieuDayDu.Contains(tuKhoa) ||
                               item.UserID.ToString().Contains(tuKhoa) ||
                               tenDocGia.Contains(tuKhoa) ||
                               tenSach.Contains(tuKhoa) ||
                               (item.CopyID != null && item.CopyID.ToLower().Contains(tuKhoa));
        
            //trùng trạng thái
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