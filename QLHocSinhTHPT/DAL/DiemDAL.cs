using System;
using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class DiemDAL
    {
        private readonly DataService diemDS = new DataService();

        public DataTable LayDsDiem()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM DIEM"))
            {
                diemDS.Load(cmd);
            }

            return diemDS;
        }

        public DataTable LayDsDiem(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM DIEM D, HOCSINH S, MONHOC H, LOAIDIEM O, LOP L " + "WHERE D.MaHocSinh = S.MaHocSinh AND D.MaMonHoc = H.MaMonHoc AND D.MaLoai = O.MaLoai AND D.MaLop = L.MaLop AND S.MaHocSinh = @maHocSinh AND H.MaMonHoc = @maMonHoc AND D.MaHocKy = @maHocKy AND D.MaNamHoc = @maNamHoc AND L.MaLop = @maLop"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                diemDS.Load(cmd);
            }

            return diemDS;
        }

        public void LuuDiem(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop, string maLoaiDiem, float diemSo)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO DIEM " + "VALUES(@maHocSinh, @maMonHoc, @maHocKy, @maNamHoc, @maLop, @maLoaiDiem, @diemSo)"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maLoaiDiem", SqlDbType.VarChar).Value = maLoaiDiem;
                cmd.Parameters.Add("diemSo", SqlDbType.Float).Value = Math.Round(diemSo, 2);

                diemDS.Load(cmd);
            }
        }

        public void XoaDiem(int stt)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM DIEM " + "WHERE STT = @stt"))
            {
                cmd.Parameters.Add("stt", SqlDbType.Int).Value = stt;

                diemDS.Load(cmd);
            }
        }

        public DataTable LayDsDiemHocSinh(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM DIEM D, LOAIDIEM L " + "WHERE D.MaLoai = L.MaLoai AND D.MaHocSinh = @maHocSinh AND D.MaMonHoc = @maMonHoc AND D.MaHocKy = @maHocKy AND D.MaNamHoc = @maNamHoc AND D.MaLop = @maLop"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maMonHoc", SqlDbType.VarChar).Value = maMonHoc;
                cmd.Parameters.Add("maHocKy", SqlDbType.VarChar).Value = maHocKy;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;

                diemDS.Load(cmd);
            }

            return diemDS;
        }

        public DataTable LayPhieuDiemCaNhanForReport(string maHocSinh, string maLop, string maNamHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT HS.MaHocSinh, HS.HoTen, MH.MaMonHoc, MH.TenMonHoc, HK.MaHocKy, HK.TenHocKy, NH.MaNamHoc, NH.TenNamHoc, LP.MaLop, LP.TenLop, KQHK1MH.DTBKiemTra AS DTBKTHK1, KQHK1MH.DTBMonHocKy AS DTBHK1, KQHK2MH.DTBKiemTra AS DTBKTHK2, KQHK2MH.DTBMonHocKy AS DTBHK2, KQHK1TH.MaHocLuc AS MaHLHK1, HLHK1.TenHocLuc AS TenHLHK1, KQHK1TH.MaHanhKiem AS MaHKHK1, HKIEMHK1.TenHanhKiem AS TenHKHK1, KQHK1TH.DTBMonHocKy AS DTBCMHK1, KQHK2TH.MaHocLuc AS MaHLHK2, HLHK2.TenHocLuc AS TenHLHK2, KQHK2TH.MaHanhKiem AS MaHKHK2, HKIEMHK2.TenHanhKiem AS TenHKHK2, KQHK2TH.DTBMonHocKy AS DTBCMHK2, KQCNMH.DiemThiLai, KQCNMH.DTBCaNam AS DTBCN, KQCNTH.MaHocLuc AS MaHLCN, HLCN.TenHocLuc AS TenHLCN, KQCNTH.MaHanhKiem AS MaHKCN, HKIEMCN.TenHanhKiem AS TenHKCN, KQCNTH.MaKetQua, KQ.TenKetQua, KQCNTH.DTBCaNam AS DTBCMCN " + "FROM HOCSINH HS, MONHOC MH, HOCKY HK, NAMHOC NH, LOP LP, HOCLUC HLHK1, HOCLUC HLHK2, HOCLUC HLCN, HANHKIEM HKIEMHK1, HANHKIEM HKIEMHK2, HANHKIEM HKIEMCN, KETQUA KQ, KQ_HOC_KY_MON_HOC KQHK1MH, KQ_HOC_KY_TONG_HOP KQHK1TH, KQ_HOC_KY_MON_HOC KQHK2MH, KQ_HOC_KY_TONG_HOP KQHK2TH, KQ_CA_NAM_MON_HOC KQCNMH, KQ_CA_NAM_TONG_HOP KQCNTH " + "WHERE KQHK1MH.MaHocSinh = HS.MaHocSinh AND KQHK1MH.MaMonHoc = MH.MaMonHoc AND KQHK1MH.MaHocKy = HK.MaHocKy AND KQHK1MH.MaNamHoc = NH.MaNamHoc AND KQHK1MH.MaLop = LP.MaLop AND KQHK1TH.MaHocSinh = HS.MaHocSinh AND KQHK1TH.MaHocKy = HK.MaHocKy AND KQHK1TH.MaNamHoc = NH.MaNamHoc AND KQHK1TH.MaLop = LP.MaLop AND KQHK1TH.MaHocLuc = HLHK1.MaHocLuc AND KQHK1TH.MaHanhKiem = HKIEMHK1.MaHanhKiem AND KQHK2MH.MaHocSinh = HS.MaHocSinh AND KQHK2MH.MaMonHoc = MH.MaMonHoc AND KQHK2MH.MaHocKy = HK.MaHocKy AND KQHK2MH.MaNamHoc = NH.MaNamHoc AND KQHK2MH.MaLop = LP.MaLop AND KQHK2TH.MaHocSinh = HS.MaHocSinh AND KQHK2TH.MaHocKy = HK.MaHocKy AND KQHK2TH.MaNamHoc = NH.MaNamHoc AND KQHK2TH.MaLop = LP.MaLop AND KQHK2TH.MaHocLuc = HLHK2.MaHocLuc AND KQHK2TH.MaHanhKiem = HKIEMHK2.MaHanhKiem AND KQCNMH.MaHocSinh = HS.MaHocSinh AND KQCNMH.MaMonHoc = MH.MaMonHoc AND KQCNMH.MaNamHoc = NH.MaNamHoc AND KQCNMH.MaLop = LP.MaLop AND KQCNTH.MaHocSinh = HS.MaHocSinh AND KQCNTH.MaNamHoc = NH.MaNamHoc AND KQCNTH.MaLop = LP.MaLop AND KQCNTH.MaHocLuc = HLCN.MaHocLuc AND KQCNTH.MaHanhKiem = HKIEMCN.MaHanhKiem AND KQCNTH.MaKetQua = KQ.MaKetQua AND NH.MaNamHoc = @maNamHoc AND LP.MaLop = @maLop AND HS.MahocSinh = @maHocSinh"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;

                diemDS.Load(cmd);
            }

            return diemDS;
        }
    }
}