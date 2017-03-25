using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using System;
using System.Collections;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmXemDiem : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();
        private HocKyBLL hocKyBLL = new HocKyBLL();
        private LopBLL lopBLL = new LopBLL();
        private HocSinhBLL hocSinhBLL = new HocSinhBLL();
        private MonHocBLL monHocBLL = new MonHocBLL();
        private DiemBLL diemBLL = new DiemBLL();

        public frmXemDiem()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmXemDiem_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
            hocKyBLL.HienThiComboBox(cmbHocKy);
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
            {
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
                hocSinhBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbHocSinh);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("Bạn có muốn xóa dòng này không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IEnumerator ie = lVXemDiem.SelectedItems.GetEnumerator();
                while (ie.MoveNext())
                {
                    ListViewItem item = (ListViewItem)ie.Current;
                    int stt = Convert.ToInt32(item.SubItems[0].Text);
                    diemBLL.XoaDiem(stt);
                    lVXemDiem.Items.Remove(item);
                }
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHienThiDanhSach_Click(object sender, EventArgs e)
        {
            diemBLL.HienThiDanhSachXemDiem(lVXemDiem, cmbHocSinh.SelectedValue.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString());
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
    }
}