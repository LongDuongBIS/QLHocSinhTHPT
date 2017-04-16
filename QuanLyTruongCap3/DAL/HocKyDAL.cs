using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class HocKyDAL
    {
        private readonly DataService hocKyDS = new DataService();

        public DataTable LayDsHocKy()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCKY"))
            {
                hocKyDS.Load(cmd);
            }

            return hocKyDS;
        }

        public DataRow ThemDongMoi()
        {
            return hocKyDS.NewRow();
        }

        public void ThemHocKy(DataRow row)
        {
            hocKyDS.Rows.Add(row);
        }

        public bool LuuHocKy()
        {
            return hocKyDS.ExecuteNonQuery() > 0;
        }
    }
}