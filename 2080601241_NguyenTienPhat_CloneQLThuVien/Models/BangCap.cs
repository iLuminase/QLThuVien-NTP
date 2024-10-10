using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Windows.Forms;

namespace _2080601241_NguyenTienPhat_CloneQLThuVien.Models
{
    internal class BangCap
    {
        Database db;
        SqlConnection sqlConn;

        // Khai báo kết nối
        private string connectionString = "Data Source=_HEHENIKEN; Database=QLTHUVIEN; Integrated Security = True";
        public BangCap()
        {
            db = new Database();
        }
        //Lay danh sach bang cap
        public DataTable LayDSBangcap()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM BANGCAP";
            dt = db.Execute(sql);
            return dt;
        }

        public void ThemBangCap(string tenbangcap)
        {
            string sql = "INSERT INTO BANGCAP (TenBangCap) VALUES (@TenBangCap)";
            SqlParameter parameter = new SqlParameter("@TenBangCap", tenbangcap);
            db.ExecuteNonQuery(sql, parameter);
        }

        public void CapNhatBangCap(int maBangCap, string tenbangcap)
        {
            string sql = "UPDATE BangCap SET TenBangCap = @TenBangCap WHERE MaBangCap = @MaBangCap";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TenBangCap", tenbangcap),
        new SqlParameter("@MaBangCap", maBangCap)
            };
            db.ExecuteNonQuery(sql, parameters);
        }

        public void XoaBangCap(int maBangCap)
        {
            string sql = "DELETE FROM BangCap WHERE MaBangCap = @MaBangCap";
            SqlParameter parameter = new SqlParameter("@MaBangCap", maBangCap);
            db.ExecuteNonQuery(sql, parameter);
        }

        public bool KiemTraBangCapDuocSuDung(int maBangCap)
        {
            // Sử dụng chuỗi kết nối từ thuộc tính connectionString
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM NhanVien WHERE MaBangCap = @MaBangCap";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaBangCap", maBangCap);

                    try
                    {
                        // Mở kết nối
                        connection.Open();

                        // Thực thi truy vấn và lấy kết quả
                        int count = (int)command.ExecuteScalar();

                        // Trả về true nếu có nhân viên sử dụng bằng cấp này
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi kiểm tra bằng cấp: " + ex.Message);
                        return false;
                    }
                }
            }
        }

    }
}
