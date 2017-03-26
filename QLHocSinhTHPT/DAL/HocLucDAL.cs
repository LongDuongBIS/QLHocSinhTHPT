using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class HocLucDAL
    {
        private readonly DataService hocLucDS = new DataService();

        public DataTable LayDsHocLuc()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCLUC"))
            {
                hocLucDS.Load(cmd);
            }

            return hocLucDS;
        }

        public DataRow ThemDongMoi()
        {
            return hocLucDS.NewRow();
        }

        public void ThemHocLuc(DataRow row)
        {
            hocLucDS.Rows.Add(row);
        }

        public bool LuuHocLuc()
        {
            return hocLucDS.ExecuteNonQuery() > 0;
        }
    }
}