using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class KhoiLopDAL : System.IDisposable
    {
        private readonly DataService khoiLopDS = new DataService();

        public void Dispose()
        {
            khoiLopDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

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

        public bool LuuKhoiLop()
        {
            return khoiLopDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return khoiLopDS.NewRow();
        }

        public void ThemKhoiLop(DataRow row)
        {
            khoiLopDS.Rows.Add(row);
        }
    }
}