using System;
using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class KQHocKyTongHopDAL
    {
        private readonly DataService kqHocKyTongHopDS = new DataService();

        public void LuuKetQua(string maHocSinh, string maLop, string maHocKy, string maNamHoc, string maHocLuc, string maHanhKiem, float diemTBChungCacMonHK)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO KQ_HOC_KY_TONG_HOP " + "VALUES(@maHocSinh, @maLop, @maHocKy, @maNamHoc, @maHocLuc, @maHanhKiem, @diemTBChungCacMonHK)"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maHocLuc", SqlDbType.VarChar).Value = maHocLuc;
                cmd.Parameters.Add("maHanhKiem", SqlDbType.VarChar).Value = maHanhKiem;
                cmd.Parameters.Add("diemTBChungCacMonHK", SqlDbType.Float).Value = Math.Round(diemTBChungCacMonHK, 2);

                kqHocKyTongHopDS.Load(cmd);
            }
        }

        public void XoaKetQua(string maHocSinh, string maLop, string maHocKy, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM KQ_HOC_KY_TONG_HOP " + "WHERE MaHocSinh = @maHocSinh AND MaLop = @maLop AND MaHocKy = @maHocKy AND MaNamHoc = @maNamHoc"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                kqHocKyTongHopDS.Load(cmd);
            }
        }

        public DataTable LayDsKQHocKyTongHopForReport(string maLop, string maHocKy, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCSINH HS " + "INNER JOIN KQ_HOC_KY_TONG_HOP KQ ON KQ.MaHocSinh = HS.MaHocSinh " + "INNER JOIN LOP L ON KQ.MaLop = L.MaLop " + "INNER JOIN HOCKY HK ON KQ.MaHocKy = HK.MaHocKy " + "INNER JOIN NAMHOC NH ON KQ.MaNamHoc = NH.MaNamHoc " + "INNER JOIN HOCLUC HL ON KQ.MaHocLuc = HL.MaHocLuc " + "INNER JOIN HANHKIEM HKIEM ON KQ.MaHanhKiem = HKIEM.MaHanhKiem " + "WHERE KQ.MaLop = @maLop AND KQ.MaHocKy = @maHocKy AND KQ.MaNamHoc = @maNamHoc"))
            {
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                kqHocKyTongHopDS.Load(cmd);
            }

            return kqHocKyTongHopDS;
        }
    }
}