using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

namespace QuanLyThuVien.GUI.UserControls;

partial class ucBorrow : System.Windows.Forms.UserControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //1. Tiêu đề chính của màn hình
        lblTieuDe = new System.Windows.Forms.Label();

        //2. Cụm control khung Lập phiếu mượng sách
        grpLapPhieu = new System.Windows.Forms.GroupBox();
        lblDocGia = new System.Windows.Forms.Label();
        cboDocGia = new System.Windows.Forms.ComboBox();
        lblMaVach = new System.Windows.Forms.Label();
        txtMaVach = new System.Windows.Forms.TextBox();
        lblNgayMuon = new System.Windows.Forms.Label();
        dtpNgayMuon = new System.Windows.Forms.DateTimePicker();
        lblNgayHanTra = new System.Windows.Forms.Label();
        dtpNgayHanTra = new System.Windows.Forms.DateTimePicker();
        lblTieuDeTrangThai = new System.Windows.Forms.Label();
        lblTrangThaiMuon = new System.Windows.Forms.Label();
        btnChoMuon = new System.Windows.Forms.Button();

        //3.Cụm control khung xử lý trả sách và báo mất
        grpTraSach = new System.Windows.Forms.GroupBox();
        lblMaPhieu = new System.Windows.Forms.Label();
        txtMaPhieu = new System.Windows.Forms.TextBox();
        lblMaVachTra = new System.Windows.Forms.Label();
        txtMaVachTra = new System.Windows.Forms.TextBox();
        lblNgayTra = new System.Windows.Forms.Label();
        dtpNgayTra = new System.Windows.Forms.DateTimePicker();
        lblXuLy = new System.Windows.Forms.Label();
        cboTinhTrangSach = new System.Windows.Forms.ComboBox();
        lblTongTienPhat = new System.Windows.Forms.Label();
        btnNhanTraSach = new System.Windows.Forms.Button();

        //4.Cụm control khung danh sách phiếu mượn, trả
        grpDanhSach = new System.Windows.Forms.GroupBox();
        lblThongKeNho = new System.Windows.Forms.Label();
        txtTimKiem = new System.Windows.Forms.TextBox();
        cboLocTrangThai = new System.Windows.Forms.ComboBox();
        btnTimKiem = new System.Windows.Forms.Button();
        lblGhiChuNgayTra = new System.Windows.Forms.Label();
        dgvDanhSach = new System.Windows.Forms.DataGridView();
        lblChanTrang = new System.Windows.Forms.Label();

        //5.Tạm dừng vẽ layout trong lúc gán thuộc tính
        grpLapPhieu.SuspendLayout();
        grpTraSach.SuspendLayout();
        grpDanhSach.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDanhSach).BeginInit();
        SuspendLayout();

        // Tiêu đề chính nằm góc trên bên trái màn hình
       // lblTieuDe.AutoSize = true;
       // lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
       // lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
       // lblTieuDe.Location = new System.Drawing.Point(20, 15);
       // lblTieuDe.Name = "lblTieuDe";
       // lblTieuDe.Size = new System.Drawing.Size(210, 28);
       // lblTieuDe.Text = "MƯỢN - TRẢ SÁCH";

        // Khung bên trên trái: chứa toàn bộ chức năng lập phiếu mượn sách
        grpLapPhieu.BackColor = System.Drawing.Color.White;
        grpLapPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        grpLapPhieu.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        grpLapPhieu.Location = new System.Drawing.Point(16, 12);
        grpLapPhieu.TabIndex = 0;
        grpLapPhieu.TabStop = false;
        grpLapPhieu.Size = new System.Drawing.Size(376, 322);
        grpLapPhieu.Anchor =
            System.Windows.Forms.AnchorStyles.Top |
            System.Windows.Forms.AnchorStyles.Left;
        grpLapPhieu.Text = "LẬP PHIẾU MƯỢN SÁCH";

        // Nhãn chữ "Chọn độc giả"
        lblDocGia.AutoSize = true;
        lblDocGia.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblDocGia.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblDocGia.Location = new System.Drawing.Point(16, 30);
        lblDocGia.Name = "lblDocGia";
        lblDocGia.Size = new System.Drawing.Size(125, 15);
        lblDocGia.Text = "Chọn độc giả";

        // Ô sổ xuống chọn độc giả mượn sách, gắn sự kiện đổi người là cập nhật số sách đang giữ
        cboDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboDocGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboDocGia.Location = new System.Drawing.Point(16, 50);
        cboDocGia.Name = "cboDocGia";
        cboDocGia.Size = new System.Drawing.Size(344, 25);
        cboDocGia.TabIndex = 0;
        cboDocGia.SelectedIndexChanged += cboDocGia_SelectedIndexChanged;

        // Nhãn chữ "Quét mã vạch sách"
        lblMaVach.AutoSize = true;
        lblMaVach.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblMaVach.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblMaVach.Location = new System.Drawing.Point(16, 86);
        lblMaVach.Name = "lblMaVach";
        lblMaVach.Size = new System.Drawing.Size(242, 15);
        lblMaVach.Text = "Quét mã vạch sách";

        // Ô nhập hoặc quét mã vạch bản sao sách (CopyID) cần cho mượn
        txtMaVach.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtMaVach.Location = new System.Drawing.Point(16, 106);
        txtMaVach.Name = "txtMaVach";
        txtMaVach.PlaceholderText = "SACH01_1";
        txtMaVach.Size = new System.Drawing.Size(344, 25);
        txtMaVach.TabIndex = 1;

        // Nhãn chữ "Ngày mượn"
        lblNgayMuon.AutoSize = true;
        lblNgayMuon.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblNgayMuon.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblNgayMuon.Location = new System.Drawing.Point(16, 142);
        lblNgayMuon.Name = "lblNgayMuon";
        lblNgayMuon.Size = new System.Drawing.Size(68, 15);
        lblNgayMuon.Text = "Ngày mượn";

        // Ô chọn ngày mượn sách theo định dạng ngắn
        dtpNgayMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        dtpNgayMuon.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        dtpNgayMuon.Location = new System.Drawing.Point(16, 162);
        dtpNgayMuon.Name = "dtpNgayMuon";
        dtpNgayMuon.Size = new System.Drawing.Size(146, 25);
        dtpNgayMuon.TabIndex = 2;

        // Nhãn chữ "Ngày Hạn Trả"
        lblNgayHanTra.AutoSize = true;
        lblNgayHanTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblNgayHanTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblNgayHanTra.Location = new System.Drawing.Point(178, 142);
        lblNgayHanTra.Name = "lblNgayHanTra";
        lblNgayHanTra.Size = new System.Drawing.Size(75, 15);
        lblNgayHanTra.Text = "Ngày Hạn Trả";

        // Ô chọn ngày hẹn trả sách
        dtpNgayHanTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        dtpNgayHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        dtpNgayHanTra.Location = new System.Drawing.Point(178, 162);
        dtpNgayHanTra.Name = "dtpNgayHanTra";
        dtpNgayHanTra.Size = new System.Drawing.Size(146, 25);
        dtpNgayHanTra.TabIndex = 3;

        // Nhãn chữ "Trạng thái mượn"
        lblTieuDeTrangThai.AutoSize = true;
        lblTieuDeTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblTieuDeTrangThai.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblTieuDeTrangThai.Location = new System.Drawing.Point(16, 198);
        lblTieuDeTrangThai.Name = "lblTieuDeTrangThai";
        lblTieuDeTrangThai.Size = new System.Drawing.Size(93, 15);
        lblTieuDeTrangThai.Text = "Trạng thái mượn";

        // Thanh nền xanh hiện nhanh số cuốn đang mượn, nợ phạt và tình trạng thẻ của độc giả đang chọn
        lblTrangThaiMuon.AutoSize = false;
        lblTrangThaiMuon.BackColor = System.Drawing.Color.FromArgb(220, 252, 231);
        lblTrangThaiMuon.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblTrangThaiMuon.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
        lblTrangThaiMuon.Location = new System.Drawing.Point(16, 218);
        lblTrangThaiMuon.Name = "lblTrangThaiMuon";
        lblTrangThaiMuon.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
        lblTrangThaiMuon.Size = new System.Drawing.Size(308, 28);
        lblTrangThaiMuon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblTrangThaiMuon.Text = "Đang mượn: 0/3 cuốn | Nợ phạt: 0 đ | Thẻ: Hợp lệ";

        // Nút xác nhận cho mượn sách (màu xanh dương), gắn sự kiện bấm nút btnChoMuon_Click
        btnChoMuon.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        btnChoMuon.FlatAppearance.BorderSize = 0;
        btnChoMuon.Cursor = System.Windows.Forms.Cursors.Hand;
        btnChoMuon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
        btnChoMuon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnChoMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        btnChoMuon.ForeColor = System.Drawing.Color.White;
        btnChoMuon.Location = new System.Drawing.Point(16, 256);
        btnChoMuon.Name = "btnChoMuon";
        btnChoMuon.Size = new System.Drawing.Size(344, 38);
        btnChoMuon.TabIndex = 4;
        btnChoMuon.Text = "Cho mượn sách";
        btnChoMuon.UseVisualStyleBackColor = false;
        btnChoMuon.Click += btnChoMuon_Click;
        
        // Thêm toàn bộ các control mượn sách vào bên trong khung grpLapPhieu
        grpLapPhieu.Controls.Add(lblDocGia);
        grpLapPhieu.Controls.Add(cboDocGia);
        grpLapPhieu.Controls.Add(lblMaVach);
        grpLapPhieu.Controls.Add(txtMaVach);
        grpLapPhieu.Controls.Add(lblNgayMuon);
        grpLapPhieu.Controls.Add(dtpNgayMuon);
        grpLapPhieu.Controls.Add(lblNgayHanTra);
        grpLapPhieu.Controls.Add(dtpNgayHanTra);
        grpLapPhieu.Controls.Add(lblTieuDeTrangThai);
        grpLapPhieu.Controls.Add(lblTrangThaiMuon);
        grpLapPhieu.Controls.Add(btnChoMuon);

        // Khung bên dưới trái: chứa toàn bộ chức năng nhận trả sách và báo làm mất
        grpTraSach.BackColor = System.Drawing.Color.White;
        grpTraSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        grpTraSach.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        grpTraSach.Location = new System.Drawing.Point(16, 340);
        grpTraSach.Size = new System.Drawing.Size(376, 300);
        grpTraSach.Anchor =
            System.Windows.Forms.AnchorStyles.Top |
            System.Windows.Forms.AnchorStyles.Left;
        grpTraSach.TabIndex = 1;
        grpTraSach.TabStop = false;
        grpTraSach.Text = "XỬ LÝ TRẢ SÁCH VÀ BÁO MẤT";

        // Nhãn chữ "Mã phiếu"
        lblMaPhieu.AutoSize = true;
        lblMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblMaPhieu.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblMaPhieu.Location = new System.Drawing.Point(16, 28);
        lblMaPhieu.Name = "lblMaPhieu";
        lblMaPhieu.Size = new System.Drawing.Size(113, 15);
        lblMaPhieu.Text = "Mã phiếu";

        // Ô hiện mã phiếu vừa bấm chọn từ bảng (chỉ đọc để tránh người dùng tự sửa nhầm)
        txtMaPhieu.BackColor = System.Drawing.Color.FromArgb(248,250,252);
        txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtMaPhieu.Location = new System.Drawing.Point(16, 48);
        txtMaPhieu.Name = "txtMaPhieu";
        txtMaPhieu.Size = new System.Drawing.Size(344, 25);
        txtMaPhieu.TabIndex = 0;
        txtMaPhieu.ReadOnly = true;

        // Nhãn chữ "Mã Vạch (CopyID)"
        lblMaVachTra.AutoSize = true;
        lblMaVachTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblMaVachTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblMaVachTra.Location = new System.Drawing.Point(16, 84);
        lblMaVachTra.Name = "lblMaVachTra";
        lblMaVachTra.Size = new System.Drawing.Size(99, 15);
        lblMaVachTra.Text = "Mã Vạch (CopyID)";

        // Ô hiện mã vạch cuốn sách của phiếu đang chọn (chỉ đọc)
        txtMaVachTra.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        txtMaVachTra.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtMaVachTra.Location = new System.Drawing.Point(16, 104);
        txtMaVachTra.Name = "txtMaVachTra";
        txtMaVachTra.ReadOnly = true;
        txtMaVachTra.Size = new System.Drawing.Size(344, 25);
        txtMaVachTra.TabIndex = 1;

        // Nhãn chữ "Ngày trả"
        lblNgayTra.AutoSize = true;
        lblNgayTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblNgayTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblNgayTra.Location = new System.Drawing.Point(16, 140);
        lblNgayTra.Name = "lblNgayTra";
        lblNgayTra.Size = new System.Drawing.Size(51, 15);
        lblNgayTra.Text = "Ngày trả";

        // Ô chọn ngày trả thực tế, đổi ngày sẽ tự gọi hàm CapNhatTienPhat để tính lại phạt trễ hạn
        dtpNgayTra.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        dtpNgayTra.Location = new System.Drawing.Point(16, 160);
        dtpNgayTra.Name = "dtpNgayTra";
        dtpNgayTra.Size = new System.Drawing.Size(146, 25);
        dtpNgayTra.TabIndex = 2;
        dtpNgayTra.ValueChanged += CapNhatTienPhat;

        // Nhãn chữ "Xử lý"
        lblXuLy.AutoSize = true;
        lblXuLy.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblXuLy.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblXuLy.Location = new System.Drawing.Point(178, 140);
        lblXuLy.Name = "lblXuLy";
        lblXuLy.Size = new System.Drawing.Size(32, 15);
        lblXuLy.Text = "Xử lý";

        // Ô chọn hình thức xử lý (Trả sách hoặc Làm mất sách), đổi mục sẽ tự gọi lại CapNhatTienPhat
        cboTinhTrangSach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboTinhTrangSach.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboTinhTrangSach.Location = new System.Drawing.Point(178, 160);
        cboTinhTrangSach.Name = "cboTinhTrangSach";
        cboTinhTrangSach.Size = new System.Drawing.Size(146, 25);
        cboTinhTrangSach.TabIndex = 3;
        cboTinhTrangSach.SelectedIndexChanged += CapNhatTienPhat;

        // Dòng chữ màu đỏ hiện số tiền phạt dự kiến (trễ hạn hoặc đền mất sách)
        lblTongTienPhat.AutoSize = true;
        lblTongTienPhat.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
        lblTongTienPhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblTongTienPhat.Location = new System.Drawing.Point(16, 196);
        lblTongTienPhat.Name = "lblTongTienPhat";
        lblTongTienPhat.Size = new System.Drawing.Size(102, 15);
        lblTongTienPhat.Text = "Tiền phạt: 0đ";

        // Nút xác nhận nhận trả sách hoặc báo mất (màu xanh lá), gắn sự kiện btnNhanTraSach_Click
        btnNhanTraSach.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
        btnNhanTraSach.FlatAppearance.BorderSize = 0;
        btnNhanTraSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnNhanTraSach.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        btnNhanTraSach.ForeColor = System.Drawing.Color.White;
        btnNhanTraSach.Location = new System.Drawing.Point(16, 222);
        btnNhanTraSach.Name = "btnNhanTraSach";
        btnNhanTraSach.Size = new System.Drawing.Size(344, 38);
        btnNhanTraSach.TabIndex = 4;
        btnNhanTraSach.Text = "NHẬN TRẢ SÁCH";
        btnNhanTraSach.Cursor = System.Windows.Forms.Cursors.Hand;
        btnNhanTraSach.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(21, 128, 61);
        btnNhanTraSach.UseVisualStyleBackColor = false;
        btnNhanTraSach.Click += btnNhanTraSach_Click;
    
        // Thêm toàn bộ các control trả sách vào bên trong khung grpTraSach
        grpTraSach.Controls.Add(lblMaPhieu);
        grpTraSach.Controls.Add(txtMaPhieu);
        grpTraSach.Controls.Add(lblMaVachTra);
        grpTraSach.Controls.Add(txtMaVachTra);
        grpTraSach.Controls.Add(lblNgayTra);
        grpTraSach.Controls.Add(dtpNgayTra);
        grpTraSach.Controls.Add(lblXuLy);
        grpTraSach.Controls.Add(cboTinhTrangSach);
        grpTraSach.Controls.Add(lblTongTienPhat);
        grpTraSach.Controls.Add(btnNhanTraSach);
    
        // Khung lớn bên phải: chứa thanh tìm kiếm, bộ lọc và bảng danh sách phiếu mượn trả (neo 4 cạnh để tự giãn khi phóng to)
        grpDanhSach.BackColor = System.Drawing.Color.White;
        grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        grpDanhSach.Location = new System.Drawing.Point(408, 12);
        grpDanhSach.Name = "grpDanhSach";
        grpDanhSach.Size = new System.Drawing.Size(576, 628);
        grpDanhSach.Anchor =
            System.Windows.Forms.AnchorStyles.Top |
            System.Windows.Forms.AnchorStyles.Bottom |
            System.Windows.Forms.AnchorStyles.Left |
            System.Windows.Forms.AnchorStyles.Right;
        Size = new System.Drawing.Size(1000, 664);
        grpDanhSach.TabIndex = 2;
        grpDanhSach.TabStop = false;
        
        //khung tự động giãn ra khi phóng to màn hình
        grpDanhSach.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                             System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        grpDanhSach.Text = "DANH SÁCH PHIẾU MƯỢN, TRẢ";

        // Dòng thống kê nhanh góc trên phải hiện tổng phiếu đang mượn và quá hạn
        lblThongKeNho.AutoSize = false;
        lblThongKeNho.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblThongKeNho.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        lblThongKeNho.Location = new System.Drawing.Point(340, 18);
        lblThongKeNho.Name = "lblThongKeNho";
        lblThongKeNho.Size = new System.Drawing.Size(220, 20);
        lblThongKeNho.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblThongKeNho.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        lblThongKeNho.Text = "0 phiếu đang mượn, 0 quá hạn";

        // Ô nhập từ khóa tìm kiếm (tự giãn ngang theo chiều rộng khung)
        txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtTimKiem.Location = new System.Drawing.Point(16, 42);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tìm mã phiếu, độc giả, tên sách";
        txtTimKiem.Anchor =
            System.Windows.Forms.AnchorStyles.Top |
            System.Windows.Forms.AnchorStyles.Left;
        txtTimKiem.Size = new System.Drawing.Size(270, 25);
        txtTimKiem.TabIndex = 0;

        // Ô sổ xuống lọc danh sách theo trạng thái (Tất cả, Đang mượn, Đã trả, Làm mất)
        cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboLocTrangThai.Location = new System.Drawing.Point(282, 42);
        cboLocTrangThai.Name = "cboLocTrangThai";
        cboLocTrangThai.Size = new System.Drawing.Size(175, 25);
        cboLocTrangThai.Anchor =
            System.Windows.Forms.AnchorStyles.Top |
            System.Windows.Forms.AnchorStyles.Left;
        cboLocTrangThai.TabIndex = 1;

        // Nút bấm thực hiện lọc danh sách theo từ khóa và trạng thái
        btnTimKiem.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
        btnTimKiem.Anchor =
            System.Windows.Forms.AnchorStyles.Top |
            System.Windows.Forms.AnchorStyles.Left;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
        btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
        btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        btnTimKiem.ForeColor = System.Drawing.Color.White;
        btnTimKiem.Location = new System.Drawing.Point(463, 40);
        btnTimKiem.Size = new System.Drawing.Size(105, 32);
        btnTimKiem.TabIndex = 2;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;

        // Dòng ghi chú nhỏ giải thích cột Ngày trả nằm ngay phía trên bảng
        lblGhiChuNgayTra.AutoSize = true;
        lblGhiChuNgayTra.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblGhiChuNgayTra.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        lblGhiChuNgayTra.Location = new System.Drawing.Point(16, 75);
        lblGhiChuNgayTra.Name = "lblGhiChuNgayTra";
        lblGhiChuNgayTra.Size = new System.Drawing.Size(173, 15);
        lblGhiChuNgayTra.Text = "Ngày trả sẽ được ghi nhận khi nhận sách";

        // Bảng DataGridView hiển thị danh sách phiếu mượn trả: chỉ đọc, chọn nguyên dòng, cột tự giãn đầy khung và gắn sự kiện click, tô màu
        dgvDanhSach.AllowUserToAddRows = false;
        dgvDanhSach.AllowUserToDeleteRows = false;
        dgvDanhSach.RowTemplate.Height = 34;
        dgvDanhSach.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
        
        // ép các cột tự động chia đều khoảng cách lấp đầy bảng
        dgvDanhSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dgvDanhSach.BackgroundColor = System.Drawing.Color.White;
        dgvDanhSach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        dgvDanhSach.ColumnHeadersHeight = 35;
        dgvDanhSach.Location = new System.Drawing.Point(16, 98);
        dgvDanhSach.MultiSelect = false;
        dgvDanhSach.Name = " dgvDanhSach";
        dgvDanhSach.ReadOnly = true;
        dgvDanhSach.RowHeadersVisible = false;
        dgvDanhSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvDanhSach.Size = new System.Drawing.Size(573, 485);
        dgvDanhSach.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                             System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dgvDanhSach.ColumnHeadersHeight = 40;
        
        //Chặn user kéo chỉnh chiều cao tiêu đề cột để tránh vỡ layout
        dgvDanhSach.ColumnHeadersHeightSizeMode =
            System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        
        //custom viền xám mảnh và đổi nền header
        dgvDanhSach.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        dgvDanhSach.ColumnHeadersDefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dgvDanhSach.ColumnHeadersHeight = 38;
        dgvDanhSach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        dgvDanhSach.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
        dgvDanhSach.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
        dgvDanhSach.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dgvDanhSach.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        dgvDanhSach.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        dgvDanhSach.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        dgvDanhSach.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(239, 246, 255);
        dgvDanhSach.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        dgvDanhSach.TabIndex = 3;
        
        //nối sự kiện bấm vào dòng và sự kiện tự động tô màu chữ trạng thái
        dgvDanhSach.CellClick += dgvDanhSach_CellClick;
        dgvDanhSach.CellFormatting += dgvDanhSach_CellFormatting;

        // Dòng chân trang góc dưới trái hiện tổng số phiếu đang hiển thị và mã phiếu vừa chọn
        lblChanTrang.AutoSize = true;
        lblChanTrang.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblChanTrang.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        lblChanTrang.Location = new System.Drawing.Point(16, 593);
        lblChanTrang.Name = "lblChanTrang";
        lblChanTrang.Size = new System.Drawing.Size(215, 15);
        lblChanTrang.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        lblChanTrang.Text = "Hiển thị 0 phiếu | đã chọn: chưa chọn";
        
        // Thêm toàn bộ thanh tìm kiếm, bảng và chân trang vào bên trong khung grpDanhSach
        grpDanhSach.Controls.Add(lblThongKeNho);
        grpDanhSach.Controls.Add(txtTimKiem);
        grpDanhSach.Controls.Add(cboLocTrangThai);
        grpDanhSach.Controls.Add(btnTimKiem);
        grpDanhSach.Controls.Add(lblGhiChuNgayTra);
        grpDanhSach.Controls.Add(dgvDanhSach);
        grpDanhSach.Controls.Add(lblChanTrang);

        // Cấu hình màn hình chính ucBorrow kích thước chuẩn 1000x700, gắn sự kiện ucBorrow_Load và vẽ lại toàn bộ layout
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        
        //Tắt tự động phóng to windows để giữ đúng form 1000x700
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
       // Controls.Add(lblTieuDe);
        Controls.Add(grpLapPhieu);
        Controls.Add(grpTraSach);
        Controls.Add(grpDanhSach);
        Name = "ucBorrow";
        Size = new System.Drawing.Size(1000, 664);
        Load += ucBorrow_Load;
        grpLapPhieu.ResumeLayout(false);
        grpLapPhieu.PerformLayout();
        grpTraSach.ResumeLayout(false);
        grpTraSach.PerformLayout();
        grpDanhSach.ResumeLayout(false);
        grpDanhSach.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDanhSach).EndInit();
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label lblTieuDe;
    private System.Windows.Forms.GroupBox grpLapPhieu;
    private System.Windows.Forms.Label lblDocGia;
    private System.Windows.Forms.ComboBox cboDocGia;
    private System.Windows.Forms.Label lblMaVach;
    private System.Windows.Forms.TextBox txtMaVach;
    private System.Windows.Forms.Label lblNgayMuon;
    private System.Windows.Forms.DateTimePicker dtpNgayMuon;
    private System.Windows.Forms.Label lblNgayHanTra;
    private System.Windows.Forms.DateTimePicker dtpNgayHanTra;
    private System.Windows.Forms.Label lblTieuDeTrangThai;
    private System.Windows.Forms.Label lblTrangThaiMuon;
    private System.Windows.Forms.Button btnChoMuon;
    private System.Windows.Forms.GroupBox grpTraSach;
    private System.Windows.Forms.Label lblMaPhieu;
    private System.Windows.Forms.TextBox txtMaPhieu;
    private System.Windows.Forms.Label lblMaVachTra;
    private System.Windows.Forms.TextBox txtMaVachTra;
    private System.Windows.Forms.Label lblNgayTra;
    private System.Windows.Forms.DateTimePicker dtpNgayTra;
    private System.Windows.Forms.Label lblXuLy;
    private System.Windows.Forms.ComboBox cboTinhTrangSach;
    private System.Windows.Forms.Label lblTongTienPhat;
    private System.Windows.Forms.Button btnNhanTraSach;
    private System.Windows.Forms.GroupBox grpDanhSach;
    private System.Windows.Forms.Label lblThongKeNho;
    private System.Windows.Forms.TextBox txtTimKiem;
    private System.Windows.Forms.ComboBox cboLocTrangThai;
    private System.Windows.Forms.Button btnTimKiem;
    private System.Windows.Forms.Label lblGhiChuNgayTra;
    private System.Windows.Forms.DataGridView dgvDanhSach;
    private System.Windows.Forms.Label lblChanTrang;
    
    


}
