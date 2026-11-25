namespace QuanLyThuVien;

partial class FormMain
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.pnlMenu = new System.Windows.Forms.Panel();
        this.pnlUserBottom = new System.Windows.Forms.Panel();
        this.pnlMenuBottomLine = new System.Windows.Forms.Panel();
        this.lblVersion = new System.Windows.Forms.Label();
        this.btnDangXuat = new System.Windows.Forms.Button();
        this.lblUserRole = new System.Windows.Forms.Label();
        this.lblUserName = new System.Windows.Forms.Label();
        this.btnBaoCao = new System.Windows.Forms.Button();
        this.btnMuonTra = new System.Windows.Forms.Button();
        this.btnKhoSach = new System.Windows.Forms.Button();
        this.btnDocGia = new System.Windows.Forms.Button();
        this.pnlLogo = new System.Windows.Forms.Panel();
        this.lblLogoSub = new System.Windows.Forms.Label();
        this.lblLogo = new System.Windows.Forms.Label();
        this.pnlHeader = new System.Windows.Forms.Panel();
        this.lblAvatar = new System.Windows.Forms.Label();
        this.lblWelcome = new System.Windows.Forms.Label();
        this.lblPageTitle = new System.Windows.Forms.Label();
        this.pnlHeaderLine = new System.Windows.Forms.Panel();
        this.pnlStatusBar = new System.Windows.Forms.Panel();
        this.pnlStatusLine = new System.Windows.Forms.Panel();
        this.lblStatus = new System.Windows.Forms.Label();
        this.lblSystemName = new System.Windows.Forms.Label();
        this.pnlHienThi = new System.Windows.Forms.Panel();
        this.pnlMenu.SuspendLayout();
        this.pnlUserBottom.SuspendLayout();
        this.pnlLogo.SuspendLayout();
        this.pnlHeader.SuspendLayout();
        this.pnlStatusBar.SuspendLayout();
        this.SuspendLayout();
        // 
        // pnlMenu
        // 
        this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(24, 33, 47);
        this.pnlMenu.Controls.Add(this.btnBaoCao);
        this.pnlMenu.Controls.Add(this.btnMuonTra);
        this.pnlMenu.Controls.Add(this.btnKhoSach);
        this.pnlMenu.Controls.Add(this.btnDocGia);
        this.pnlMenu.Controls.Add(this.pnlLogo);
        this.pnlMenu.Controls.Add(this.pnlUserBottom);
        this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
        this.pnlMenu.Location = new System.Drawing.Point(0, 0);
        this.pnlMenu.Name = "pnlMenu";
        this.pnlMenu.Size = new System.Drawing.Size(250, 750);
        this.pnlMenu.TabIndex = 0;
        // 
        // pnlLogo
        // 
        this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(24, 33, 47);
        this.pnlLogo.Controls.Add(this.lblLogoSub);
        this.pnlLogo.Controls.Add(this.lblLogo);
        this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlLogo.Location = new System.Drawing.Point(0, 0);
        this.pnlLogo.Name = "pnlLogo";
        this.pnlLogo.Size = new System.Drawing.Size(250, 92);
        this.pnlLogo.TabIndex = 0;
        // 
        // lblLogo
        // 
        this.lblLogo.AutoSize = true;
        this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblLogo.ForeColor = System.Drawing.Color.White;
        this.lblLogo.Location = new System.Drawing.Point(18, 22);
        this.lblLogo.Name = "lblLogo";
        this.lblLogo.Size = new System.Drawing.Size(195, 21);
        this.lblLogo.TabIndex = 0;
        this.lblLogo.Text = "📖 THƯ VIỆN ĐẠI HỌC";
        // 
        // lblLogoSub
        // 
        this.lblLogoSub.AutoSize = true;
        this.lblLogoSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblLogoSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
        this.lblLogoSub.Location = new System.Drawing.Point(18, 50);
        this.lblLogoSub.Name = "lblLogoSub";
        this.lblLogoSub.Size = new System.Drawing.Size(150, 15);
        this.lblLogoSub.TabIndex = 1;
        this.lblLogoSub.Text = "Hệ thống quản lý thư viện";
        // 
        // btnDocGia
        // 
        this.btnDocGia.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.btnDocGia.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnDocGia.FlatAppearance.BorderSize = 0;
        this.btnDocGia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
        this.btnDocGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDocGia.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
        this.btnDocGia.ForeColor = System.Drawing.Color.White;
        this.btnDocGia.Location = new System.Drawing.Point(14, 108);
        this.btnDocGia.Name = "btnDocGia";
        this.btnDocGia.Padding = new System.Windows.Forms.Padding(12, 0, 8, 0);
        this.btnDocGia.Size = new System.Drawing.Size(222, 46);
        this.btnDocGia.TabIndex = 1;
        this.btnDocGia.Text = "1. Quản lý Độc giả (ucUser)";
        this.btnDocGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnDocGia.UseVisualStyleBackColor = false;
        // 
        // btnKhoSach
        // 
        this.btnKhoSach.BackColor = System.Drawing.Color.Transparent;
        this.btnKhoSach.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnKhoSach.FlatAppearance.BorderSize = 0;
        this.btnKhoSach.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(38, 53, 77);
        this.btnKhoSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnKhoSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.btnKhoSach.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
        this.btnKhoSach.Location = new System.Drawing.Point(14, 164);
        this.btnKhoSach.Name = "btnKhoSach";
        this.btnKhoSach.Padding = new System.Windows.Forms.Padding(12, 0, 8, 0);
        this.btnKhoSach.Size = new System.Drawing.Size(222, 46);
        this.btnKhoSach.TabIndex = 2;
        this.btnKhoSach.Text = "2. Quản lý Kho sách (ucBook)";
        this.btnKhoSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnKhoSach.UseVisualStyleBackColor = false;
        // 
        // btnMuonTra
        // 
        this.btnMuonTra.BackColor = System.Drawing.Color.Transparent;
        this.btnMuonTra.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnMuonTra.FlatAppearance.BorderSize = 0;
        this.btnMuonTra.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(38, 53, 77);
        this.btnMuonTra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnMuonTra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.btnMuonTra.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
        this.btnMuonTra.Location = new System.Drawing.Point(14, 220);
        this.btnMuonTra.Name = "btnMuonTra";
        this.btnMuonTra.Padding = new System.Windows.Forms.Padding(12, 0, 8, 0);
        this.btnMuonTra.Size = new System.Drawing.Size(222, 46);
        this.btnMuonTra.TabIndex = 3;
        this.btnMuonTra.Text = "3. Mượn - Trả sách (ucBorrow)";
        this.btnMuonTra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnMuonTra.UseVisualStyleBackColor = false;
        // 
        // btnBaoCao
        // 
        this.btnBaoCao.BackColor = System.Drawing.Color.Transparent;
        this.btnBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnBaoCao.FlatAppearance.BorderSize = 0;
        this.btnBaoCao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(38, 53, 77);
        this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.btnBaoCao.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
        this.btnBaoCao.Location = new System.Drawing.Point(14, 276);
        this.btnBaoCao.Name = "btnBaoCao";
        this.btnBaoCao.Padding = new System.Windows.Forms.Padding(12, 0, 8, 0);
        this.btnBaoCao.Size = new System.Drawing.Size(222, 54);
        this.btnBaoCao.TabIndex = 4;
        this.btnBaoCao.Text = "4. Báo cáo - Thống kê\r\n(ucReport)";
        this.btnBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnBaoCao.UseVisualStyleBackColor = false;
        // 
        // pnlUserBottom
        // 
        this.pnlUserBottom.BackColor = System.Drawing.Color.FromArgb(24, 33, 47);
        this.pnlUserBottom.Controls.Add(this.lblVersion);
        this.pnlUserBottom.Controls.Add(this.btnDangXuat);
        this.pnlUserBottom.Controls.Add(this.lblUserRole);
        this.pnlUserBottom.Controls.Add(this.lblUserName);
        this.pnlUserBottom.Controls.Add(this.pnlMenuBottomLine);
        this.pnlUserBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlUserBottom.Location = new System.Drawing.Point(0, 654);
        this.pnlUserBottom.Name = "pnlUserBottom";
        this.pnlUserBottom.Size = new System.Drawing.Size(250, 96);
        this.pnlUserBottom.TabIndex = 5;
        // 
        // pnlMenuBottomLine
        // 
        this.pnlMenuBottomLine.BackColor = System.Drawing.Color.FromArgb(42, 55, 74);
        this.pnlMenuBottomLine.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlMenuBottomLine.Location = new System.Drawing.Point(0, 0);
        this.pnlMenuBottomLine.Name = "pnlMenuBottomLine";
        this.pnlMenuBottomLine.Size = new System.Drawing.Size(250, 1);
        this.pnlMenuBottomLine.TabIndex = 0;
        // 
        // btnDangXuat
        // 
        this.btnDangXuat.BackColor = System.Drawing.Color.Transparent;
        this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnDangXuat.FlatAppearance.BorderSize = 0;
        this.btnDangXuat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(38, 53, 77);
        this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.btnDangXuat.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
        this.btnDangXuat.Location = new System.Drawing.Point(14, 14);
        this.btnDangXuat.Name = "btnDangXuat";
        this.btnDangXuat.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
        this.btnDangXuat.Size = new System.Drawing.Size(222, 36);
        this.btnDangXuat.TabIndex = 1;
        this.btnDangXuat.Text = "↪  Đăng xuất";
        this.btnDangXuat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnDangXuat.UseVisualStyleBackColor = false;
        // 
        // lblVersion
        // 
        this.lblVersion.AutoSize = true;
        this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblVersion.Location = new System.Drawing.Point(18, 62);
        this.lblVersion.Name = "lblVersion";
        this.lblVersion.Size = new System.Drawing.Size(205, 15);
        this.lblVersion.TabIndex = 2;
        this.lblVersion.Text = "Phiên bản 1.0 • Kết nối cơ sở dữ liệu";
        // 
        // lblUserName
        // 
        this.lblUserName.AutoSize = true;
        this.lblUserName.Location = new System.Drawing.Point(0, 0);
        this.lblUserName.Name = "lblUserName";
        this.lblUserName.Size = new System.Drawing.Size(44, 17);
        this.lblUserName.TabIndex = 3;
        this.lblUserName.Text = "Admin";
        this.lblUserName.Visible = false;
        // 
        // lblUserRole
        // 
        this.lblUserRole.AutoSize = true;
        this.lblUserRole.Location = new System.Drawing.Point(0, 0);
        this.lblUserRole.Name = "lblUserRole";
        this.lblUserRole.Size = new System.Drawing.Size(80, 17);
        this.lblUserRole.TabIndex = 4;
        this.lblUserRole.Text = "Quản trị viên";
        this.lblUserRole.Visible = false;
        // 
        // pnlHeader
        // 
        this.pnlHeader.BackColor = System.Drawing.Color.White;
        this.pnlHeader.Controls.Add(this.lblAvatar);
        this.pnlHeader.Controls.Add(this.lblWelcome);
        this.pnlHeader.Controls.Add(this.lblPageTitle);
        this.pnlHeader.Controls.Add(this.pnlHeaderLine);
        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlHeader.Location = new System.Drawing.Point(250, 0);
        this.pnlHeader.Name = "pnlHeader";
        this.pnlHeader.Size = new System.Drawing.Size(1000, 54);
        this.pnlHeader.TabIndex = 1;
        // 
        // lblPageTitle
        // 
        this.lblPageTitle.AutoSize = true;
        this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
        this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblPageTitle.Location = new System.Drawing.Point(20, 14);
        this.lblPageTitle.Name = "lblPageTitle";
        this.lblPageTitle.Size = new System.Drawing.Size(185, 25);
        this.lblPageTitle.TabIndex = 0;
        this.lblPageTitle.Text = "QUẢN LÝ ĐỘC GIẢ";
        // 
        // lblWelcome
        // 
        this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblWelcome.Location = new System.Drawing.Point(680, 17);
        this.lblWelcome.Name = "lblWelcome";
        this.lblWelcome.Size = new System.Drawing.Size(300, 20);
        this.lblWelcome.TabIndex = 1;
        this.lblWelcome.Text = "Xin chào: Thủ thư  |  Ngày: 07/11/2026";
        this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lblAvatar
        // 
        this.lblAvatar.Location = new System.Drawing.Point(948, 11);
        this.lblAvatar.Name = "lblAvatar";
        this.lblAvatar.Size = new System.Drawing.Size(32, 32);
        this.lblAvatar.TabIndex = 2;
        this.lblAvatar.Text = "A";
        this.lblAvatar.Visible = false;
        // 
        // pnlHeaderLine
        // 
        this.pnlHeaderLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlHeaderLine.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlHeaderLine.Location = new System.Drawing.Point(0, 53);
        this.pnlHeaderLine.Name = "pnlHeaderLine";
        this.pnlHeaderLine.Size = new System.Drawing.Size(1000, 1);
        this.pnlHeaderLine.TabIndex = 3;
        // 
        // pnlStatusBar
        // 
        this.pnlStatusBar.BackColor = System.Drawing.Color.White;
        this.pnlStatusBar.Controls.Add(this.lblSystemName);
        this.pnlStatusBar.Controls.Add(this.lblStatus);
        this.pnlStatusBar.Controls.Add(this.pnlStatusLine);
        this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlStatusBar.Location = new System.Drawing.Point(250, 718);
        this.pnlStatusBar.Name = "pnlStatusBar";
        this.pnlStatusBar.Size = new System.Drawing.Size(1000, 32);
        this.pnlStatusBar.TabIndex = 2;
        // 
        // pnlStatusLine
        // 
        this.pnlStatusLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlStatusLine.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlStatusLine.Location = new System.Drawing.Point(0, 0);
        this.pnlStatusLine.Name = "pnlStatusLine";
        this.pnlStatusLine.Size = new System.Drawing.Size(1000, 1);
        this.pnlStatusLine.TabIndex = 0;
        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = true;
        this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblStatus.Location = new System.Drawing.Point(20, 8);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(185, 15);
        this.lblStatus.TabIndex = 1;
        this.lblStatus.Text = "Sẵn sàng  |  Dữ liệu đã đồng bộ";
        // 
        // lblSystemName
        // 
        this.lblSystemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.lblSystemName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
        this.lblSystemName.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblSystemName.Location = new System.Drawing.Point(760, 8);
        this.lblSystemName.Name = "lblSystemName";
        this.lblSystemName.Size = new System.Drawing.Size(220, 16);
        this.lblSystemName.TabIndex = 2;
        this.lblSystemName.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN";
        this.lblSystemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // pnlHienThi
        // 
        this.pnlHienThi.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
        this.pnlHienThi.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlHienThi.Location = new System.Drawing.Point(250, 54);
        this.pnlHienThi.Name = "pnlHienThi";
        this.pnlHienThi.Size = new System.Drawing.Size(1000, 664);
        this.pnlHienThi.TabIndex = 3;
        // 
        // FormMain
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
        this.ClientSize = new System.Drawing.Size(1250, 750);
        this.Controls.Add(this.pnlHienThi);
        this.Controls.Add(this.pnlStatusBar);
        this.Controls.Add(this.pnlHeader);
        this.Controls.Add(this.pnlMenu);
        this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "FormMain";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Hệ thống Quản lý Thư viện";
        this.pnlMenu.ResumeLayout(false);
        this.pnlUserBottom.ResumeLayout(false);
        this.pnlUserBottom.PerformLayout();
        this.pnlLogo.ResumeLayout(false);
        this.pnlLogo.PerformLayout();
        this.pnlHeader.ResumeLayout(false);
        this.pnlHeader.PerformLayout();
        this.pnlStatusBar.ResumeLayout(false);
        this.pnlStatusBar.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel pnlMenu;
    private System.Windows.Forms.Panel pnlLogo;
    private System.Windows.Forms.Label lblLogo;
    private System.Windows.Forms.Label lblLogoSub;
    private System.Windows.Forms.Button btnDocGia;
    private System.Windows.Forms.Button btnKhoSach;
    private System.Windows.Forms.Button btnMuonTra;
    private System.Windows.Forms.Button btnBaoCao;
    private System.Windows.Forms.Panel pnlUserBottom;
    private System.Windows.Forms.Panel pnlMenuBottomLine;
    private System.Windows.Forms.Label lblVersion;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.Label lblUserRole;
    private System.Windows.Forms.Button btnDangXuat;
    private System.Windows.Forms.Panel pnlHeader;
    private System.Windows.Forms.Label lblPageTitle;
    private System.Windows.Forms.Label lblWelcome;
    private System.Windows.Forms.Label lblAvatar;
    private System.Windows.Forms.Panel pnlHeaderLine;
    private System.Windows.Forms.Panel pnlStatusBar;
    private System.Windows.Forms.Panel pnlStatusLine;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label lblSystemName;
    private System.Windows.Forms.Panel pnlHienThi;
}