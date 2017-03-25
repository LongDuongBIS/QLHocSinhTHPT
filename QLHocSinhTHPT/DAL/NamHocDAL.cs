using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class NamHocDAL
    {
        private readonly DataService namHocDS = new DataService();

        public DataTable LayDsNamHoc()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NAMHOC"))
            {
                namHocDS.Load(cmd);
            }

            return namHocDS;
        }

        public DataRow ThemDongMoi()
        {
            return namHocDS.NewRow();
        }

        public void ThemNamHoc(DataRow row)
        {
            namHocDS.Rows.Add(row);
        }

        public bool LuuNamHoc()
        {
            return namHocDS.ExecuteNonQuery() > 0;
        }
    }
}