using _2080601241_NguyenTienPhat_CloneQLThuVien.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2080601241_NguyenTienPhat_CloneQLThuVien
{
    public partial class frmSach : Form
    {
        Sach s = new Sach();
        public bool themmoi = false;

        // Thuc hien tim kiem bang Binding src
        BindingSource bs = new BindingSource();
        public frmSach()
        {
            InitializeComponent();
        }

        private void frmSach_Load(object sender, EventArgs e)
        {
            dgvBook.DataSource = s.LayDsSach(); // Load danh sách sách
            CapNhatTongSoSach(); // Cập nhật tổng số sách khi form load
            dgvBook.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            setNull();
           
        }

        private void HienThiDanhSachSach()
        {
            DataTable dt = s.LayDsSach();
            bs.DataSource = dt;
            dgvBook.DataSource = bs;
            dgvBook.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void setNull()
        {
            txtBookID.Text = "";
            txtBookName.Text = "";
            txtBAuthor.Text = "";
            txtCost.Text = "";
            txtExporter.Text = "";
            txtTotalBook.Text = "";
            txtYearEx.Text = "";
            dtpDayImport.Text = "";
        }
        private bool KiemTraNhapLieu()
        {
            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrWhiteSpace(txtBookName.Text))
            {
                MessageBox.Show("Tên sách không được để trống!", "Thông báo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBAuthor.Text))
            {
                MessageBox.Show("Tên tác giả không được để trống!", "Thông báo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtExporter.Text))
            {
                MessageBox.Show("Nhà xuất bản không được để trống!", "Thông báo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtYearEx.Text) || !int.TryParse(txtYearEx.Text, out _))
            {
                MessageBox.Show("Năm xuất bản không hợp lệ!", "Thông báo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCost.Text) || !float.TryParse(txtCost.Text, out _))
            {
                MessageBox.Show("Trị giá không hợp lệ!", "Thông báo");
                return false;
            }
            return true; // Nếu tất cả các kiểm tra đều qua, trả về true
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Đặt biến themmoi thành true để chỉ ra rằng đang ở chế độ thêm mới
            setButton(true);
            themmoi = true;
            txtBookName.Focus();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvBook.CurrentRow != null)
            {
                // Lấy thông tin từ hàng đã chọn
                DataGridViewRow row = dgvBook.CurrentRow;

                // Hiển thị dữ liệu vào các TextBox
                txtBookID.Text = row.Cells["MaSach"].Value.ToString();
                txtBookName.Text = row.Cells["TenSach"].Value.ToString();
                txtBAuthor.Text = row.Cells["TacGia"].Value.ToString();
                txtYearEx.Text = row.Cells["NamXuatBan"].Value.ToString();
                txtExporter.Text = row.Cells["NhaXuatBan"].Value.ToString();
                txtCost.Text = row.Cells["TriGia"].Value.ToString();
                dtpDayImport.Value = DateTime.Parse(row.Cells["NgayNhap"].Value.ToString());

                // Đặt biến themmoi thành false
                themmoi = false;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime ngayNhap = dtpDayImport.Value;

            if (!KiemTraNhapLieu()) // Giả sử bạn có phương thức này để kiểm tra dữ liệu đầu vào
            {
                return; // Nếu kiểm tra không thành công, thoát ra
            }

            if (themmoi)
            {
                // Thêm mới sách
                s.ThemSach(txtBookName.Text, txtBAuthor.Text, int.Parse(txtYearEx.Text),
                           txtExporter.Text, float.Parse(txtCost.Text), ngayNhap);
                MessageBox.Show("Thêm mới thành công");
            }
            else
            {
                // Cập nhật sách
                int maSach = int.Parse(txtBookID.Text); // Lấy MaSach để cập nhật
                s.CapNhatSach(maSach, txtBookName.Text, txtBAuthor.Text, int.Parse(txtYearEx.Text),
                              txtExporter.Text, float.Parse(txtCost.Text), ngayNhap);
                MessageBox.Show("Cập nhật thành công");
            }

            // Làm mới danh sách và cập nhật tổng số sách
            dgvBook.DataSource = s.LayDsSach(); // Refresh the DataGridView
            setNull();
            CapNhatTongSoSach();
        }


        // Xóa sách
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mã sách có rỗng không
            if (!string.IsNullOrEmpty(txtBookID.Text))
            {
                // Hỏi người dùng có chắc chắn muốn xóa không
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Lấy mã sách từ TextBox
                    int maSach = int.Parse(txtBookID.Text);

                    // Gọi phương thức xóa sách từ lớp Sach
                    s.XoaSach(maSach);

                    // Hiển thị lại danh sách sau khi xóa
                    dgvBook.DataSource = s.LayDsSach();
                    MessageBox.Show("Xóa sách thành công!");

                    // Xóa sạch các TextBox sau khi xóa thành công
                    setNull();
                    CapNhatTongSoSach();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Sự kiện khi người dùng nhấp vào ô trong DataGridView
        private void dgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có click vào một dòng hợp lệ không
            if (e.RowIndex >= 0) // Đảm bảo hàng được chọn là hợp lệ
            {
                // Lấy dữ liệu của hàng được chọn
                DataGridViewRow row = dgvBook.Rows[e.RowIndex];

                // Kiểm tra các ô có giá trị không null
                if (row.Cells["MaSach"].Value != null &&
                    row.Cells["TenSach"].Value != null &&
                    row.Cells["TacGia"].Value != null &&
                    row.Cells["NamXuatBan"].Value != null &&
                    row.Cells["NhaXuatBan"].Value != null &&
                    row.Cells["TriGia"].Value != null &&
                    row.Cells["NgayNhap"].Value != null)
                {
                    // Hiển thị dữ liệu từ hàng đã chọn vào các TextBox
                    txtBookID.Text = row.Cells["MaSach"].Value.ToString();
                    txtBookName.Text = row.Cells["TenSach"].Value.ToString();
                    txtBAuthor.Text = row.Cells["TacGia"].Value.ToString();
                    txtYearEx.Text = row.Cells["NamXuatBan"].Value.ToString();
                    txtExporter.Text = row.Cells["NhaXuatBan"].Value.ToString();
                    txtCost.Text = row.Cells["TriGia"].Value.ToString();

                    // Kiểm tra và chuyển đổi giá trị Ngày Nhập
                    string dateValue = row.Cells["NgayNhap"].Value.ToString();
                    if (DateTime.TryParse(dateValue, out DateTime parsedDate))
                    {
                        dtpDayImport.Value = parsedDate; // Chỉ đặt giá trị nếu chuyển đổi thành công
                    }
                    else
                    {
                        MessageBox.Show("Ngày nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtpDayImport.Value = DateTime.Now; // Hoặc một giá trị mặc định
                    }

                    themmoi = false;
                }
                else
                {
                    // Không hiển thị thông báo nếu hàng không hợp lệ
                    // Thay vào đó, có thể chỉ cần đặt các TextBox về rỗng nếu cần
                    setNull();
                }
            }
        }


        private void CapNhatTongSoSach()
        {
            // Đếm số dòng trong DataGridView
            int totalBooks = dgvBook.Rows.Count-1; //-1 dong null

            // Hiển thị tổng số sách (giả sử có nhãn lblTotalBooks)
            txtTotalBook.Text =  totalBooks.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim(); // Lấy từ khóa tìm kiếm
            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu không có từ khóa tìm kiếm, hiển thị toàn bộ danh sách sách
                dgvBook.DataSource = s.LayDsSach();
            }
            else
            {
                // Lọc danh sách sách theo từ khóa
                dgvBook.DataSource = s.TimKiemSach(keyword);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setButton(false);
        }

        private void setButton(bool v)
        {
            btnSave.Enabled = v;
            btnCancel.Enabled = v;
            btnDelete.Enabled = !v;
            btnClose.Enabled = !v;
            btnAdd.Enabled = !v;
            btnSua.Enabled = !v;
        }
    }
}
