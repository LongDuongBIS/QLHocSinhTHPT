using DevComponents.DotNetBar.Controls;
using QLHocSinhTHPT.DAL;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT.BLL
{
    public class HanhKiemBLL
    {
        private readonly HanhKiemDAL hanhKiemDAL = new HanhKiemDAL();

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

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = hanhKiemDAL.LayDsHanhKiem();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return hanhKiemDAL.ThemDongMoi();
        }

        public void ThemHanhKiem(DataRow row)
        {
            hanhKiemDAL.ThemHanhKiem(row);
        }

        public bool LuuHanhKiem()
        {
            return hanhKiemDAL.LuuHanhKiem();
        }
    }
}