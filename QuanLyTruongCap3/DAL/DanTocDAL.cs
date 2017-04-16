using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class DanTocDAL : System.IDisposable
    {
        private readonly DataService danTocDS = new DataService();

        public void Dispose()
        {
            danTocDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsDanToc()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM DANTOC"))
            {
                danTocDS.Load(cmd);
            }

            return danTocDS;
        }

        public bool LuuDanToc()
        {
            return danTocDS.ExecuteNonQuery() > 0;
        }

        public void ThemDanToc(DataRow row)
        {
            danTocDS.Rows.Add(row);
        }

        public DataRow ThemDongMoi()
        {
            return danTocDS.NewRow();
        }
    }
}