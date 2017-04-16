using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class LoaiDiemDAL : System.IDisposable
    {
        private readonly DataService loaiDiemDS = new DataService();

        public void Dispose()
        {
            loaiDiemDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsLoaiDiem()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOAIDIEM"))
            {
                loaiDiemDS.Load(cmd);
            }

            return loaiDiemDS;
        }

        public bool LuuLoaiDiem()
        {
            return loaiDiemDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return loaiDiemDS.NewRow();
        }

        public void ThemLoaiDiem(DataRow row)
        {
            loaiDiemDS.Rows.Add(row);
        }
    }
}