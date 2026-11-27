using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using QuanLyThuVien.BLL;

namespace QuanLyThuVien;

public partial class ucReport : UserControl
{
    
    private readonly ReportBLL _reportBLL = new();

   
    private List<(string Nhan, int SoLuot)> _duLieuBieuDo = new();

    public ucReport()
    {
        InitializeComponent();

        
        cboLoaiBaoCao.Items.Clear();
        cboLoaiBaoCao.Items.AddRange(new object[]
        {
            "Tổng hợp hoạt động thư viện",
            "Danh sách sách đang cho mượn",
            "Danh sách độc giả quá hạn chưa trả"
        });
        cboLoaiBaoCao.SelectedIndex = 0;

       
        dtpTuNgay.Value = DateTime.Now.AddDays(-27);
        dtpDenNgay.Value = DateTime.Now;

        
        pnlChartCanvas.Paint += PnlChartCanvas_Paint;
        btnXemBaoCao.Click += (s, e) => TaiDuLieuBaoCao();
        cboLoaiBaoCao.SelectedIndexChanged += (s, e) => TaiDuLieuBaoCao();
        btnXuatExcel.Click += BtnXuatExcel_Click;
        this.Load += (s, e) => TaiDuLieuBaoCao();
    }

    
    public void TaiDuLieuBaoCao()
    {
        try
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            
            var kpi = _reportBLL.LayTongQuan(tuNgay, denNgay);
            lblCapNhat.Text = $"Cập nhật: {DateTime.Now:dd/MM/yyyy}";

            lblKpiTongSachVal.Text = kpi.TongBanSach.ToString();
            lblKpiTongSachSub.Text = $"{kpi.TongDauSach} đầu sách • {kpi.BanSachSanCo} bản sẵn có";

            lblKpiDocGiaVal.Text = kpi.TongDocGia.ToString();
            lblKpiDocGiaSub.Text = $"{kpi.TheHoatDong} thẻ hoạt động • {kpi.TheBiKhoa} thẻ khóa";

            lblKpiDangMuonVal.Text = kpi.PhieuDangMuon.ToString();
            lblKpiDangMuonSub.Text = $"{kpi.PhieuDangMuon} bản sách chưa trả";

            lblKpiQuaHanVal.Text = kpi.PhieuQuaHan.ToString();
            lblKpiQuaHanSub.Text = $"Nằm trong {kpi.PhieuDangMuon} phiếu đang mượn";

           
            _duLieuBieuDo = _reportBLL.LayBieuDoTheoTuan(tuNgay, denNgay);
            int tongLuotKy = _duLieuBieuDo.Sum(x => x.SoLuot);
            lblLuotMuonKy.Text = $"Lượt mượn trong kỳ: {tongLuotKy}";
            lblDaTra.Text = $"Đã trả: {kpi.PhieuDaTraTrongKy} phiếu";
            pnlChartCanvas.Invalidate(); 
            
            int loai = cboLoaiBaoCao.SelectedIndex;
            DataTable dt = _reportBLL.LayDuLieuBang(loai, tuNgay, denNgay);
            HienThiBang(dt, loai);
        }
        catch (ArgumentException ex) 
        {
            MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex) 
        {
            MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Hệ thống quản lý thư viện",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

   
    private void HienThiBang(DataTable dt, int loai)
    {
        dgvSachMuon.Rows.Clear();

        if (loai == 0) 
        {
            lblTopSachTitle.Text = "Sách được mượn trong kỳ";
            lblTopSachSub.Text = "Giảm dần theo lượt";
            colMaSach.HeaderText = "Mã sách";
            colTenSach.HeaderText = "Tên sách";
            colLuotMuon.HeaderText = "Lượt";
            colChuaTra.HeaderText = "Chưa trả";

            int tongLuot = 0, tongChuaTra = 0;
            foreach (DataRow r in dt.Rows)
            {
                int luot = Convert.ToInt32(r["LuotMuon"]);
                int chuaTra = Convert.ToInt32(r["ChuaTra"]);
                tongLuot += luot;
                tongChuaTra += chuaTra;
                dgvSachMuon.Rows.Add(r["MaSach"], r["TenSach"], luot, chuaTra);
            }

            lblTongDauSach.Text = $"Tổng cộng: {dt.Rows.Count} đầu sách";
            lblTongLuotChuaTra.Text = $"{tongLuot} lượt mượn / {tongChuaTra} chưa trả";
        }
        else if (loai == 1) 
        {
            lblTopSachTitle.Text = "Sách đang cho mượn";
            lblTopSachSub.Text = "Xếp theo hạn trả";
            colMaSach.HeaderText = "Mã vạch";
            colTenSach.HeaderText = "Tên sách";
            colLuotMuon.HeaderText = "Độc giả";
            colChuaTra.HeaderText = "Hạn trả";

            foreach (DataRow r in dt.Rows)
            {
                dgvSachMuon.Rows.Add(r["MaVach"], r["TenSach"], r["DocGia"], r["HanTra"]);
            }

            lblTongDauSach.Text = $"Tổng cộng: {dt.Rows.Count} cuốn đang mượn";
            lblTongLuotChuaTra.Text = "Trạng thái: Đang mượn";
        }
        else 
        {
            lblTopSachTitle.Text = "Độc giả quá hạn chưa trả";
            lblTopSachSub.Text = "Giảm dần số ngày trễ";
            colMaSach.HeaderText = "Độc giả";
            colTenSach.HeaderText = "Tên sách";
            colLuotMuon.HeaderText = "Hạn trả";
            colChuaTra.HeaderText = "Trễ (ngày)";

            foreach (DataRow r in dt.Rows)
            {
                dgvSachMuon.Rows.Add(r["DocGia"], r["TenSach"], r["HanTra"], r["SoNgayTre"]);
            }

            lblTongDauSach.Text = $"Tổng cộng: {dt.Rows.Count} phiếu quá hạn";
            lblTongLuotChuaTra.Text = "Cần nhắc trả & thu phạt";
        }
    }

    
    private void BtnXuatExcel_Click(object? sender, EventArgs e)
    {
        if (dgvSachMuon.Rows.Count == 0)
        {
            MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        
        using var sfd = new SaveFileDialog
        {
            Filter = "Excel CSV (*.csv)|*.csv",
            FileName = $"BaoCaoThuVien_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        var sb = new StringBuilder();
        
        var headers = dgvSachMuon.Columns.Cast<DataGridViewColumn>().Select(c => $"\"{c.HeaderText}\"");
        sb.AppendLine(string.Join(",", headers));

        
        foreach (DataGridViewRow row in dgvSachMuon.Rows)
        {
            var cells = row.Cells.Cast<DataGridViewCell>()
                .Select(c => $"\"{c.Value?.ToString()?.Replace("\"", "\"\"")}\"");
            sb.AppendLine(string.Join(",", cells));
        }

       
        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
        MessageBox.Show("Xuất tệp Excel thành công!", "Hệ thống quản lý thư viện",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    
    private void PnlChartCanvas_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias; 
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit; 

        int width = pnlChartCanvas.Width;
        int height = pnlChartCanvas.Height;

        
        int padLeft = 36, padRight = 16, padTop = 30, padBottom = 64;
        int chartW = width - padLeft - padRight;
        int chartH = height - padTop - padBottom;
        if (chartW <= 0 || chartH <= 0) return;

        
        using Font titleFont = new("Segoe UI", 8.5F);
        using Font axisFont = new("Segoe UI", 8F);
        using Font valueFont = new("Segoe UI", 9F, FontStyle.Bold);
        using Brush mutedBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
        using Brush barBrush = new SolidBrush(Color.FromArgb(37, 99, 235));
        using Pen gridPen = new(Color.FromArgb(241, 245, 249), 1F);
        using Pen axisPen = new(Color.FromArgb(226, 232, 240), 1F);

        g.DrawString("Lượt mượn", titleFont, mutedBrush, 4, 4);

        
        int maxVal = 4;
        int tongLuot = 0;
        foreach (var item in _duLieuBieuDo)
        {
            if (item.SoLuot > maxVal) maxVal = item.SoLuot;
            tongLuot += item.SoLuot;
        }

        int steps = 4; 
        int stepVal = Math.Max(1, (int)Math.Ceiling(maxVal / (double)steps));
        int topVal = stepVal * steps;

       
        for (int i = 0; i <= steps; i++)
        {
            int val = i * stepVal;
            float y = padTop + chartH - (chartH * (float)val / topVal);
            g.DrawLine(i == 0 ? axisPen : gridPen, padLeft, y, width - padRight, y);

            string yLabel = val.ToString();
            SizeF lblSize = g.MeasureString(yLabel, axisFont);
            g.DrawString(yLabel, axisFont, mutedBrush, padLeft - lblSize.Width - 8, y - lblSize.Height / 2);
        }

        
        g.DrawLine(axisPen, padLeft, padTop, padLeft, padTop + chartH);

        
        int count = _duLieuBieuDo.Count;
        if (count > 0)
        {
            float slotW = (float)chartW / count; 
            float barW = Math.Min(48F, slotW * 0.45F);

            for (int i = 0; i < count; i++)
            {
                var (nhan, soLuot) = _duLieuBieuDo[i];
                float barH = topVal > 0 ? (chartH * (float)soLuot / topVal) : 0; 
                float x = padLeft + i * slotW + (slotW - barW) / 2F; 
                float y = padTop + chartH - barH; 
                if (barH > 0)
                {
                    g.FillRectangle(barBrush, x, y, barW, barH); 

                    
                    string valText = soLuot.ToString();
                    SizeF valSize = g.MeasureString(valText, valueFont);
                    float valX = x + (barW - valSize.Width) / 2F;
                    if (barH >= 24)
                        g.DrawString(valText, valueFont, Brushes.White, valX, y + 6);
                    else
                        g.DrawString(valText, valueFont, barBrush, valX, y - valSize.Height - 2);
                }

                
                SizeF xSize = g.MeasureString(nhan, axisFont);
                float xLabelPos = padLeft + i * slotW + (slotW - xSize.Width) / 2F;
                g.DrawString(nhan, axisFont, mutedBrush, xLabelPos, padTop + chartH + 8);
            }
        }

        
        string legendText = $"Lượt mượn theo tuần • Tổng: {tongLuot} lượt";
        SizeF legendSize = g.MeasureString(legendText, titleFont);
        float legendX = (width - (14 + legendSize.Width)) / 2F;
        float legendY = height - 24;

        g.FillRectangle(barBrush, legendX, legendY + 3, 10, 10);
        g.DrawString(legendText, titleFont, mutedBrush, legendX + 15, legendY);
    }
}