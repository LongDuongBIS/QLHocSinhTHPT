using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class HanhKiemBLL
    {
        private readonly HanhKiemDAL hanhKiemDAL = new HanhKiemDAL();

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            var bS = new BindingSource
            {

                DataSource = hanhKiemDAL.LayDsHanhKiem()
            };
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = hanhKiemDAL.LayDsHanhKiem();
            comboBox.DisplayMember = "TenHanhKiem";
            comboBox.ValueMember = "MaHanhKiem";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = hanhKiemDAL.LayDsHanhKiem();
            cmbColumn.DisplayMember = "TenHanhKiem";
            cmbColumn.ValueMember = "MaHanhKiem";
            cmbColumn.DataPropertyName = "MaHanhKiem";
            cmbColumn.HeaderText = "Hạnh kiểm";
        }

        public bool LuuHanhKiem()
        {
            return hanhKiemDAL.LuuHanhKiem();
        }

        public DataRow ThemDongMoi()
        {
            return hanhKiemDAL.ThemDongMoi();
        }

        public void ThemHanhKiem(DataRow row)
        {
            hanhKiemDAL.ThemHanhKiem(row);
        }
    }
}