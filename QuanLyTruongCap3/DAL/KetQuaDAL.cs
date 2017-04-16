using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class KetQuaDAL : System.IDisposable
    {
        private readonly DataService ketQuaDS = new DataService();

        public void Dispose()
        {
            ketQuaDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsKetQua()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM KETQUA"))
            {
                ketQuaDS.Load(cmd);
            }

            return ketQuaDS;
        }

        public bool LuuKetQua()
        {
            return ketQuaDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return ketQuaDS.NewRow();
        }

        public void ThemKetQua(DataRow row)
        {
            ketQuaDS.Rows.Add(row);
        }
    }
}