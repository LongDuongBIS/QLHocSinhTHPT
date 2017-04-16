using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class KetQuaBLL
    {
        private readonly KetQuaDAL ketQuaDAL = new KetQuaDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = ketQuaDAL.LayDsKetQua();
            comboBox.DisplayMember = "TenKetQua";
            comboBox.ValueMember = "MaKetQua";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = ketQuaDAL.LayDsKetQua();
            cmbColumn.DisplayMember = "TenKetQua";
            cmbColumn.ValueMember = "MaKetQua";
            cmbColumn.DataPropertyName = "MaKetQua";
            cmbColumn.HeaderText = "Kết quả";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = ketQuaDAL.LayDsKetQua();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return ketQuaDAL.ThemDongMoi();
        }

        public void ThemKetQua(DataRow row)
        {
            ketQuaDAL.ThemKetQua(row);
        }

        public bool LuuKetQua()
        {
            return ketQuaDAL.LuuKetQua();
        }
    }
}