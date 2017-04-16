using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class DanTocBLL
    {
        private readonly DanTocDAL danTocDAL = new DanTocDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = danTocDAL.LayDsDanToc();
            comboBox.DisplayMember = "TenDanToc";
            comboBox.ValueMember = "MaDanToc";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = danTocDAL.LayDsDanToc();
            cmbColumn.DisplayMember = "TenDanToc";
            cmbColumn.ValueMember = "MaDanToc";
            cmbColumn.DataPropertyName = "MaDanToc";
            cmbColumn.HeaderText = "Dân tộc";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = danTocDAL.LayDsDanToc();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return danTocDAL.ThemDongMoi();
        }

        public void ThemDanToc(DataRow row)
        {
            danTocDAL.ThemDanToc(row);
        }

        public bool LuuDanToc()
        {
            return danTocDAL.LuuDanToc();
        }
    }
}