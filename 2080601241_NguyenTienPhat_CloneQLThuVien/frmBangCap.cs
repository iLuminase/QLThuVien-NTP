using _2080601241_NguyenTienPhat_CloneQLThuVien.Models;
using Microsoft.IdentityModel.Tokens;
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
    public partial class frmBangCap : Form
    {
        BindingSource bs = new BindingSource();
        BangCap bc = new BangCap();
        bool themmoi = false;
        public frmBangCap()
        {
            InitializeComponent();
            setNull();
       
        }
        public bool KiemTraHopLe()
        {
            if(string.IsNullOrWhiteSpace(txtTenBangCap.Text) )
            {
                txtTenBangCap.Focus();
                MessageBox.Show("Tên bằng cấp không được để trống", "Thông báo");
                return false;
            }    
            return true;
        }
        public void setButton(bool val)
        {
            btnThem.Enabled = !val;
            btnSua.Enabled = !val;
            btnXoa.Enabled = !val;
            btnLuu.Enabled = val;
            btnDong.Enabled = !val;
            btnHuy.Enabled = val;
        }
        public void setNull()
        {
            txtTenBangCap.Text = "";
            txtMabangcap.Text = "";
        }

        private void frmBangCap_Load(object sender, EventArgs e)
        {
            dgvBangCap.DataSource = bc.LayDSBangcap();
            DataTable dt = bc.LayDSBangcap();
            dgvBangCap.DataSource = bs;
            bs.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themmoi = true;
            setButton(true);
            btnThem.Enabled = false;
            txtTenBangCap.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(!KiemTraHopLe())
            {
                return;
            }    
            if(themmoi)
            {
                bc.ThemBangCap(txtTenBangCap.Text);
                MessageBox.Show("Thêm mới thành công");
            }    
            else
            {
                if (dgvBangCap.CurrentCell != null)
                {
                    string maBangCap = dgvBangCap.CurrentRow.Cells["MaBangCap"].Value.ToString();

                    bc.CapNhatBangCap(int.Parse(maBangCap), txtTenBangCap.Text);
                    MessageBox.Show($"Cập nhật thông tin bằng cấp {txtTenBangCap.Text} thành công");
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn một bằng cấp để sửa", "Thông báo");
                }
            }
            dgvBangCap.DataSource = bc.LayDSBangcap();
            setNull();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(false);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void dgvBangCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvBangCap.Rows[e.RowIndex];
                txtMabangcap.Text = row.Cells["MaBangCap"].Value.ToString();
                txtTenBangCap.Text = row.Cells["TenBangCap"].Value.ToString();
                
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvBangCap.CurrentCell != null)
            {
                // Lấy mã bằng cấp của hàng đang chọn
                string maBangCap = dgvBangCap.CurrentRow.Cells["MaBangCap"].Value.ToString();

                // Kiểm tra xem bằng cấp này có được sử dụng trong bảng Nhân viên hay không
                if (bc.KiemTraBangCapDuocSuDung(int.Parse(maBangCap)))
                {
                    MessageBox.Show("Không thể xóa bằng cấp này vì có nhân viên đang sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Hỏi người dùng xác nhận có muốn xóa không
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bằng cấp này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Thực hiện xóa
                        bc.XoaBangCap(int.Parse(maBangCap));
                        MessageBox.Show("Xóa bằng cấp thành công", "Thông báo");

                        // Cập nhật lại dữ liệu
                        dgvBangCap.DataSource = bc.LayDSBangcap();
                        setNull();
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn một bằng cấp để xóa", "Thông báo");
            }
        }
    }
}