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

        //lblTieuDe
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
        lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        lblTieuDe.Location = new System.Drawing.Point(20, 15);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new System.Drawing.Size(210, 28);
        lblTieuDe.Text = "MƯỢN - TRẢ SÁCH";

        //grpLapPhieu
        grpLapPhieu.BackColor = System.Drawing.Color.White;
        grpLapPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        grpLapPhieu.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        grpLapPhieu.Location = new System.Drawing.Point(20, 55);
        grpLapPhieu.TabIndex = 0;
        grpLapPhieu.TabStop = false;
        grpLapPhieu.Size = new System.Drawing.Size(345, 315);
        grpLapPhieu.Text = "LẬP PHIẾU MƯỢN SÁCH";

        //lblDocGia
        lblDocGia.AutoSize = true;
        lblDocGia.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblDocGia.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblDocGia.Location = new System.Drawing.Point(16, 30);
        lblDocGia.Name = "lblDocGia";
        lblDocGia.Size = new System.Drawing.Size(125, 15);
        lblDocGia.Text = "Chọn độc giả";

        //cboDocGia
        cboDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboDocGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboDocGia.Location = new System.Drawing.Point(16, 50);
        cboDocGia.Name = "cboDocGia";
        cboDocGia.Size = new System.Drawing.Size(308, 25);
        cboDocGia.TabIndex = 0;
        cboDocGia.SelectedIndexChanged += cboDocGia_SelectedIndexChanged;

        //lblMaVach
        lblMaVach.AutoSize = true;
        lblMaVach.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblMaVach.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblMaVach.Location = new System.Drawing.Point(16, 86);
        lblMaVach.Name = "lblMaVach";
        lblMaVach.Size = new System.Drawing.Size(242, 15);
        lblMaVach.Text = "Quét mã vạch sách";

        //txtMaVach
        txtMaVach.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtMaVach.Location = new System.Drawing.Point(16, 106);
        txtMaVach.Name = "txtMaVach";
        txtMaVach.PlaceholderText = "SACH01_1";
        txtMaVach.Size = new System.Drawing.Size(308, 25);
        txtMaVach.TabIndex = 1;

        //lblNgayMuon
        lblNgayMuon.AutoSize = true;
        lblNgayMuon.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblNgayMuon.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblNgayMuon.Location = new System.Drawing.Point(16, 142);
        lblNgayMuon.Name = "lblNgayMuon";
        lblNgayMuon.Size = new System.Drawing.Size(68, 15);
        lblNgayMuon.Text = "Ngày mượn";

        //dtpNgayMuon
        dtpNgayMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        dtpNgayMuon.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        dtpNgayMuon.Location = new System.Drawing.Point(16, 162);
        dtpNgayMuon.Name = "dtpNgayMuon";
        dtpNgayMuon.Size = new System.Drawing.Size(146, 25);
        dtpNgayMuon.TabIndex = 2;

        //lblNgayHanTra
        lblNgayHanTra.AutoSize = true;
        lblNgayHanTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblNgayHanTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblNgayHanTra.Location = new System.Drawing.Point(178, 142);
        lblNgayHanTra.Name = "lblNgayHanTra";
        lblNgayHanTra.Size = new System.Drawing.Size(75, 15);
        lblNgayHanTra.Text = "Ngày Hạn Trả";

        //dtpNgayHanTra
        dtpNgayHanTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        dtpNgayHanTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        dtpNgayHanTra.Location = new System.Drawing.Point(178, 162);
        dtpNgayHanTra.Name = "dtpNgayHanTra";
        dtpNgayHanTra.Size = new System.Drawing.Size(146, 25);
        dtpNgayHanTra.TabIndex = 3;

        //lblTieuDeTrangThai
        lblTieuDeTrangThai.AutoSize = true;
        lblTieuDeTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblTieuDeTrangThai.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblTieuDeTrangThai.Location = new System.Drawing.Point(16, 198);
        lblTieuDeTrangThai.Name = "lblTieuDeTrangThai";
        lblTieuDeTrangThai.Size = new System.Drawing.Size(93, 15);
        lblTieuDeTrangThai.Text = "Trạng thái mượn";

        //lblTrangThaiMuon
        lblTrangThaiMuon.AutoSize = false;
        lblTrangThaiMuon.BackColor = System.Drawing.Color.FromArgb(220, 252, 231);
        lblTrangThaiMuon.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblTrangThaiMuon.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
        lblTrangThaiMuon.Location = new System.Drawing.Point(16, 218);
        lblTrangThaiMuon.Name = "lblTrangThaiMuon";
        lblTrangThaiMuon.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
        lblTrangThaiMuon.Size = new System.Drawing.Size(313, 28);
        lblTrangThaiMuon.Text = "Đang mượn: 0/3 cuốn | Nợ phạt: 0 đ | Thẻ: Hợp lệ";

        //btnChoMuon
        btnChoMuon.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        btnChoMuon.FlatAppearance.BorderSize = 0;
        btnChoMuon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnChoMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        btnChoMuon.ForeColor = System.Drawing.Color.White;
        btnChoMuon.Location = new System.Drawing.Point(16, 256);
        btnChoMuon.Name = "btnChoMuon";
        btnChoMuon.Size = new System.Drawing.Size(145, 38);
        btnChoMuon.TabIndex = 4;
        btnChoMuon.Text = "Cho mượn sách";
        btnChoMuon.UseVisualStyleBackColor = false;
        btnChoMuon.Click += btnChoMuon_Click;

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

        //grpTraSach xếp dọc Mã phiếu và Mã vạch
        grpTraSach.BackColor = System.Drawing.Color.White;
        grpTraSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        grpTraSach.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        grpTraSach.Location = new System.Drawing.Point(20, 382);
        grpTraSach.Size = new System.Drawing.Size(345, 300);
        grpTraSach.TabIndex = 1;
        grpTraSach.TabStop = false;
        grpTraSach.Text = "XỬ LÝ TRẢ SÁCH VÀ BÁO MẤT";

        //lblMaPhieu
        lblMaPhieu.AutoSize = true;
        lblMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblMaPhieu.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblMaPhieu.Location = new System.Drawing.Point(16, 28);
        lblMaPhieu.Name = "lblMaPhieu";
        lblMaPhieu.Size = new System.Drawing.Size(113, 15);
        lblMaPhieu.Text = "Mã phiếu";

        //txtMaPhieu
        txtMaPhieu.BackColor = System.Drawing.Color.White;
        txtMaPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtMaPhieu.Location = new System.Drawing.Point(16, 48);
        txtMaPhieu.Name = "txtMaPhieu";
        txtMaPhieu.Size = new System.Drawing.Size(308, 25);
        txtMaPhieu.TabIndex = 0;

        //lblMaVachTra
        lblMaVachTra.AutoSize = true;
        lblMaVachTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblMaVachTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblMaVachTra.Location = new System.Drawing.Point(16, 84);
        lblMaVachTra.Name = "lblMaVachTra";
        lblMaVachTra.Size = new System.Drawing.Size(99, 15);
        lblMaVachTra.Text = "Mã Vạch (CopyID)";

        //txtMaVachTra
        txtMaVachTra.BackColor = System.Drawing.Color.White;
        txtMaVachTra.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtMaVachTra.Location = new System.Drawing.Point(16, 104);
        txtMaVachTra.Name = "txtMaVachTra";
        txtMaVachTra.ReadOnly = true;
        txtMaVachTra.Size = new System.Drawing.Size(308, 25);
        txtMaVachTra.TabIndex = 1;

        //lblNgayTra
        lblNgayTra.AutoSize = true;
        lblNgayTra.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblNgayTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblNgayTra.Location = new System.Drawing.Point(16, 140);
        lblNgayTra.Name = "lblNgayTra";
        lblNgayTra.Size = new System.Drawing.Size(51, 15);
        lblNgayTra.Text = "Ngày trả";

        //dtpNgayTra
        dtpNgayTra.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        dtpNgayTra.Location = new System.Drawing.Point(16, 160);
        dtpNgayTra.Name = "dtpNgayTra";
        dtpNgayTra.Size = new System.Drawing.Size(146, 25);
        dtpNgayTra.TabIndex = 2;
        dtpNgayTra.ValueChanged += CapNhatTienPhat;

        //lblXuLy
        lblXuLy.AutoSize = true;
        lblXuLy.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblXuLy.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        lblXuLy.Location = new System.Drawing.Point(178, 140);
        lblXuLy.Name = "lblXuLy";
        lblXuLy.Size = new System.Drawing.Size(32, 15);
        lblXuLy.Text = "Xử lý";

        //cboTinhTrangSach
        cboTinhTrangSach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboTinhTrangSach.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboTinhTrangSach.Location = new System.Drawing.Point(178, 160);
        cboTinhTrangSach.Name = "cboTinhTrangSach";
        cboTinhTrangSach.Size = new System.Drawing.Size(146, 25);
        cboTinhTrangSach.TabIndex = 3;
        cboTinhTrangSach.SelectedIndexChanged += CapNhatTienPhat;

        //lblTongTienPhat
        lblTongTienPhat.AutoSize = true;
        lblTongTienPhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblTongTienPhat.Location = new System.Drawing.Point(16, 196);
        lblTongTienPhat.Name = "lblTongTienPhat";
        lblTongTienPhat.Size = new System.Drawing.Size(102, 15);
        lblTongTienPhat.Text = "Tiền phạt: 0đ";

        //btnNhanTraSach
        btnNhanTraSach.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
        btnNhanTraSach.FlatAppearance.BorderSize = 0;
        btnNhanTraSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnNhanTraSach.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        btnNhanTraSach.ForeColor = System.Drawing.Color.White;
        btnNhanTraSach.Location = new System.Drawing.Point(16, 222);
        btnNhanTraSach.Name = "btnNhanTraSach";
        btnNhanTraSach.Size = new System.Drawing.Size(145, 38);
        btnNhanTraSach.TabIndex = 4;
        btnNhanTraSach.Text = "NHẬN TRẢ SÁCH";
        btnNhanTraSach.UseVisualStyleBackColor = false;
        btnNhanTraSach.Click += btnNhanTraSach_Click;
    
        //grpTraSach
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
    
        //grpDanhSach
        grpDanhSach.BackColor = System.Drawing.Color.White;
        grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        grpDanhSach.Location = new System.Drawing.Point(380, 52);
        grpDanhSach.Name = "grpDanhSach";
        grpDanhSach.Size = new System.Drawing.Size(600, 628);
        grpDanhSach.TabIndex = 2;
        grpDanhSach.TabStop = false;
        
        //khung tự động giãn ra khi phóng to màn hình
        grpDanhSach.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                             System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        grpDanhSach.Text = "DANH SÁCH PHIẾU MƯỢN, TRẢ";

        //lblThongKeNho
        lblThongKeNho.AutoSize = false;
        lblThongKeNho.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblThongKeNho.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        lblThongKeNho.Location = new System.Drawing.Point(360, 18);
        lblThongKeNho.Name = "lblThongKeNho";
        lblThongKeNho.Size = new System.Drawing.Size(220, 20);
        lblThongKeNho.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        lblThongKeNho.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        lblThongKeNho.Text = "0 phiếu đang mượn, 0 quá hạn";

        //txtTimKiem
        txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtTimKiem.Location = new System.Drawing.Point(16, 42);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tìm mã phiếu, độc giả, tên sách";
        txtTimKiem.Size = new System.Drawing.Size(270, 25);
        txtTimKiem.TabIndex = 0;

        //cboLocTrangThai
        cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboLocTrangThai.Location = new System.Drawing.Point(296, 42);
        cboLocTrangThai.Name = "cboLocTrangThai";
        cboLocTrangThai.Size = new System.Drawing.Size(175, 25);
        cboLocTrangThai.TabIndex = 1;

        //btnTimkiem
        btnTimKiem.BackColor = System.Drawing.Color.White;
        btnTimKiem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
        btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        btnTimKiem.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        btnTimKiem.Location = new System.Drawing.Point(481, 40);
        btnTimKiem.Size = new System.Drawing.Size(108, 29);
        btnTimKiem.TabIndex = 2;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;

        //lblGhiChuNgayTra
        lblGhiChuNgayTra.AutoSize = true;
        lblGhiChuNgayTra.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        lblGhiChuNgayTra.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        lblGhiChuNgayTra.Location = new System.Drawing.Point(16, 75);
        lblGhiChuNgayTra.Name = "lblGhiChuNgayTra";
        lblGhiChuNgayTra.Size = new System.Drawing.Size(173, 15);
        lblGhiChuNgayTra.Text = "Ngày trả sẽ được ghi nhận khi nhận sách";

        //dgvDanhSach
        dgvDanhSach.AllowUserToAddRows = false;
        dgvDanhSach.AllowUserToDeleteRows = false;
        
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

        //lblChanTrang
        lblChanTrang.AutoSize = true;
        lblChanTrang.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblChanTrang.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        lblChanTrang.Location = new System.Drawing.Point(16, 593);
        lblChanTrang.Name = "lblChanTrang";
        lblChanTrang.Size = new System.Drawing.Size(215, 15);
        lblChanTrang.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        lblChanTrang.Text = "Hiển thị 0 phiếu | đã chọn: chưa chọn";

        grpDanhSach.Controls.Add(lblThongKeNho);
        grpDanhSach.Controls.Add(txtTimKiem);
        grpDanhSach.Controls.Add(cboLocTrangThai);
        grpDanhSach.Controls.Add(btnTimKiem);
        grpDanhSach.Controls.Add(lblGhiChuNgayTra);
        grpDanhSach.Controls.Add(dgvDanhSach);
        grpDanhSach.Controls.Add(lblChanTrang);

        //ucBorrow chuẩn kích thước 1000x700
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        
        //Tắt tự động phóng to windows để giữ đúng form 1000x700
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
        Controls.Add(lblTieuDe);
        Controls.Add(grpLapPhieu);
        Controls.Add(grpTraSach);
        Controls.Add(grpDanhSach);
        Name = "ucBorrow";
        Size = new System.Drawing.Size(1000, 700);
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
