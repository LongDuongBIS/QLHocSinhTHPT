using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class HocLucDAL : System.IDisposable
    {
        private readonly DataService hocLucDS = new DataService();

        public void Dispose()
        {
            hocLucDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsHocLuc()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCLUC"))
            {
                hocLucDS.Load(cmd);
            }

            return hocLucDS;
        }

        public bool LuuHocLuc()
        {
            return hocLucDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return hocLucDS.NewRow();
        }

        public void ThemHocLuc(DataRow row)
        {
            hocLucDS.Rows.Add(row);
        }
    }
}