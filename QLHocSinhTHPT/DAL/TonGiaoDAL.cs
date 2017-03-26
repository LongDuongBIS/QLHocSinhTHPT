using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class TonGiaoDAL
    {
        private readonly DataService tonGiaoDS = new DataService();

        public DataTable LayDsTonGiao()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM TONGIAO"))
            {
                tonGiaoDS.Load(cmd);
            }

            return tonGiaoDS;
        }

        public DataRow ThemDongMoi()
        {
            return tonGiaoDS.NewRow();
        }

        public void ThemTonGiao(DataRow row)
        {
            tonGiaoDS.Rows.Add(row);
        }

        public bool LuuTonGiao()
        {
            return tonGiaoDS.ExecuteNonQuery() > 0;
        }
    }
}