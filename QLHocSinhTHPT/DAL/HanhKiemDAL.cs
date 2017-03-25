using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class HanhKiemDAL
    {
        private readonly DataService hanhKiemDS = new DataService();

        public DataTable LayDsHanhKiem()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HANHKIEM"))
            {
                hanhKiemDS.Load(cmd);
            }

            return hanhKiemDS;
        }

        public DataRow ThemDongMoi()
        {
            return hanhKiemDS.NewRow();
        }

        public void ThemHanhKiem(DataRow row)
        {
            hanhKiemDS.Rows.Add(row);
        }

        public bool LuuHanhKiem()
        {
            return hanhKiemDS.ExecuteNonQuery() > 0;
        }
    }
}