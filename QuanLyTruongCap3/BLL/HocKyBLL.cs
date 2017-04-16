using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class HocKyBLL
    {
        private readonly HocKyDAL hocKyDAL = new HocKyDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = hocKyDAL.LayDsHocKy();
            comboBox.DisplayMember = "TenHocKy";
            comboBox.ValueMember = "MaHocKy";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = hocKyDAL.LayDsHocKy();
            cmbColumn.DisplayMember = "TenHocKy";
            cmbColumn.ValueMember = "MaHocKy";
            cmbColumn.DataPropertyName = "MaHocKy";
            cmbColumn.HeaderText = "Học kỳ";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = hocKyDAL.LayDsHocKy();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return hocKyDAL.ThemDongMoi();
        }

        public void ThemHocKy(DataRow row)
        {
            hocKyDAL.ThemHocKy(row);
        }

        public bool LuuHocKy()
        {
            return hocKyDAL.LuuHocKy();
        }
    }
}