using DevComponents.DotNetBar.Controls;
using QLHocSinhTHPT.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT.BLL
{
    public class DiemBLL
    {
        private DiemDAL diemDAL = new DiemDAL();
        private readonly HocKyDAL hocKyDAL = new HocKyDAL();
        private MonHocDAL monHocDAL = new MonHocDAL();

        public void LuuDiem(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop, string maLoaiDiem, float diemSo)
        {
            diemDAL.LuuDiem(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop, maLoaiDiem, diemSo);
        }

        public void XoaDiem(int stt)
        {
            diemDAL.XoaDiem(stt);
        }

        public void HienThiDanhSachXemDiem(ListViewEx lVXemDiem, string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
        {
            lVXemDiem.Items.Clear();

            DataTable dt = diemDAL.LayDsDiem(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop);

            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();

                item.Text = row["STT"].ToString();
                item.SubItems.Add(row["MaHocSinh"].ToString());
                item.SubItems.Add(row["HoTen"].ToString());
                item.SubItems.Add(row["TenMonHoc"].ToString());
                item.SubItems.Add(row["TenLoai"].ToString());
                item.SubItems.Add(row["Diem"].ToString());

                lVXemDiem.Items.Add(item);
            }
        }

        public float DiemTrungBinhKiemTra(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
        {
            DataTable dt = diemDAL.LayDsDiemHocSinh(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop);

            float tongDiem = 0;
            int tongHeSo = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaLoai"].ToString() != "LD0004")
                {
                    tongDiem += Convert.ToSingle(row["Diem"].ToString()) * Convert.ToInt32(row["HeSo"].ToString());
                    tongHeSo += Convert.ToInt32(row["HeSo"].ToString());
                }
            }

            if (tongHeSo > 0)
                return tongDiem / tongHeSo;
            return 0;
        }

        public float DiemTrungBinhMonHocKy(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
        {
            DataTable dt = diemDAL.LayDsDiemHocSinh(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop);

            float tongDiem = 0;
            int tongHeSo = 0;

            foreach (DataRow row in dt.Rows)
            {
                tongDiem += Convert.ToSingle(row["Diem"].ToString()) * Convert.ToInt32(row["HeSo"].ToString());
                tongHeSo += Convert.ToInt32(row["HeSo"].ToString());
            }

            if (tongHeSo > 0)
                return tongDiem / tongHeSo;
            return 0;
        }

        public float DiemTrungBinhChungCacMonHocKy(string maHocSinh, string maLop, string maHocKy, string maNamHoc)
        {
            float tongDiemCacMon = 0;
            float diemTBTungMon = 0;
            int tongHeSoCacMon = 0;

            DataTable dt = monHocDAL.LayDsMonHoc(maNamHoc, maLop);

            foreach (DataRow row in dt.Rows)
            {
                diemTBTungMon = DiemTrungBinhMonHocKy(maHocSinh, row["MaMonHoc"].ToString(), maHocKy, maNamHoc, maLop);
                tongDiemCacMon += diemTBTungMon * Convert.ToInt32(row["HeSo"].ToString());
                tongHeSoCacMon += Convert.ToInt32(row["HeSo"].ToString());
            }
            if (tongHeSoCacMon > 0)
                return tongDiemCacMon / tongHeSoCacMon;
            return 0;
        }

        public float DiemTrungBinhChungCacMonCaNam(string maHocSinh, string maLop, string maNamHoc)
        {
            float tongDiemCacMon = 0;
            float diemTBTungMon = 0;
            int tongHeSoCacMon = 0;

            DataTable dt = monHocDAL.LayDsMonHoc(maNamHoc, maLop);

            foreach (DataRow row in dt.Rows)
            {
                diemTBTungMon = DiemTrungBinhMonCaNam(maHocSinh, row["MaMonHoc"].ToString(), maNamHoc, maLop);
                tongDiemCacMon += diemTBTungMon * Convert.ToInt32(row["HeSo"].ToString());
                tongHeSoCacMon += Convert.ToInt32(row["HeSo"].ToString());
            }
            if (tongHeSoCacMon > 0)
                return tongDiemCacMon / tongHeSoCacMon;
            return 0;
        }

        public float DiemTrungBinhMonCaNam(string maHocSinh, string maMonHoc, string maNamHoc, string maLop)
        {
            DataTable dt = new DataTable();
            dt = hocKyDAL.LayDsHocKy();

            float tongDiem = 0;
            int tongHeSo = 0;

            foreach (DataRow row in dt.Rows)
            {
                tongDiem += DiemTrungBinhMonHocKy(maHocSinh, maMonHoc, row["MaHocKy"].ToString(), maNamHoc, maLop) * Convert.ToInt32(row["HeSo"].ToString());
                tongHeSo += Convert.ToInt32(row["HeSo"].ToString());
            }

            if (tongHeSo > 0)
                return tongDiem / tongHeSo;
            return 0;
        }
    }
}