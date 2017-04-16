using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class KQHocKyMonHocDAL : IDisposable
    {
        private readonly DataService kqHocKyMonHocDS = new DataService();

        public void Dispose()
        {
            kqHocKyMonHocDS.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataTable LayDsKQHocKyMonHocForReport(string maLop, string maMonHoc, string maHocKy, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCSINH HS " + "INNER JOIN KQ_HOC_KY_MON_HOC KQ ON KQ.MaHocSinh = HS.MaHocSinh " + "INNER JOIN LOP L ON KQ.MaLop = L.MaLop " + "INNER JOIN MONHOC MH ON KQ.MaMonHoc = MH.MaMonHoc " + "INNER JOIN HOCKY HK ON KQ.MaHocKy = HK.MaHocKy " + "INNER JOIN NAMHOC NH ON KQ.MaNamHoc = NH.MaNamHoc " + "WHERE KQ.MaLop = @maLop AND KQ.MaMonHoc = @maMonHoc AND KQ.MaHocKy = @maHocKy AND KQ.MaNamHoc = @maNamHoc"))
            {
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                kqHocKyMonHocDS.Load(cmd);
            }

            return kqHocKyMonHocDS;
        }

        public void LuuKetQua(string maHocSinh, string maLop, string maMonHoc, string maHocKy, string maNamHoc, float diemTBKT, float diemTBMonHK)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO KQ_HOC_KY_MON_HOC " + "VALUES(@maHocSinh, @maLop, @maMonHoc, @maHocKy, @maNamHoc, @diemTBKT, @diemTBMonHK)"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("diemTBKT", SqlDbType.Float).Value = Math.Round(diemTBKT, 2);
                cmd.Parameters.Add("diemTBMonHK", SqlDbType.Float).Value = Math.Round(diemTBMonHK, 2);

                kqHocKyMonHocDS.Load(cmd);
            }
        }

        public void XoaKetQua(string maHocSinh, string maLop, string maMonHoc, string maHocKy, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM KQ_HOC_KY_MON_HOC " + "WHERE MaHocSinh = @maHocSinh AND MaLop = @maLop AND MaMonHoc = @maMonHoc AND MaHocKy = @maHocKy AND MaNamHoc = @maNamHoc"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                kqHocKyMonHocDS.Load(cmd);
            }
        }
    }
}