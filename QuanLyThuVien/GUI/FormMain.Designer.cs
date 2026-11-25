namespace QuanLyThuVien;

partial class FormMain
{
    /// <summary>
    ///  Biến quản lý tài nguyên do Windows Forms Designer tạo.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Giải phóng tài nguyên khi form được đóng.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        // Tạo bộ chứa tài nguyên và thiết lập kích thước mặc định của form.
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Text = "Form1";
    }

    #endregion
}