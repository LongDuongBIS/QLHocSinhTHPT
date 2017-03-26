using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT
{
    public partial class frmNamHoc : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();

        public frmNamHoc()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmNamHoc_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThi(dGVNamHoc, bindingNavigatorNamHoc);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVNamHoc.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorNamHoc.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVNamHoc.RowCount == 0;

            DataRow row = namHocBLL.ThemDongMoi();
            row["MaNamHoc"] = string.Empty;
            row["TenNamHoc"] = string.Empty;
            namHocBLL.ThemNamHoc(row);
            bindingNavigatorNamHoc.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVNamHoc.Rows)
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
            if (KiemTraTruocKhiLuu("colMaNamHoc") == true && KiemTraTruocKhiLuu("colTenNamHoc") == true)
            {
                bindingNavigatorPositionItem.Focus();
                namHocBLL.LuuNamHoc();
            }
        }

        private void dGVNamHoc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}