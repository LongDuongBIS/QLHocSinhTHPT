using QuanLyTruongCap3.DAL;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyTruongCap3.BLL
{
    public class KQCaNamTongHopBLL
    {
        private DiemBLL diemBLL = new DiemBLL();
        private HocLucBLL hocLucBLL = new HocLucBLL();
        private KQCaNamTongHopDAL kqCaNamTongHopDAL = new KQCaNamTongHopDAL();

        public static IList<KQCaNamTongHopDTO> LayDsKQCaNamTongHop(string maLop, string maNamHoc)
        {
            DataTable dt = new KQCaNamTongHopDAL().LayDsKQCaNamTongHopForReport(maLop, maNamHoc);

            IList<KQCaNamTongHopDTO> dS = new List<KQCaNamTongHopDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                KQCaNamTongHopDTO kqCaNamTongHopDTO = new KQCaNamTongHopDTO();

                HocSinhDTO hocSinhDTO = new HocSinhDTO();
                hocSinhDTO.MaHocSinh = Convert.ToString(Row["MaHocSinh"]);
                hocSinhDTO.HoTen = Convert.ToString(Row["HoTen"]);

                LopDTO lopDTO = new LopDTO();
                lopDTO.MaLop = Convert.ToString(Row["MaLop"]);
                lopDTO.TenLop = Convert.ToString(Row["TenLop"]);

                NamHocDTO namHocDTO = new NamHocDTO();
                namHocDTO.MaNamHoc = Convert.ToString(Row["MaNamHoc"]);
                namHocDTO.TenNamHoc = Convert.ToString(Row["TenNamHoc"]);

                HocLucDTO hocLucDTO = new HocLucDTO();
                hocLucDTO.MaHocLuc = Convert.ToString(Row["MaHocLuc"]);
                hocLucDTO.TenHocLuc = Convert.ToString(Row["TenHocLuc"]);

                HanhKiemDTO hanhKiemDTO = new HanhKiemDTO();
                hanhKiemDTO.MaHanhKiem = Convert.ToString(Row["MaHanhKiem"]);
                hanhKiemDTO.TenHanhKiem = Convert.ToString(Row["TenHanhKiem"]);

                KetQuaDTO ketQuaDTO = new KetQuaDTO();
                ketQuaDTO.MaKetQua = Convert.ToString(Row["MaKetQua"]);
                ketQuaDTO.TenKetQua = Convert.ToString(Row["TenKetQua"]);

                kqCaNamTongHopDTO.HocSinh = hocSinhDTO;
                kqCaNamTongHopDTO.Lop = lopDTO;
                kqCaNamTongHopDTO.NamHoc = namHocDTO;
                kqCaNamTongHopDTO.HocLuc = hocLucDTO;
                kqCaNamTongHopDTO.HanhKiem = hanhKiemDTO;
                kqCaNamTongHopDTO.KetQua = ketQuaDTO;
                kqCaNamTongHopDTO.DTBCaNam = Convert.ToSingle(Row["DTBCaNam"]);

                dS.Add(kqCaNamTongHopDTO);
            }
            return dS;
        }

        public void LuuKetQua(string maHocSinh, string maLop, string maNamHoc)
        {
            float diemTBChungCacMonCN = (float)Math.Round(diemBLL.DiemTrungBinhChungCacMonCaNam(maHocSinh, maLop, maNamHoc), 2);
            string hocLuc = hocLucBLL.XepLoaiLocLucCaNam(maHocSinh, maLop, maNamHoc);
            string ketQua = "KQ0001";

            kqCaNamTongHopDAL.XoaKetQua(maHocSinh, maLop, maNamHoc);
            kqCaNamTongHopDAL.LuuKetQua(maHocSinh, maLop, maNamHoc, hocLuc, "HK0001", diemTBChungCacMonCN, ketQua);
        }
    }
}