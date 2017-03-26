using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmHocKy : Office2007Form
    {
        private HocKyBLL hocKyBLL = new HocKyBLL();

        public frmHocKy()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmHocKy_Load(object sender, EventArgs e)
        {
            hocKyBLL.HienThi(dGVHocKy, bindingNavigatorHocKy);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVHocKy.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorHocKy.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVHocKy.RowCount == 0;

            DataRow row = hocKyBLL.ThemDongMoi();
            row["MaHocKy"] = string.Empty;
            row["TenHocKy"] = string.Empty;
            row["HeSo"] = 0;
            hocKyBLL.ThemHocKy(row);
            bindingNavigatorHocKy.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVHocKy.Rows)
            {
                if (row.Cells[cellString].Value != null)
                {
                    string str = row.Cells[cellString].Value.ToString();
                    if (str == string.Empty || str == "0")
                    {
                        MessageBoxEx.Show("Giá trị của ô không được rỗng và hệ số phải lớn hơn 0!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaHocKy") == true && KiemTraTruocKhiLuu("colTenHocKy") == true && KiemTraTruocKhiLuu("colHeSo") == true)
            {
                bindingNavigatorPositionItem.Focus();
                hocKyBLL.LuuHocKy();
            }
        }

        private void dGVHocKy_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}