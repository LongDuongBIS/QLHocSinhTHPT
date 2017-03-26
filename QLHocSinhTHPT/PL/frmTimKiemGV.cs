using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using System;
using System.Windows.Forms;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT
{
    public partial class frmTimKiemGV : Office2007Form
    {
        private MonHocBLL monHocBLL = new MonHocBLL();
        private GiaoVienBLL giaoVienBLL = new GiaoVienBLL();

        public frmTimKiemGV()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmTimKiemGV_Load(object sender, EventArgs e)
        {
            monHocBLL.HienThiComboBox(cmbCMon);
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            giaoVienBLL.TimKiemGiaoVien(txtHoTen, cmbTheoDChi, txtDiaChi, cmbTheoCMon, cmbCMon, dGVKetQuaTimKiem, bindingNavigatorKetQuaTimKiem);

            if (dGVKetQuaTimKiem.RowCount == 0)
                MessageBoxEx.Show("Không có giáo viên cần tìm trong hệ thống!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}