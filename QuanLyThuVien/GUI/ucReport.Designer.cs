namespace QuanLyThuVien;

partial class ucReport
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
        System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dgvDefaultStyle = new System.Windows.Forms.DataGridViewCellStyle();
        this.pnlDieuKienBorder = new System.Windows.Forms.Panel();
        this.pnlDieuKien = new System.Windows.Forms.Panel();
        this.lblDieuKienTitle = new System.Windows.Forms.Label();
        this.lblCapNhat = new System.Windows.Forms.Label();
        this.lblTuNgay = new System.Windows.Forms.Label();
        this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
        this.lblDenNgay = new System.Windows.Forms.Label();
        this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
        this.lblLoaiBaoCao = new System.Windows.Forms.Label();
        this.cboLoaiBaoCao = new System.Windows.Forms.ComboBox();
        this.btnXemBaoCao = new System.Windows.Forms.Button();
        this.btnXuatExcel = new System.Windows.Forms.Button();
        this.pnlKpiTongSachBorder = new System.Windows.Forms.Panel();
        this.pnlKpiTongSach = new System.Windows.Forms.Panel();
        this.lblKpiTongSachTitle = new System.Windows.Forms.Label();
        this.lblKpiTongSachVal = new System.Windows.Forms.Label();
        this.lblKpiTongSachSub = new System.Windows.Forms.Label();
        this.pnlKpiDocGiaBorder = new System.Windows.Forms.Panel();
        this.pnlKpiDocGia = new System.Windows.Forms.Panel();
        this.lblKpiDocGiaTitle = new System.Windows.Forms.Label();
        this.lblKpiDocGiaVal = new System.Windows.Forms.Label();
        this.lblKpiDocGiaSub = new System.Windows.Forms.Label();
        this.pnlKpiDangMuonBorder = new System.Windows.Forms.Panel();
        this.pnlKpiDangMuon = new System.Windows.Forms.Panel();
        this.lblKpiDangMuonTitle = new System.Windows.Forms.Label();
        this.lblKpiDangMuonVal = new System.Windows.Forms.Label();
        this.lblKpiDangMuonSub = new System.Windows.Forms.Label();
        this.pnlKpiQuaHanBorder = new System.Windows.Forms.Panel();
        this.pnlKpiQuaHan = new System.Windows.Forms.Panel();
        this.lblKpiQuaHanTitle = new System.Windows.Forms.Label();
        this.lblKpiQuaHanVal = new System.Windows.Forms.Label();
        this.lblKpiQuaHanSub = new System.Windows.Forms.Label();
        this.pnlBieuDoBorder = new System.Windows.Forms.Panel();
        this.pnlBieuDo = new System.Windows.Forms.Panel();
        this.lblBieuDoTitle = new System.Windows.Forms.Label();
        this.lblBieuDoSub = new System.Windows.Forms.Label();
        this.pnlChartCanvas = new System.Windows.Forms.Panel();
        this.pnlBieuDoFooter = new System.Windows.Forms.Panel();
        this.lblLuotMuonKy = new System.Windows.Forms.Label();
        this.lblDaTra = new System.Windows.Forms.Label();
        this.pnlTopSachBorder = new System.Windows.Forms.Panel();
        this.pnlTopSach = new System.Windows.Forms.Panel();
        this.lblTopSachTitle = new System.Windows.Forms.Label();
        this.lblTopSachSub = new System.Windows.Forms.Label();
        this.dgvSachMuon = new System.Windows.Forms.DataGridView();
        this.colMaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colTenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colLuotMuon = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colChuaTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.pnlTopSachFooterLine = new System.Windows.Forms.Panel();
        this.lblTongDauSach = new System.Windows.Forms.Label();
        this.lblTongLuotChuaTra = new System.Windows.Forms.Label();
        this.pnlDieuKienBorder.SuspendLayout();
        this.pnlDieuKien.SuspendLayout();
        this.pnlKpiTongSachBorder.SuspendLayout();
        this.pnlKpiTongSach.SuspendLayout();
        this.pnlKpiDocGiaBorder.SuspendLayout();
        this.pnlKpiDocGia.SuspendLayout();
        this.pnlKpiDangMuonBorder.SuspendLayout();
        this.pnlKpiDangMuon.SuspendLayout();
        this.pnlKpiQuaHanBorder.SuspendLayout();
        this.pnlKpiQuaHan.SuspendLayout();
        this.pnlBieuDoBorder.SuspendLayout();
        this.pnlBieuDo.SuspendLayout();
        this.pnlBieuDoFooter.SuspendLayout();
        this.pnlTopSachBorder.SuspendLayout();
        this.pnlTopSach.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvSachMuon)).BeginInit();
        this.SuspendLayout();
        // 
        // pnlDieuKienBorder
        // 
        this.pnlDieuKienBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlDieuKienBorder.Controls.Add(this.pnlDieuKien);
        this.pnlDieuKienBorder.Location = new System.Drawing.Point(16, 14);
        this.pnlDieuKienBorder.Name = "pnlDieuKienBorder";
        this.pnlDieuKienBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlDieuKienBorder.Size = new System.Drawing.Size(968, 104);
        this.pnlDieuKienBorder.TabIndex = 0;
        // 
        // pnlDieuKien
        // 
        this.pnlDieuKien.BackColor = System.Drawing.Color.White;
        this.pnlDieuKien.Controls.Add(this.btnXuatExcel);
        this.pnlDieuKien.Controls.Add(this.btnXemBaoCao);
        this.pnlDieuKien.Controls.Add(this.cboLoaiBaoCao);
        this.pnlDieuKien.Controls.Add(this.lblLoaiBaoCao);
        this.pnlDieuKien.Controls.Add(this.dtpDenNgay);
        this.pnlDieuKien.Controls.Add(this.lblDenNgay);
        this.pnlDieuKien.Controls.Add(this.dtpTuNgay);
        this.pnlDieuKien.Controls.Add(this.lblTuNgay);
        this.pnlDieuKien.Controls.Add(this.lblCapNhat);
        this.pnlDieuKien.Controls.Add(this.lblDieuKienTitle);
        this.pnlDieuKien.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlDieuKien.Location = new System.Drawing.Point(1, 1);
        this.pnlDieuKien.Name = "pnlDieuKien";
        this.pnlDieuKien.Size = new System.Drawing.Size(966, 102);
        this.pnlDieuKien.TabIndex = 0;
        // 
        // lblDieuKienTitle
        // 
        this.lblDieuKienTitle.AutoSize = true;
        this.lblDieuKienTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
        this.lblDieuKienTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblDieuKienTitle.Location = new System.Drawing.Point(18, 12);
        this.lblDieuKienTitle.Name = "lblDieuKienTitle";
        this.lblDieuKienTitle.Size = new System.Drawing.Size(134, 19);
        this.lblDieuKienTitle.TabIndex = 0;
        this.lblDieuKienTitle.Text = "Điều kiện báo cáo";
        // 
        // lblCapNhat
        // 
        this.lblCapNhat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblCapNhat.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblCapNhat.Location = new System.Drawing.Point(748, 13);
        this.lblCapNhat.Name = "lblCapNhat";
        this.lblCapNhat.Size = new System.Drawing.Size(200, 18);
        this.lblCapNhat.TabIndex = 1;
        this.lblCapNhat.Text = "Cập nhật: 07/11/2026";
        this.lblCapNhat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lblTuNgay
        // 
        this.lblTuNgay.AutoSize = true;
        this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblTuNgay.Location = new System.Drawing.Point(18, 38);
        this.lblTuNgay.Name = "lblTuNgay";
        this.lblTuNgay.Size = new System.Drawing.Size(53, 15);
        this.lblTuNgay.TabIndex = 2;
        this.lblTuNgay.Text = "Từ ngày";
        // 
        // dtpTuNgay
        // 
        this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
        this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dtpTuNgay.Location = new System.Drawing.Point(18, 58);
        this.dtpTuNgay.Name = "dtpTuNgay";
        this.dtpTuNgay.Size = new System.Drawing.Size(148, 25);
        this.dtpTuNgay.TabIndex = 3;
        // 
        // lblDenNgay
        // 
        this.lblDenNgay.AutoSize = true;
        this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblDenNgay.Location = new System.Drawing.Point(180, 38);
        this.lblDenNgay.Name = "lblDenNgay";
        this.lblDenNgay.Size = new System.Drawing.Size(61, 15);
        this.lblDenNgay.TabIndex = 4;
        this.lblDenNgay.Text = "Đến ngày";
        // 
        // dtpDenNgay
        // 
        this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
        this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dtpDenNgay.Location = new System.Drawing.Point(180, 58);
        this.dtpDenNgay.Name = "dtpDenNgay";
        this.dtpDenNgay.Size = new System.Drawing.Size(148, 25);
        this.dtpDenNgay.TabIndex = 5;
        // 
        // lblLoaiBaoCao
        // 
        this.lblLoaiBaoCao.AutoSize = true;
        this.lblLoaiBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblLoaiBaoCao.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblLoaiBaoCao.Location = new System.Drawing.Point(342, 38);
        this.lblLoaiBaoCao.Name = "lblLoaiBaoCao";
        this.lblLoaiBaoCao.Size = new System.Drawing.Size(78, 15);
        this.lblLoaiBaoCao.TabIndex = 6;
        this.lblLoaiBaoCao.Text = "Loại báo cáo";
        // 
        // cboLoaiBaoCao
        // 
        this.cboLoaiBaoCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboLoaiBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.System;
        this.cboLoaiBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.cboLoaiBaoCao.FormattingEnabled = true;
        this.cboLoaiBaoCao.Items.AddRange(new object[] {
            "Tổng hợp hoạt động thư viện",
            "Thống kê sách được mượn nhiều",
            "Danh sách phiếu quá hạn"});
        this.cboLoaiBaoCao.Location = new System.Drawing.Point(342, 58);
        this.cboLoaiBaoCao.Name = "cboLoaiBaoCao";
        this.cboLoaiBaoCao.Size = new System.Drawing.Size(298, 25);
        this.cboLoaiBaoCao.TabIndex = 7;
        // 
        // btnXemBaoCao
        // 
        this.btnXemBaoCao.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.btnXemBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnXemBaoCao.FlatAppearance.BorderSize = 0;
        this.btnXemBaoCao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
        this.btnXemBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnXemBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.btnXemBaoCao.ForeColor = System.Drawing.Color.White;
        this.btnXemBaoCao.Location = new System.Drawing.Point(656, 54);
        this.btnXemBaoCao.Name = "btnXemBaoCao";
        this.btnXemBaoCao.Size = new System.Drawing.Size(136, 33);
        this.btnXemBaoCao.TabIndex = 8;
        this.btnXemBaoCao.Text = "📊 Xem báo cáo";
        this.btnXemBaoCao.UseVisualStyleBackColor = false;
        // 
        // btnXuatExcel
        // 
        this.btnXuatExcel.BackColor = System.Drawing.Color.White;
        this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnXuatExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
        this.btnXuatExcel.FlatAppearance.BorderSize = 1;
        this.btnXuatExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.btnXuatExcel.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.btnXuatExcel.Location = new System.Drawing.Point(804, 54);
        this.btnXuatExcel.Name = "btnXuatExcel";
        this.btnXuatExcel.Size = new System.Drawing.Size(144, 33);
        this.btnXuatExcel.TabIndex = 9;
        this.btnXuatExcel.Text = "⬇ Xuất tệp Excel";
        this.btnXuatExcel.UseVisualStyleBackColor = false;
        // 
        // KPI Card 1: Tổng số bản sách
        // 
        this.pnlKpiTongSachBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlKpiTongSachBorder.Controls.Add(this.pnlKpiTongSach);
        this.pnlKpiTongSachBorder.Location = new System.Drawing.Point(16, 132);
        this.pnlKpiTongSachBorder.Name = "pnlKpiTongSachBorder";
        this.pnlKpiTongSachBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlKpiTongSachBorder.Size = new System.Drawing.Size(231, 92);
        this.pnlKpiTongSachBorder.TabIndex = 1;
        // 
        // pnlKpiTongSach
        // 
        this.pnlKpiTongSach.BackColor = System.Drawing.Color.White;
        this.pnlKpiTongSach.Controls.Add(this.lblKpiTongSachSub);
        this.pnlKpiTongSach.Controls.Add(this.lblKpiTongSachVal);
        this.pnlKpiTongSach.Controls.Add(this.lblKpiTongSachTitle);
        this.pnlKpiTongSach.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlKpiTongSach.Location = new System.Drawing.Point(1, 1);
        this.pnlKpiTongSach.Name = "pnlKpiTongSach";
        this.pnlKpiTongSach.Size = new System.Drawing.Size(229, 90);
        this.pnlKpiTongSach.TabIndex = 0;
        // 
        // lblKpiTongSachTitle
        // 
        this.lblKpiTongSachTitle.AutoSize = true;
        this.lblKpiTongSachTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblKpiTongSachTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiTongSachTitle.Location = new System.Drawing.Point(16, 12);
        this.lblKpiTongSachTitle.Name = "lblKpiTongSachTitle";
        this.lblKpiTongSachTitle.Size = new System.Drawing.Size(106, 15);
        this.lblKpiTongSachTitle.TabIndex = 0;
        this.lblKpiTongSachTitle.Text = "Tổng số bản sách";
        // 
        // lblKpiTongSachVal
        // 
        this.lblKpiTongSachVal.AutoSize = true;
        this.lblKpiTongSachVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblKpiTongSachVal.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.lblKpiTongSachVal.Location = new System.Drawing.Point(14, 31);
        this.lblKpiTongSachVal.Name = "lblKpiTongSachVal";
        this.lblKpiTongSachVal.Size = new System.Drawing.Size(58, 32);
        this.lblKpiTongSachVal.TabIndex = 1;
        this.lblKpiTongSachVal.Text = "120";
        // 
        // lblKpiTongSachSub
        // 
        this.lblKpiTongSachSub.AutoSize = true;
        this.lblKpiTongSachSub.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblKpiTongSachSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiTongSachSub.Location = new System.Drawing.Point(16, 66);
        this.lblKpiTongSachSub.Name = "lblKpiTongSachSub";
        this.lblKpiTongSachSub.Size = new System.Drawing.Size(166, 15);
        this.lblKpiTongSachSub.TabIndex = 2;
        this.lblKpiTongSachSub.Text = "8 đầu sách • 112 bản sẵn có";
        // 
        // KPI Card 2: Độc giả đã đăng ký
        // 
        this.pnlKpiDocGiaBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlKpiDocGiaBorder.Controls.Add(this.pnlKpiDocGia);
        this.pnlKpiDocGiaBorder.Location = new System.Drawing.Point(261, 132);
        this.pnlKpiDocGiaBorder.Name = "pnlKpiDocGiaBorder";
        this.pnlKpiDocGiaBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlKpiDocGiaBorder.Size = new System.Drawing.Size(232, 92);
        this.pnlKpiDocGiaBorder.TabIndex = 2;
        // 
        // pnlKpiDocGia
        // 
        this.pnlKpiDocGia.BackColor = System.Drawing.Color.White;
        this.pnlKpiDocGia.Controls.Add(this.lblKpiDocGiaSub);
        this.pnlKpiDocGia.Controls.Add(this.lblKpiDocGiaVal);
        this.pnlKpiDocGia.Controls.Add(this.lblKpiDocGiaTitle);
        this.pnlKpiDocGia.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlKpiDocGia.Location = new System.Drawing.Point(1, 1);
        this.pnlKpiDocGia.Name = "pnlKpiDocGia";
        this.pnlKpiDocGia.Size = new System.Drawing.Size(230, 90);
        this.pnlKpiDocGia.TabIndex = 0;
        // 
        // lblKpiDocGiaTitle
        // 
        this.lblKpiDocGiaTitle.AutoSize = true;
        this.lblKpiDocGiaTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblKpiDocGiaTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiDocGiaTitle.Location = new System.Drawing.Point(16, 12);
        this.lblKpiDocGiaTitle.Name = "lblKpiDocGiaTitle";
        this.lblKpiDocGiaTitle.Size = new System.Drawing.Size(116, 15);
        this.lblKpiDocGiaTitle.TabIndex = 0;
        this.lblKpiDocGiaTitle.Text = "Độc giả đã đăng ký";
        // 
        // lblKpiDocGiaVal
        // 
        this.lblKpiDocGiaVal.AutoSize = true;
        this.lblKpiDocGiaVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblKpiDocGiaVal.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblKpiDocGiaVal.Location = new System.Drawing.Point(14, 31);
        this.lblKpiDocGiaVal.Name = "lblKpiDocGiaVal";
        this.lblKpiDocGiaVal.Size = new System.Drawing.Size(28, 32);
        this.lblKpiDocGiaVal.TabIndex = 1;
        this.lblKpiDocGiaVal.Text = "5";
        // 
        // lblKpiDocGiaSub
        // 
        this.lblKpiDocGiaSub.AutoSize = true;
        this.lblKpiDocGiaSub.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblKpiDocGiaSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiDocGiaSub.Location = new System.Drawing.Point(16, 66);
        this.lblKpiDocGiaSub.Name = "lblKpiDocGiaSub";
        this.lblKpiDocGiaSub.Size = new System.Drawing.Size(166, 15);
        this.lblKpiDocGiaSub.TabIndex = 2;
        this.lblKpiDocGiaSub.Text = "4 thẻ hoạt động • 1 thẻ khóa";
        // 
        // KPI Card 3: Phiếu đang mượn
        // 
        this.pnlKpiDangMuonBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlKpiDangMuonBorder.Controls.Add(this.pnlKpiDangMuon);
        this.pnlKpiDangMuonBorder.Location = new System.Drawing.Point(507, 132);
        this.pnlKpiDangMuonBorder.Name = "pnlKpiDangMuonBorder";
        this.pnlKpiDangMuonBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlKpiDangMuonBorder.Size = new System.Drawing.Size(232, 92);
        this.pnlKpiDangMuonBorder.TabIndex = 3;
        // 
        // pnlKpiDangMuon
        // 
        this.pnlKpiDangMuon.BackColor = System.Drawing.Color.White;
        this.pnlKpiDangMuon.Controls.Add(this.lblKpiDangMuonSub);
        this.pnlKpiDangMuon.Controls.Add(this.lblKpiDangMuonVal);
        this.pnlKpiDangMuon.Controls.Add(this.lblKpiDangMuonTitle);
        this.pnlKpiDangMuon.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlKpiDangMuon.Location = new System.Drawing.Point(1, 1);
        this.pnlKpiDangMuon.Name = "pnlKpiDangMuon";
        this.pnlKpiDangMuon.Size = new System.Drawing.Size(230, 90);
        this.pnlKpiDangMuon.TabIndex = 0;
        // 
        // lblKpiDangMuonTitle
        // 
        this.lblKpiDangMuonTitle.AutoSize = true;
        this.lblKpiDangMuonTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblKpiDangMuonTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiDangMuonTitle.Location = new System.Drawing.Point(16, 12);
        this.lblKpiDangMuonTitle.Name = "lblKpiDangMuonTitle";
        this.lblKpiDangMuonTitle.Size = new System.Drawing.Size(106, 15);
        this.lblKpiDangMuonTitle.TabIndex = 0;
        this.lblKpiDangMuonTitle.Text = "Phiếu đang mượn";
        // 
        // lblKpiDangMuonVal
        // 
        this.lblKpiDangMuonVal.AutoSize = true;
        this.lblKpiDangMuonVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblKpiDangMuonVal.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);
        this.lblKpiDangMuonVal.Location = new System.Drawing.Point(14, 31);
        this.lblKpiDangMuonVal.Name = "lblKpiDangMuonVal";
        this.lblKpiDangMuonVal.Size = new System.Drawing.Size(28, 32);
        this.lblKpiDangMuonVal.TabIndex = 1;
        this.lblKpiDangMuonVal.Text = "8";
        // 
        // lblKpiDangMuonSub
        // 
        this.lblKpiDangMuonSub.AutoSize = true;
        this.lblKpiDangMuonSub.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblKpiDangMuonSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiDangMuonSub.Location = new System.Drawing.Point(16, 66);
        this.lblKpiDangMuonSub.Name = "lblKpiDangMuonSub";
        this.lblKpiDangMuonSub.Size = new System.Drawing.Size(118, 15);
        this.lblKpiDangMuonSub.TabIndex = 2;
        this.lblKpiDangMuonSub.Text = "8 bản sách chưa trả";
        // 
        // KPI Card 4: Phiếu quá hạn
        // 
        this.pnlKpiQuaHanBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlKpiQuaHanBorder.Controls.Add(this.pnlKpiQuaHan);
        this.pnlKpiQuaHanBorder.Location = new System.Drawing.Point(753, 132);
        this.pnlKpiQuaHanBorder.Name = "pnlKpiQuaHanBorder";
        this.pnlKpiQuaHanBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlKpiQuaHanBorder.Size = new System.Drawing.Size(231, 92);
        this.pnlKpiQuaHanBorder.TabIndex = 4;
        // 
        // pnlKpiQuaHan
        // 
        this.pnlKpiQuaHan.BackColor = System.Drawing.Color.White;
        this.pnlKpiQuaHan.Controls.Add(this.lblKpiQuaHanSub);
        this.pnlKpiQuaHan.Controls.Add(this.lblKpiQuaHanVal);
        this.pnlKpiQuaHan.Controls.Add(this.lblKpiQuaHanTitle);
        this.pnlKpiQuaHan.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlKpiQuaHan.Location = new System.Drawing.Point(1, 1);
        this.pnlKpiQuaHan.Name = "pnlKpiQuaHan";
        this.pnlKpiQuaHan.Size = new System.Drawing.Size(229, 90);
        this.pnlKpiQuaHan.TabIndex = 0;
        // 
        // lblKpiQuaHanTitle
        // 
        this.lblKpiQuaHanTitle.AutoSize = true;
        this.lblKpiQuaHanTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblKpiQuaHanTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiQuaHanTitle.Location = new System.Drawing.Point(16, 12);
        this.lblKpiQuaHanTitle.Name = "lblKpiQuaHanTitle";
        this.lblKpiQuaHanTitle.Size = new System.Drawing.Size(87, 15);
        this.lblKpiQuaHanTitle.TabIndex = 0;
        this.lblKpiQuaHanTitle.Text = "Phiếu quá hạn";
        // 
        // lblKpiQuaHanVal
        // 
        this.lblKpiQuaHanVal.AutoSize = true;
        this.lblKpiQuaHanVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblKpiQuaHanVal.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
        this.lblKpiQuaHanVal.Location = new System.Drawing.Point(14, 31);
        this.lblKpiQuaHanVal.Name = "lblKpiQuaHanVal";
        this.lblKpiQuaHanVal.Size = new System.Drawing.Size(28, 32);
        this.lblKpiQuaHanVal.TabIndex = 1;
        this.lblKpiQuaHanVal.Text = "2";
        // 
        // lblKpiQuaHanSub
        // 
        this.lblKpiQuaHanSub.AutoSize = true;
        this.lblKpiQuaHanSub.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblKpiQuaHanSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblKpiQuaHanSub.Location = new System.Drawing.Point(16, 66);
        this.lblKpiQuaHanSub.Name = "lblKpiQuaHanSub";
        this.lblKpiQuaHanSub.Size = new System.Drawing.Size(172, 15);
        this.lblKpiQuaHanSub.TabIndex = 2;
        this.lblKpiQuaHanSub.Text = "Nằm trong 8 phiếu đang mượn";
        // 
        // Left Chart Card: Hoạt động mượn sách
        // 
        this.pnlBieuDoBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlBieuDoBorder.Controls.Add(this.pnlBieuDo);
        this.pnlBieuDoBorder.Location = new System.Drawing.Point(16, 238);
        this.pnlBieuDoBorder.Name = "pnlBieuDoBorder";
        this.pnlBieuDoBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlBieuDoBorder.Size = new System.Drawing.Size(477, 412);
        this.pnlBieuDoBorder.TabIndex = 5;
        // 
        // pnlBieuDo
        // 
        this.pnlBieuDo.BackColor = System.Drawing.Color.White;
        this.pnlBieuDo.Controls.Add(this.pnlBieuDoFooter);
        this.pnlBieuDo.Controls.Add(this.pnlChartCanvas);
        this.pnlBieuDo.Controls.Add(this.lblBieuDoSub);
        this.pnlBieuDo.Controls.Add(this.lblBieuDoTitle);
        this.pnlBieuDo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlBieuDo.Location = new System.Drawing.Point(1, 1);
        this.pnlBieuDo.Name = "pnlBieuDo";
        this.pnlBieuDo.Size = new System.Drawing.Size(475, 410);
        this.pnlBieuDo.TabIndex = 0;
        // 
        // lblBieuDoTitle
        // 
        this.lblBieuDoTitle.AutoSize = true;
        this.lblBieuDoTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
        this.lblBieuDoTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblBieuDoTitle.Location = new System.Drawing.Point(18, 14);
        this.lblBieuDoTitle.Name = "lblBieuDoTitle";
        this.lblBieuDoTitle.Size = new System.Drawing.Size(156, 19);
        this.lblBieuDoTitle.TabIndex = 0;
        this.lblBieuDoTitle.Text = "Hoạt động mượn sách";
        // 
        // lblBieuDoSub
        // 
        this.lblBieuDoSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblBieuDoSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblBieuDoSub.Location = new System.Drawing.Point(347, 15);
        this.lblBieuDoSub.Name = "lblBieuDoSub";
        this.lblBieuDoSub.Size = new System.Drawing.Size(110, 18);
        this.lblBieuDoSub.TabIndex = 1;
        this.lblBieuDoSub.Text = "Theo tuần";
        this.lblBieuDoSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // pnlChartCanvas
        // 
        this.pnlChartCanvas.BackColor = System.Drawing.Color.White;
        this.pnlChartCanvas.Location = new System.Drawing.Point(16, 44);
        this.pnlChartCanvas.Name = "pnlChartCanvas";
        this.pnlChartCanvas.Size = new System.Drawing.Size(443, 312);
        this.pnlChartCanvas.TabIndex = 2;
        // 
        // pnlBieuDoFooter
        // 
        this.pnlBieuDoFooter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        this.pnlBieuDoFooter.Controls.Add(this.lblDaTra);
        this.pnlBieuDoFooter.Controls.Add(this.lblLuotMuonKy);
        this.pnlBieuDoFooter.Location = new System.Drawing.Point(16, 364);
        this.pnlBieuDoFooter.Name = "pnlBieuDoFooter";
        this.pnlBieuDoFooter.Size = new System.Drawing.Size(443, 34);
        this.pnlBieuDoFooter.TabIndex = 3;
        // 
        // lblLuotMuonKy
        // 
        this.lblLuotMuonKy.AutoSize = true;
        this.lblLuotMuonKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblLuotMuonKy.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblLuotMuonKy.Location = new System.Drawing.Point(12, 9);
        this.lblLuotMuonKy.Name = "lblLuotMuonKy";
        this.lblLuotMuonKy.Size = new System.Drawing.Size(134, 15);
        this.lblLuotMuonKy.TabIndex = 0;
        this.lblLuotMuonKy.Text = "Lượt mượn trong kỳ: 10";
        // 
        // lblDaTra
        // 
        this.lblDaTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblDaTra.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
        this.lblDaTra.Location = new System.Drawing.Point(271, 8);
        this.lblDaTra.Name = "lblDaTra";
        this.lblDaTra.Size = new System.Drawing.Size(160, 18);
        this.lblDaTra.TabIndex = 1;
        this.lblDaTra.Text = "Đã trả: 2 phiếu";
        this.lblDaTra.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // Right Table Card: Sách được mượn trong kỳ
        // 
        this.pnlTopSachBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlTopSachBorder.Controls.Add(this.pnlTopSach);
        this.pnlTopSachBorder.Location = new System.Drawing.Point(507, 238);
        this.pnlTopSachBorder.Name = "pnlTopSachBorder";
        this.pnlTopSachBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlTopSachBorder.Size = new System.Drawing.Size(477, 412);
        this.pnlTopSachBorder.TabIndex = 6;
        // 
        // pnlTopSach
        // 
        this.pnlTopSach.BackColor = System.Drawing.Color.White;
        this.pnlTopSach.Controls.Add(this.lblTongLuotChuaTra);
        this.pnlTopSach.Controls.Add(this.lblTongDauSach);
        this.pnlTopSach.Controls.Add(this.pnlTopSachFooterLine);
        this.pnlTopSach.Controls.Add(this.dgvSachMuon);
        this.pnlTopSach.Controls.Add(this.lblTopSachSub);
        this.pnlTopSach.Controls.Add(this.lblTopSachTitle);
        this.pnlTopSach.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlTopSach.Location = new System.Drawing.Point(1, 1);
        this.pnlTopSach.Name = "pnlTopSach";
        this.pnlTopSach.Size = new System.Drawing.Size(475, 410);
        this.pnlTopSach.TabIndex = 0;
        // 
        // lblTopSachTitle
        // 
        this.lblTopSachTitle.AutoSize = true;
        this.lblTopSachTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
        this.lblTopSachTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblTopSachTitle.Location = new System.Drawing.Point(18, 14);
        this.lblTopSachTitle.Name = "lblTopSachTitle";
        this.lblTopSachTitle.Size = new System.Drawing.Size(184, 19);
        this.lblTopSachTitle.TabIndex = 0;
        this.lblTopSachTitle.Text = "Sách được mượn trong kỳ";
        // 
        // lblTopSachSub
        // 
        this.lblTopSachSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblTopSachSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblTopSachSub.Location = new System.Drawing.Point(317, 15);
        this.lblTopSachSub.Name = "lblTopSachSub";
        this.lblTopSachSub.Size = new System.Drawing.Size(140, 18);
        this.lblTopSachSub.TabIndex = 1;
        this.lblTopSachSub.Text = "Giảm dần theo lượt";
        this.lblTopSachSub.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // dgvSachMuon
        // 
        this.dgvSachMuon.AllowUserToAddRows = false;
        this.dgvSachMuon.AllowUserToDeleteRows = false;
        this.dgvSachMuon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvSachMuon.BackgroundColor = System.Drawing.Color.White;
        this.dgvSachMuon.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.dgvSachMuon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        this.dgvSachMuon.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dgvHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        dgvHeaderStyle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
        dgvHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
        dgvHeaderStyle.SelectionForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        dgvHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvSachMuon.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
        this.dgvSachMuon.ColumnHeadersHeight = 34;
        this.dgvSachMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        this.dgvSachMuon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSach,
            this.colTenSach,
            this.colLuotMuon,
            this.colChuaTra});
        dgvDefaultStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvDefaultStyle.BackColor = System.Drawing.Color.White;
        dgvDefaultStyle.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular);
        dgvDefaultStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
        dgvDefaultStyle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
        dgvDefaultStyle.SelectionBackColor = System.Drawing.Color.FromArgb(239, 246, 255);
        dgvDefaultStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.dgvSachMuon.DefaultCellStyle = dgvDefaultStyle;
        this.dgvSachMuon.EnableHeadersVisualStyles = false;
        this.dgvSachMuon.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.dgvSachMuon.Location = new System.Drawing.Point(16, 46);
        this.dgvSachMuon.MultiSelect = false;
        this.dgvSachMuon.Name = "dgvSachMuon";
        this.dgvSachMuon.ReadOnly = true;
        this.dgvSachMuon.RowHeadersVisible = false;
        this.dgvSachMuon.RowTemplate.Height = 32;
        this.dgvSachMuon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvSachMuon.Size = new System.Drawing.Size(443, 312);
        this.dgvSachMuon.TabIndex = 2;
        // 
        // Columns
        // 
        this.colMaSach.FillWeight = 68F;
        this.colMaSach.HeaderText = "Mã sách";
        this.colMaSach.Name = "colMaSach";
        this.colMaSach.ReadOnly = true;
        this.colTenSach.FillWeight = 185F;
        this.colTenSach.HeaderText = "Tên sách";
        this.colTenSach.Name = "colTenSach";
        this.colTenSach.ReadOnly = true;
        this.colLuotMuon.FillWeight = 52F;
        this.colLuotMuon.HeaderText = "Lượt";
        this.colLuotMuon.Name = "colLuotMuon";
        this.colLuotMuon.ReadOnly = true;
        this.colChuaTra.FillWeight = 68F;
        this.colChuaTra.HeaderText = "Chưa trả";
        this.colChuaTra.Name = "colChuaTra";
        this.colChuaTra.ReadOnly = true;
        // 
        // pnlTopSachFooterLine
        // 
        this.pnlTopSachFooterLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlTopSachFooterLine.Location = new System.Drawing.Point(16, 366);
        this.pnlTopSachFooterLine.Name = "pnlTopSachFooterLine";
        this.pnlTopSachFooterLine.Size = new System.Drawing.Size(443, 1);
        this.pnlTopSachFooterLine.TabIndex = 3;
        // 
        // lblTongDauSach
        // 
        this.lblTongDauSach.AutoSize = true;
        this.lblTongDauSach.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
        this.lblTongDauSach.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblTongDauSach.Location = new System.Drawing.Point(18, 377);
        this.lblTongDauSach.Name = "lblTongDauSach";
        this.lblTongDauSach.Size = new System.Drawing.Size(145, 16);
        this.lblTongDauSach.TabIndex = 4;
        this.lblTongDauSach.Text = "Tổng cộng: 8 đầu sách";
        // 
        // lblTongLuotChuaTra
        // 
        this.lblTongLuotChuaTra.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular);
        this.lblTongLuotChuaTra.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblTongLuotChuaTra.Location = new System.Drawing.Point(247, 376);
        this.lblTongLuotChuaTra.Name = "lblTongLuotChuaTra";
        this.lblTongLuotChuaTra.Size = new System.Drawing.Size(210, 18);
        this.lblTongLuotChuaTra.TabIndex = 5;
        this.lblTongLuotChuaTra.Text = "10 lượt mượn / 8 chưa trả";
        this.lblTongLuotChuaTra.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ucReport
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
        this.Controls.Add(this.pnlTopSachBorder);
        this.Controls.Add(this.pnlBieuDoBorder);
        this.Controls.Add(this.pnlKpiQuaHanBorder);
        this.Controls.Add(this.pnlKpiDangMuonBorder);
        this.Controls.Add(this.pnlKpiDocGiaBorder);
        this.Controls.Add(this.pnlKpiTongSachBorder);
        this.Controls.Add(this.pnlDieuKienBorder);
        this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.Name = "ucReport";
        this.Size = new System.Drawing.Size(1000, 664);
        this.pnlDieuKienBorder.ResumeLayout(false);
        this.pnlDieuKien.ResumeLayout(false);
        this.pnlDieuKien.PerformLayout();
        this.pnlKpiTongSachBorder.ResumeLayout(false);
        this.pnlKpiTongSach.ResumeLayout(false);
        this.pnlKpiTongSach.PerformLayout();
        this.pnlKpiDocGiaBorder.ResumeLayout(false);
        this.pnlKpiDocGia.ResumeLayout(false);
        this.pnlKpiDocGia.PerformLayout();
        this.pnlKpiDangMuonBorder.ResumeLayout(false);
        this.pnlKpiDangMuon.ResumeLayout(false);
        this.pnlKpiDangMuon.PerformLayout();
        this.pnlKpiQuaHanBorder.ResumeLayout(false);
        this.pnlKpiQuaHan.ResumeLayout(false);
        this.pnlKpiQuaHan.PerformLayout();
        this.pnlBieuDoBorder.ResumeLayout(false);
        this.pnlBieuDo.ResumeLayout(false);
        this.pnlBieuDo.PerformLayout();
        this.pnlBieuDoFooter.ResumeLayout(false);
        this.pnlBieuDoFooter.PerformLayout();
        this.pnlTopSachBorder.ResumeLayout(false);
        this.pnlTopSach.ResumeLayout(false);
        this.pnlTopSach.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvSachMuon)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel pnlDieuKienBorder;
    private System.Windows.Forms.Panel pnlDieuKien;
    private System.Windows.Forms.Label lblDieuKienTitle;
    private System.Windows.Forms.Label lblCapNhat;
    private System.Windows.Forms.Label lblTuNgay;
    private System.Windows.Forms.DateTimePicker dtpTuNgay;
    private System.Windows.Forms.Label lblDenNgay;
    private System.Windows.Forms.DateTimePicker dtpDenNgay;
    private System.Windows.Forms.Label lblLoaiBaoCao;
    private System.Windows.Forms.ComboBox cboLoaiBaoCao;
    private System.Windows.Forms.Button btnXemBaoCao;
    private System.Windows.Forms.Button btnXuatExcel;

    private System.Windows.Forms.Panel pnlKpiTongSachBorder;
    private System.Windows.Forms.Panel pnlKpiTongSach;
    private System.Windows.Forms.Label lblKpiTongSachTitle;
    private System.Windows.Forms.Label lblKpiTongSachVal;
    private System.Windows.Forms.Label lblKpiTongSachSub;

    private System.Windows.Forms.Panel pnlKpiDocGiaBorder;
    private System.Windows.Forms.Panel pnlKpiDocGia;
    private System.Windows.Forms.Label lblKpiDocGiaTitle;
    private System.Windows.Forms.Label lblKpiDocGiaVal;
    private System.Windows.Forms.Label lblKpiDocGiaSub;

    private System.Windows.Forms.Panel pnlKpiDangMuonBorder;
    private System.Windows.Forms.Panel pnlKpiDangMuon;
    private System.Windows.Forms.Label lblKpiDangMuonTitle;
    private System.Windows.Forms.Label lblKpiDangMuonVal;
    private System.Windows.Forms.Label lblKpiDangMuonSub;

    private System.Windows.Forms.Panel pnlKpiQuaHanBorder;
    private System.Windows.Forms.Panel pnlKpiQuaHan;
    private System.Windows.Forms.Label lblKpiQuaHanTitle;
    private System.Windows.Forms.Label lblKpiQuaHanVal;
    private System.Windows.Forms.Label lblKpiQuaHanSub;

    private System.Windows.Forms.Panel pnlBieuDoBorder;
    private System.Windows.Forms.Panel pnlBieuDo;
    private System.Windows.Forms.Label lblBieuDoTitle;
    private System.Windows.Forms.Label lblBieuDoSub;
    private System.Windows.Forms.Panel pnlChartCanvas;
    private System.Windows.Forms.Panel pnlBieuDoFooter;
    private System.Windows.Forms.Label lblLuotMuonKy;
    private System.Windows.Forms.Label lblDaTra;

    private System.Windows.Forms.Panel pnlTopSachBorder;
    private System.Windows.Forms.Panel pnlTopSach;
    private System.Windows.Forms.Label lblTopSachTitle;
    private System.Windows.Forms.Label lblTopSachSub;
    private System.Windows.Forms.DataGridView dgvSachMuon;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMaSach;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTenSach;
    private System.Windows.Forms.DataGridViewTextBoxColumn colLuotMuon;
    private System.Windows.Forms.DataGridViewTextBoxColumn colChuaTra;
    private System.Windows.Forms.Panel pnlTopSachFooterLine;
    private System.Windows.Forms.Label lblTongDauSach;
    private System.Windows.Forms.Label lblTongLuotChuaTra;
}