using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class NgheNghiepBLL
    {
        private readonly NgheNghiepDAL ngheNghiepDAL = new NgheNghiepDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = ngheNghiepDAL.LayDsNgheNghiep();
            comboBox.DisplayMember = "TenNghe";
            comboBox.ValueMember = "MaNghe";
        }

        public void HienThiDataGridViewComboBoxColumnNNCha(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = ngheNghiepDAL.LayDsNgheNghiep();
            cmbColumn.DisplayMember = "TenNghe";
            cmbColumn.ValueMember = "MaNghe";
            cmbColumn.DataPropertyName = "MaNNghiepCha";
            cmbColumn.HeaderText = "Nghề nghiệp cha";
        }

        public void HienThiDataGridViewComboBoxColumnNNMe(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = ngheNghiepDAL.LayDsNgheNghiep();
            cmbColumn.DisplayMember = "TenNghe";
            cmbColumn.ValueMember = "MaNghe";
            cmbColumn.DataPropertyName = "MaNNghiepMe";
            cmbColumn.HeaderText = "Nghề nghiệp mẹ";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = ngheNghiepDAL.LayDsNgheNghiep();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return ngheNghiepDAL.ThemDongMoi();
        }

        public void ThemNgheNghiep(DataRow row)
        {
            ngheNghiepDAL.ThemNgheNghiep(row);
        }

        public bool LuuNgheNghiep()
        {
            return ngheNghiepDAL.LuuNgheNghiep();
        }
    }
}