using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmPhanCong : Office2007Form
    {
        private PhanCongBLL phanCongBLL = new PhanCongBLL();
        private NamHocBLL namHocBLL = new NamHocBLL();
        private LopBLL lopBLL = new LopBLL();
        private MonHocBLL monHocBLL = new MonHocBLL();
        private GiaoVienBLL giaoVienBLL = new GiaoVienBLL();

        public frmPhanCong()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmPhanCong_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
            lopBLL.HienThiComboBox(cmbLop);
            monHocBLL.HienThiComboBox(cmbMonHoc);
            giaoVienBLL.HienThiComboBox(cmbGiaoVien);

            namHocBLL.HienThiDataGridViewComboBoxColumn(colMaNamHoc);
            lopBLL.HienThiDataGridViewComboBoxColumn(colMaLop);
            monHocBLL.HienThiDataGridViewComboBoxColumn(colMaMonHoc);
            giaoVienBLL.HienThiDataGridViewComboBoxColumn(colMaGiaoVien);

            phanCongBLL.HienThi(dGVPhanCong, bindingNavigatorPhanCong, txtSTT, cmbNamHoc, cmbLop, cmbMonHoc, cmbGiaoVien);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVPhanCong.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorPhanCong.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVPhanCong.RowCount == 0;

            DataRow row = phanCongBLL.ThemDongMoi();
            row["STT"] = dGVPhanCong.RowCount + 1;
            row["MaNamHoc"] = string.Empty;
            row["MaLop"] = string.Empty;
            row["MaMonHoc"] = string.Empty;
            row["MaGiaoVien"] = string.Empty;
            phanCongBLL.ThemPhanCong(row);
            bindingNavigatorPhanCong.BindingSource.MoveLast();
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            phanCongBLL.HienThi(dGVPhanCong, bindingNavigatorPhanCong, txtSTT, cmbNamHoc, cmbLop, cmbMonHoc, cmbGiaoVien);
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVPhanCong.Rows)
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
            if (KiemTraTruocKhiLuu("colMaNamHoc") == true && KiemTraTruocKhiLuu("colMaLop") == true && KiemTraTruocKhiLuu("colMaMonHoc") == true && KiemTraTruocKhiLuu("colMaGiaoVien") == true)
            {
                bindingNavigatorPositionItem.Focus();
                phanCongBLL.LuuPhanCong();
            }
        }

        private void dGVPhanCong_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TimKiemPhanCong();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == string.Empty)
                MessageBoxEx.Show("Chưa nhập nội dung cần tìm kiếm vào khung!", "LỖI TÌM KIẾM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            TimKiemPhanCong();
        }

        private void TimKiemPhanCong()
        {
            if (chkTimTheoTenLop.Checked == true)
                phanCongBLL.TimTheoTenLop(txtTimKiem.Text);
            else
                phanCongBLL.TimTheoTenGV(txtTimKiem.Text);
        }

        private void btnThemNamHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNamHoc();
            namHocBLL.HienThiDataGridViewComboBoxColumn(colMaNamHoc);
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormLopHoc();
            lopBLL.HienThiDataGridViewComboBoxColumn(colMaLop);
        }

        private void btnThemMonHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormMonHoc();
            monHocBLL.HienThiDataGridViewComboBoxColumn(colMaMonHoc);
        }

        private void btnThemGiaoVien_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormGiaoVien();
            giaoVienBLL.HienThiDataGridViewComboBoxColumn(colMaGiaoVien);
        }

        private void btnLuuVaoDS_Click(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null && cmbMonHoc.SelectedValue != null && cmbGiaoVien.SelectedValue != null)
            {
                phanCongBLL.LuuPhanCong(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbGiaoVien.SelectedValue.ToString());

                phanCongBLL.HienThi(dGVPhanCong, bindingNavigatorPhanCong, txtSTT, cmbNamHoc, cmbLop, cmbMonHoc, cmbGiaoVien);

                bindingNavigatorPhanCong.BindingSource.MoveLast();
            }
            else
                MessageBoxEx.Show("Giá trị của các ô không được rỗng!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }
    }
}