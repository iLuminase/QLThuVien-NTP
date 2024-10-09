using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace _2080601241_NguyenTienPhat_CloneQLThuVien
{
    class Database

    {
        SqlConnection sqlConn; //Doi tuong ket noi CSDL
        SqlDataAdapter da;//Bo dieu phoi du lieu
        DataSet ds; //Doi tuong chhua CSDL khi giao tiep
        public Database()
        {
            string strCnn = "Data Source=_HEHENIKEN; Database=QLTHUVIEN; Integrated Security = True";
            sqlConn = new SqlConnection(strCnn);
        }
        //Phuong thuc de thuc hien cau lenh strSQL truy vân du lieu
        public DataTable Execute(string sqlStr)
        {
            da = new SqlDataAdapter(sqlStr, sqlConn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        //Phuong thuc de thuc hien cac lenh Them, Xoa, Sua
        public void ExecuteNonQuery(string strSQL)
        {
            SqlCommand sqlcmd = new SqlCommand(strSQL, sqlConn);
            sqlConn.Open(); //Mo ket noi
            sqlcmd.ExecuteNonQuery();//Lenh hien lenh Them/Xoa/Sua
            sqlConn.Close();//Dong ket noi
        }
        public void ExecuteNonQuery(string strSQL, params SqlParameter[] parameters)
        {
            using (SqlCommand sqlcmd = new SqlCommand(strSQL, sqlConn))
            {
                // Thêm các tham số vào SqlCommand nếu có
                if (parameters != null)
                {
                    sqlcmd.Parameters.AddRange(parameters);
                }

                sqlConn.Open(); // Mở kết nối
                sqlcmd.ExecuteNonQuery(); // Thực thi lệnh
                sqlConn.Close(); // Đóng kết nối
            }
        }

    }
}
