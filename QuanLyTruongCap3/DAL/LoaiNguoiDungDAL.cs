using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class LoaiNguoiDungDAL : System.IDisposable
    {
        private readonly DataService loaiNguoiDungDS = new DataService();

        public void Dispose()
        {
            loaiNguoiDungDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsLoaiNguoiDung()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOAINGUOIDUNG"))
            {
                loaiNguoiDungDS.Load(cmd);
            }

            return loaiNguoiDungDS;
        }

        public bool LuuLoaiNguoiDung()
        {
            return loaiNguoiDungDS.ExecuteNonQuery() > 0;
        }

        public DataRow ThemDongMoi()
        {
            return loaiNguoiDungDS.NewRow();
        }

        public void ThemLoaiNguoiDung(DataRow row)
        {
            loaiNguoiDungDS.Rows.Add(row);
        }
    }
}