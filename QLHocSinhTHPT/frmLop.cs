using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Component;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmLop : Office2007Form
    {
        private LopBLL lopBLL = new LopBLL();
        private KhoiLopBLL khoiLopBLL = new KhoiLopBLL();
        private NamHocBLL namHocBLL = new NamHocBLL();
        private GiaoVienBLL giaoVienBLL = new GiaoVienBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmLop()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            khoiLopBLL.HienThiComboBox(cmbKhoiLop);
            namHocBLL.HienThiComboBox(cmbNamHoc);
            giaoVienBLL.HienThiComboBox(cmbGiaoVien);
            khoiLopBLL.HienThiDataGridViewComboBoxColumn(colMaKhoiLop);
            namHocBLL.HienThiDataGridViewComboBoxColumn(colMaNamHoc);
            giaoVienBLL.HienThiDataGridViewComboBoxColumn(colMaGiaoVien);

            lopBLL.HienThi(dGVLop, bindingNavigatorLop, txtMaLop, txtTenLop, cmbKhoiLop, cmbNamHoc, iniSiSo, cmbGiaoVien);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVLop.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorLop.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVLop.RowCount == 0;

            DataRow row = lopBLL.ThemDongMoi();
            row["MaLop"] = string.Empty;
            row["TenLop"] = string.Empty;
            row["MaKhoiLop"] = string.Empty;
            row["MaNamHoc"] = string.Empty;
            row["SiSo"] = 0;
            row["MaGiaoVien"] = string.Empty;
            lopBLL.ThemLop(row);
            bindingNavigatorLop.BindingSource.MoveLast();
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            lopBLL.HienThi(dGVLop, bindingNavigatorLop, txtMaLop, txtTenLop, cmbKhoiLop, cmbNamHoc, iniSiSo, cmbGiaoVien);
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVLop.Rows)
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

        public bool KiemTraSiSoTruocKhiLuu(string siSoColumn)
        {
            foreach (DataGridViewRow row in dGVLop.Rows)
            {
                if (row.Cells[siSoColumn].Value != null)
                {
                    try
                    {
                        int siSo = Convert.ToInt32(row.Cells[siSoColumn].Value.ToString());

                        if (quyDinh.KiemTraSiSo(siSo) == false)
                        {
                            MessageBoxEx.Show("Sỉ số không đúng quy định!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    catch
                    {
                        MessageBoxEx.Show("Sỉ số phải là một số nguyên!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaLop") == true && KiemTraTruocKhiLuu("colTenLop") == true && KiemTraTruocKhiLuu("colMaKhoiLop") == true && KiemTraTruocKhiLuu("colMaNamHoc") == true && KiemTraTruocKhiLuu("colMaGiaoVien") == true && KiemTraSiSoTruocKhiLuu("colSiSo") == true)
            {
                bindingNavigatorPositionItem.Focus();
                lopBLL.LuuLop();
            }
        }

        private void dGVLop_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiemLop();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemLop();
        }

        private void TimKiemLop()
        {
            if (chkTimTheoMa.Checked == true)
                lopBLL.TimTheoMa(txtTimKiem.Text);
            else
                lopBLL.TimTheoTen(txtTimKiem.Text);
        }

        private void btnThemKhoiLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKhoiLop();
            khoiLopBLL.HienThiDataGridViewComboBoxColumn(colMaKhoiLop);
        }

        private void btnThemNamHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNamHoc();
            namHocBLL.HienThiDataGridViewComboBoxColumn(colMaNamHoc);
        }

        private void btnThemGiaoVien_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormGiaoVien();
            giaoVienBLL.HienThiDataGridViewComboBoxColumn(colMaGiaoVien);
        }

        private void btnLuuVaoDS_Click(object sender, EventArgs e)
        {
            if (txtMaLop.Text != string.Empty && txtTenLop.Text != string.Empty && cmbKhoiLop.SelectedValue != null && cmbNamHoc.SelectedValue != null && cmbGiaoVien.SelectedValue != null && quyDinh.KiemTraSiSo(iniSiSo.Value) == true)
            {
                lopBLL.LuuLop(txtMaLop.Text, txtTenLop.Text, cmbKhoiLop.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString(), iniSiSo.Value, cmbGiaoVien.SelectedValue.ToString());
                lopBLL.HienThi(dGVLop, bindingNavigatorLop, txtMaLop, txtTenLop, cmbKhoiLop, cmbNamHoc, iniSiSo, cmbGiaoVien);

                bindingNavigatorLop.BindingSource.MoveLast();
            }
            else
                MessageBoxEx.Show("Giá trị của các ô không được rỗng và sỉ số phải theo quy định!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}