using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmKetQua : Office2007Form
    {
        private KetQuaBLL ketQuaBLL = new KetQuaBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmKetQua()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmKetQua_Load(object sender, EventArgs e)
        {
            ketQuaBLL.HienThi(dGVKetQua, bindingNavigatorKetQua);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVKetQua.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                bindingNavigatorKetQua.BindingSource.RemoveCurrent();
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVKetQua.RowCount == 0;

            DataRow row = ketQuaBLL.ThemDongMoi();
            row["MaKetQua"] = string.Format("KQ{0}", quyDinh.LaySTT(dGVKetQua.Rows.Count + 1));
            row["TenKetQua"] = string.Empty;
            ketQuaBLL.ThemKetQua(row);
            bindingNavigatorKetQua.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVKetQua.Rows)
            {
                if (row.Cells[cellString].Value != null)
                {
                    string str = row.Cells[cellString].Value.ToString();
                    if (str == string.Empty)
                    {
                        MessageBoxEx.Show("Giá trị của ô không được rỗng!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaKetQua") == true && KiemTraTruocKhiLuu("colTenKetQua") == true)
            {
                bindingNavigatorPositionItem.Focus();
                ketQuaBLL.LuuKetQua();
            }
        }

        private void dGVKetQua_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}