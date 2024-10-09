using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace _2080601241_NguyenTienPhat_CloneQLThuVien.Models
{
    internal class Sach
    {
        Database db;
        SqlConnection sqlConn;
        // Khai báo kết nối
        private string connectionString = "Data Source=_HEHENIKEN; Database=QLTHUVIEN; Integrated Security = True";
        public Sach()
        {
            db = new Database();
        }
        public DataTable LayDsSach()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT * From SACH ";
            dt = db.Execute(strSQL);
            //Goi phuong thuc truy xuat du lieu 
            return dt;
        }


        public void ThemSach(string tensach, string tacgia, int namxuatban, string nhaxuatban, float trigia, DateTime ngaynhap)
        {
            // Không bao gồm MaSach trong câu lệnh INSERT
            string sql = "INSERT INTO SACH (TenSach, TacGia, NamXuatBan, NhaXuatBan, TriGia, NgayNhap) " +
                         "VALUES (@TenSach, @TacGia, @NamXuatBan, @NhaXuatBan, @TriGia, @NgayNhap)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenSach", tensach);
                cmd.Parameters.AddWithValue("@TacGia", tacgia);
                cmd.Parameters.AddWithValue("@NamXuatBan", namxuatban);
                cmd.Parameters.AddWithValue("@NhaXuatBan", nhaxuatban);
                cmd.Parameters.AddWithValue("@TriGia", trigia);
                cmd.Parameters.AddWithValue("@NgayNhap", ngaynhap);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CapNhatSach(int masach, string tensach, string tacgia, int namxuatban, string nhaxuatban, float trigia, DateTime ngaynhap)
        {
            string sql = "UPDATE SACH SET TenSach = @TenSach, TacGia = @TacGia, NamXuatBan = @NamXuatBan, " +
                         "NhaXuatBan = @NhaXuatBan, TriGia = @TriGia, NgayNhap = @NgayNhap WHERE MaSach = @MaSach";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSach", masach);
                cmd.Parameters.AddWithValue("@TenSach", tensach);
                cmd.Parameters.AddWithValue("@TacGia", tacgia);
                cmd.Parameters.AddWithValue("@NamXuatBan", namxuatban);
                cmd.Parameters.AddWithValue("@NhaXuatBan", nhaxuatban);
                cmd.Parameters.AddWithValue("@TriGia", trigia);
                cmd.Parameters.AddWithValue("@NgayNhap", ngaynhap);
                conn.Open();
                cmd.ExecuteNonQuery(); 
            }
        }

        public DataTable TimKiemSach(string keyword)
        {
            string query = "SELECT * FROM SACH WHERE TenSach LIKE @Keyword OR TacGia LIKE @Keyword";

            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            da.SelectCommand.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt; // Trả về kết quả tìm kiếm dưới dạng DataTable
        }
        // Xóa Sách
        public void XoaSach(int masach)
        {
            string sql = "DELETE FROM SACH WHERE MaSach = @MaSach";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSach", masach);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
