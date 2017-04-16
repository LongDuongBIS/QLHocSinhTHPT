using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class TonGiaoBLL
    {
        private readonly TonGiaoDAL tonGiaoDAL = new TonGiaoDAL();

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = tonGiaoDAL.LayDsTonGiao();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = tonGiaoDAL.LayDsTonGiao();
            comboBox.DisplayMember = "TenTonGiao";
            comboBox.ValueMember = "MaTonGiao";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = tonGiaoDAL.LayDsTonGiao();
            cmbColumn.DisplayMember = "TenTonGiao";
            cmbColumn.ValueMember = "MaTonGiao";
            cmbColumn.DataPropertyName = "MaTonGiao";
            cmbColumn.HeaderText = "Tôn giáo";
        }

        public bool LuuTonGiao()
        {
            return tonGiaoDAL.LuuTonGiao();
        }

        public DataRow ThemDongMoi()
        {
            return tonGiaoDAL.ThemDongMoi();
        }

        public void ThemTonGiao(DataRow row)
        {
            tonGiaoDAL.ThemTonGiao(row);
        }
    }
}