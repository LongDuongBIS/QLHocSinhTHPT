using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmLoaiDiem : Office2007Form
    {
        private LoaiDiemBLL loaiDiemBLL = new LoaiDiemBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmLoaiDiem()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmLoaiDiem_Load(object sender, EventArgs e)
        {
            loaiDiemBLL.HienThi(dGVLoaiDiem, bindingNavigatorLoaiDiem);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVLoaiDiem.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorLoaiDiem.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVLoaiDiem.RowCount == 0;

            DataRow row = loaiDiemBLL.ThemDongMoi();
            row["MaLoai"] = string.Format("LD{0}", QuyDinh.LaySTT(dGVLoaiDiem.Rows.Count + 1));
            row["TenLoai"] = string.Empty;
            row["HeSo"] = 0;
            loaiDiemBLL.ThemLoaiDiem(row);
            bindingNavigatorLoaiDiem.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVLoaiDiem.Rows)
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
            if (KiemTraTruocKhiLuu("colMaLoai") == true && KiemTraTruocKhiLuu("colTenLoai") == true && KiemTraTruocKhiLuu("colHeSo") == true)
            {
                bindingNavigatorPositionItem.Focus();
                loaiDiemBLL.LuuLoaiDiem();
            }
        }

        private void dGVLoaiDiem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}