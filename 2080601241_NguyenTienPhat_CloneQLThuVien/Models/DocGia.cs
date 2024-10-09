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
        public void CapNhatDocGia(int maDocGia, string hoTenDocGia, DateTime ngaySinh, string diaChi, string email, DateTime ngayLapThe, DateTime ngayHetHan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE DocGia SET HoTenDocGia = @HoTenDocGia, NgaySinh = @NgaySinh, DiaChi = @DiaChi, Email = @Email, NgayLapThe = @NgayLapThe, NgayHetHan = @NgayHetHan WHERE MaDocGia = @MaDocGia";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Khai báo các tham số
                    cmd.Parameters.AddWithValue("@HoTenDocGia", hoTenDocGia);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@NgayLapThe", ngayLapThe);
                    cmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHan);
                    cmd.Parameters.AddWithValue("@MaDocGia", maDocGia); // Không quên khai báo tham số mã độc giả

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
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