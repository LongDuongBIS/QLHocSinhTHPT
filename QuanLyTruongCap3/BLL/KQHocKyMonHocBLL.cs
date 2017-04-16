using QuanLyTruongCap3.DAL;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyTruongCap3.BLL
{
    public class KQHocKyMonHocBLL
    {
        private DiemBLL diemBLL = new DiemBLL();
        private KQHocKyMonHocDAL kqHocKyMonHocDAL = new KQHocKyMonHocDAL();

        public static IList<KQHocKyMonHocDTO> LayDsKQHocKyMonHoc(string maLop, string maMonHoc, string maHocKy, string maNamHoc)
        {
            DataTable dt = new KQHocKyMonHocDAL().LayDsKQHocKyMonHocForReport(maLop, maMonHoc, maHocKy, maNamHoc);

            IList<KQHocKyMonHocDTO> dS = new List<KQHocKyMonHocDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                KQHocKyMonHocDTO kqHocKyMonHocDTO = new KQHocKyMonHocDTO();

                HocSinhDTO hocSinhDTO = new HocSinhDTO();
                hocSinhDTO.MaHocSinh = Convert.ToString(Row["MaHocSinh"]);
                hocSinhDTO.HoTen = Convert.ToString(Row["HoTen"]);

                LopDTO lopDTO = new LopDTO();
                lopDTO.MaLop = Convert.ToString(Row["MaLop"]);
                lopDTO.TenLop = Convert.ToString(Row["TenLop"]);

                MonHocDTO monHocDTO = new MonHocDTO();
                monHocDTO.MaMonHoc = Convert.ToString(Row["MaMonHoc"]);
                monHocDTO.TenMonHoc = Convert.ToString(Row["TenMonHoc"]);

                HocKyDTO hocKyDTO = new HocKyDTO();
                hocKyDTO.MaHocKy = Convert.ToString(Row["MaHocKy"]);
                hocKyDTO.TenHocKy = Convert.ToString(Row["TenHocKy"]);

                NamHocDTO namHocDTO = new NamHocDTO();
                namHocDTO.MaNamHoc = Convert.ToString(Row["MaNamHoc"]);
                namHocDTO.TenNamHoc = Convert.ToString(Row["TenNamHoc"]);

                kqHocKyMonHocDTO.HocSinh = hocSinhDTO;
                kqHocKyMonHocDTO.Lop = lopDTO;
                kqHocKyMonHocDTO.MonHoc = monHocDTO;
                kqHocKyMonHocDTO.HocKy = hocKyDTO;
                kqHocKyMonHocDTO.NamHoc = namHocDTO;
                kqHocKyMonHocDTO.DTBKiemTra = Convert.ToSingle(Row["DTBKiemTra"]);
                kqHocKyMonHocDTO.DTBMonHocKy = Convert.ToSingle(Row["DTBMonHocKy"]);

                dS.Add(kqHocKyMonHocDTO);
            }
            return dS;
        }

        public void LuuKetQua(string maHocSinh, string maLop, string maMonHoc, string maHocKy, string maNamHoc)
        {
            float diemTBKT = (float)Math.Round(diemBLL.DiemTrungBinhKiemTra(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop), 2);
            float diemTBMonHK = (float)Math.Round(diemBLL.DiemTrungBinhMonHocKy(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop), 2);

            kqHocKyMonHocDAL.XoaKetQua(maHocSinh, maLop, maMonHoc, maHocKy, maNamHoc);
            kqHocKyMonHocDAL.LuuKetQua(maHocSinh, maLop, maMonHoc, maHocKy, maNamHoc, diemTBKT, diemTBMonHK);
        }
    }
}