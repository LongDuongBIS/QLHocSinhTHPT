using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class NgheNghiepDAL
    {
        private readonly DataService ngheNghiepDS = new DataService();

        public DataTable LayDsNgheNghiep()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NGHENGHIEP"))
            {
                ngheNghiepDS.Load(cmd);
            }

            return ngheNghiepDS;
        }

        public DataRow ThemDongMoi()
        {
            return ngheNghiepDS.NewRow();
        }

        public void ThemNgheNghiep(DataRow row)
        {
            ngheNghiepDS.Rows.Add(row);
        }

        public bool LuuNgheNghiep()
        {
            return ngheNghiepDS.ExecuteNonQuery() > 0;
        }
    }
}