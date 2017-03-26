using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmKhoiLop : Office2007Form
    {
        private KhoiLopBLL khoiLopBLL = new KhoiLopBLL();

        public frmKhoiLop()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmKhoiLop_Load(object sender, EventArgs e)
        {
            khoiLopBLL.HienThi(dGVKhoiLop, bindingNavigatorKhoiLop);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVKhoiLop.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorKhoiLop.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVKhoiLop.RowCount == 0;

            DataRow row = khoiLopBLL.ThemDongMoi();
            row["MaKhoiLop"] = string.Empty;
            row["TenKhoiLop"] = string.Empty;
            khoiLopBLL.ThemKhoiLop(row);
            bindingNavigatorKhoiLop.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVKhoiLop.Rows)
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
            if (KiemTraTruocKhiLuu("colMaKhoiLop") == true && KiemTraTruocKhiLuu("colTenkhoiLop") == true)
            {
                bindingNavigatorPositionItem.Focus();
                khoiLopBLL.LuuKhoiLop();
            }
        }

        private void dGVKhoiLop_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}