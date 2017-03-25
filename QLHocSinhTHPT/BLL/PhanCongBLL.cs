using DevComponents.DotNetBar.Controls;
using QLHocSinhTHPT.DAL;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT.BLL
{
    public class PhanCongBLL
    {
        private readonly PhanCongDAL phanCongDAL = new PhanCongDAL();

        public void HienThi(DataGridView dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = phanCongDAL.LayDsPhanCong();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN, TextBoxX txtSTT, ComboBoxEx cmbNamHoc, ComboBoxEx cmbLop, ComboBoxEx cmbMonHoc, ComboBoxEx cmbGiaoVien)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = phanCongDAL.LayDsPhanCong();

            txtSTT.DataBindings.Clear();
            txtSTT.DataBindings.Add("Text", bS, "STT");

            cmbNamHoc.DataBindings.Clear();
            cmbNamHoc.DataBindings.Add("SelectedValue", bS, "MaNamHoc");

            cmbLop.DataBindings.Clear();
            cmbLop.DataBindings.Add("SelectedValue", bS, "MaLop");

            cmbMonHoc.DataBindings.Clear();
            cmbMonHoc.DataBindings.Add("SelectedValue", bS, "MaMonHoc");

            cmbGiaoVien.DataBindings.Clear();
            cmbGiaoVien.DataBindings.Add("SelectedValue", bS, "MaGiaoVien");

            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return phanCongDAL.ThemDongMoi();
        }

        public void ThemPhanCong(DataRow row)
        {
            phanCongDAL.ThemPhanCong(row);
        }

        public bool LuuPhanCong()
        {
            return phanCongDAL.LuuPhanCong();
        }

        public void LuuPhanCong(string maNamHoc, string maLop, string maMonHoc, string maGiaoVien)
        {
            phanCongDAL.LuuPhanCong(maNamHoc, maLop, maMonHoc, maGiaoVien);
        }

        public void TimTheoTenLop(string tenLop)
        {
            phanCongDAL.TimTheoTenLop(tenLop);
        }

        public void TimTheoTenGV(string tenGiaoVien)
        {
            phanCongDAL.TimTheoTenGV(tenGiaoVien);
        }
    }
}