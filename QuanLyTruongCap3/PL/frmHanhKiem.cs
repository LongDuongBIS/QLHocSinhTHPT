using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmHanhKiem : Office2007Form
    {
        private HanhKiemBLL hanhKiemBLL = new HanhKiemBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmHanhKiem()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmHanhKiem_Load(object sender, EventArgs e)
        {
            hanhKiemBLL.HienThi(dGVHanhKiem, bindingNavigatorHanhKiem);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVHanhKiem.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorHanhKiem.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVHanhKiem.RowCount == 0;

            DataRow row = hanhKiemBLL.ThemDongMoi();
            row["MaHanhKiem"] = string.Format("HK{0}", quyDinh.LaySTT(dGVHanhKiem.Rows.Count + 1));
            row["TenHanhKiem"] = string.Empty;
            hanhKiemBLL.ThemHanhKiem(row);
            bindingNavigatorHanhKiem.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVHanhKiem.Rows)
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
            if (KiemTraTruocKhiLuu("colMaHanhKiem") == true && KiemTraTruocKhiLuu("colTenHanhKiem") == true)
            {
                bindingNavigatorPositionItem.Focus();
                hanhKiemBLL.LuuHanhKiem();
            }
        }

        private void dGVHanhKiem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}