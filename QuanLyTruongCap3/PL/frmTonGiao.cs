using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmTonGiao : Office2007Form
    {
        private TonGiaoBLL tonGiaoBLL = new TonGiaoBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmTonGiao()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmTonGiao_Load(object sender, EventArgs e)
        {
            tonGiaoBLL.HienThi(dGVTonGiao, bindingNavigatorTonGiao);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVTonGiao.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorTonGiao.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled &= dGVTonGiao.RowCount != 0;

            DataRow row = tonGiaoBLL.ThemDongMoi();
            row["MaTonGiao"] = string.Format("TG{0}", quyDinh.LaySTT(dGVTonGiao.Rows.Count + 1));
            row["TenTonGiao"] = string.Empty;
            tonGiaoBLL.ThemTonGiao(row);
            bindingNavigatorTonGiao.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVTonGiao.Rows)
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
            if (KiemTraTruocKhiLuu("colMaTonGiao") == true && KiemTraTruocKhiLuu("colTenTonGiao") == true)
            {
                bindingNavigatorPositionItem.Focus();
                tonGiaoBLL.LuuTonGiao();
            }
        }

        private void dGVTonGiao_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}