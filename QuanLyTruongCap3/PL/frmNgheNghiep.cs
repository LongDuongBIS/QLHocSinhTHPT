using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmNgheNghiep : Office2007Form
    {
        private NgheNghiepBLL ngheNghiepBLL = new NgheNghiepBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmNgheNghiep()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmNgheNghiep_Load(object sender, EventArgs e)
        {
            ngheNghiepBLL.HienThi(dGVNgheNghiep, bindingNavigatorNgheNghiep);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVNgheNghiep.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorNgheNghiep.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVNgheNghiep.RowCount == 0;

            DataRow row = ngheNghiepBLL.ThemDongMoi();
            row["MaNghe"] = string.Format("NN{0}", QuyDinh.LaySTT(dGVNgheNghiep.Rows.Count + 1));
            row["TenNghe"] = string.Empty;
            ngheNghiepBLL.ThemNgheNghiep(row);
            bindingNavigatorNgheNghiep.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVNgheNghiep.Rows)
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
            if (KiemTraTruocKhiLuu("colMaNghe") == true && KiemTraTruocKhiLuu("colTenNghe") == true)
            {
                bindingNavigatorPositionItem.Focus();
                ngheNghiepBLL.LuuNgheNghiep();
            }
        }

        private void dGVNgheNghiep_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}