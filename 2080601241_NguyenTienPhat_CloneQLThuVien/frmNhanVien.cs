using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
namespace _2080601241_NguyenTienPhat_CloneQLThuVien
{
    public partial class frmNhanVien : Form
    {

        NhanVien nv = new NhanVien();
        public bool themmoi = false;
        SqlConnection sqlConn; //khai báo biến connection
        SqlDataAdapter da; //khai báo biến dataAdapter
        DataSet ds = new DataSet(); //khai báo 1 dataset
        public string srvName = "_HEHENIKEN"; //chỉ định tên server
        public string dbName = "QLTHUVIEN"; //chỉ định tên CSDL
        void KetnoiCSDL() //thực hiện kết nối bằng chuỗi kết nối
        {
            string connStr = "Data source=" + srvName + ";database=" + dbName + ";Integrated Security = True";
            sqlConn = new SqlConnection(connStr);
        }

       
        DataTable layDanhSachNhanVien() //lấy danh sách nhân viên
        {
            
            string sql = "Select * from NhanVien";
            da = new SqlDataAdapter(sql, sqlConn);
            da.Fill(ds);
            return ds.Tables[0];
        }
        void LoadListview()
        {
            
            lsvNhanVien.FullRowSelect = true; //cho phép chọn 1 dòng
            lsvNhanVien.View = View.Details; //cho phép hiển thị thông tin chi tiết dạng bảng
            DataTable dt = layDanhSachNhanVien();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lvi =
                lsvNhanVien.Items.Add(dt.Rows[i]["Hotennhanvien"].ToString());
                //dòng thứ i, tên cột là nhân viên
                lvi.SubItems.Add(dt.Rows[i][2].ToString()); //dùng chỉ số cột : dòng thứ i,cột thứ 1
                lvi.SubItems.Add(dt.Rows[i][4].ToString());
                lvi.SubItems.Add(dt.Rows[i][3].ToString());
            }
        }
        public frmNhanVien()
        {
            InitializeComponent();
            lsvNhanVien.Columns.Add("Mã NV:", 90, HorizontalAlignment.Left);
            lsvNhanVien.Columns.Add("Họ tên:", 220, HorizontalAlignment.Left);
            lsvNhanVien.Columns.Add("Ngày Sinh:", 110, HorizontalAlignment.Left);
            lsvNhanVien.Columns.Add("Địa chỉ:", 280, HorizontalAlignment.Left);
            lsvNhanVien.Columns.Add("Điện thoại:", 110, HorizontalAlignment.Center);
            lsvNhanVien.Columns.Add("Bằng cấp:", 110, HorizontalAlignment.Center);

            HienthiNhanvien();
            HienthiBangCap();
            setButton(true);
            setNull();
        }
        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            HienthiNhanvien();
            
        }
        private void HienthiBangCap()
        {
            DataTable dt = nv.LayBangcap();
            cbxBangCap.DataSource = dt;
            cbxBangCap.DisplayMember = "TenBangCap";
            cbxBangCap.ValueMember = "MaBangCap";
        }
        void setNull()
        {
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }
        void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnXoa.Enabled = val;
            btnSua.Enabled = val;
            btnThoat.Enabled = val;
            btnLuu.Enabled = !val;
            btnHuy.Enabled = !val;
        }


        // Kiểm tra các trường nhập liệu trước khi lưu
        private bool KiemTraNhapLieu()
        {
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống.");
                txtHoTen.Focus();
                return false;
            }

            // Kiểm tra ngày sinh
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.");
                dtpNgaySinh.Focus();
                return false;
            }

            // Kiểm tra địa chỉ (không bắt buộc, nhưng có thể kiểm tra nếu cần)
            if (txtDiaChi.Text.Length > 200)
            {
                MessageBox.Show("Địa chỉ quá dài.");
                txtDiaChi.Focus();
                return false;
            }

            // Kiểm tra điện thoại (chỉ cho phép nhập số và độ dài hợp lệ)
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtDienThoai.Text, @"^\d{10,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập 10 hoặc 11 chữ số.");
                txtDienThoai.Focus();
                return false;
            }

            // Kiểm tra bằng cấp
            if (cbxBangCap.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn bằng cấp.");
                cbxBangCap.Focus();
                return false;
            }

            return true;
        }
        void HienthiNhanvien()
        {
            DataTable dt = nv.LayDSNhanvien();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lvi =
                lsvNhanVien.Items.Add(dt.Rows[i][0].ToString());
                lvi.SubItems.Add(dt.Rows[i][1].ToString());
                lvi.SubItems.Add(dt.Rows[i][2].ToString());
                lvi.SubItems.Add(dt.Rows[i][3].ToString());
                lvi.SubItems.Add(dt.Rows[i][4].ToString());
                lvi.SubItems.Add(dt.Rows[i][5].ToString());
            }
        }


        private void lsvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvNhanVien.SelectedIndices.Count > 0)
            {
                txtHoTen.Text = lsvNhanVien.SelectedItems[0].SubItems[1].Text;
                //Chuyen sang kieu dateTime
                dtpNgaySinh.Value = DateTime.Parse(lsvNhanVien.SelectedItems[0].SubItems[2].Text);
                txtDiaChi.Text =
                lsvNhanVien.SelectedItems[0].SubItems[3].Text;
                txtDienThoai.Text =
                lsvNhanVien.SelectedItems[0].SubItems[4].Text;
                //Tìm vị trí của Tên bằng cấp trong Combobox
                cbxBangCap.SelectedIndex =
                cbxBangCap.FindString(lsvNhanVien.SelectedItems[0].SubItems[5].Text);
            }
        }

        //Còn tiếp cho các sự kiện khác
        private void btnThem_Click(object sender, EventArgs e)
        {
            setNull();
            themmoi = true;
            setButton(false);
            txtHoTen.Focus();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lsvNhanVien.SelectedIndices.Count > 0)
            {
                themmoi = false;
                setButton(false);
            }
            else
                MessageBox.Show("Bạn phải chọn mẫu tin cập nhật",
               "Sửa mẫu tin");

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lsvNhanVien.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc xóa không ? ", "Xóa bằng cấp", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    nv.XoaNhanVien(lsvNhanVien.SelectedItems[0].SubItems[0].Text);
                    lsvNhanVien.Items.RemoveAt(lsvNhanVien.SelectedIndices[0]);
                    setNull();
                }
            }
            else
                MessageBox.Show("Bạn phải chọn mẩu tin cần xóa");
        }
        

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim();

            // Gọi phương thức tìm kiếm trong NhanVien
            DataTable dtTimKiem = nv.TimKiemNhanVien(keyword);

            // Xóa các mục hiện tại trong ListView trước khi hiển thị kết quả mới
            lsvNhanVien.Items.Clear();

            // Hiển thị kết quả tìm kiếm trong ListView
            foreach (DataRow row in dtTimKiem.Rows)
            {
                ListViewItem lvi = lsvNhanVien.Items.Add(row["MaNhanVien"].ToString());
                lvi.SubItems.Add(row["HoTenNhanVien"].ToString());
                lvi.SubItems.Add(row["NgaySinh"].ToString());
                lvi.SubItems.Add(row["DiaChi"].ToString());
                lvi.SubItems.Add(row["DienThoai"].ToString());
                lvi.SubItems.Add(row["TenBangCap"].ToString());
            }
        }

        private void frmNhanVien_Load_1(object sender, EventArgs e)
        {

        }

        private void SaveNV_click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập vào
            if (!KiemTraNhapLieu())
            {
                return;
            }
            // Lấy giá trị ngày sinh từ dtpNgaySinh dưới dạng DateTime
            DateTime ngaySinh = dtpNgaySinh.Value;

            // Kiểm tra xem đang ở trạng thái thêm mới hay cập nhật
            if (themmoi)
            {
                // Gọi phương thức thêm nhân viên, truyền ngày sinh trực tiếp dưới dạng DateTime
                nv.ThemNhanVien(txtHoTen.Text, ngaySinh, txtDiaChi.Text, txtDienThoai.Text, cbxBangCap.SelectedValue.ToString());
                MessageBox.Show("Thêm mới thành công");
            }
            else
            {
                // Gọi phương thức cập nhật nhân viên, truyền ngày sinh trực tiếp dưới dạng DateTime
                nv.CapNhatNhanVien(lsvNhanVien.SelectedItems[0].SubItems[0].Text, txtHoTen.Text, ngaySinh, txtDiaChi.Text, txtDienThoai.Text, cbxBangCap.SelectedValue.ToString());
                MessageBox.Show("Cập nhật thành công");
            }

            // Làm mới ListView và các trường nhập liệu
            lsvNhanVien.Items.Clear();
            HienthiNhanvien();
            setNull();
        }
    }
}