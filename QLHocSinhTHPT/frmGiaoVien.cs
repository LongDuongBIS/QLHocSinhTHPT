using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmGiaoVien : Office2007Form
    {
        private GiaoVienBLL giaoVienBLL = new GiaoVienBLL();
        private MonHocBLL monHocBLL = new MonHocBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmGiaoVien()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            monHocBLL.HienThiComboBox(cmbMonHoc);
            monHocBLL.HienThiDataGridViewComboBoxColumnGiaoVien(colMaMonHoc);

            giaoVienBLL.HienThi(dGVGiaoVien, bindingNavigatorGiaoVien, txtMaGiaoVien, txtTenGiaoVien, txtDiaChi, txtDienThoai, cmbMonHoc);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVGiaoVien.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorGiaoVien.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVGiaoVien.RowCount == 0;

            DataRow row = giaoVienBLL.ThemDongMoi();
            row["MaGiaoVien"] = string.Format("GV{0}", quyDinh.LaySTT(dGVGiaoVien.Rows.Count + 1));
            row["TenGiaoVien"] = string.Empty;
            row["DiaChi"] = string.Empty;
            row["DienThoai"] = string.Empty;
            row["MaMonHoc"] = string.Empty;
            giaoVienBLL.ThemGiaoVien(row);
            bindingNavigatorGiaoVien.BindingSource.MoveLast();
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            giaoVienBLL.HienThi(dGVGiaoVien, bindingNavigatorGiaoVien, txtMaGiaoVien, txtTenGiaoVien, txtDiaChi, txtDienThoai, cmbMonHoc);
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVGiaoVien.Rows)
            {
                if (row.Cells[cellString].Value != null)
                {
                    string str = row.Cells[cellString].Value.ToString();
                    if (str == string.Empty)
                    {
                        MessageBoxEx.Show(string.Format("Thông tin giáo viên {0} không hợp lệ!", row.Cells["colTenGiaoVien"].Value), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaGiaoVien") == true && KiemTraTruocKhiLuu("colTenGiaoVien") == true && KiemTraTruocKhiLuu("colDiaChi") == true && KiemTraTruocKhiLuu("colDienThoai") == true && KiemTraTruocKhiLuu("colMaMonHoc") == true)
            {
                bindingNavigatorPositionItem.Focus();
                giaoVienBLL.LuuGiaoVien();
            }
        }

        private void dGVGiaoVien_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiemGiaoVien();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemGiaoVien();
        }

        private void TimKiemGiaoVien()
        {
            if (chkTimTheoMa.Checked == true)
                giaoVienBLL.TimTheoMa(txtTimKiem.Text);
            else
                giaoVienBLL.TimTheoTen(txtTimKiem.Text);
        }

        private void btnThemMonHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormMonHoc();
            monHocBLL.HienThiDataGridViewComboBoxColumnGiaoVien(colMaMonHoc);
        }

        private void btnLuuVaoDS_Click(object sender, EventArgs e)
        {
            if (txtTenGiaoVien.Text != string.Empty && txtDiaChi.Text != string.Empty && txtDienThoai.Text != string.Empty && cmbMonHoc.SelectedValue != null)
            {
                giaoVienBLL.LuuGiaoVien(txtMaGiaoVien.Text, txtTenGiaoVien.Text, txtDiaChi.Text, txtDienThoai.Text, cmbMonHoc.SelectedValue.ToString());
                giaoVienBLL.HienThi(dGVGiaoVien, bindingNavigatorGiaoVien, txtMaGiaoVien, txtTenGiaoVien, txtDiaChi, txtDienThoai, cmbMonHoc);

                bindingNavigatorGiaoVien.BindingSource.MoveLast();
            }
            else
                MessageBoxEx.Show("Giá trị của các ô không được rỗng!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}