using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using System.Collections.Generic;
using DevComponents.Editors;
using QLHocSinhTHPT.DTO;
using QLHocSinhTHPT.DataLayer;

namespace QLHocSinhTHPT.Controller
{
    public class KQHocKyMonHocCtrl
    {
        #region Fields
        DiemCtrl            m_DiemCtrl          = new DiemCtrl();
        KQHocKyMonHocData   m_KQHocKyMonHocData = new KQHocKyMonHocData();
        #endregion

        #region Luu ket qua
        public void LuuKetQua(String maHocSinh, String maLop, String maMonHoc, String maHocKy, String maNamHoc)
        {
            float diemTBKT      = (float)Math.Round(m_DiemCtrl.DiemTrungBinhKiemTra(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop), 2);
            float diemTBMonHK   = (float)Math.Round(m_DiemCtrl.DiemTrungBinhMonHocKy(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop), 2);

            m_KQHocKyMonHocData.XoaKetQua(maHocSinh, maLop, maMonHoc, maHocKy, maNamHoc);
            m_KQHocKyMonHocData.LuuKetQua(maHocSinh, maLop, maMonHoc, maHocKy, maNamHoc, diemTBKT, diemTBMonHK);
        }
        #endregion

        #region Lay danh sach ket qua hoc ky mon hoc do vao report
        public static IList<KQHocKyMonHocDTO> LayDsKQHocKyMonHoc(String maLop, String maMonHoc, String maHocKy, String maNamHoc)
        {
            KQHocKyMonHocData m_KQHKMHData = new KQHocKyMonHocData();
            DataTable m_DT = m_KQHKMHData.LayDsKQHocKyMonHocForReport(maLop, maMonHoc, maHocKy, maNamHoc);

            IList<KQHocKyMonHocDTO> dS = new List<KQHocKyMonHocDTO>();

            foreach (DataRow Row in m_DT.Rows)
            {
                KQHocKyMonHocDTO ketqua = new KQHocKyMonHocDTO();

                HocSinhDTO hs      = new HocSinhDTO();
                hs.MaHocSinh        = Convert.ToString(Row["MaHocSinh"]);
                hs.HoTen            = Convert.ToString(Row["HoTen"]);

                LopDTO l           = new LopDTO();
                l.MaLop             = Convert.ToString(Row["MaLop"]);
                l.TenLop            = Convert.ToString(Row["TenLop"]);

                MonHocDTO mh       = new MonHocDTO();
                mh.MaMonHoc         = Convert.ToString(Row["MaMonHoc"]);
                mh.TenMonHoc        = Convert.ToString(Row["TenMonHoc"]);

                HocKyDTO hk        = new HocKyDTO();
                hk.MaHocKy          = Convert.ToString(Row["MaHocKy"]);
                hk.TenHocKy         = Convert.ToString(Row["TenHocKy"]);

                NamHocDTO nh       = new NamHocDTO();
                nh.MaNamHoc         = Convert.ToString(Row["MaNamHoc"]);
                nh.TenNamHoc        = Convert.ToString(Row["TenNamHoc"]);

                ketqua.HocSinh      = hs;
                ketqua.Lop          = l;
                ketqua.MonHoc       = mh;
                ketqua.HocKy        = hk;
                ketqua.NamHoc       = nh;
                ketqua.DTBKiemTra   = Convert.ToSingle(Row["DTBKiemTra"]);
                ketqua.DTBMonHocKy  = Convert.ToSingle(Row["DTBMonHocKy"]);

                dS.Add(ketqua);
            }
            return dS;
        }
        #endregion
    }
}
