using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class NguoiDungDAL
    {
        private readonly DataService nguoiDungDS = new DataService();

        public DataTable LayDsNguoiDung()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NGUOIDUNG"))
            {
                nguoiDungDS.Load(cmd);
            }

            return nguoiDungDS;
        }

        public DataTable LayDsNguoiDung(string username)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NGUOIDUNG " + "WHERE TenDNhap = @ten"))
            {
                cmd.Parameters.Add("ten", SqlDbType.VarChar).Value = username;

                nguoiDungDS.Load(cmd);
            }

            return nguoiDungDS;
        }

        public DataRow ThemDongMoi()
        {
            return nguoiDungDS.NewRow();
        }

        public void ThemNguoiDung(DataRow row)
        {
            nguoiDungDS.Rows.Add(row);
        }

        public bool LuuNguoiDung()
        {
            return nguoiDungDS.ExecuteNonQuery() > 0;
        }

        public void ChangePassword(string userName, string newPassword)
        {
            nguoiDungDS.ChangePassword(userName, newPassword);
        }
    }
}