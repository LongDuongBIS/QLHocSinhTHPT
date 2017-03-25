using DevComponents.DotNetBar.Controls;
using QLHocSinhTHPT.Component;
using QLHocSinhTHPT.DAL;
using QLHocSinhTHPT.DTO;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT.BLL
{
    public class NguoiDungBLL
    {
        private readonly NguoiDungDAL nguoiDungDAL = new NguoiDungDAL();
        private NguoiDungDTO nguoiDungDTO = new NguoiDungDTO();
        private LoaiNguoiDungDTO loaiNguoiDungDTO = new LoaiNguoiDungDTO();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = nguoiDungDAL.LayDsNguoiDung();
            comboBox.DisplayMember = "TenND";
            comboBox.ValueMember = "MaND";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = nguoiDungDAL.LayDsNguoiDung();
            cmbColumn.DisplayMember = "TenND";
            cmbColumn.ValueMember = "MaND";
            cmbColumn.DataPropertyName = "MaND";
            cmbColumn.HeaderText = "Người dùng";
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = nguoiDungDAL.LayDsNguoiDung();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public DataRow ThemDongMoi()
        {
            return nguoiDungDAL.ThemDongMoi();
        }

        public void ThemNguoiDung(DataRow row)
        {
            nguoiDungDAL.ThemNguoiDung(row);
        }

        public bool LuuNguoiDung()
        {
            return nguoiDungDAL.LuuNguoiDung();
        }

        public int DangNhap(string username, string password)
        {
            DataTable dt = nguoiDungDAL.LayDsNguoiDung(username);

            if (dt.Rows.Count == 0)
                return 0;

            string systemPassword = dt.Rows[0]["MatKhau"].ToString();

            if (systemPassword != password)
                return 1;
            nguoiDungDTO.TenND = dt.Rows[0]["TenND"].ToString();
            loaiNguoiDungDTO.MaLoai = dt.Rows[0]["MaLoai"].ToString();

            nguoiDungDTO.LoaiND = loaiNguoiDungDTO;

            Utilities.NguoiDung = nguoiDungDTO;
            return 2;
        }

        public void ChangePassword(string userName, string newPassword)
        {
            nguoiDungDAL.ChangePassword(userName, newPassword);
        }
    }
}