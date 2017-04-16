using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class HocKyDAL : System.IDisposable
    {
        private readonly DataService hocKyDS = new DataService();

        public void Dispose()
        {
            hocKyDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsHocKy()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCKY"))
            {
                hocKyDS.Load(cmd);
            }

            return hocKyDS;
        }

        public bool LuuHocKy()
        {
            return hocKyDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return hocKyDS.NewRow();
        }

        public void ThemHocKy(DataRow row)
        {
            hocKyDS.Rows.Add(row);
        }
    }
}