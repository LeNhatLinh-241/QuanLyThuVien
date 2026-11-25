using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
namespace QuanLyThuVien.GUI.UserControls;

public partial class ucBorrow : UserControl
{
    //Gọi các hàm xử lý bên BLL đ lấy dữ liệu, mượn, trả, tính phạt
    private BorrowRecordBLL borrowBll = new BorrowRecordBLL();
    
    /**
     * Bảng tra cứu nhanh để đổi mã sang tên khi hiện lên bảng:
     * -mapTenDocGia: từ mã độc giả ID lấy ra họ tên
     * -mapTenSach: từ mã vạch (copyID) lấy ra tên sách và không phân biệt chữ hoa chữ thường
     */
    
    private Dictionary<int, string> mapTenDocGia = new Dictionary<int, string>();
    private Dictionary<string, string> mapTenSach = new  Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    
    //3 biến tạm lưu thông tin của phiếu  
    private string ngayHanTraTam = "";      //giữ hạn trả để tính số ngày trễ
    private string ngayMuonTam = "";        //giữ ngày mượn để chặn chọn ngày trả nhỏ hơn ngày mượn
    private int trangThaiPhieuDangChon = -1;//-1: chưa chọn, 0: đang mượng, 1: đã trả, 2: làm mất
    
    //Đánh dấu form đã tải xong lần đầu chưa, tránh vưừa mở app bị tải dữ liệu 2 lần 
    private bool daTaiLanDau = false;
    public ucBorrow()
    {
        InitializeComponent();
        
        //Khi chuyển từ tab khác quay lại màn hình này thì tự tải lại dữ liệu mới 
        this.VisibleChanged += ucBorrow_VisibleChanged;
        
        //Cho phép bấm phím Enter ở ô mã vạch và ô tìm kiếm thay vì phải bấm chuột
        txtMaVach.KeyDown += txtMaVach_KeyDown;
        txtTimKiem.KeyDown += txtTimKiem_KeyDown;
    }
    
    //Chạy 1 lần khi mở màn hình mượn - trả
    private void ucBorrow_Load(object sender, EventArgs e)
    {
        //Mặc định mượn hôm nay, hạn trả sau 7 ngày
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
    
       //Nạp 2 lựa chọn xử lý bên khung trả sách
        cboTinhTrangSach.Items.Clear();
        cboTinhTrangSach.Items.Add("Trả sách");
        cboTinhTrangSach.Items.Add("Làm mất sách");
        cboTinhTrangSach.SelectedIndex = 0;
    
        //Nạp các mục lọc trạng thại ở thanh tìm kiếm
        cboLocTrangThai.Items.Clear();
        cboLocTrangThai.Items.Add("Tất cả trạng thái");
        cboLocTrangThai.Items.Add("Đang mượn");
        cboLocTrangThai.Items.Add("Đã trả");
        cboLocTrangThai.Items.Add("Làm mất");
        cboLocTrangThai.SelectedIndex = 0;
    
        //Tô màu xen kẽ các dòng trên bảng cho dễ nhìn
        dgvDanhSach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        
        //Cho ô độc giả vừa sổ xống chọn được, vừa gõ ch tìm tên nhanh được
        cboDocGia.DropDownStyle = ComboBoxStyle.DropDown;
        cboDocGia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cboDocGia.AutoCompleteSource = AutoCompleteSource.ListItems;
        
        //bật gợi ý cho ô mã vạch , danh sách gợi ý lấy từ hàm LoadGoiYMaVach
        txtMaVach.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txtMaVach.AutoCompleteSource = AutoCompleteSource.CustomSource;
        
        //LoadDanhSachPhieuMuon();
        ReloadData();
        daTaiLanDau = true;
    }
    
    //Tự chạy khi người dùng chuyển từ tab khác như kho sách, độc giả và quay lại tab này
    private void ucBorrow_VisibleChanged(object sender, EventArgs e)
    {
        if (daTaiLanDau && this.Visible && !this.DesignMode)
        {
            ReloadData();
        }
    }
    
    //Gom 4 hàm tải d liệu vào chung 1 chỗ để gọi lại khi mở form hoặc chuyển tab
    public void ReloadData()
    {
        LoadMapTraCuu();    
        LoadDanhSachDocGia();
        LoadGoiYMaVach();
        LoadDanhSachPhieuMuon();
    }
    
    //Lấy tên độc giả và tên sách từ sqlite lưu sẵn vào 2 biến map
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
    
    //Lấy danh sách độc giả đang hoạt động từ sqlite đổ vào ô chọn độc giả
    private void LoadDanhSachDocGia()
    {
        try
        {
            //ghi nhớ lại người đang chọn hiện tại trước khi tải lại, để không bị nhảy về người đầu tiên
            object dangChon = cboDocGia.SelectedValue;
            List<UserDTO> dsDocGia = borrowBll.LayDanhSachDocGia();
            
            //Gán DisplayMember (chữ hiện ra) và ValueMember(mã ID lấy ngầm) trước khi nạp dữ liệu
            cboDocGia.DataSource = null;
            cboDocGia.DisplayMember = "HoTen";
            cboDocGia.ValueMember = "ID";
            cboDocGia.DataSource = dsDocGia;
            
            //Nếu người chọn trước đó vẫn còn trong danh sách thì chọn lại đúng người dùng đó
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
    
    //Lấy các mã vạch sách đang rảnh tức chưa ai mượn đổ vào gợi ý cho ô txtMaVach
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
    
    //Xóa trắng khung trả sách sau khi trả xong để không bị bấm trả nhầm lại phiếu cũ
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
    
    //mỗi khi người dùng bấm phím trong ô nhập mã vạch, gõ xong bấm enter là lập phiếu luôn, không cần cầm chuột bấm nút
    private void txtMaVach_KeyDown(object sender, KeyEventArgs e)
    {
        //biến e chứa thông tin phím vừa bấm, chỉ xử lý khi đúng là phím Enter
        if (e.KeyCode == Keys.Enter)
        {
            //chặn tiếng "tinh" báo lỗi mặc định của winforms khi bấm Enter
            e.SuppressKeyPress = true;
            
            //Gọi lại luôn hàm xử l của nút cho mượn sách ể dùng chung code, tránh viết lập
            btnChoMuon_Click(sender, e);
        }
    }
    
    //Mỗi khi người dùng bấm phím trong ô tìm kiếm và gõ xong từ khóa bấm Enter sẽ lọc ngay danh sách, không cần rê chuột bấm nút tìm kiếm
    private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
    {
        //biến e chứa thông tin phím vừa bấm, chỉ xử lý khi đúng là phím Enter
        if (e.KeyCode == Keys.Enter)
        {
            //chặn tiếng "tinh" báo lỗi mặc định của winforms khi bấm Enter
            e.SuppressKeyPress = true;
            
            //gọi lại nút tìm kiếm để dùng chung logic lọc dữ liệu, tránh viết lặp code
            btnTimKiem_Click(sender, e);
        }
    }

    //Đổi tên cột sang tiếng việt cho dễ nhìn
    private void DinhDangCotBang()
    {
        //Kiểm tra bảng đã có cột nào chưa rồi mới chỉnh để tránh lỗi khi chưa có dữ liệu
        if (dgvDanhSach.Columns.Count > 0)
        {
            //kiểm tra từng cột khác null trước khi gán thuộc tính để phòng trường hợp tên biến bên DTO thay đổi sẽ không bị văng lỗi
            if (dgvDanhSach.Columns["RecordID"] != null)
            {
                //HeaderText là chữ tiêu đề hiển thị cho người dùng nhìn thấy trên bảng
                dgvDanhSach.Columns["RecordID"].HeaderText = "Mã Phiếu";
                
                //Cột mã phiếu ngắn nên chỉ cần trọng số nhỏ là 75
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
    
    //Tải toàn bộ danh sách phiếu mượn từ sqlite đổ lên bảng DataGridView
    //Đồng thời đếm nhanh số phiếu đang mượn, quá hạn để cập nhật dòng thống kê
    private void LoadDanhSachPhieuMuon()
    {
        try
        {
            // Gọi xuống tầng BLL để lấy danh sách tất cả phiếu mượn hiện có trong SQLite
            List<BorrowRecordDTO> list = borrowBll.LayDanhSachPhieuMuon();
            dgvDanhSach.DataSource = null;
            dgvDanhSach.DataSource = list;
            
            // Gọi lại hàm định dạng để đổi tên cột sang tiếng Việt và chia tỷ lệ độ rộng
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
            // Khai báo 2 biến đếm để hiện thống kê nhanh ở góc trên bên phải
            int dangMuon = 0;
            int quaHan = 0;
            
            // Duyệt qua từng phiếu trong danh sách vừa tải
            foreach (BorrowRecordDTO item in list)
            {
                if (item.TrangThai == 0)
                {
                    dangMuon++;
                    DateTime hanTra;
                    
                    // Dùng TryParse để đổi chuỗi ngày hạn trả sang kiểu DateTime một cách an toàn, tránh văng lỗi nếu chuỗi rỗng hoặc sai định dạng
                    // So sánh theo thuộc tính .Date để bỏ qua giờ phút giây, nếu hạn trả nhỏ hơn ngày hôm nay thì tính là quá hạn
                    if (DateTime.TryParse(item.NgayHanTra, out hanTra) && hanTra.Date < DateTime.Now.Date)
                    {
                        quaHan++;
                    }
                }
            }
            
            // Hiển thị số lượng đếm được lên dòng thống kê góc trên phải
            lblThongKeNho.Text = dangMuon + " phiếu đang mượn " + quaHan + " quá hạn";
            
            // Cập nhật tổng số phiếu và mã phiếu đang chọn
            // Nếu chưa chọn phiếu nào (txtMaPhieu trống) thì hiện chưa chọn
            lblChanTrang.Text = "Hiển thị " + list.Count + "phiếu | đã chọn: " +
                                (string.IsNullOrEmpty(txtMaPhieu.Text) ? "Chưa chọn " : "PM" + txtMaPhieu.Text.PadLeft(3, '0'));
        }
        catch (Exception ex)
        {
            // Bắt lỗi nếu không kết nối hoặc đọc được dữ liệu từ SQLite để chương trình không bị tắt đột ngột
            MessageBox.Show("không tải được danh sách: " + ex.Message, "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
    
    /**
     * tự động chạy mỗi khi người dùng chọn sang một độg giả khác hoặ được gọi lại sau khi mượn trả xong
     * đếm xem độc giả đang chọn giữ mấy cuốn sách và còn nợ bao nhiêu tiền phạt để hiện lên thanh trạng thái
     */
    private void cboDocGia_SelectedIndexChanged(object sender, EventArgs e)
    {
        /**
         * bảo vệ tránh lỗi ép kiểu khi combobox mới bắt đầu nạp dữ liệu hoặc người dùng đang gõ chữ chưa khớp ai,
         * 
         */
        if (cboDocGia.SelectedValue == null || !(cboDocGia.SelectedValue is int))
        {
            return;
        }
        // Lấy mã ID ngầm bên dưới (nhờ đã gán ValueMember = "ID" lúc nạp danh sách độc giả)
        int userId = Convert.ToInt32(cboDocGia.SelectedValue);
        
        // Lấy toàn bộ danh sách phiếu mượn từ tầng BLL để lọc riêng các phiếu của độc giả này
        List<BorrowRecordDTO> list = borrowBll.LayDanhSachPhieuMuon();
        
        // 2 biến lưu kết quả cộng dồn cho độc giả đang chọn
        int soSachDangMuon = 0;
        double noPhat = 0;

        foreach (BorrowRecordDTO item in list)
        {
            
            // Chỉ xét các phiếu thuộc về đúng độc giả đang chọn
            if (item.UserID == userId)
            {
                // Trạng thái bằng 0 là đang mượn nên cộng thêm 1 cuốn
                if (item.TrangThai == 0)
                {
                    soSachDangMuon++;
                }
                
                // Chỉ cộng vào tổng nợ phạt nếu phiếu có tiền phạt và chưa thu tiền (TrangThaiThuTien bằng 0)
                if (item.TienPhat > 0 && item.TrangThaiThuTien == 0)
                {
                    noPhat += item.TienPhat;
                }
            }
        }
        // ToString("N0") dùng để tách dấu phẩy hàng nghìn và bỏ phần thập phân (ví dụ 15000 thành 15,000)
        lblTrangThaiMuon.Text = "Đang Mượn: " + soSachDangMuon + "/3 cuốn | Nợ Phạt: " + noPhat.ToString(
            "N0") + "đ | Thẻ: Hợp lệ";
    }
    
    /**
     * Chạy khi người dùng bấm nút Cho mượn sách
     * Quy trình gồm 3 bước: kiểm tra đầu vào trên giao diện, gửi xuống BLL xử lý nghiệp vụ và lưu DB, rồi làm mới lại màn hình
     */
    private void btnChoMuon_Click(object sender, EventArgs e)
    {
        // Kiểm tra xem đã chọn độc giả hợp lệ chưa (nếu người dùng gõ tên không có trong danh sách thì SelectedValue sẽ bằng null)
        if (cboDocGia.SelectedValue == null)
        {
            MessageBox.Show("Vui lòng chọn độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        // Lấy .Date để chỉ so sánh ngày/tháng/năm, bỏ qua giờ phút giây (cho phép chọn ngày hạn trả trùng ngay ngày mượn)
        if (dtpNgayHanTra.Value.Date < dtpNgayMuon.Value.Date)
        {
            MessageBox.Show("Ngày hạn trả không được nhỏ hơn ngày mượn ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }
        /**
         * Lấy mã độc giả và mã vạch sách người dùng vừa nhập
         * Hàm Trim() dùng để cắt bỏ khoảng trắng thừa ở hai đầu phòng trường hợp người dùng copy nhầm
         */
        int userId = Convert.ToInt32(cboDocGia.SelectedValue);
        string copyId = txtMaVach.Text.Trim();
        
        // Chặn trường hợp bỏ trống ô mã vạch hoặc chỉ gõ toàn dấu cách
        if (string.IsNullOrEmpty(copyId))
        {
            MessageBox.Show("Vui lòng nhập hoặc quét mã vạch sách", "THÔNG BÁO", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtMaVach.Focus();
            return;
        }
        
        /**
         * // Gửi thông tin xuống tầng BLL để kiểm tra thẻ bị khóa, còn nợ phạt, mượn quá 3 cuốn, sách đang mượn
         * Biến thongBao hứng chuỗi kết quả từ BLL trả về để biết thành công hay thất bại vì lý do gì
         */
        string thongBao = borrowBll.MuonSach(userId, copyId, dtpNgayMuon.Value, dtpNgayHanTra.Value);

        // Kiểm tra chuỗi trả về có chứa chữ "thành công" không, dùng OrdinalIgnoreCase để không bị lỗi khi khác chữ hoa/thường
        if (thongBao.Contains("Thành công", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            // Xóa mã vạch cũ để thủ thư sẵn sàng quét cuốn tiếp theo
            txtMaVach.Clear();
           
            // Tải lại bảng danh sách để hiện phiếu mượn mới và cập nhật số lượng thống kê
            LoadDanhSachPhieuMuon();
            
            // Nạp lại map tra cứu tên trước khi tải lại bảng để dữ liệu hiển thị luôn đồng bộ
            LoadMapTraCuu();
            
            // Tải lại gợi ý mã vạch để loại bỏ cuốn sách vừa cho mượn
            LoadGoiYMaVach();
           
            // Gọi lại sự kiện đổi độc giả để thanh trạng thái màu xanh tự cộng thêm 1 cuốn đang mượn
            cboDocGia_SelectedIndexChanged(null, null);
        }
        else
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    
    /**
     * Tự động chạy khi người dùng click vào bất kỳ ô nào của một dòng trên bảng danh sách
     * lấy thông tin phiếu ở dòng vừa chọn đổ sang khung Trả sách, khóa nút nếu phiếu đã xử lý xong và gọi tính tiền phạt
     */
    private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // e.RowIndex nhỏ hơn 0 nghĩa là người dùng bấm trúng hàng tiêu đề cột chứ không phải dòng dữ liệu nên bỏ qua
        // Kiểm tra thêm CurrentRow khác null để tránh lỗi khi bảng đang trống
        if (e.RowIndex < 0 || dgvDanhSach.CurrentRow == null)
        {
            return;
        }
        
        // Lấy nguyên dòng đang được chọn trên bảng
        DataGridViewRow row = dgvDanhSach.CurrentRow;
        
        // Lấy giá trị gốc từ từng ô theo tên cột của DTO để không bị dính chữ "PM001" hay tên sách do CellFormatting vẽ đè
        // Dấu ?.ToString() ?? "" là cách viết an toàn: nếu ô đó bị null thì lấy chuỗi rỗng "" thay vì văng lỗi
        txtMaPhieu.Text = row.Cells["RecordID"].Value?.ToString() ?? "";
        txtMaVachTra.Text = row.Cells["CopyID"].Value?.ToString() ?? "";
        
        // Lưu 3 thông tin này vào biến tạm ở ngoài vì khung Trả sách không có ô nhập riêng để chứa chúng
        ngayHanTraTam = row.Cells["NgayHanTra"].Value?.ToString() ?? "";
        ngayMuonTam = row.Cells["NgayMuon"].Value?.ToString() ?? "";
        
        // Nếu ô trạng thái bị null thì gán mặc định là -1 (chưa xác định)
        trangThaiPhieuDangChon = Convert.ToInt32(row.Cells["TrangThai"].Value ?? -1);
        
        // Kiểm tra xem phiếu này có đang mượn (trạng thái bằng 0) hay không
        bool dangMuon = (trangThaiPhieuDangChon == 0);
        
        // Chỉ cho phép bấm nút Nhận trả sách khi phiếu đang mượn; nếu đã trả hoặc báo mất rồi thì khóa nút lại
        btnNhanTraSach.Enabled = dangMuon;
        
        // Đổi màu nút tương ứng: đang mượn thì giữ màu xanh lá, đã khóa thì chuyển sang màu xám cho người dùng dễ nhận biết
        btnNhanTraSach.BackColor = dangMuon ? Color.FromArgb(22, 163, 74) : Color.FromArgb(148, 163, 184);
        
        // Cập nhật dòng chân trang cho biết bảng đang có bao nhiêu phiếu và đang chọn phiếu nào
        lblChanTrang.Text = "Hiển thị " + dgvDanhSach.Rows.Count + " phiếu | Đã chọn: #" + txtMaPhieu.Text;
        
        // Gọi ngay hàm tính tiền phạt dự kiến để thủ thư thấy số tiền phạt liền lúc vừa chọn dòng
        CapNhatTienPhat(null, null);
    }
    //
    //tính tiền phạt dự kiến khi chọn ngày trả hoặc đổi giữa Trả sách / làm mất
    private void CapNhatTienPhat(object sender, EventArgs e)
    {
        // Chưa chọn phiếu nào hoặc phiếu đó không có hạn trả thì dừng lại, không cần tính
        if (string.IsNullOrEmpty(txtMaPhieu.Text) || string.IsNullOrEmpty(ngayHanTraTam))
        {
            return;
        }
        
        // Nếu phiếu đã trả hoặc báo mất trước đó (khác 0) thì chỉ báo đã xử lý xong, không tính phạt lại nữa
        if (trangThaiPhieuDangChon != 0)
        {
            lblTongTienPhat.Text = "Phiếu này đã xử lý xong";
            return;
        }
        
        // Quy đổi vị trí mục đang chọn sang mã trạng thái quy ước trong DB:
        // Vị trí 0 ("Trả sách") tương ứng mã 1, vị trí 1 ("Làm mất sách") tương ứng mã 2
        int trangThaiTra = (cboTinhTrangSach.SelectedIndex == 0) ? 1 : 2;
        
        // Gửi hạn trả, ngày trả, hình thức xử lý và mã vạch (để tra giá tiền sách nếu báo mất) xuống BLL tính toán
        double tienPhat = borrowBll.TinhTienPhat(ngayHanTraTam, dtpNgayTra.Value, trangThaiTra, txtMaVachTra.Text);
        lblTongTienPhat.Text = "Tiền phạt: " + tienPhat.ToString("N0") + " đ";
    }
     
    /**
     * Chạy khi người dùng bấm nút Nhận trả sách bên khung Trả sách
     * Quy trình gồm 3 bước: kiểm tra lại phiếu và ngày trả, gọi xuống BLL cập nhật DB, rồi dọn khung trả và tải lại bảng
     */
    
    private void btnNhanTraSach_Click(object sender, EventArgs e)
    {
        // Dùng TryParse để vừa kiểm tra xem ô Mã phiếu có đang trống không, vừa đổi chữ thành số nguyên recordId để gửi xuống BLL
        if (!int.TryParse(txtMaPhieu.Text.Trim(), out int recordId))
        {
            MessageBox.Show("Chọn một phiếu mượn bên bảng trước khi trả!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }
        
        // Kiểm tra thêm một lớp ở đây để chắc chắn phiếu đang ở trạng thái Đang mượn (mã 0), tránh trả trùng phiếu đã xong
        if (trangThaiPhieuDangChon != 0)
        {
            MessageBox.Show("Phiếu này đã được trả hoặc báo mất rồi", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }
        
        // Đổi chuỗi ngày mượn tạm sang kiểu DateTime rồi so sánh theo .Date (bỏ qua giờ phút giây)
        // Giúp cho phép mượn và trả ngay trong cùng một ngày mà không bị báo lỗi
        if (DateTime.TryParse(ngayMuonTam, out DateTime ngayMuon) && dtpNgayTra.Value < ngayMuon.Date)
        {
            MessageBox.Show("Ngày trả không được nhỏ hơn ngày mượn", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }
        
        // Lấy mã vạch cuốn sách đang trả để tầng BLL cập nhật lại trạng thái sách trong kho (về không mượn hoặc bị mất)
        string copyId = txtMaVachTra.Text;
        int trangThaiTra = (cboTinhTrangSach.SelectedIndex == 0) ? 1 : 2;
        
        // Gọi xuống BLL để tính phạt chốt sổ, lưu ngày trả và đổi trạng thái bản sao sách trong SQLite
        // Tham số cuối truyền true (daDongPhat) theo quy ước trả sách trực tiếp tại quầy thì thu tiền phạt luôn
        string thongBao =
            borrowBll.TraSachHoacBaoMat(recordId, copyId, ngayHanTraTam, dtpNgayTra.Value, trangThaiTra, true);
        
        // Kiểm tra chuỗi kết quả trả về không phân biệt chữ hoa thường để biết thao tác có thành công không
        if (thongBao.Contains("Thành công", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            // Xóa trắng khung trả sách trước để dòng chân trang bên dưới cập nhật về lại chưa chọn
            ResetKhungTraSach();
           
            // Nạp lại danh sách gợi ý mã vạch vì cuốn sách vừa trả đã rảnh trở lại, có thể cho người khác mượn tiếp
            LoadGoiYMaVach();
            
            // Tải lại bảng để thấy dòng phiếu vừa xử lý đổi sang trạng thái Đã trả hoặc Làm mất
            LoadDanhSachPhieuMuon();
            
            // Gọi lại sự kiện đổi độc giả để thanh trạng thái màu xanh tự trừ bớt 1 cuốn đang mượn
            cboDocGia_SelectedIndexChanged(null, null);
        }
        else
        {
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    // chức năng của DataGridView, tự động chạy cho từng ô mỗi khi bảng vẽ dữ liệu lên màn hình
    // Chỉ thay đổi chữ hiển thị và màu sắc bên ngoài cho dễ nhìn, còn dữ liệu gốc bên dưới vẫn giữ nguyên (CellClick vẫn lấy đúng ID thật)
    private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        // Bỏ qua hàng tiêu đề cột (chỉ số hàng nhỏ hơn 0)
        if (e.RowIndex < 0)
        {
            return;
        }
        
        // Lấy tên gốc của cột đang được vẽ (trùng với tên thuộc tính trong BorrowRecordDTO) để biết cần format kiểu nào
        string colName = dgvDanhSach.Columns[e.ColumnIndex].Name;
    
        //cột mã phiếu đổi số nguyên thành dạng có 3 chữ số kèm chữ PM (ví dụ số 1 thành "PM001") và tô chữ xanh dương
        if (colName == "RecordID" && e.Value != null)
        {
            if (int.TryParse(e.Value.ToString(), out int id))
            {
                e.Value = "PM" + id.ToString("D3");
                e.FormattingApplied = true;
            }

            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
        }
        
        // 2. cột độc giả lấy mã UserID đi tra trong mapTenDocGia để hiện họ tên thay vì hiện mã số
        if (colName == "UserID" && e.Value != null)
        {
            if (int.TryParse(e.Value.ToString(), out int userId) && mapTenDocGia.TryGetValue(userId, out string tenDocGia))
            {
                e.Value = tenDocGia;
                e.FormattingApplied = true;
            }
        }
        
        // cột sách nượn tra tên sách từ mapTenSach rồi ghép chung với mã vạch
        if (colName == "CopyID" && e.Value != null)
        {
            string copyId = e.Value.ToString() ?? "";
            string tenSach = mapTenSach.TryGetValue(copyId, out string ten) ? ten : "(Sách đã xóa)";
            e.Value = copyId + " · " + tenSach;
            e.FormattingApplied = true;
        }
        
        // cột tiền phạt định dạng có dấu phẩy hàng nghìn và ghép thêm đơn vị "đ"
        if (colName == "TienPhat" && e.Value != null)
        {
            if (double.TryParse(e.Value.ToString(), out double tien))
            {
                e.Value = tien.ToString("N0") + " đ";
                e.FormattingApplied = true;
            }
        }
        
        //cột ngày trả phiếu nào đang mượn chưa có ngày trả thực tế (null hoặc rỗng) thì hiện dấu gạch ngang cho gọn
        if (colName == "NgayTraThucTe" && (e.Value == null || string.IsNullOrEmpty(e.Value.ToString())))
        {
            e.Value = "-";
            e.FormattingApplied = true;
        }
        
        // cột trạng thái đổi các mã số (0, 1, 2 sang chữ tiếng Việt và tô màu nền, màu chữ nổi bật theo tình trạng
        if (colName == "TrangThai" && e.Value != null)
        {
            int trangThai = Convert.ToInt32(e.Value);
            
            //kiểm tra xem phiếu đang mượn có bị trễ hạn hay chưa
            string hanTraStr = dgvDanhSach.Rows[e.RowIndex].Cells["NgayHanTra"].Value?.ToString() ?? "";

            if (trangThai == 0)
            {
                //quá hạn thì tính số ngày trễ và tô nền đỏ chữ đỏ
                if (DateTime.TryParse(hanTraStr, out DateTime hanTra) && hanTra.Date < DateTime.Now.Date)
                {
                    int soNgayTre = (DateTime.Now.Date - hanTra.Date).Days;
                    e.Value = "Quá hạn " + soNgayTre + " ngày";
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
                }
                else
                {
                    //đang mượn trong hạn thì tô nền avngf nhạt, chữ nâu vàng
                    e.Value = "Đang mượn";
                    e.CellStyle.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(100, 83, 9);
                }
            }
            else if (trangThai == 1)
            {
                //đã trả th tô nền xanh lá nhạt, chữ xanh đậm
                e.Value = "Đã Trả";
                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(220, 252, 231);
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            }
            else if(trangThai ==2)
            {
                //làm mất thì tô nền đỏ nht, chữ đỏ đậm cảnh báo
                e.Value = "Làm mất";
                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            }
            e.FormattingApplied = true;
        }
    }
    
    // Chạy khi người dùng bấm nút Tìm kiếm hoặc nhấn Enter trong ô tìm kiếm
    //lấy từ khóa và trạng thái cần lọc, duyệt danh sách gốc để nhặt ra các phiếu khớp cả 2 điều kiện rồi đổ lại lên bảng
    private void btnTimKiem_Click(object sender, EventArgs e)
    {
        
        // Cắt khoảng trắng thừa hai đầu và đưa từ khóa về chữ thường hết để tìm kiếm không bị phân biệt chữ hoa hay chữ thường
        string tuKhoa = txtTimKiem.Text.Trim().ToLower();
        
        // Quy đổi vị trí mục đang chọn trên cboLocTrangThai về đúng mã trạng thái trong DB bằng cách trừ đi 1:
        // vị trí 0 ("Tất cả") thành -1, vị trí 1 ("Đang mượn") thành 0, vị trí 2 ("Đã trả") thành 1, vị trí 3 ("Làm mất") thành 2
        int trangThaiLoc = cboLocTrangThai.SelectedIndex - 1;
        
        // Lấy toàn bộ danh sách phiếu mượn mới nhất từ BLL làm nguồn gốc để lọc
        List<BorrowRecordDTO> dsGoc = borrowBll.LayDanhSachPhieuMuon();
        
        // Tạo một danh sách rỗng để chứa riêng các phiếu thỏa mãn điều kiện tìm kiếm
        List<BorrowRecordDTO> dsLoc = new List<BorrowRecordDTO>();

        foreach (BorrowRecordDTO item in dsGoc)
        {   
            // Ghép sẵn chuỗi "pm001" dạng chữ thường để người dùng gõ "PM001" hay "pm001" đều tìm ra được
            string maPhieuDayDu = "pm" + item.RecordID.ToString("D3");
            
            // Tra tên độc giả và tên sách từ 2 bảng map rồi đổi sang chữ thường để hỗ trợ tìm kiếm theo tên
            // Nếu không tìm thấy trong map thì trả về chuỗi rỗng "" để tránh lỗi null
            string tenDocGia = mapTenDocGia.TryGetValue(item.UserID, out string dg) ? dg.ToLower() : "";
            string tenSach = (item.CopyID != null && mapTenSach.TryGetValue(item.CopyID, out string ts)) ? ts.ToLower() : "";
            
            
            //  Đk1 khớp từ khóa: đúng khi ô tìm kiếm đang trống,
            // hoặc từ khóa xuất hiện trong bất kỳ thông tin nào (mã phiếu số, mã phiếu PM..., mã độc giả, tên độc giả, tên sách, mã vạch)
             
            bool trungTuKhoa = string.IsNullOrEmpty(tuKhoa) || item.RecordID.ToString().Contains(tuKhoa) ||
                               item.UserID.ToString().Contains(tuKhoa) || maPhieuDayDu.Contains(tuKhoa) ||
                               item.UserID.ToString().Contains(tuKhoa) ||
                               tenDocGia.Contains(tuKhoa) ||
                               tenSach.Contains(tuKhoa) ||
                               (item.CopyID != null && item.CopyID.ToLower().Contains(tuKhoa));
        
            // Điều kiện 2 khớp trạng thái: đúng khi đang chọn "Tất cả trạng thái" (-1),
            // hoặc mã trạng thái của phiếu khớp đúng với mục đang lọc
            bool trungTrangThai = (trangThaiLoc == -1) || (item.TrangThai == trangThaiLoc);
            
            // Phải thỏa đồng thời cả 2 điều kiện (từ khóa và trạng thái) mới thêm phiếu đó vào danh sách kết quả
            if (trungTuKhoa && trungTrangThai)
            {
                dsLoc.Add(item);
            }
        }
        // Gán null trước rồi mới nạp danh sách đã lọc lên bảng để làm mới sạch lưới
        dgvDanhSach.DataSource = null;
        dgvDanhSach.DataSource = dsLoc;
        
        // Gọi lại hàm định dạng vì mỗi lần gán DataSource mới bảng sẽ tự vẽ lại cột theo tên tiếng Anh của DTO
        DinhDangCotBang();
        lblChanTrang.Text = "Hiển thị " + dsLoc.Count + " / " + dsGoc.Count + " phiếu";
    }
}