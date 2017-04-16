using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmNguoiDung : Office2007Form
    {
        private NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
        private LoaiNguoiDungBLL loaiNguoiDungBLL = new LoaiNguoiDungBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmNguoiDung()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            loaiNguoiDungBLL.HienThiDataGridViewComboBoxColumn(colMaLoai);
            nguoiDungBLL.HienThi(dGVNguoiDung, bindingNavigatorNguoiDung);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVNguoiDung.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorNguoiDung.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVNguoiDung.RowCount == 0;

            DataRow row = nguoiDungBLL.ThemDongMoi();
            row["MaND"] = string.Format("ND{0}", quyDinh.LaySTT(dGVNguoiDung.Rows.Count + 1));
            row["MaLoai"] = string.Empty;
            row["TenND"] = string.Empty;
            row["TenDNhap"] = string.Empty;
            row["MatKhau"] = string.Empty;
            nguoiDungBLL.ThemNguoiDung(row);
            bindingNavigatorNguoiDung.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVNguoiDung.Rows)
            {
                if (row.Cells[cellString].Value != null)
                {
                    string str = row.Cells[cellString].Value.ToString();
                    if (str == string.Empty)
                    {
                        MessageBoxEx.Show("Thông tin người dùng không hợp lệ!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaND") == true && KiemTraTruocKhiLuu("colMaLoai") == true && KiemTraTruocKhiLuu("colTenND") == true && KiemTraTruocKhiLuu("colTenDNhap") == true && KiemTraTruocKhiLuu("colMatKhau") == true)
            {
                bindingNavigatorPositionItem.Focus();
                nguoiDungBLL.LuuNguoiDung();
            }
        }

        private void dGVNguoiDung_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnThemLoaiND_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormLoaiNguoiDung();
            loaiNguoiDungBLL.HienThiDataGridViewComboBoxColumn(colMaLoai);
        }
    }
}