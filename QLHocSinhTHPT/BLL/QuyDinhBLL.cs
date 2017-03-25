using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using QLHocSinhTHPT.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT.BLL
{
    public class QuyDinhBLL
    {
        private readonly QuyDinhDAL quyDinhDAL = new QuyDinhDAL();

        public void HienThi(IntegerInput txtSiSoCanDuoi, IntegerInput txtSiSoCanTren, IntegerInput txtDoTuoiCanDuoi, IntegerInput txtDoTuoiCanTren, CheckBoxX ckbThang10, CheckBoxX ckbThang100, TextBoxX txtTenTruong, TextBoxX txtDiaChiTruong)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = quyDinhDAL.LayDsQuyDinh();

            DataTable dt = quyDinhDAL.LayDsQuyDinh();
            int thangDiem = Convert.ToInt32(dt.Rows[0]["ThangDiem"]);

            if (thangDiem == 10)
                ckbThang10.Checked = true;
            else
                ckbThang100.Checked = true;

            txtSiSoCanDuoi.DataBindings.Clear();
            txtSiSoCanDuoi.DataBindings.Add("Value", bS, "SiSoCanDuoi");

            txtSiSoCanTren.DataBindings.Clear();
            txtSiSoCanTren.DataBindings.Add("Value", bS, "SiSoCanTren");

            txtDoTuoiCanDuoi.DataBindings.Clear();
            txtDoTuoiCanDuoi.DataBindings.Add("Value", bS, "TuoiCanDuoi");

            txtDoTuoiCanTren.DataBindings.Clear();
            txtDoTuoiCanTren.DataBindings.Add("Value", bS, "TuoiCanTren");

            txtTenTruong.DataBindings.Clear();
            txtTenTruong.DataBindings.Add("Text", bS, "TenTruong");

            txtDiaChiTruong.DataBindings.Clear();
            txtDiaChiTruong.DataBindings.Add("Text", bS, "DiaChiTruong");
        }

        public void CapNhatQuyDinhSiSo(int siSoCanDuoi, int siSoCanTren)
        {
            quyDinhDAL.CapNhatQuyDinhSiSo(siSoCanDuoi, siSoCanTren);
        }

        public void CapNhatQuyDinhDoTuoi(int tuoiCanDuoi, int tuoiCanTren)
        {
            quyDinhDAL.CapNhatQuyDinhDoTuoi(tuoiCanDuoi, tuoiCanTren);
        }

        public void CapNhatQuyDinhTruong(string tenTruong, string diaChiTruong)
        {
            quyDinhDAL.CapNhatQuyDinhTruong(tenTruong, diaChiTruong);
        }

        public void CapNhatQuyDinhThangDiem(int thangDiem)
        {
            quyDinhDAL.CapNhatQuyDinhThangDiem(thangDiem);
        }
    }
}