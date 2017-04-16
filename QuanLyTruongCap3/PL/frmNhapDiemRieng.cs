using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmNhapDiemRieng : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();
        private HocKyBLL hocKyBLL = new HocKyBLL();
        private LopBLL lopBLL = new LopBLL();
        private HocSinhBLL hocSinhBLL = new HocSinhBLL();
        private MonHocBLL monHocBLL = new MonHocBLL();
        private LoaiDiemBLL loaiDiemBLL = new LoaiDiemBLL();
        private DiemBLL diemBLL = new DiemBLL();
        private QuyDinh quyDinh = new QuyDinh();

        public frmNhapDiemRieng()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmNhapDiemRieng_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
            hocKyBLL.HienThiComboBox(cmbHocKy);
            loaiDiemBLL.HienThiComboBox(cmbLoaiDiem);
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
            {
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
                hocSinhBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbHocSinh);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("Bạn có muốn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IEnumerator ie = lVDiem.SelectedItems.GetEnumerator();
                while (ie.MoveNext())
                {
                    ListViewItem item = (ListViewItem)ie.Current;
                    lVDiem.Items.Remove(item);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int numberOfRow = lVDiem.Items.Count;
            for (int i = 0; i < numberOfRow; i++)
            {
                ListViewItem item = lVDiem.Items[i];
                DiemDTO diem = new DiemDTO();
                diem = (DiemDTO)item.Tag;

                diemBLL.LuuDiem(diem.HocSinh.MaHocSinh, diem.MonHoc.MaMonHoc, diem.HocKy.MaHocKy, diem.NamHoc.MaNamHoc, diem.Lop.MaLop, diem.LoaiDiem.MaLoai, diem.Diem);
            }
            lVDiem.Items.Clear();

            MessageBoxEx.Show("Đã lưu vào bảng điểm!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormXemDiem();
        }

        private void btnLuuVaoDS_Click(object sender, EventArgs e)
        {
            if (QuyDinh.KiemTraDiem(txtDiem.Text) == false || txtDiem.Text == string.Empty)
            {
                MessageBoxEx.Show("Giá trị điểm không hợp lệ!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ListViewItem item = new ListViewItem();

                item.Text = cmbHocSinh.SelectedValue.ToString();
                item.SubItems.Add(cmbHocSinh.Text);
                item.SubItems.Add(cmbHocKy.Text);
                item.SubItems.Add(cmbMonHoc.Text);
                item.SubItems.Add(cmbLoaiDiem.Text);
                item.SubItems.Add(txtDiem.Text);

                DiemDTO diem = new DiemDTO();
                diem.HocSinh.MaHocSinh = cmbHocSinh.SelectedValue.ToString();
                diem.MonHoc.MaMonHoc = cmbMonHoc.SelectedValue.ToString();
                diem.HocKy.MaHocKy = cmbHocKy.SelectedValue.ToString();
                diem.NamHoc.MaNamHoc = cmbNamHoc.SelectedValue.ToString();
                diem.Lop.MaLop = cmbLop.SelectedValue.ToString();
                diem.LoaiDiem.MaLoai = cmbLoaiDiem.SelectedValue.ToString();
                diem.Diem = Convert.ToSingle(txtDiem.Text);

                item.Tag = diem;

                lVDiem.Items.Add(item);
            }
        }

        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }

        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
            {
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
                hocSinhBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbHocSinh);
            }

            cmbMonHoc.DataBindings.Clear();
            cmbHocSinh.DataBindings.Clear();
        }

        private void txtDiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (QuyDinh.KiemTraDiem(txtDiem.Text) == false || txtDiem.Text == string.Empty)
                {
                    MessageBoxEx.Show("Giá trị điểm không hợp lệ!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = cmbHocSinh.SelectedValue.ToString();
                    item.SubItems.Add(cmbHocSinh.Text);
                    item.SubItems.Add(cmbHocKy.Text);
                    item.SubItems.Add(cmbMonHoc.Text);
                    item.SubItems.Add(cmbLoaiDiem.Text);
                    item.SubItems.Add(txtDiem.Text);

                    DiemDTO diem = new DiemDTO();
                    diem.HocSinh.MaHocSinh = cmbHocSinh.SelectedValue.ToString();
                    diem.MonHoc.MaMonHoc = cmbMonHoc.SelectedValue.ToString();
                    diem.HocKy.MaHocKy = cmbHocKy.SelectedValue.ToString();
                    diem.NamHoc.MaNamHoc = cmbNamHoc.SelectedValue.ToString();
                    diem.Lop.MaLop = cmbLop.SelectedValue.ToString();
                    diem.LoaiDiem.MaLoai = cmbLoaiDiem.SelectedValue.ToString();
                    diem.Diem = Convert.ToSingle(txtDiem.Text);

                    item.Tag = diem;

                    lVDiem.Items.Add(item);
                }
            }
        }

        private void txtDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}