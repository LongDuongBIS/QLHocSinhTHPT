using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class HanhKiemDAL : System.IDisposable
    {
        private readonly DataService hanhKiemDS = new DataService();

        public void Dispose()
        {
            hanhKiemDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsHanhKiem()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HANHKIEM"))
            {
                hanhKiemDS.Load(cmd);
            }

            return hanhKiemDS;
        }

        public bool LuuHanhKiem()
        {
            return hanhKiemDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return hanhKiemDS.NewRow();
        }

        public void ThemHanhKiem(DataRow row)
        {
            hanhKiemDS.Rows.Add(row);
        }
    }
}