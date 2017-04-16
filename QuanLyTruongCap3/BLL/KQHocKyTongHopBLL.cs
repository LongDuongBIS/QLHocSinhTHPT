using QuanLyTruongCap3.DAL;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyTruongCap3.BLL
{
    public class KQHocKyTongHopBLL
    {
        private DiemBLL diemBLL = new DiemBLL();
        private HocLucBLL hocLucBLL = new HocLucBLL();
        private KQHocKyTongHopDAL kqHocKyTongHopDAL = new KQHocKyTongHopDAL();

        public void LuuKetQua(string maHocSinh, string maLop, string maHocKy, string maNamHoc)
        {
            float diemTBChungCacMonHK = (float)Math.Round(diemBLL.DiemTrungBinhChungCacMonHocKy(maHocSinh, maLop, maHocKy, maNamHoc), 2);
            string hocLuc = hocLucBLL.XepLoaiLocLucHocKy(maHocSinh, maLop, maHocKy, maNamHoc);

            kqHocKyTongHopDAL.XoaKetQua(maHocSinh, maLop, maHocKy, maNamHoc);
            kqHocKyTongHopDAL.LuuKetQua(maHocSinh, maLop, maHocKy, maNamHoc, hocLuc, "HK0001", diemTBChungCacMonHK);
        }

        public static IList<KQHocKyTongHopDTO> LayDsKQHocKyTongHop(string maLop, string maHocKy, string maNamHoc)
        {
            DataTable dt = new KQHocKyTongHopDAL().LayDsKQHocKyTongHopForReport(maLop, maHocKy, maNamHoc);

            IList<KQHocKyTongHopDTO> dS = new List<KQHocKyTongHopDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                KQHocKyTongHopDTO kqHocKyTongHopDTO = new KQHocKyTongHopDTO();

                HocSinhDTO hocSinhDTO = new HocSinhDTO();
                hocSinhDTO.MaHocSinh = Convert.ToString(Row["MaHocSinh"]);
                hocSinhDTO.HoTen = Convert.ToString(Row["HoTen"]);

                LopDTO lopDTO = new LopDTO();
                lopDTO.MaLop = Convert.ToString(Row["MaLop"]);
                lopDTO.TenLop = Convert.ToString(Row["TenLop"]);

                HocKyDTO hocKyDTO = new HocKyDTO();
                hocKyDTO.MaHocKy = Convert.ToString(Row["MaHocKy"]);
                hocKyDTO.TenHocKy = Convert.ToString(Row["TenHocKy"]);

                NamHocDTO namHocDTO = new NamHocDTO();
                namHocDTO.MaNamHoc = Convert.ToString(Row["MaNamHoc"]);
                namHocDTO.TenNamHoc = Convert.ToString(Row["TenNamHoc"]);

                HocLucDTO hocLucDTO = new HocLucDTO();
                hocLucDTO.MaHocLuc = Convert.ToString(Row["MaHocLuc"]);
                hocLucDTO.TenHocLuc = Convert.ToString(Row["TenHocLuc"]);

                HanhKiemDTO hanhKiemDTO = new HanhKiemDTO();
                hanhKiemDTO.MaHanhKiem = Convert.ToString(Row["MaHanhKiem"]);
                hanhKiemDTO.TenHanhKiem = Convert.ToString(Row["TenHanhKiem"]);

                kqHocKyTongHopDTO.HocSinh = hocSinhDTO;
                kqHocKyTongHopDTO.Lop = lopDTO;
                kqHocKyTongHopDTO.HocKy = hocKyDTO;
                kqHocKyTongHopDTO.NamHoc = namHocDTO;
                kqHocKyTongHopDTO.HocLuc = hocLucDTO;
                kqHocKyTongHopDTO.HanhKiem = hanhKiemDTO;
                kqHocKyTongHopDTO.DTBMonHocKy = Convert.ToSingle(Row["DTBMonHocKy"]);

                dS.Add(kqHocKyTongHopDTO);
            }
            return dS;
        }
    }
}