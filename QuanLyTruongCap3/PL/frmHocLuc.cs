using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmHocLuc : Office2007Form
    {
        private HocLucBLL hocLucBLL = new HocLucBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmHocLuc()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmHocLuc_Load(object sender, EventArgs e)
        {
            hocLucBLL.HienThi(dGVHocLuc, bindingNavigatorHocLuc);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVHocLuc.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorHocLuc.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVHocLuc.RowCount == 0;

            DataRow row = hocLucBLL.ThemDongMoi();
            row["MaHocLuc"] = string.Format("HL{0}", QuyDinh.LaySTT(dGVHocLuc.Rows.Count + 1));
            row["TenHocLuc"] = string.Empty;
            row["DiemCanTren"] = 0;
            row["DiemCanDuoi"] = 0;
            row["DiemKhongChe"] = 0;
            hocLucBLL.ThemHocLuc(row);
            bindingNavigatorHocLuc.BindingSource.MoveLast();
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVHocLuc.Rows)
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

        public bool KiemTraDiemTruocKhiLuu(string loaiDiem)
        {
            foreach (DataGridViewRow row in dGVHocLuc.Rows)
            {
                if (row.Cells[loaiDiem].Value != null)
                {
                    string diem = row.Cells[loaiDiem].Value.ToString();
                    if (diem == string.Empty || QuyDinh.KiemTraDiem(diem) == false)
                    {
                        MessageBoxEx.Show("Giá trị điểm không hợp lệ!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaHocLuc") == true && KiemTraTruocKhiLuu("colTenHocLuc") == true && KiemTraDiemTruocKhiLuu("colDiemCanTren") == true && KiemTraDiemTruocKhiLuu("colDiemCanDuoi") == true && KiemTraDiemTruocKhiLuu("colDiemKhongChe") == true)
            {
                bindingNavigatorPositionItem.Focus();
                hocLucBLL.LuuHocLuc();
            }
        }

        private void dGVHocLuc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}