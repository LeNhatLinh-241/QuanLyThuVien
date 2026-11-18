using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien.UI.UserControls
{
    partial class ucBook
    {
        private DataGridView dgvDanhSachSach = null!;

        private void InitializeComponent()
        {
            SuspendLayout();

            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9F);
            Size = new Size(1240, 760);

            var page = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(241, 245, 249),
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12, 10, 12, 8)
            };
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));

            var header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            header.Controls.Add(new Label
            {
                Text = "QUẢN LÝ KHO SÁCH & QUẢN LÝ MÃ VẠCH",
                Dock = DockStyle.Left,
                Width = 850,
                Padding = new Padding(18, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59)
            });
            header.Controls.Add(new Label
            {
                Text = $"Xin chào: Thủ thư  |  Ngày: {DateTime.Now:dd/MM/yyyy}",
                Dock = DockStyle.Right,
                Width = 600,
                Padding = new Padding(0, 0, 14, 0),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(100, 116, 139)
            });

            var content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 10, 0, 8),
                BackColor = Color.Transparent
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 470));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var categoriesPanel = CreateCategoriesPanel();
            categoriesPanel.Margin = new Padding(0, 0, 10, 0);
            content.Controls.Add(categoriesPanel, 0, 0);
            content.Controls.Add(CreateBookTitlesPanel(), 1, 0);

            var footer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            footer.Controls.Add(new Label
            {
                Text = "Sẵn sàng  |  Dữ liệu đã đồng bộ",
                Dock = DockStyle.Left,
                Width = 600,
                Padding = new Padding(8, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(100, 116, 139)
            });
            footer.Controls.Add(new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN",
                Dock = DockStyle.Right,
                Width = 260,
                Padding = new Padding(0, 0, 8, 0),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(100, 116, 139)
            });

            page.Controls.Add(header, 0, 0);
            page.Controls.Add(content, 0, 1);
            page.Controls.Add(footer, 0, 2);
            Controls.Add(page);

            ResumeLayout(false);
        }

        private static Control CreateCategoriesPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.White
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30 ));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            layout.Controls.Add(CreateSectionTitle("THỂ LOẠI SÁCH (CÂY PHÂN CẤP)"), 0, 0);
            layout.Controls.Add(new Label
            {
                Text = "4 nhóm",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(100, 116, 139)
            }, 0, 1);

            var tree = new TreeView
            {
                Name = "tvTheLoai",
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(51, 65, 85),
                Font = new Font("Segoe UI", 9F),
                HideSelection = false,
                FullRowSelect = true,
                ShowLines = true,
                ShowPlusMinus = true,
                ShowRootLines = true,
                ItemHeight = 40,
                Indent = 18
            };
            var technology = tree.Nodes.Add("Công nghệ thông tin");
            technology.Nodes.Add("Lập trình C#");
            technology.Nodes.Add("Cơ sở dữ liệu");
            tree.Nodes.Add("Kinh tế");
            tree.Nodes.Add("Ngoại ngữ");
            
            technology.Expand();
            tree.SelectedNode = technology;
            layout.Controls.Add(tree, 0, 2);
            
            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0, 3, 0, 0),
                Margin = Padding.Empty
            };
            
            var btnThem = CreateButton(
                "Thêm",
                Color.FromArgb(37, 99, 235),
                Color.White,
                120
            );
            btnThem.Height = 48;

            var btnSua = CreateButton(
                "Sửa",
                Color.White,
                Color.FromArgb(30, 41, 59),
                90
            );
            btnSua.Height = 48;

            var btnXoa = CreateButton(
                "Xóa thể loại",
                Color.White,
                Color.FromArgb(220, 38, 38),
                200
            );
            
            foreach (Button btn in new[] { btnThem, btnSua, btnXoa })
            {
                btn.Height = 48;
                btn.Margin = new Padding(2);
                btn.Padding = new Padding(0);
            }
            
            buttons.Controls.Add(btnThem);
            buttons.Controls.Add(btnSua);
            buttons.Controls.Add(btnXoa);

            layout.Controls.Add(buttons, 0, 3);

            layout.Controls.Add(buttons, 0, 3);

            panel.Controls.Add(layout);
            return panel;
        }

        private Control CreateBookTitlesPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.White
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 57));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var titleHeader = new Panel { Dock = DockStyle.Fill };
            titleHeader.Controls.Add(CreateSectionTitle("ĐẦU SÁCH - BookTitles"));
            titleHeader.Controls.Add(new Label
            {
                Text = "Đang chọn: SACH01",
                Dock = DockStyle.Right,
                Width = 300,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(100, 116, 139)
            });
            layout.Controls.Add(titleHeader, 0, 0);
            layout.Controls.Add(CreateBookFields(), 0, 1);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 2, 0, 0)
            };
            var btnThem = CreateButton(
                "+ Thêm & Sinh mã vạch",
                Color.FromArgb(37, 99, 235),
                Color.White,
                305
            );
            btnThem.Height = 50;
            
            var btnSua = CreateButton(
                "Sửa",
                Color.White,
                Color.FromArgb(30, 41, 59),
                70
            );
            btnSua.Height = 48;
            
            var btnXoa = CreateButton(
                "Xóa",
                Color.White,
                Color.FromArgb(220, 38, 38),
                66
            );
            btnXoa.Height = 48;
            
            buttons.Controls.Add(btnThem);
            buttons.Controls.Add(btnSua);
            buttons.Controls.Add(btnXoa);
            
            layout.Controls.Add(buttons, 0, 2);
            
            dgvDanhSachSach = CreateBookTitlesGrid();
            layout.Controls.Add(dgvDanhSachSach, 0, 3);

            panel.Controls.Add(layout);
            return panel;
        }

        private static Control CreateBookFields()
        {
            var fields = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                BackColor = Color.White,
                Margin = Padding.Empty
            };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18));

            fields.Controls.Add(CreateField("Tên đầu sách", new TextBox { Text = "Lập trình C# căn bản" }), 0, 0);
            fields.Controls.Add(CreateField("Tác giả", new TextBox { Text = "Phạm Hữu Khang" }), 1, 0);
            fields.Controls.Add(CreateField("Giá tiền", new TextBox { Text = "120.000" }), 2, 0);

            var category = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            category.Items.AddRange(new object[]
            {
                "Công nghệ thông tin / Lập trình C#",
                "Kinh tế",
                "Ngoại ngữ"
            });
            category.SelectedIndex = 0;
            fields.Controls.Add(CreateField("Thể loại", category), 3, 0);
            fields.Controls.Add(CreateField("Số lượng nhập", new TextBox { Text = "10" }), 4, 0);
            return fields;
        }

        private static Control CreateField(string label, Control editor)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(2, 0, 5, 0),
                BackColor = Color.White
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.Controls.Add(new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(51, 65, 85)
            }, 0, 0);
            editor.Dock = DockStyle.Fill;
            editor.Margin = new Padding(0, 0, 0, 2);
            layout.Controls.Add(editor, 0, 1);
            return layout;
        }

        private static DataGridView CreateBookTitlesGrid()
        {
            var grid = new DataGridView
            {
                Name = "dgvDanhSachSach",
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 50,
                RowTemplate = { Height = 45 },
                Font = new Font("Segoe UI", 9F),
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(239, 246, 255),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    SelectionBackColor = Color.FromArgb(239, 246, 255),
                    SelectionForeColor = Color.FromArgb(30, 41, 59)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    ForeColor = Color.FromArgb(30, 41, 59),
                    BackColor = Color.White,
                    SelectionBackColor = Color.FromArgb(219, 234, 254),
                    SelectionForeColor = Color.FromArgb(30, 41, 59),
                    Padding = new Padding(4)
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 250, 252)
                }
            };
            AddGridColumn(grid, "Mã đầu sách", 15);
            AddGridColumn(grid, "Tên đầu sách", 29);
            AddGridColumn(grid, "Tác giả", 20);
            AddGridColumn(grid, "Thể loại", 19);
            AddGridColumn(grid, "Giá", 9);
            AddGridColumn(grid, "Nhập", 8);
            return grid;
        }

        private static void AddGridColumn(DataGridView grid, string header, float fillWeight)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                FillWeight = fillWeight,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private static Label CreateSectionTitle(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59)
            };
        }

        private static Button CreateButton(string text, Color backColor, Color foreColor, int width)
        {
            return new Button
            {
                Text = text,
                Width = width,
                Height = 32,
                Margin = new Padding(2),
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }
    }
}
