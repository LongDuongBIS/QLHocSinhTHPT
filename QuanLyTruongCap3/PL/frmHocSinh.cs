using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmHocSinh : Office2007Form
    {
        private HocSinhBLL hocSinhBLL = new HocSinhBLL();
        private DanTocBLL danTocBLL = new DanTocBLL();
        private TonGiaoBLL tonGiaoBLL = new TonGiaoBLL();
        private NgheNghiepBLL ngheNghiepChaBLL = new NgheNghiepBLL();
        private NgheNghiepBLL ngheNghiepMeBLL = new NgheNghiepBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmHocSinh()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmHocSinh_Load(object sender, EventArgs e)
        {
            danTocBLL.HienThiComboBox(cmbDanToc);
            tonGiaoBLL.HienThiComboBox(cmbTonGiao);
            ngheNghiepChaBLL.HienThiComboBox(cmbNgheNghiepCha);
            ngheNghiepMeBLL.HienThiComboBox(cmbNgheNghiepMe);

            danTocBLL.HienThiDataGridViewComboBoxColumn(colMaDanToc);
            tonGiaoBLL.HienThiDataGridViewComboBoxColumn(colMaTonGiao);
            ngheNghiepChaBLL.HienThiDataGridViewComboBoxColumnNNCha(colMaNNghiepCha);
            ngheNghiepMeBLL.HienThiDataGridViewComboBoxColumnNNMe(colMaNNghiepMe);

            hocSinhBLL.HienThi(dGVHocSinh, bindingNavigatorHocSinh, txtMaHocSinh, txtTenHocSinh, txtGioiTinh, ckbGTinhNam, ckbGTinhNu, dtpNgaySinh, txtNoiSinh, cmbDanToc, cmbTonGiao, txtHoTenCha, cmbNgheNghiepCha, txtHoTenMe, cmbNgheNghiepMe);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dGVHocSinh.RowCount == 0)
                bindingNavigatorDeleteItem.Enabled = false;
            else if (MessageBoxEx.Show("Bạn có chắc chắn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigatorHocSinh.BindingSource.RemoveCurrent();
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bindingNavigatorDeleteItem.Enabled |= dGVHocSinh.RowCount == 0;

            DataRow row = hocSinhBLL.ThemDongMoi();
            row["MaHocSinh"] = string.Format("HS{0}", QuyDinh.LaySTT(dGVHocSinh.Rows.Count + 1));
            row["HoTen"] = string.Empty;
            row["GioiTinh"] = false;
            row["NgaySinh"] = DateTime.Today;
            row["NoiSinh"] = string.Empty;
            row["MaDanToc"] = string.Empty;
            row["MaTonGiao"] = string.Empty;
            row["HoTenCha"] = string.Empty;
            row["MaNNghiepCha"] = string.Empty;
            row["HoTenMe"] = string.Empty;
            row["MaNNghiepMe"] = string.Empty;
            hocSinhBLL.ThemHocSinh(row);
            bindingNavigatorHocSinh.BindingSource.MoveLast();
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            hocSinhBLL.HienThi(dGVHocSinh, bindingNavigatorHocSinh, txtMaHocSinh, txtTenHocSinh, txtGioiTinh, ckbGTinhNam, ckbGTinhNu, dtpNgaySinh, txtNoiSinh, cmbDanToc, cmbTonGiao, txtHoTenCha, cmbNgheNghiepCha, txtHoTenMe, cmbNgheNghiepMe);
        }

        public bool KiemTraTruocKhiLuu(string cellString)
        {
            foreach (DataGridViewRow row in dGVHocSinh.Rows)
            {
                if (row.Cells[cellString].Value != null)
                {
                    string str = row.Cells[cellString].Value.ToString();
                    if (str == string.Empty)
                    {
                        MessageBoxEx.Show(string.Format("Thông tin học sinh {0} không hợp lệ!", row.Cells["colHoTen"].Value), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        public Boolean KiemTraDoTuoiTruocKhiLuu(String doTuoiColumn)
        {
            foreach (DataGridViewRow row in dGVHocSinh.Rows)
            {
                if (row.Cells[doTuoiColumn].Value != null)
                {
                    DateTime ngaySinh = Convert.ToDateTime(row.Cells[doTuoiColumn].Value.ToString());

                    if (QuyDinh.KiemTraDoTuoi(ngaySinh) == false)
                    {
                        MessageBoxEx.Show(string.Format("Tuổi học sinh {0} không đúng quy định!", row.Cells["colHoTen"].Value), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (KiemTraTruocKhiLuu("colMaHocSinh") == true && KiemTraTruocKhiLuu("colHoTen") == true && KiemTraTruocKhiLuu("colNoiSinh") == true && KiemTraTruocKhiLuu("colMaDanToc") == true && KiemTraTruocKhiLuu("colMaTonGiao") == true && KiemTraTruocKhiLuu("colHoTenCha") == true && KiemTraTruocKhiLuu("colMaNNghiepCha") == true && KiemTraTruocKhiLuu("colHoTenMe") == true && KiemTraTruocKhiLuu("colMaNNghiepMe") == true)
            {
                if (KiemTraDoTuoiTruocKhiLuu("colNgaySinh") == true)
                {
                    bindingNavigatorPositionItem.Focus();
                    hocSinhBLL.LuuHocSinh();
                }
            }
        }

        private void dGVHocSinh_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiemHocSinh();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemHocSinh();
        }

        private void TimKiemHocSinh()
        {
            if (chkTimTheoMa.Checked == true)
                hocSinhBLL.TimTheoMa(txtTimKiem.Text);
            else
                hocSinhBLL.TimTheoTen(txtTimKiem.Text);
        }

        private void dGVHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtGioiTinh.Text == "True")
                ckbGTinhNu.Checked = true;
            else
                ckbGTinhNam.Checked = true;
        }

        private void btnThemDanToc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormDanToc();
            danTocBLL.HienThiDataGridViewComboBoxColumn(colMaDanToc);
        }

        private void btnThemTonGiao_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormTonGiao();
            tonGiaoBLL.HienThiDataGridViewComboBoxColumn(colMaTonGiao);
        }

        private void btnThemNNCha_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNgheNghiep();
            ngheNghiepChaBLL.HienThiDataGridViewComboBoxColumnNNCha(colMaNNghiepCha);
        }

        private void btnThemNNMe_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNgheNghiep();
            ngheNghiepMeBLL.HienThiDataGridViewComboBoxColumnNNMe(colMaNNghiepMe);
        }

        private void btnLuuVaoDS_Click(object sender, EventArgs e)
        {
            bool gioiTinh = false || ckbGTinhNu.Checked == true;

            if (txtMaHocSinh.Text != string.Empty && txtTenHocSinh.Text != string.Empty && txtNoiSinh.Text != string.Empty && txtHoTenCha.Text != string.Empty && txtHoTenMe.Text != string.Empty && true && cmbDanToc.SelectedValue != null && cmbTonGiao.SelectedValue != null && cmbNgheNghiepCha.SelectedValue != null && cmbNgheNghiepMe.SelectedValue != null)
            {
                if (QuyDinh.KiemTraDoTuoi(dtpNgaySinh.Value) == true)
                {
                    hocSinhBLL.LuuHocSinh(txtMaHocSinh.Text, txtTenHocSinh.Text, gioiTinh, dtpNgaySinh.Value, txtNoiSinh.Text, cmbDanToc.SelectedValue.ToString(), cmbTonGiao.SelectedValue.ToString(), txtHoTenCha.Text, cmbNgheNghiepCha.SelectedValue.ToString(), txtHoTenMe.Text, cmbNgheNghiepMe.SelectedValue.ToString());
                    hocSinhBLL.HienThi(dGVHocSinh, bindingNavigatorHocSinh, txtMaHocSinh, txtTenHocSinh, txtGioiTinh, ckbGTinhNam, ckbGTinhNu, dtpNgaySinh, txtNoiSinh, cmbDanToc, cmbTonGiao, txtHoTenCha, cmbNgheNghiepCha, txtHoTenMe, cmbNgheNghiepMe);

                    bindingNavigatorHocSinh.BindingSource.MoveLast();
                }
                else
                    MessageBoxEx.Show(string.Format("Tuổi của học sinh {0} không hợp lệ!", txtTenHocSinh.Text), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBoxEx.Show("Giá trị của các ô không được rỗng!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}