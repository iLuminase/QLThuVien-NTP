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
        private bool KiemTraNhapLieu()
        {
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtTenDG.Text))
            {

                txtTenDG.Focus();
                MessageBox.Show("Họ tên không được để trống");
                return false;
            }
            // Kiểm tra ngày sinh
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại");
                dtpNgaySinh.Focus();
                return false;
            }
            if (txtDiaChi.Text.Length > 200)
            {
                MessageBox.Show("Địa chỉ quá dài!");
                txtDiaChi.Focus();
                return false;
            }
            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("Địa chỉ Email quá dài!");
                txtEmail.Focus();
                return false;
            }
            if (dtpNgayLapThe.Value < dtpNgaySinh.Value)
            {
                MessageBox.Show("Ngày lập thẻ phải lớn hơn ngày sinh");
                dtpNgayLapThe.Focus();
                return false;
            }
            if (dtpNgayHetHan.Value < dtpNgayLapThe.Value)
            {
                MessageBox.Show("Ngày hết hạn sử dụng phải lớn hơn ngày lập thẻ");
                dtpNgayHetHan.Focus();
                return false;
            }
            return true;
        }
        private void dgvDocGia_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow != null && dgvDocGia.CurrentRow.Cells["MaDocGia"].Value != null)
            {
                // Lấy dòng hiện tại và cập nhật các điều khiển nhập liệu
                DataGridViewRow row = dgvDocGia.CurrentRow;

                txtTenDG.Text = row.Cells["HoTenDocGia"].Value?.ToString() ?? string.Empty;
                dtpNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgaySinh"].Value.ToString())
                    : DateTime.Now;

                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
                dtpNgayLapThe.Value = row.Cells["NgayLapThe"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgayLapThe"].Value.ToString())
                    : DateTime.Now;

                dtpNgayHetHan.Value = row.Cells["NgayHetHan"].Value != DBNull.Value
                    ? DateTime.Parse(row.Cells["NgayHetHan"].Value.ToString())
                    : DateTime.Now;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themmoi = true;
            setButton(false);
            txtTenDG.Focus(); // Đặt con trỏ vào trường "Tên Đọc Giả"

        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra chỉ số dòng hợp lệ
            if (e.RowIndex >= 0 && dgvDocGia.Rows[e.RowIndex].Cells[0].Value != null)
            {
                // Lấy dòng đã chọn
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

                // Kiểm tra giá trị của từng ô trước khi gán vào điều khiển tương ứng
                if (row.Cells[1].Value != null)
                    txtTenDG.Text = row.Cells[1].Value.ToString();

                if (row.Cells[2].Value != null && DateTime.TryParse(row.Cells[2].Value.ToString(), out DateTime ngaySinh))
                    dtpNgaySinh.Value = ngaySinh;

                if (row.Cells[3].Value != null)
                {
                    txtDiaChi.Text = row.Cells[3].Value.ToString();
                }
                if (row.Cells[4].Value != null)
                    txtEmail.Text = row.Cells[4].Value.ToString();

                if (row.Cells[5].Value != null && DateTime.TryParse(row.Cells[5].Value.ToString(), out DateTime ngayLapThe))
                    dtpNgayLapThe.Value = ngayLapThe;

                if (row.Cells[6].Value != null && DateTime.TryParse(row.Cells[6].Value.ToString(), out DateTime ngayHetHan))
                    dtpNgayHetHan.Value = ngayHetHan;
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraNhapLieu()) // Kiểm tra xem người dùng đã nhập liệu đầy đủ chưa
            {
                return;
            }

            DateTime ngaySinh = dtpNgaySinh.Value;
            DateTime ngayLapThe = dtpNgayLapThe.Value;
            DateTime ngayHetHan = dtpNgayHetHan.Value;

            if (themmoi)
            {
                // Thêm mới độc giả
                dg.ThemDocGia(txtTenDG.Text, ngaySinh, txtDiaChi.Text, txtEmail.Text, ngayLapThe, ngayHetHan);
                MessageBox.Show("Thêm mới thành công");
            }
            else
            {
                // Cập nhật độc giả
                if (dgvDocGia.CurrentRow != null)
                {
                    // Lấy mã độc giả từ dòng hiện tại
                    string maDocGia = dgvDocGia.CurrentRow.Cells["MaDocGia"].Value.ToString(); // Thay đổi tên cột nếu cần

                    // Gọi phương thức cập nhật
                    dg.CapNhatDocGia(int.Parse(maDocGia), txtTenDG.Text, ngaySinh, txtDiaChi.Text, txtEmail.Text, ngayLapThe, ngayHetHan);
                    MessageBox.Show($"Cập nhật thông tin cho đọc giả {txtTenDG.Text} thành công");
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn một độc giả để sửa", "Thông báo");
                }
            }

            // Làm mới DataGridView
            dgvDocGia.DataSource = dg.LayDSDocGia(); // Load lại danh sách đọc giả
            setNull(); // Đặt lại các điều khiển nhập liệu về trạng thái ban đầu
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
            if (!KiemTraNhapLieu())
            {
                return;
            }

            // Kiểm tra xem có dòng nào đang được chọn không
            if (dgvDocGia.CurrentRow != null && dgvDocGia.CurrentRow.Cells["MaDocGia"].Value != null)
            {
                // Lấy thông tin từ dòng hiện tại trong DataGridView
                DataGridViewRow row = dgvDocGia.CurrentRow;

                // Điền thông tin vào các điều khiển nhập liệu
                txtTenDG.Text = row.Cells["HoTenDocGia"].Value.ToString();
                dtpNgaySinh.Value = DateTime.Parse(row.Cells["NgaySinh"].Value.ToString());
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                dtpNgayLapThe.Value = DateTime.Parse(row.Cells["NgayLapThe"].Value.ToString());
                dtpNgayHetHan.Value = DateTime.Parse(row.Cells["NgayHetHan"].Value.ToString());

                // Bật nút Lưu để lưu thay đổi sau khi sửa
                btnLuu.Enabled = true;
                btnSua.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn phải chọn một đọc giả để sửa", "Thông báo");
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
