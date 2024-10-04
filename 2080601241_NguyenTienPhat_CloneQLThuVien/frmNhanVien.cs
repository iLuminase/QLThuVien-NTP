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
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            HienthiNhanvien();
        }

        private void lsvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvNhanVien.SelectedIndices.Count > 0)
            {
                txtHoTen.Text = lsvNhanVien.SelectedItems[0].SubItems[1].Text;
                //Chuyen sang kieu dateTime
                dtpNgaySinh.Value = DateTime.Parse(lsvNhanVien.SelectedItems[0].SubItems[2].Text);
                txtDiaChi.Text = lsvNhanVien.SelectedItems[0].SubItems[3].Text;
                txtDienThoai.Text = lsvNhanVien.SelectedItems[0].SubItems[4].Text;
                //Tìm vị trí của Tên bằng cấp trong Combobox
                cbxBangCap.SelectedIndex = cbxBangCap.FindString(lsvNhanVien.SelectedItems[0].SubItems[5].Text);
            }
        }

        //Còn tiếp cho các sự kiện khác
        private void btnThem_Click(object sender, EventArgs e)
        {
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
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ngay = String.Format("{0:MM/dd/yyyy}",
           dtpNgaySinh.Value);
            //Định dạng ngày tương ứng với trong CSDL SQLserver
            if (themmoi)
            {
                nv.ThemNhanVien(txtHoTen.Text, ngay, txtDiaChi.Text,
               txtDienThoai.Text, cbxBangCap.SelectedValue.ToString());
                MessageBox.Show("Thêm mới thành công");
            }
            else
            {
                nv.CapNhatNhanVien(
               lsvNhanVien.SelectedItems[0].SubItems[0].Text,
               txtHoTen.Text, ngay, txtDiaChi.Text, txtDienThoai.Text,
               cbxBangCap.SelectedValue.ToString());
                MessageBox.Show("Cập nhật thành công");
            }
            HienthiNhanvien();
            setNull();
        }
    }
}