using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class LoaiNguoiDungDAL
    {
        private readonly DataService loaiNguoiDungDS = new DataService();

        public DataTable LayDsLoaiNguoiDung()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOAINGUOIDUNG"))
            {
                loaiNguoiDungDS.Load(cmd);
            }

            return loaiNguoiDungDS;
        }

        public DataRow ThemDongMoi()
        {
            return loaiNguoiDungDS.NewRow();
        }

        public void ThemLoaiNguoiDung(DataRow row)
        {
            loaiNguoiDungDS.Rows.Add(row);
        }

        public bool LuuLoaiNguoiDung()
        {
            return loaiNguoiDungDS.ExecuteNonQuery() > 0;
        }
    }
}