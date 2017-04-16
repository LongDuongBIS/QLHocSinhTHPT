using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class NamHocBLL
    {
        private readonly NamHocDAL namHocDAL = new NamHocDAL();

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = namHocDAL.LayDsNamHoc();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = namHocDAL.LayDsNamHoc();
            comboBox.DisplayMember = "TenNamHoc";
            comboBox.ValueMember = "MaNamHoc";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = namHocDAL.LayDsNamHoc();
            cmbColumn.DisplayMember = "TenNamHoc";
            cmbColumn.ValueMember = "MaNamHoc";
            cmbColumn.DataPropertyName = "MaNamHoc";
            cmbColumn.HeaderText = "Năm học";
        }

        public bool LuuNamHoc()
        {
            return namHocDAL.LuuNamHoc();
        }

        public DataRow ThemDongMoi()
        {
            return namHocDAL.ThemDongMoi();
        }

        public void ThemNamHoc(DataRow row)
        {
            namHocDAL.ThemNamHoc(row);
        }
    }
}