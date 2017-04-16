using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class NamHocDAL : System.IDisposable
    {
        private readonly DataService namHocDS = new DataService();

        public void Dispose()
        {
            namHocDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsNamHoc()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NAMHOC"))
            {
                namHocDS.Load(cmd);
            }

            return namHocDS;
        }

        public bool LuuNamHoc()
        {
            return namHocDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return namHocDS.NewRow();
        }

        public void ThemNamHoc(DataRow row)
        {
            namHocDS.Rows.Add(row);
        }
    }
}