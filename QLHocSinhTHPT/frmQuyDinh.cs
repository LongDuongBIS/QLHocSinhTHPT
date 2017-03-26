using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using System;
using System.Windows.Forms;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT
{
    public partial class frmQuyDinh : Office2007Form
    {
        private QuyDinhBLL quyDinhBLL = new QuyDinhBLL();

        public frmQuyDinh()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmQuyDinh_Load(object sender, EventArgs e)
        {
            quyDinhBLL.HienThi(txtSiSoCanDuoi, txtSiSoCanTren, txtDoTuoiCanDuoi, txtDoTuoiCanTren, ckbThang10, ckbThang100, txtTenTruong, txtDiaChiTruong);
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (tabControlPanelSiSo.CanSelect)
                if (txtSiSoCanDuoi.Value <= 10 || txtSiSoCanTren.Value >= 60)
                    MessageBoxEx.Show("Sỉ số phải nằm trong khoảng giới hạn 10 - 60!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    quyDinhBLL.CapNhatQuyDinhSiSo(txtSiSoCanDuoi.Value, txtSiSoCanTren.Value);
                    MessageBoxEx.Show("Cập nhật thành công quy định về sỉ số!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    quyDinhBLL.HienThi(txtSiSoCanDuoi, txtSiSoCanTren, txtDoTuoiCanDuoi, txtDoTuoiCanTren, ckbThang10, ckbThang100, txtTenTruong, txtDiaChiTruong);
                }
            else if (tabControlPanelDoTuoi.CanSelect)
                if (txtDoTuoiCanDuoi.Value <= 10 || txtDoTuoiCanTren.Value >= 30)
                    MessageBoxEx.Show("Độ tuổi phải nằm trong khoảng giới hạn 10 - 30!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    quyDinhBLL.CapNhatQuyDinhDoTuoi(txtDoTuoiCanDuoi.Value, txtDoTuoiCanTren.Value);
                    MessageBoxEx.Show("Cập nhật thành công quy định về độ tuổi!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    quyDinhBLL.HienThi(txtSiSoCanDuoi, txtSiSoCanTren, txtDoTuoiCanDuoi, txtDoTuoiCanTren, ckbThang10, ckbThang100, txtTenTruong, txtDiaChiTruong);
                }
            else if (tabControlPanelTruong.CanSelect)
                if (txtTenTruong.Text == string.Empty)
                    MessageBoxEx.Show("Tên trường học là giá trị bắt buộc phải nhập!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtDiaChiTruong.Text == string.Empty)
                    MessageBoxEx.Show("Địa chỉ trường là giá trị bắt buộc phải nhập!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    quyDinhBLL.CapNhatQuyDinhTruong(txtTenTruong.Text, txtDiaChiTruong.Text);
                    MessageBoxEx.Show("Cập nhật thành công thông tin trường học!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    quyDinhBLL.HienThi(txtSiSoCanDuoi, txtSiSoCanTren, txtDoTuoiCanDuoi, txtDoTuoiCanTren, ckbThang10, ckbThang100, txtTenTruong, txtDiaChiTruong);
                }
            else if (tabControlPanelThangDiem.CanSelect)
                if (ckbThang10.Checked == true)
                {
                    quyDinhBLL.CapNhatQuyDinhThangDiem(10);
                    MessageBoxEx.Show("Cập nhật thành công quy định về thang điểm!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    quyDinhBLL.HienThi(txtSiSoCanDuoi, txtSiSoCanTren, txtDoTuoiCanDuoi, txtDoTuoiCanTren, ckbThang10, ckbThang100, txtTenTruong, txtDiaChiTruong);
                }
                else
                {
                    quyDinhBLL.CapNhatQuyDinhThangDiem(100);
                    MessageBoxEx.Show("Cập nhật thành công quy định về thang điểm!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    quyDinhBLL.HienThi(txtSiSoCanDuoi, txtSiSoCanTren, txtDoTuoiCanDuoi, txtDoTuoiCanTren, ckbThang10, ckbThang100, txtTenTruong, txtDiaChiTruong);
                }
        }
    }
}