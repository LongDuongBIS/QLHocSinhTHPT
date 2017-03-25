using QLHocSinhTHPT.DAL;
using QLHocSinhTHPT.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QLHocSinhTHPT.BLL
{
    public class KQCaNamMonHocBLL
    {
        private DiemBLL diemBLL = new DiemBLL();
        private KQCaNamMonHocDAL kqCaNamMonHocDAL = new KQCaNamMonHocDAL();

        public void LuuKetQua(string maHocSinh, string maLop, string maMonHoc, string maNamHoc)
        {
            float diemTBMonCN = (float)Math.Round(diemBLL.DiemTrungBinhMonCaNam(maHocSinh, maMonHoc, maNamHoc, maLop), 2);
            float diemThiLai = 0;

            kqCaNamMonHocDAL.XoaKetQua(maHocSinh, maLop, maMonHoc, maNamHoc);
            kqCaNamMonHocDAL.LuuKetQua(maHocSinh, maLop, maMonHoc, maNamHoc, diemThiLai, diemTBMonCN);
        }

        public static IList<KQCaNamMonHocDTO> LayDsKQCaNamMonHoc(string maLop, string maMonHoc, string maNamHoc)
        {
            DataTable dt = new KQCaNamMonHocDAL().LayDsKQCaNamMonHocForReport(maLop, maMonHoc, maNamHoc);

            IList<KQCaNamMonHocDTO> dS = new List<KQCaNamMonHocDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                KQCaNamMonHocDTO kqCaNamMonHocDTO = new KQCaNamMonHocDTO();

                HocSinhDTO hocSinhDTO = new HocSinhDTO();
                hocSinhDTO.MaHocSinh = Convert.ToString(Row["MaHocSinh"]);
                hocSinhDTO.HoTen = Convert.ToString(Row["HoTen"]);

                LopDTO lopDTO = new LopDTO();
                lopDTO.MaLop = Convert.ToString(Row["MaLop"]);
                lopDTO.TenLop = Convert.ToString(Row["TenLop"]);

                MonHocDTO monHocDTO = new MonHocDTO();
                monHocDTO.MaMonHoc = Convert.ToString(Row["MaMonHoc"]);
                monHocDTO.TenMonHoc = Convert.ToString(Row["TenMonHoc"]);

                NamHocDTO namHocDTO = new NamHocDTO();
                namHocDTO.MaNamHoc = Convert.ToString(Row["MaNamHoc"]);
                namHocDTO.TenNamHoc = Convert.ToString(Row["TenNamHoc"]);

                kqCaNamMonHocDTO.HocSinh = hocSinhDTO;
                kqCaNamMonHocDTO.Lop = lopDTO;
                kqCaNamMonHocDTO.MonHoc = monHocDTO;
                kqCaNamMonHocDTO.NamHoc = namHocDTO;
                kqCaNamMonHocDTO.DiemThiLai = Convert.ToSingle(Row["DiemThiLai"]);
                kqCaNamMonHocDTO.DTBCaNam = Convert.ToSingle(Row["DTBCaNam"]);

                dS.Add(kqCaNamMonHocDTO);
            }
            return dS;
        }
    }
}