using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmPhanLop : Office2007Form
    {
        private NamHocBLL namHocCuBLL = new NamHocBLL();
        private NamHocBLL namHocMoiBLL = new NamHocBLL();
        private KhoiLopBLL khoiLopCuBLL = new KhoiLopBLL();
        private KhoiLopBLL khoiLopMoiBLL = new KhoiLopBLL();
        private LopBLL lopCuBLL = new LopBLL();
        private LopBLL lopMoiBLL = new LopBLL();
        private HocSinhBLL hocSinhBLL = new HocSinhBLL();

        public frmPhanLop()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmPhanLop_Load(object sender, EventArgs e)
        {
            namHocCuBLL.HienThiComboBox(cmbNamHocCu);
            namHocMoiBLL.HienThiComboBox(cmbNamHocMoi);
            khoiLopCuBLL.HienThiComboBox(cmbKhoiLopCu);
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            IEnumerator ie = lVLopCu.SelectedItems.GetEnumerator();
            while (ie.MoveNext())
            {
                ListViewItem olditem = (ListViewItem)ie.Current;
                ListViewItem newitem = new ListViewItem();

                //Trạng thái học sinh đã được chuyển lớp hay chưa?
                bool state = false;

                foreach (ListViewItem item in lVLopMoi.Items)
                {
                    if (item.SubItems[0].Text == olditem.SubItems[0].Text)
                    {
                        MessageBoxEx.Show(string.Format("Học sinh {0} hiện đang học trong lớp {1}", item.SubItems[1].Text, cmbLopMoi.Text), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        state = true;
                        goto Nhan;
                    }
                }

                DataTable dt = new DataTable();
                if (cmbNamHocMoi.SelectedValue != null)
                    dt = hocSinhBLL.HienThiDsHocSinhTheoNamHoc(cmbNamHocMoi.SelectedValue.ToString());

                foreach (DataRow row in dt.Rows)
                {
                    if (olditem.SubItems[0].Text.ToString() == row["MaHocSinh"].ToString())
                    {
                        MessageBoxEx.Show(string.Format("Học sinh {0} hiện đang học trong lớp {1}", row["HoTen"], row["TenLop"]), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        state = true;
                        goto Nhan;
                    }
                }

                newitem.SubItems.Add(olditem.SubItems[1].Text);
                newitem.Tag = olditem.Tag;

                lVLopMoi.Items.Add(newitem);
                lVLopMoi.Items[lVLopMoi.Items.IndexOf(newitem)].Text = olditem.SubItems[0].Text;
                lVLopCu.Items.Remove(olditem);

            Nhan:
                if (state == true)
                    break;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("Bạn có muốn xóa học sinh này khỏi lớp mới không?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IEnumerator ie = lVLopMoi.SelectedItems.GetEnumerator();
                while (ie.MoveNext())
                {
                    ListViewItem item = (ListViewItem)ie.Current;
                    lVLopMoi.Items.Remove(item);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cmbNamHocCu.SelectedValue != null && cmbKhoiLopCu.SelectedValue != null && cmbLopCu.SelectedValue != null && cmbNamHocMoi.SelectedValue != null && cmbKhoiLopMoi.SelectedValue != null && cmbLopMoi.SelectedValue != null)
            {
                hocSinhBLL.XoaHSKhoiBangPhanLop(cmbNamHocCu.SelectedValue.ToString(), cmbKhoiLopCu.SelectedValue.ToString(), cmbLopCu.SelectedValue.ToString(), lVLopMoi);
                hocSinhBLL.LuuHSVaoBangPhanLop(cmbNamHocMoi.SelectedValue.ToString(), cmbKhoiLopMoi.SelectedValue.ToString(), cmbLopMoi.SelectedValue.ToString(), lVLopMoi);

                MessageBoxEx.Show("Đã lưu vào bảng phân lớp!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBoxEx.Show("Giá trị của các ô không được rỗng!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNamHocCu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbKhoiLopCu.DataBindings.Clear();
            cmbLopCu.DataBindings.Clear();
        }

        private void cmbNamHocMoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbKhoiLopMoi.DataBindings.Clear();
            cmbLopMoi.DataBindings.Clear();
        }

        private void cmbKhoiLopCu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHocCu.SelectedValue != null && cmbKhoiLopCu.SelectedValue != null)
            {
                lopCuBLL.HienThiComboBox(cmbKhoiLopCu.SelectedValue.ToString(), cmbNamHocCu.SelectedValue.ToString(), cmbLopCu);
                khoiLopMoiBLL.HienThiComboBox(cmbKhoiLopCu.SelectedValue.ToString(), cmbKhoiLopMoi);

                cmbLopCu.DataBindings.Clear();
                lVLopCu.Items.Clear();
            }
        }

        private void cmbKhoiLopMoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHocMoi.SelectedValue != null && cmbKhoiLopMoi.SelectedValue != null)
            {
                lopMoiBLL.HienThiComboBox(cmbKhoiLopMoi.SelectedValue.ToString(), cmbNamHocMoi.SelectedValue.ToString(), cmbLopMoi);

                cmbLopMoi.DataBindings.Clear();
                lVLopMoi.Items.Clear();
            }
        }

        private void cmbLopCu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLopCu.SelectedValue != null && cmbNamHocCu.SelectedValue != null && cmbKhoiLopCu.SelectedValue != null)
                hocSinhBLL.HienThiDsHocSinhTheoLop(cmbNamHocCu.SelectedValue.ToString(), cmbKhoiLopCu.SelectedValue.ToString(), cmbLopCu.SelectedValue.ToString(), lVLopCu);
        }

        private void cmbLopMoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLopMoi.SelectedValue != null && cmbNamHocMoi.SelectedValue != null && cmbKhoiLopMoi.SelectedValue != null)
                hocSinhBLL.HienThiDsHocSinhTheoLop(cmbNamHocMoi.SelectedValue.ToString(), cmbKhoiLopMoi.SelectedValue.ToString(), cmbLopMoi.SelectedValue.ToString(), lVLopMoi);
        }
    }
}