using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class TonGiaoDAL : System.IDisposable
    {
        private readonly DataService tonGiaoDS = new DataService();

        public void Dispose()
        {
            tonGiaoDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsTonGiao()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM TONGIAO"))
            {
                tonGiaoDS.Load(cmd);
            }

            return tonGiaoDS;
        }

        public bool LuuTonGiao()
        {
            return tonGiaoDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return tonGiaoDS.NewRow();
        }

        public void ThemTonGiao(DataRow row)
        {
            tonGiaoDS.Rows.Add(row);
        }
    }
}