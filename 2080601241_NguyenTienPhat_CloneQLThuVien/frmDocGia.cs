using System;
using System.Data;
using System.Windows.Forms;

namespace _2080601241_NguyenTienPhat_CloneQLThuVien
{
    public partial class frmDocGia : Form
    {
        DocGia dg = new DocGia();
        public bool themmoi = false;

        // Thuc hien tim kiem bang Binding src
        BindingSource bs = new BindingSource();
        public frmDocGia()
        {
            InitializeComponent();
            setButton(true);
            setNull();
        }

        private void frmDocGia_Load(object sender, EventArgs e)
        {
            dgvDocGia.DataSource = dg.LayDSDocGia();
            DataTable dt = dg.LayDSDocGia(); // Giả sử phương thức này trả về DataTable
            dgvDocGia.DataSource = bs; // Gán BindingSource cho DataGridView
            bs.DataSource = dt; // Gán nguồn dữ liệu cho BindingSource
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void setNull()
        {
            txtTenDG.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            dtpNgayLapThe.Value = DateTime.Now; // Đặt lại ngày lập thẻ
            dtpNgayHetHan.Value = DateTime.Now.AddMonths(24); // Đặt hạn sử dụng mặc định
        }

        private void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnXoa.Enabled = val;
            btnSua.Enabled = val;
            btnThoat.Enabled = val;
            btnLuu.Enabled = !val;
            btnHuy.Enabled = !val;
        }

        private void dgvDocGia_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count > 0)
            {
                // Lấy dòng đã chọn
                DataGridViewRow row = dgvDocGia.SelectedRows[0];

                // Cập nhật các TextBox từ hàng đã chọn
                txtTenDG.Text = row.Cells["HoTenDocGia"].Value?.ToString() ?? string.Empty;
                dtpNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgaySinh"].Value.ToString())
                    : DateTime.Now; // Hoặc một giá trị mặc định hợp lý

                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;

                dtpNgayLapThe.Value = row.Cells["NgayLapThe"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgayLapThe"].Value.ToString())
                    : DateTime.Now; // Hoặc một giá trị mặc định hợp lý

                dtpNgayHetHan.Value = row.Cells["NgayHetHan"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgayHetHan"].Value.ToString())
                    : DateTime.Now; // Hoặc một giá trị mặc định hợp lý
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themmoi = true;
            setButton(false);
            setNull(); // Đặt lại các trường nhập liệu
            txtTenDG.Focus(); // Đặt con trỏ vào trường "Tên Đọc Giả"
        }
        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra rằng hàng đã chọn hợp lệ
            {
                // Lấy dòng đã chọn
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                // Cập nhật các TextBox từ hàng đã chọn
                txtTenDG.Text = row.Cells["HoTenDocGia"].Value?.ToString() ?? string.Empty;
                dtpNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgaySinh"].Value.ToString())
                    : DateTime.Now; // Hoặc một giá trị mặc định hợp lý

                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;

                dtpNgayLapThe.Value = row.Cells["NgayLapThe"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgayLapThe"].Value.ToString())
                    : DateTime.Now; // Hoặc một giá trị mặc định hợp lý

                dtpNgayHetHan.Value = row.Cells["NgayHetHan"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgayHetHan"].Value.ToString())
                    : DateTime.Now; // Hoặc một giá trị mặc định hợp lý
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DateTime ngayLapThe = dtpNgayLapThe.Value;
            DateTime ngayHetHan = dtpNgayHetHan.Value;
            DateTime ngaySinh = dtpNgaySinh.Value;

            if (themmoi)
            {
                dg.ThemDocGia(txtTenDG.Text, ngaySinh, txtDiaChi.Text, txtEmail.Text, ngayLapThe, ngayHetHan);
                MessageBox.Show("Thêm mới thành công");
            }
            else
            {
                // Giả sử mã đọc giả được lấy từ dòng đã chọn
                string maDocGia = dgvDocGia.SelectedRows[0].Cells[0].Value.ToString();
                dg.CapNhatDocGia(txtTenDG.Text, ngaySinh, txtDiaChi.Text, txtEmail.Text, ngayLapThe, ngayHetHan);
                MessageBox.Show("Cập nhật thành công");
            }

            dgvDocGia.DataSource = dg.LayDSDocGia(); // Refresh the DataGridView
            setNull();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count > 0)
            {
                string maDocGia = dgvDocGia.SelectedRows[0].Cells["MaDocGia"].Value.ToString(); // Lấy mã đọc giả từ dòng đã chọn
                DialogResult dr = MessageBox.Show("Bạn có chắc xóa không?", "Xóa đọc giả", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    dg.XoaDocGia(maDocGia);
                    MessageBox.Show("Xóa thành công");
                    dgvDocGia.DataSource = dg.LayDSDocGia(); // Refresh the DataGridView
                    setNull(); // Đặt lại các trường nhập liệu
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn một đọc giả để xóa");
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ texbox
                string tenDocGia = txtTenDG.Text;
                DateTime ngaySinh = dtpNgaySinh.Value;
                string diaChi = txtDiaChi.Text;
                string email = txtEmail.Text;
                DateTime ngayLapThe = dtpNgayLapThe.Value;
                DateTime ngayHetHan = dtpNgayHetHan.Value;

                // Cập nhật thông tin đọc giả
                dg.CapNhatDocGia(tenDocGia, ngaySinh, diaChi, email, ngayLapThe, ngayHetHan);
                MessageBox.Show("Cập nhật thành công");

                // Làm mới DataGridView
                dgvDocGia.DataSource = dg.LayDSDocGia(); // Refresh the DataGridView
                setNull(); // Đặt lại các trường nhập liệu
            }
            else
            {
                MessageBox.Show("Bạn phải chọn một đọc giả để sửa");
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị tìm kiếm từ TextBox
            string searchValue = txtTimDG.Text.Trim();

            // Nếu không có gì để tìm, thì hiển thị tất cả
            if (string.IsNullOrEmpty(searchValue))
            {
                bs.RemoveFilter();
            }
            else
            {
                // Áp dụng bộ lọc theo cột "HoTen" (tên đọc giả)
                bs.Filter = string.Format("HoTenDocGia LIKE '%{0}%'", searchValue);
            }
        }
    }
}
