using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class KQCaNamTongHopDAL
    {
        private readonly DataService kqCaNamTongHopDS = new DataService();

        public void LuuKetQua(string maHocSinh, string maLop, string maNamHoc, string maHocLuc, string maHanhKiem, float diemTBChungCacMonCN, string maKetQua)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO KQ_CA_NAM_TONG_HOP " + "VALUES(@maHocSinh, @maLop, @maNamHoc, @maHocLuc, @maHanhKiem, @diemTBChungCacMonCN, @maKetQua)"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maHocLuc", SqlDbType.VarChar).Value = maHocLuc;
                cmd.Parameters.Add("maHanhKiem", SqlDbType.VarChar).Value = maHanhKiem;
                cmd.Parameters.Add("diemTBChungCacMonCN", SqlDbType.Float).Value = Math.Round(diemTBChungCacMonCN, 2);
                cmd.Parameters.Add("maKetQua", SqlDbType.VarChar).Value = maKetQua;

                kqCaNamTongHopDS.Load(cmd);
            }
        }

        public void XoaKetQua(string maHocSinh, string maLop, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM KQ_CA_NAM_TONG_HOP " + "WHERE MaHocSinh = @maHocSinh AND MaLop = @maLop AND MaNamHoc = @maNamHoc"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                kqCaNamTongHopDS.Load(cmd);
            }
        }

        public DataTable LayDsKQCaNamTongHopForReport(string maLop, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCSINH HS " + "INNER JOIN KQ_CA_NAM_TONG_HOP KQ ON KQ.MaHocSinh = HS.MaHocSinh " + "INNER JOIN LOP L ON KQ.MaLop = L.MaLop " + "INNER JOIN NAMHOC NH ON KQ.MaNamHoc = NH.MaNamHoc " + "INNER JOIN HOCLUC HL ON KQ.MaHocLuc = HL.MaHocLuc " + "INNER JOIN HANHKIEM HKIEM ON KQ.MaHanhKiem = HKIEM.MaHanhKiem " + "INNER JOIN KETQUA KQUA ON KQ.MaKetQua = KQUA.MaKetQua " + "WHERE KQ.MaLop = @maLop AND KQ.MaNamHoc = @maNamHoc"))
            {
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                kqCaNamTongHopDS.Load(cmd);
            }

            return kqCaNamTongHopDS;
        }
    }
}