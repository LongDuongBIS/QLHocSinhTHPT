using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmMonHoc : Office2007Form
    {
        private MonHocBLL monHocBLL = new MonHocBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmMonHoc()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            monHocBLL.HienThi(dGVMonHoc, bindingNavigatorMonHoc);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVMonHoc.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorMonHoc.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVMonHoc.RowCount == 0;

            DataRow row = monHocBLL.ThemDongMoi();
            row["MaMonHoc"] = string.Format("MH{0}", quyDinh.LaySTT(dGVMonHoc.Rows.Count + 1));
            row["TenMonHoc"] = string.Empty;
            row["SoTiet"] = 0;
            row["HeSo"] = 0;
            monHocBLL.ThemMonHoc(row);
            bindingNavigatorMonHoc.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVMonHoc.Rows)
            {
                if (row.Cells[cellString].Value != null)
                {
                    string str = row.Cells[cellString].Value.ToString();
                    if (str == string.Empty || str == "0")
                    {
                        MessageBoxEx.Show("Giá trị của ô không được rỗng, số tiết và hệ số phải lớn hơn 0!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaMonHoc") == true && KiemTraTruocKhiLuu("colTenMonHoc") == true && KiemTraTruocKhiLuu("colSoTiet") == true && KiemTraTruocKhiLuu("colHeSo") == true)
            {
                bindingNavigatorPositionItem.Focus();
                monHocBLL.LuuMonHoc();
            }
        }

        private void dGVMonHoc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}