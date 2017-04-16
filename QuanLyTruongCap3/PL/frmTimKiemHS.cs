using DevComponents.DotNetBar;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using System;
using System.Windows.Forms;

namespace QuanLyTruongCap3
{
    public partial class frmTimKiemHS : Office2007Form
    {
        private DanTocBLL danTocBLL = new DanTocBLL();
        private TonGiaoBLL tonGiaoBLL = new TonGiaoBLL();
        private HocSinhBLL hocSinhBLL = new HocSinhBLL();

        public frmTimKiemHS()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmTimKiemHS_Load(object sender, EventArgs e)
        {
            danTocBLL.HienThiComboBox(cmbDanToc);
            tonGiaoBLL.HienThiComboBox(cmbTonGiao);
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            hocSinhBLL.TimKiemHocSinh(txtHoTen, cmbTheoNSinh, txtNoiSinh, cmbTheoDToc, cmbDanToc, cmbTheoTGiao, cmbTonGiao, dGVKetQuaTimKiem, bindingNavigatorKetQuaTimKiem);

            if (dGVKetQuaTimKiem.RowCount == 0)
                MessageBoxEx.Show("Không có học sinh cần tìm trong hệ thống!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}