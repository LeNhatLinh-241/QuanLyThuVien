namespace QuanLyThuVien;

partial class LoginForm
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
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblSubtitle = new System.Windows.Forms.Label();
        this.lblUsername = new System.Windows.Forms.Label();
        this.pnlUsernameBorder = new System.Windows.Forms.Panel();
        this.pnlUsernameInner = new System.Windows.Forms.Panel();
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.lblPassword = new System.Windows.Forms.Label();
        this.pnlPasswordBorder = new System.Windows.Forms.Panel();
        this.pnlPasswordInner = new System.Windows.Forms.Panel();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.pnlRoleBorder = new System.Windows.Forms.Panel();
        this.pnlRoleInner = new System.Windows.Forms.Panel();
        this.lblRole = new System.Windows.Forms.Label();
        this.rdoAdmin = new System.Windows.Forms.RadioButton();
        this.rdoThuThu = new System.Windows.Forms.RadioButton();
        this.btnDangNhap = new System.Windows.Forms.Button();
        this.pnlDivider = new System.Windows.Forms.Panel();
        this.lblSupport = new System.Windows.Forms.Label();
        this.pnlUsernameBorder.SuspendLayout();
        this.pnlUsernameInner.SuspendLayout();
        this.pnlPasswordBorder.SuspendLayout();
        this.pnlPasswordInner.SuspendLayout();
        this.pnlRoleBorder.SuspendLayout();
        this.pnlRoleInner.SuspendLayout();
        this.SuspendLayout();
        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.lblTitle.Location = new System.Drawing.Point(45, 36);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(342, 25);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "ĐĂNG NHẬP HỆ THỐNG THƯ VIỆN";
        // 
        // lblSubtitle
        // 
        this.lblSubtitle.AutoSize = true;
        this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblSubtitle.Location = new System.Drawing.Point(45, 68);
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Size = new System.Drawing.Size(225, 17);
        this.lblSubtitle.TabIndex = 1;
        this.lblSubtitle.Text = "Vui lòng nhập tài khoản để tiếp tục.";
        // 
        // lblUsername
        // 
        this.lblUsername.AutoSize = true;
        this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblUsername.Location = new System.Drawing.Point(45, 112);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(160, 17);
        this.lblUsername.TabIndex = 2;
        this.lblUsername.Text = "Tên đăng nhập (Username)";
        // 
        // pnlUsernameBorder
        // 
        this.pnlUsernameBorder.BackColor = System.Drawing.Color.FromArgb(215, 222, 232);
        this.pnlUsernameBorder.Controls.Add(this.pnlUsernameInner);
        this.pnlUsernameBorder.Location = new System.Drawing.Point(45, 136);
        this.pnlUsernameBorder.Name = "pnlUsernameBorder";
        this.pnlUsernameBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlUsernameBorder.Size = new System.Drawing.Size(360, 36);
        this.pnlUsernameBorder.TabIndex = 3;
        // 
        // pnlUsernameInner
        // 
        this.pnlUsernameInner.BackColor = System.Drawing.Color.White;
        this.pnlUsernameInner.Controls.Add(this.txtUsername);
        this.pnlUsernameInner.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlUsernameInner.Location = new System.Drawing.Point(1, 1);
        this.pnlUsernameInner.Name = "pnlUsernameInner";
        this.pnlUsernameInner.Size = new System.Drawing.Size(358, 34);
        this.pnlUsernameInner.TabIndex = 0;
        // 
        // txtUsername
        // 
        this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular);
        this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.txtUsername.Location = new System.Drawing.Point(10, 7);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(338, 19);
        this.txtUsername.TabIndex = 0;
        // 
        // lblPassword
        // 
        this.lblPassword.AutoSize = true;
        this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
        this.lblPassword.Location = new System.Drawing.Point(45, 188);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(128, 17);
        this.lblPassword.TabIndex = 4;
        this.lblPassword.Text = "Mật khẩu (Password)";
        // 
        // pnlPasswordBorder
        // 
        this.pnlPasswordBorder.BackColor = System.Drawing.Color.FromArgb(215, 222, 232);
        this.pnlPasswordBorder.Controls.Add(this.pnlPasswordInner);
        this.pnlPasswordBorder.Location = new System.Drawing.Point(45, 212);
        this.pnlPasswordBorder.Name = "pnlPasswordBorder";
        this.pnlPasswordBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlPasswordBorder.Size = new System.Drawing.Size(360, 36);
        this.pnlPasswordBorder.TabIndex = 5;
        // 
        // pnlPasswordInner
        // 
        this.pnlPasswordInner.BackColor = System.Drawing.Color.White;
        this.pnlPasswordInner.Controls.Add(this.txtPassword);
        this.pnlPasswordInner.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlPasswordInner.Location = new System.Drawing.Point(1, 1);
        this.pnlPasswordInner.Name = "pnlPasswordInner";
        this.pnlPasswordInner.Size = new System.Drawing.Size(358, 34);
        this.pnlPasswordInner.TabIndex = 0;
        // 
        // txtPassword
        // 
        this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular);
        this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
        this.txtPassword.Location = new System.Drawing.Point(10, 7);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '•';
        this.txtPassword.Size = new System.Drawing.Size(338, 19);
        this.txtPassword.TabIndex = 0;
        // 
        // pnlRoleBorder
        // 
        this.pnlRoleBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlRoleBorder.Controls.Add(this.pnlRoleInner);
        this.pnlRoleBorder.Location = new System.Drawing.Point(45, 266);
        this.pnlRoleBorder.Name = "pnlRoleBorder";
        this.pnlRoleBorder.Padding = new System.Windows.Forms.Padding(1);
        this.pnlRoleBorder.Size = new System.Drawing.Size(360, 44);
        this.pnlRoleBorder.TabIndex = 6;
        // 
        // pnlRoleInner
        // 
        this.pnlRoleInner.BackColor = System.Drawing.Color.FromArgb(250, 251, 253);
        this.pnlRoleInner.Controls.Add(this.rdoThuThu);
        this.pnlRoleInner.Controls.Add(this.rdoAdmin);
        this.pnlRoleInner.Controls.Add(this.lblRole);
        this.pnlRoleInner.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlRoleInner.Location = new System.Drawing.Point(1, 1);
        this.pnlRoleInner.Name = "pnlRoleInner";
        this.pnlRoleInner.Size = new System.Drawing.Size(358, 42);
        this.pnlRoleInner.TabIndex = 0;
        // 
        // lblRole
        // 
        this.lblRole.AutoSize = true;
        this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.lblRole.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblRole.Location = new System.Drawing.Point(12, 12);
        this.lblRole.Name = "lblRole";
        this.lblRole.Size = new System.Drawing.Size(96, 17);
        this.lblRole.TabIndex = 0;
        this.lblRole.Text = "Vai trò tài khoản";
        // 
        // rdoAdmin
        // 
        this.rdoAdmin.AutoSize = true;
        this.rdoAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
        this.rdoAdmin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.rdoAdmin.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
        this.rdoAdmin.Location = new System.Drawing.Point(198, 10);
        this.rdoAdmin.Name = "rdoAdmin";
        this.rdoAdmin.Size = new System.Drawing.Size(63, 21);
        this.rdoAdmin.TabIndex = 1;
        this.rdoAdmin.Text = "Admin";
        this.rdoAdmin.UseVisualStyleBackColor = true;
        // 
        // rdoThuThu
        // 
        this.rdoThuThu.AutoSize = true;
        this.rdoThuThu.Checked = true;
        this.rdoThuThu.Cursor = System.Windows.Forms.Cursors.Hand;
        this.rdoThuThu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.rdoThuThu.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.rdoThuThu.Location = new System.Drawing.Point(272, 10);
        this.rdoThuThu.Name = "rdoThuThu";
        this.rdoThuThu.Size = new System.Drawing.Size(76, 21);
        this.rdoThuThu.TabIndex = 2;
        this.rdoThuThu.TabStop = true;
        this.rdoThuThu.Text = "Thủ thư";
        this.rdoThuThu.UseVisualStyleBackColor = true;
        // 
        // btnDangNhap
        // 
        this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        this.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnDangNhap.FlatAppearance.BorderSize = 0;
        this.btnDangNhap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
        this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
        this.btnDangNhap.ForeColor = System.Drawing.Color.White;
        this.btnDangNhap.Location = new System.Drawing.Point(45, 330);
        this.btnDangNhap.Name = "btnDangNhap";
        this.btnDangNhap.Size = new System.Drawing.Size(360, 44);
        this.btnDangNhap.TabIndex = 7;
        this.btnDangNhap.Text = "ĐĂNG NHẬP";
        this.btnDangNhap.UseVisualStyleBackColor = false;
        // 
        // pnlDivider
        // 
        this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
        this.pnlDivider.Location = new System.Drawing.Point(45, 398);
        this.pnlDivider.Name = "pnlDivider";
        this.pnlDivider.Size = new System.Drawing.Size(360, 1);
        this.pnlDivider.TabIndex = 8;
        // 
        // lblSupport
        // 
        this.lblSupport.AutoSize = true;
        this.lblSupport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblSupport.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        this.lblSupport.Location = new System.Drawing.Point(45, 416);
        this.lblSupport.Name = "lblSupport";
        this.lblSupport.Size = new System.Drawing.Size(287, 15);
        this.lblSupport.TabIndex = 9;
        this.lblSupport.Text = "Cần hỗ trợ tài khoản? Liên hệ quản trị viên thư viện.";
        // 
        // LoginForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.ClientSize = new System.Drawing.Size(450, 470);
        this.Controls.Add(this.lblSupport);
        this.Controls.Add(this.pnlDivider);
        this.Controls.Add(this.btnDangNhap);
        this.Controls.Add(this.pnlRoleBorder);
        this.Controls.Add(this.pnlPasswordBorder);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.pnlUsernameBorder);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.lblSubtitle);
        this.Controls.Add(this.lblTitle);
        this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.Name = "LoginForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN";
        this.pnlUsernameBorder.ResumeLayout(false);
        this.pnlUsernameInner.ResumeLayout(false);
        this.pnlUsernameInner.PerformLayout();
        this.pnlPasswordBorder.ResumeLayout(false);
        this.pnlPasswordInner.ResumeLayout(false);
        this.pnlPasswordInner.PerformLayout();
        this.pnlRoleBorder.ResumeLayout(false);
        this.pnlRoleInner.ResumeLayout(false);
        this.pnlRoleInner.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblSubtitle;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.Panel pnlUsernameBorder;
    private System.Windows.Forms.Panel pnlUsernameInner;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Panel pnlPasswordBorder;
    private System.Windows.Forms.Panel pnlPasswordInner;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Panel pnlRoleBorder;
    private System.Windows.Forms.Panel pnlRoleInner;
    private System.Windows.Forms.Label lblRole;
    private System.Windows.Forms.RadioButton rdoAdmin;
    private System.Windows.Forms.RadioButton rdoThuThu;
    private System.Windows.Forms.Button btnDangNhap;
    private System.Windows.Forms.Panel pnlDivider;
    private System.Windows.Forms.Label lblSupport;
}