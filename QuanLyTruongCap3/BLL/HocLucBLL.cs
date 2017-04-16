using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class HocLucBLL
    {
        private DiemBLL diemBLL = new DiemBLL();
        private readonly HocLucDAL hocLucDAL = new HocLucDAL();
        private MonHocDAL monHocDAL = new MonHocDAL();

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = hocLucDAL.LayDsHocLuc();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = hocLucDAL.LayDsHocLuc();
            comboBox.DisplayMember = "TenHocLuc";
            comboBox.ValueMember = "MaHocLuc";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = hocLucDAL.LayDsHocLuc();
            cmbColumn.DisplayMember = "TenHocLuc";
            cmbColumn.ValueMember = "MaHocLuc";
            cmbColumn.DataPropertyName = "MaHocLuc";
            cmbColumn.HeaderText = "Học lực";
        }

        public bool LuuHocLuc()
        {
            return hocLucDAL.LuuHocLuc();
        }

        public DataRow ThemDongMoi()
        {
            return hocLucDAL.ThemDongMoi();
        }

        public void ThemHocLuc(DataRow row)
        {
            hocLucDAL.ThemHocLuc(row);
        }

        public string XepLoaiHocLucMonHoc(float[] arrayDiemTBTungMon, float tongDiem)
        {
            string xepLoai = string.Empty;
            float diemTBMonNhoNhat = arrayDiemTBTungMon[0];

            for (int i = 0; i < arrayDiemTBTungMon.Length - 1; i++)
            {
                if (arrayDiemTBTungMon[i] < diemTBMonNhoNhat)
                    diemTBMonNhoNhat = arrayDiemTBTungMon[i];
            }

            DataTable dt = hocLucDAL.LayDsHocLuc();
            string[] maHocLuc = new string[dt.Rows.Count];
            float[] diemCanDuoi = new float[dt.Rows.Count];

            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                maHocLuc[count] = row["MaHocLuc"].ToString();
                diemCanDuoi[count] = float.Parse(row["DiemCanDuoi"].ToString());
                count++;
            }

            for (int i = 0; i < count - 1; i++)
            {
                if (tongDiem >= diemCanDuoi[i] && diemTBMonNhoNhat >= diemCanDuoi[i + 1])
                {
                    xepLoai = maHocLuc[i];
                    break;
                }
            }

            if (xepLoai == string.Empty)
                xepLoai = maHocLuc[count - 1];
            return xepLoai;
        }

        public string XepLoaiLocLucCaNam(string maHocSinh, string maLop, string maNamHoc)
        {
            float tongDiem = 0;
            float tongDiemCacMon = 0;
            float diemTBTungMon = 0;
            int tongHeSoCacMon = 0;

            DataTable dt = monHocDAL.LayDsMonHoc(maNamHoc, maLop);

            float[] arrayDiemTBTungMon = new float[dt.Rows.Count];

            int soMonHoc = 0;
            foreach (DataRow row in dt.Rows)
            {
                diemTBTungMon = diemBLL.DiemTrungBinhMonCaNam(maHocSinh, row["MaMonHoc"].ToString(), maNamHoc, maLop);
                arrayDiemTBTungMon[soMonHoc++] = diemTBTungMon;

                tongDiemCacMon += diemTBTungMon * Convert.ToInt32(row["HeSo"].ToString());
                tongHeSoCacMon += Convert.ToInt32(row["HeSo"].ToString());
            }
            if (tongHeSoCacMon > 0)
                tongDiem = tongDiemCacMon / tongHeSoCacMon;
            else
                tongDiem = 0;

            return XepLoaiHocLucMonHoc(arrayDiemTBTungMon, tongDiem);
        }

        public string XepLoaiLocLucHocKy(string maHocSinh, string maLop, string maHocKy, string maNamHoc)
        {
            float tongDiem = 0;
            float tongDiemCacMon = 0;
            float diemTBTungMon = 0;
            int tongHeSoCacMon = 0;

            DataTable dt = monHocDAL.LayDsMonHoc(maNamHoc, maLop);

            float[] arrayDiemTBTungMon = new float[dt.Rows.Count];

            int soMonHoc = 0;
            foreach (DataRow row in dt.Rows)
            {
                diemTBTungMon = diemBLL.DiemTrungBinhMonHocKy(maHocSinh, row["MaMonHoc"].ToString(), maHocKy, maNamHoc, maLop);
                arrayDiemTBTungMon[soMonHoc++] = diemTBTungMon;

                tongDiemCacMon += diemTBTungMon * Convert.ToInt32(row["HeSo"].ToString());
                tongHeSoCacMon += Convert.ToInt32(row["HeSo"].ToString());
            }
            if (tongHeSoCacMon > 0)
                tongDiem = tongDiemCacMon / tongHeSoCacMon;
            else
                tongDiem = 0;

            return XepLoaiHocLucMonHoc(arrayDiemTBTungMon, tongDiem);
        }
    }
}