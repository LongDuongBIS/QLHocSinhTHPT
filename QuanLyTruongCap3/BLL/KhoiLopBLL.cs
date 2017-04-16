using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class KhoiLopBLL
    {
        private readonly KhoiLopDAL khoiLopDAL = new KhoiLopDAL();

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = khoiLopDAL.LayDsKhoiLop();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = khoiLopDAL.LayDsKhoiLop();
            comboBox.DisplayMember = "TenKhoiLop";
            comboBox.ValueMember = "MaKhoiLop";
        }

        public void HienThiComboBox(string khoiLopCu, ComboBoxEx cmbKhoiLopMoi)
        {
            cmbKhoiLopMoi.DataSource = khoiLopDAL.LayDsKhoiLop(khoiLopCu);
            cmbKhoiLopMoi.DisplayMember = "TenKhoiLop";
            cmbKhoiLopMoi.ValueMember = "MaKhoiLop";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = khoiLopDAL.LayDsKhoiLop();
            cmbColumn.DisplayMember = "TenKhoiLop";
            cmbColumn.ValueMember = "MaKhoiLop";
            cmbColumn.DataPropertyName = "MaKhoiLop";
            cmbColumn.HeaderText = "Khối lớp";
        }

        public bool LuuKhoiLop()
        {
            return khoiLopDAL.LuuKhoiLop();
        }

        public DataRow ThemDongMoi()
        {
            return khoiLopDAL.ThemDongMoi();
        }

        public void ThemKhoiLop(DataRow row)
        {
            khoiLopDAL.ThemKhoiLop(row);
        }
    }
}