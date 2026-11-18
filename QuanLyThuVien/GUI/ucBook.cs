using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien.UI.UserControls
{
    public partial class ucBook : UserControl
    {
        public ucBook()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            dgvDanhSachSach.Rows.Add(
                "SACH01", "Lập trình C# căn bản", "Phạm Hữu Khang",
                "Công nghệ", "120.000", "10"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH02", "Cơ sở dữ liệu", "Nguyễn Thị Ngọc Mai",
                "Công nghệ", "110.000", "8"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH03", "Cấu trúc dữ liệu", "Đỗ Xuân Lôi",
                "Công nghệ", "130.000", "12"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH04", "Kinh tế học vĩ mô", "Nguyễn Văn Dân",
                "Kinh tế", "95.000", "15"
            );
            dgvDanhSachSach.Rows.Add(
                "SACH05", "Dế Mèn phiêu lưu ký", "Tô Hoài",
                "Văn học", "45.000", "20"
            );

            for (var copyNumber = 1; copyNumber <= 4; copyNumber++)
            {
                var rowIndex = dgvCuonSachVatLy.Rows.Add(
                    $"SACH01_{copyNumber}",
                    "SACH01",
                    "Sẵn sàng trên kệ",
                    "0",
                    "Kệ A1-01"
                );
                dgvCuonSachVatLy.Rows[rowIndex].Cells[2].Style.ForeColor =
                    Color.FromArgb(22, 163, 74);
            }

            dgvDanhSachSach.Rows[0].Selected = true;
        }
    }
}
