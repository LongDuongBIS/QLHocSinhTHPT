using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class MonHocBLL
    {
        private readonly MonHocDAL monHocDAL = new MonHocDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = monHocDAL.LayDsMonHoc();
            comboBox.DisplayMember = "TenMonHoc";
            comboBox.ValueMember = "MaMonHoc";
        }

        public void HienThiComboBox(string namHoc, string lop, ComboBoxEx comboBox)
        {
            comboBox.DataSource = monHocDAL.LayDsMonHoc(namHoc, lop);
            comboBox.DisplayMember = "TenMonHoc";
            comboBox.ValueMember = "MaMonHoc";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = monHocDAL.LayDsMonHoc();
            cmbColumn.DisplayMember = "TenMonHoc";
            cmbColumn.ValueMember = "MaMonHoc";
            cmbColumn.DataPropertyName = "MaMonHoc";
            cmbColumn.HeaderText = "Môn học";
        }

        public void HienThiDataGridViewComboBoxColumnGiaoVien(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = monHocDAL.LayDsMonHoc();
            cmbColumn.DisplayMember = "TenMonHoc";
            cmbColumn.ValueMember = "MaMonHoc";
            cmbColumn.DataPropertyName = "MaMonHoc";
            cmbColumn.HeaderText = "Chuyên môn";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = monHocDAL.LayDsMonHoc();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return monHocDAL.ThemDongMoi();
        }

        public void ThemMonHoc(DataRow row)
        {
            monHocDAL.ThemMonHoc(row);
        }

        public bool LuuMonHoc()
        {
            return monHocDAL.LuuMonHoc();
        }
    }
}