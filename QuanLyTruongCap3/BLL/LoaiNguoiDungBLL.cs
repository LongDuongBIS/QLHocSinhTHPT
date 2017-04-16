using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class LoaiNguoiDungBLL
    {
        private readonly LoaiNguoiDungDAL loaiNguoiDungDAL = new LoaiNguoiDungDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = loaiNguoiDungDAL.LayDsLoaiNguoiDung();
            comboBox.DisplayMember = "TenLoaiND";
            comboBox.ValueMember = "MaLoai";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = loaiNguoiDungDAL.LayDsLoaiNguoiDung();
            cmbColumn.DisplayMember = "TenLoaiND";
            cmbColumn.ValueMember = "MaLoai";
            cmbColumn.DataPropertyName = "MaLoai";
            cmbColumn.HeaderText = "Loại người dùng";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = loaiNguoiDungDAL.LayDsLoaiNguoiDung();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return loaiNguoiDungDAL.ThemDongMoi();
        }

        public void ThemLoaiNguoiDung(DataRow row)
        {
            loaiNguoiDungDAL.ThemLoaiNguoiDung(row);
        }

        public bool LuuLoaiNguoiDung()
        {
            return loaiNguoiDungDAL.LuuLoaiNguoiDung();
        }
    }
}