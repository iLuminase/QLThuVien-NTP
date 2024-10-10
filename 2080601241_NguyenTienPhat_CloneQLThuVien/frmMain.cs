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
    public partial class frmMain : Form
    {
        public string srvName = "_HEHENIKEN"; //chỉ định tên server
        public string dbName = "QLTHUVIEN"; //chỉ định tên CSDL
        public frmMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        // Ẩn các form đang mửo
        private void hideForm()
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Hide();

            }

        }
        private void tsbNhanVien_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmNhanVien")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmNhanVien frm = new frmNhanVien();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsbDocGia_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmDocGia")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmDocGia frm = new frmDocGia();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsbSach_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmSach")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmSach frm = new frmSach();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsbBangCap_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmBangCap")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmBangCap frm = new frmBangCap();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsbPhieuMuon_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmPhieuMuon")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmPhieuMuon frm = new frmPhieuMuon();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
        private void tsbPhieuThu_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmPhieuThu")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmPhieuThu frm = new frmPhieuThu();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsbCTPM_Click(object sender, EventArgs e)
        {
            hideForm();
            foreach (Form f in this.MdiChildren) // Đối với mỗi form con trong giao diện..
            {
                if (f.Name == "frmCTPhieuMuon")
                {
                    f.Activate(); // Mở giao diện
                    f.BringToFront(); // Đẩy lên phía trên
                    f.WindowState = FormWindowState.Maximized; // FullScreen
                    f.Show();
                    return;
                }
            }
            frmChiTietPhieuMuon frm = new frmChiTietPhieuMuon();
            frm.MdiParent = this; // Giúp giao diện MDI chỉ nằm trong form cha (frmMain)
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsbThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
