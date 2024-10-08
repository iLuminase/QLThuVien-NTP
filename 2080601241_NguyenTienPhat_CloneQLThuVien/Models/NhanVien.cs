using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _2080601241_NguyenTienPhat_CloneQLThuVien
{
    internal class NhanVien
    {
        Database db;
        SqlConnection sqlConn;
        public NhanVien()
        {
            db = new Database();
        }
        public DataTable LayDSNhanvien()
        {
            
            string strSQL = "Select MaNhanVien, HoTenNhanVien, NgaySinh,DiaChi, DienThoai," +
                " TenBangCap From NhanVien N, BANGCAP B Where N.MaBangCap = B.MaBangCap";
            DataTable dt = db.Execute(strSQL);
            //Goi phuong thuc truy xuat du lieu 
            return dt;
        }
        public DataTable LayBangcap()
        {
            string strSQL = "Select * from bangcap";
            //Goi phuong thuc truy xuat du lieu 
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        // Ham xoa 1 nhan vien 
        public void XoaNhanVien(string index_nv)
        {
            string sql = "Delete from NhanVien where MaNhanVien = " + index_nv;
            // Thao tac voi du lieu ma ko can tra ve
            db.ExecuteNonQuery(sql);
        }
        // Thêm nhân viên
        public void ThemNhanVien(string ten, DateTime ngaysinh, string diachi, string dienthoai, string index_bc)
        {
            string sql = string.Format("Insert Into NhanVien (HoTenNhanVien, NgaySinh, DiaChi, DienThoai, MaBangCap) " +
                                       "Values (N'{0}', '{1}', N'{2}', '{3}', {4})",
                                       ten, ngaysinh.ToString("yyyy-MM-dd"), diachi, dienthoai, index_bc);
            db.ExecuteNonQuery(sql);
        }

        // Cập nhật nhân viên
        public void CapNhatNhanVien(string index_nv, string hoten, DateTime ngaysinh, string diachi, string dienthoai, string index_bc)
        {
            string sql = string.Format("Update NhanVien set HoTenNhanVien = N'{0}', NgaySinh = '{1}', DiaChi = N'{2}', DienThoai = '{3}', MaBangCap = {4} " +
                                       "where MaNhanVien = {5}",
                                       hoten, ngaysinh.ToString("yyyy-MM-dd"), diachi, dienthoai, index_bc, index_nv);
            db.ExecuteNonQuery(sql);
        }

        public DataTable TimKiemNhanVien(string keyword)
        {
            string sql = string.Format("Select MaNhanVien, HoTenNhanVien, NgaySinh, DiaChi, DienThoai, TenBangCap " +
                                       "From NhanVien N, BANGCAP B " +
                                       "Where N.MaBangCap = B.MaBangCap " +
                                       "And LOWER(HoTenNhanVien) LIKE LOWER(N'%{0}%')", keyword);
            return db.Execute(sql); // Trả về DataTable kết quả tìm kiếm
        }

    }
}
 
