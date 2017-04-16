using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class NgheNghiepDAL : System.IDisposable
    {
        private readonly DataService ngheNghiepDS = new DataService();

        public void Dispose()
        {
            ngheNghiepDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsNgheNghiep()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NGHENGHIEP"))
            {
                ngheNghiepDS.Load(cmd);
            }

            return ngheNghiepDS;
        }

        public bool LuuNgheNghiep()
        {
            return ngheNghiepDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return ngheNghiepDS.NewRow();
        }

        public void ThemNgheNghiep(DataRow row)
        {
            ngheNghiepDS.Rows.Add(row);
        }
    }
}