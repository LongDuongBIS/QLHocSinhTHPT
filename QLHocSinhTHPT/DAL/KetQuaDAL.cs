using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class KetQuaDAL
    {
        private readonly DataService ketQuaDS = new DataService();

        public DataTable LayDsKetQua()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM KETQUA"))
            {
                ketQuaDS.Load(cmd);
            }

            return ketQuaDS;
        }

        public DataRow ThemDongMoi()
        {
            return ketQuaDS.NewRow();
        }

        public void ThemKetQua(DataRow row)
        {
            ketQuaDS.Rows.Add(row);
        }

        public bool LuuKetQua()
        {
            return ketQuaDS.ExecuteNonQuery() > 0;
        }
    }
}