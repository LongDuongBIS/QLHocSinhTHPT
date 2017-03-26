using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class KhoiLopDAL
    {
        private readonly DataService khoiLopDS = new DataService();

        public DataTable LayDsKhoiLop()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM KHOILOP"))
            {
                khoiLopDS.Load(cmd);
            }

            return khoiLopDS;
        }

        public DataTable LayDsKhoiLop(string khoiLopCu)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string sql = "SELECT * " + "FROM KHOILOP " + "WHERE MaKhoiLop = @khoiLopCu ";
                cmd.Parameters.Add("khoiLopCu", SqlDbType.VarChar).Value = khoiLopCu;

                if (khoiLopCu == "KHOI10")
                    sql += "OR MaKhoiLop = 'KHOI11'";
                else if (khoiLopCu == "KHOI11")
                    sql += "OR MaKhoiLop = 'KHOI12'";

                cmd.CommandText = sql;

                khoiLopDS.Load(cmd);
            }

            return khoiLopDS;
        }

        public DataRow ThemDongMoi()
        {
            return khoiLopDS.NewRow();
        }

        public void ThemKhoiLop(DataRow row)
        {
            khoiLopDS.Rows.Add(row);
        }

        public bool LuuKhoiLop()
        {
            return khoiLopDS.ExecuteNonQuery() > 0;
        }
    }
}