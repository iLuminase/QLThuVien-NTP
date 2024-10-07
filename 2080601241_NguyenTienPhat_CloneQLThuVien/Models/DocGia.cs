using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _2080601241_NguyenTienPhat_CloneQLThuVien
{
    internal class DocGia
    {
        Database db;
        SqlConnection sqlConn;

        // Khai báo kết nối
        private string connectionString = "Data Source=_HEHENIKEN; Database=QLTHUVIEN; Integrated Security = True"; 
        public DocGia()
        {
            db = new Database();
        }

        // Lấy danh sách đọc giả
        public DataTable LayDSDocGia()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT MaDocGia, HoTenDocGia, NgaySinh, DiaChi, Email, NgayLapThe, NgayHetHan FROM DocGia";
            dt = db.Execute(strSQL);
            return dt;
        }

        // Thêm 1 đọc giả mới
        public void ThemDocGia(string ten, DateTime ngaysinh, string diachi, string email, DateTime ngayLapThe, DateTime ngayHetHan)
        {
            string sql = string.Format("INSERT INTO DocGia (HoTenDocGia, NgaySinh, DiaChi, Email, NgayLapThe, NgayHetHan) " +
                "VALUES (N'{0}', '{1}', N'{2}', '{3}', '{4}', '{5}')",
                ten, ngaysinh.ToString("yyyy-MM-dd"), diachi, email, ngayLapThe.ToString("yyyy-MM-dd"), ngayHetHan.ToString("yyyy-MM-dd"));
            db.ExecuteNonQuery(sql);
        }

        // Cập nhật thông tin đọc giả
        public void CapNhatDocGia(string ten, DateTime ngaysinh, string diachi, string email, DateTime ngayLapThe, DateTime ngayHetHan)
        {
            string query = "UPDATE DocGia SET HoTen = @HoTen, NgaySinh = @NgaySinh, DiaChi = @DiaChi, Email = @Email," +
                " NgayLapThe = @NgayLapThe, NgayHetHan = @NgayHetHan WHERE MaDocGia = @MaDocGia";
            db.ExecuteNonQuery(query);
        }


            public void XoaDocGia(string maDocGia)
            {
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConn.Open();
                        string query = "DELETE FROM DocGia WHERE MaDocGia = @MaDocGia";

                        using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                        {
                            // Khai báo biến @MaDocGia
                            cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Xử lý ngoại lệ (nếu cần)
                        MessageBox.Show($"Lỗi: {ex.Message}");
                    }
                    finally
                    {
                        sqlConn.Close(); // Đảm bảo kết nối được đóng
                    }
                }
            }

            // Các phương thức khác của bạn
        }
    }