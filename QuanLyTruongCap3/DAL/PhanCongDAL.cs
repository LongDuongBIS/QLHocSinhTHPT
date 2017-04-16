using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class PhanCongDAL
    {
        private readonly DataService phanCongDS = new DataService();

        public DataTable LayDsPhanCong()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM PHANCONG"))
            {
                phanCongDS.Load(cmd);
            }

            return phanCongDS;
        }

        public DataRow ThemDongMoi()
        {
            return phanCongDS.NewRow();
        }

        public void ThemPhanCong(DataRow row)
        {
            phanCongDS.Rows.Add(row);
        }

        public bool LuuPhanCong()
        {
            return phanCongDS.ExecuteNonQuery() > 0;
        }

        public void LuuPhanCong(string maNamHoc, string maLop, string maMonHoc, string maGiaoVien)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO PHANCONG " + "VALUES(@maNamHoc, @maLop, @maMonHoc, @maGiaoVien)"))
            {
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maGiaoVien", SqlDbType.VarChar).Value = maGiaoVien;

                phanCongDS.Load(cmd);
            }
        }

        public DataTable TimTheoTenLop(string ten)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT P.STT, P.MaNamHoc, P.MaLop, P.MaMonHoc, P.MaGiaoVien " + "FROM PHANCONG P, LOP L " + "WHERE P.MaLop = L.MaLop AND L.TenLop LIKE '%' + @ten + '%'"))
            {
                cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;

                phanCongDS.Load(cmd);
            }

            return phanCongDS;
        }

        public DataTable TimTheoTenGV(string ten)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT P.STT, P.MaNamHoc, P.MaLop, P.MaMonHoc, P.MaGiaoVien " + "FROM PHANCONG P, GIAOVIEN G " + "WHERE P.MaGiaoVien = G.MaGiaoVien AND G.TenGiaoVien LIKE '%' + @ten + '%'"))
            {
                cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;

                phanCongDS.Load(cmd);
            }

            return phanCongDS;
        }
    }
}