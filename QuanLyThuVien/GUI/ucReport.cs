using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace QuanLyThuVien;

public partial class ucReport : UserControl
{
    private List<(string Nhan, int SoLuot)> _duLieuBieuDo = new()
    {
        ("12-18/10", 1),
        ("19-25/10", 3),
        ("26/10-01/11", 4),
        ("02-07/11", 2)
    };

    public ucReport()
    {
        InitializeComponent();

        if (cboLoaiBaoCao.Items.Count > 0)
            cboLoaiBaoCao.SelectedIndex = 0;

        pnlChartCanvas.Paint += PnlChartCanvas_Paint;
    }

    /// <summary>
    /// Gọi hàm này sau khi truy vấn CSDL để cập nhật lại biểu đồ cột
    /// </summary>
    public void CapNhatBieuDo(List<(string Nhan, int SoLuot)> duLieuMoi)
    {
        _duLieuBieuDo = duLieuMoi ?? new List<(string, int)>();
        pnlChartCanvas.Invalidate();
    }

    private void PnlChartCanvas_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        int width = pnlChartCanvas.Width;
        int height = pnlChartCanvas.Height;

        int padLeft = 36;
        int padRight = 16;
        int padTop = 30;
        int padBottom = 64;

        int chartW = width - padLeft - padRight;
        int chartH = height - padTop - padBottom;
        if (chartW <= 0 || chartH <= 0) return;

        using Font titleFont = new("Segoe UI", 8.5F, FontStyle.Regular);
        using Font axisFont = new("Segoe UI", 8F, FontStyle.Regular);
        using Font valueFont = new("Segoe UI", 9F, FontStyle.Bold);
        using Brush mutedBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
        using Brush barBrush = new SolidBrush(Color.FromArgb(37, 99, 235));
        using Pen gridPen = new(Color.FromArgb(241, 245, 249), 1F);
        using Pen axisPen = new(Color.FromArgb(226, 232, 240), 1F);

        // Nhãn trục Y phía trên
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

        // Vẽ lưới ngang và số trục Y
        for (int i = 0; i <= steps; i++)
        {
            int val = i * stepVal;
            float y = padTop + chartH - (chartH * (float)val / topVal);

            g.DrawLine(i == 0 ? axisPen : gridPen, padLeft, y, width - padRight, y);

            string yLabel = val.ToString();
            SizeF lblSize = g.MeasureString(yLabel, axisFont);
            g.DrawString(yLabel, axisFont, mutedBrush, padLeft - lblSize.Width - 8, y - lblSize.Height / 2);
        }

        // Vẽ trục Y mờ
        g.DrawLine(axisPen, padLeft, padTop, padLeft, padTop + chartH);

        // Vẽ các cột
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
                    {
                        g.DrawString(valText, valueFont, Brushes.White, valX, y + 6);
                    }
                    else
                    {
                        g.DrawString(valText, valueFont, barBrush, valX, y - valSize.Height - 2);
                    }
                }

                // Nhãn trục X
                SizeF xSize = g.MeasureString(nhan, axisFont);
                float xLabelPos = padLeft + i * slotW + (slotW - xSize.Width) / 2F;
                g.DrawString(nhan, axisFont, mutedBrush, xLabelPos, padTop + chartH + 8);
            }
        }

        // Chú thích phía dưới
        string legendText = $"Lượt mượn theo tuần • Tổng: {tongLuot} lượt";
        SizeF legendSize = g.MeasureString(legendText, titleFont);
        float totalLegendW = 14 + legendSize.Width;
        float legendX = (width - totalLegendW) / 2F;
        float legendY = height - 24;

        g.FillRectangle(barBrush, legendX, legendY + 3, 10, 10);
        g.DrawString(legendText, titleFont, mutedBrush, legendX + 15, legendY);
    }
}