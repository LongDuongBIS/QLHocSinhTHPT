using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using System;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmMain : Office2007RibbonForm
    {
        private NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
        private frmDoiMatKhau frmDoiMatKhau;
        private frmDangNhap frmDangNhap;
        private frmNguoiDung frmNguoiDung;
        private frmConnection frmConnection;

        public frmMain()
        {
            InitializeComponent();
            frmSplash f = new frmSplash();
            f.Show();
            System.Threading.Thread.Sleep(2000);
            f.Close();
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            if (DataService.OpenConnection())
            {
                Default();
                DangNhap();

                this.Cursor = MyCursors.Create(System.IO.Path.Combine(Application.StartupPath, "Pointer.cur"));

                // Create the list of frequently used commands for the QAT Customize menu
                ribbonControl.QatFrequentCommands.Add(btnDangNhap);
                ribbonControl.QatFrequentCommands.Add(btnDangXuat);
                ribbonControl.QatFrequentCommands.Add(btnThoat);

                // Load Quick Access Toolbar layout if one is saved from last session...
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\DevComponents\Ribbon");
                if (key != null)
                {
                    try
                    {
                        string layout = key.GetValue("RibbonPadCSLayout", "").ToString();
                        if (!string.IsNullOrEmpty(layout))
                            ribbonControl.QatLayout = layout;
                    }
                    finally
                    {
                        key.Close();
                    }
                }

                // Pulse the Application Button
                buttonFile.Pulse(11);
            }
            else
            {
                Default();
                ReConnection();
            }
        }

        public void ReConnection()
        {
            MessageBoxEx.Show("Lỗi kết nối đến cơ sở dữ liệu! Xin vui lòng thiết lập lại kết nối...", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (frmConnection == null || frmConnection.IsDisposed)
                frmConnection = new frmConnection();

            if (frmConnection.ShowDialog() == DialogResult.OK)
            {
                MessageBoxEx.Show("Đã thiết lập kết nối cho lần chạy đầu tiên." + "\nHãy khởi động lại chương trình để thực thi kết nối!", "SUCCESSED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                return;
        }

        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save Quick Access Toolbar layout if it has changed...
            if (ribbonControl.QatLayoutChanged)
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\DevComponents\Ribbon");
                try
                {
                    key.SetValue("RibbonPadCSLayout", ribbonControl.QatLayout);
                }
                finally
                {
                    key.Close();
                }
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (frmDangNhap == null || frmDangNhap.IsDisposed)
                frmDangNhap = new frmDangNhap();

            frmDangNhap.txtUsername.Text = string.Empty;
            frmDangNhap.txtPassword.Text = string.Empty;
            frmDangNhap.lblUserError.Text = string.Empty;
            frmDangNhap.lblPassError.Text = string.Empty;

            DangNhap();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            lblTenNguoiDung.Text = "Không có";
            Default();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (frmDoiMatKhau == null || frmDoiMatKhau.IsDisposed)
                frmDoiMatKhau = new frmDoiMatKhau();

            frmDoiMatKhau.txtOldPassword.Text = string.Empty;
            frmDoiMatKhau.txtNewPassword.Text = string.Empty;
            frmDoiMatKhau.txtReNPassword.Text = string.Empty;
            frmDoiMatKhau.lblOldPassError.Text = string.Empty;
            frmDoiMatKhau.lblNewPassError.Text = string.Empty;
            frmDoiMatKhau.lblReNPassError.Text = string.Empty;

            DoiMatKhau();
        }

        private void btnQLNguoiDung_Click(object sender, EventArgs e)
        {
            if (frmNguoiDung == null || frmNguoiDung.IsDisposed)
            {
                frmNguoiDung = new frmNguoiDung();
                frmNguoiDung.MdiParent = this;
                frmNguoiDung.Show();
            }
            else
                frmNguoiDung.Activate();
        }

        private void btnSaoLuu_Click(object sender, EventArgs e)
        {
            if (backupDialog.ShowDialog() == DialogResult.OK)
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(string.Format("BACKUP DATABASE {0} " + "TO DISK = '{1}'", Utilities.DatabaseName, backupDialog.FileName)))
                {
                    DataService ds = new DataService();
                    ds.Load(cmd);
                }

                MessageBoxEx.Show("Sao lưu dữ liệu thành công!", "BACKUP COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                return;
        }

        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            if (restoreDialog.ShowDialog() == DialogResult.OK)
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(string.Format("USE master " + "RESTORE DATABASE {0} " + "FROM DISK = '{1}'", Utilities.DatabaseName, restoreDialog.FileName)))
                {
                    DataService data = new DataService();
                    data.Load(cmd);
                }

                MessageBoxEx.Show("Phục hồi dữ liệu thành công!", "RESTORE COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                return;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormLopHoc();
        }

        private void btnKhoiLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKhoiLop();
        }

        private void btnHocKy_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormHocKy();
        }

        private void btnNamHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNamHoc();
        }

        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormMonHoc();
        }

        private void btnDiem_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNhapDiemChung();
        }

        private void ribbonBarMonHoc_LaunchDialog(object sender, EventArgs e)
        {
            ThamSo.ShowFormNhapDiemRieng();
        }

        private void btnKetQua_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKetQua();
        }

        private void btnHocLuc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormHocLuc();
        }

        private void btnHanhKiem_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormHanhKiem();
        }

        private void btnHocSinh_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormHocSinh();
        }

        private void btnPhanLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormPhanLop();
        }

        private void btnDanToc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormDanToc();
        }

        private void btnTonGiao_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormTonGiao();
        }

        private void btnNgheNghiep_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNgheNghiep();
        }

        private void btnGiaoVien_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormGiaoVien();
        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormPhanCong();
        }

        private void btnKQHKTheoLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKQHKTheoLop();
        }

        private void btnKQHKTheoMon_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKQHKTheoMon();
        }

        private void btnKQCNTheoLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKQCNTheoLop();
        }

        private void btnKQCNTheoMon_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormKQCNTheoMon();
        }

        private void btnDanhSachHocSinh_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormDanhSachHocSinh();
        }

        private void btnDanhSachGiaoVien_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormDanhSachGiaoVien();
        }

        private void btnDanhSachLopHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormDanhSachLopHoc();
        }

        private void btnTimKiemHS_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormTimKiemHS();
        }

        private void btnTimKiemGV_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormTimKiemGV();
        }

        private frmQuyDinh frmQuyDinh = new frmQuyDinh();

        private void btnDoTuoi_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormQuyDinh();
            frmQuyDinh.tabControlPanelDoTuoi.Select();
        }

        private void btnSiSo_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormQuyDinh();
            frmQuyDinh.tabControlPanelSiSo.Select();
        }

        private void btnThangDiem_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormQuyDinh();
            frmQuyDinh.tabControlPanelThangDiem.Select();
        }

        private void btnTruong_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormQuyDinh();
            frmQuyDinh.tabControlPanelTruong.Select();
        }

        private void btnHuongDan_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider.HelpNamespace, HelpNavigator.TableOfContents);
        }

        private void btnThongTin_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormThongTin();
        }

        public void DangNhap()
        {
        Nhan:
            if (frmDangNhap == null || frmDangNhap.IsDisposed)
                frmDangNhap = new frmDangNhap();

            if (frmDangNhap.ShowDialog() == DialogResult.OK)
            {
                if (frmDangNhap.txtUsername.Text == string.Empty)
                {
                    frmDangNhap.lblPassError.Text = string.Empty;
                    frmDangNhap.lblUserError.Text = "Bạn chưa nhập tên!";
                    goto Nhan;
                }

                if (frmDangNhap.txtPassword.Text == string.Empty)
                {
                    frmDangNhap.lblUserError.Text = string.Empty;
                    frmDangNhap.lblPassError.Text = "Bạn chưa nhập mật khẩu!";
                    goto Nhan;
                }

                int ketQua = nguoiDungBLL.DangNhap(frmDangNhap.txtUsername.Text, frmDangNhap.txtPassword.Text);

                switch (ketQua)
                {
                    case 0:
                        frmDangNhap.lblPassError.Text = string.Empty;
                        frmDangNhap.lblUserError.Text = "Người dùng này không tồn tại!";
                        goto Nhan;
                    case 1:
                        frmDangNhap.lblUserError.Text = string.Empty;
                        frmDangNhap.lblPassError.Text = "Mật khẩu không hợp lệ!";
                        goto Nhan;
                    case 2:
                        lblTenNguoiDung.Text = Utilities.NguoiDung.TenND;
                        Permissions(Utilities.NguoiDung.LoaiND.MaLoai);
                        break;
                }
            }
            else
                return;
        }

        public void Permissions(string permission)
        {
            switch (permission)
            {
                case "LND001": IsBGH(); break;
                case "LND002": IsGiaoVien(); break;
                case "LND003": IsGiaoVu(); break;
                default: Default(); break;
            }
        }

        public void DoiMatKhau()
        {
        Nhan:
            if (frmDoiMatKhau.ShowDialog() == DialogResult.OK)
            {
                if (frmDoiMatKhau.txtOldPassword.Text == string.Empty)
                {
                    frmDoiMatKhau.lblOldPassError.Text = "Chưa nhập mật khẩu hiện tại!";
                    frmDoiMatKhau.lblNewPassError.Text = string.Empty;
                    frmDoiMatKhau.lblReNPassError.Text = string.Empty;
                    goto Nhan;
                }

                if (frmDoiMatKhau.txtNewPassword.Text == string.Empty)
                {
                    frmDoiMatKhau.lblOldPassError.Text = string.Empty;
                    frmDoiMatKhau.lblNewPassError.Text = "Chưa nhập mật khẩu mới!";
                    frmDoiMatKhau.lblReNPassError.Text = string.Empty;
                    goto Nhan;
                }

                if (frmDoiMatKhau.txtReNPassword.Text == string.Empty)
                {
                    frmDoiMatKhau.lblOldPassError.Text = string.Empty;
                    frmDoiMatKhau.lblNewPassError.Text = string.Empty;
                    frmDoiMatKhau.lblReNPassError.Text = "Chưa nhập xác nhận mật khẩu!";
                    goto Nhan;
                }

                string username = frmDangNhap.txtUsername.Text;
                string password = frmDangNhap.txtPassword.Text;

                string oldPassword = frmDoiMatKhau.txtOldPassword.Text;
                string newPassword = frmDoiMatKhau.txtNewPassword.Text;
                string reNewPassword = frmDoiMatKhau.txtReNPassword.Text;

                if (password != oldPassword)
                {
                    frmDoiMatKhau.lblOldPassError.Text = "Nhập sai mật khẩu cũ!";
                    frmDoiMatKhau.lblNewPassError.Text = string.Empty;
                    frmDoiMatKhau.lblReNPassError.Text = string.Empty;
                    goto Nhan;
                }
                if (newPassword != reNewPassword)
                {
                    frmDoiMatKhau.lblOldPassError.Text = string.Empty;
                    frmDoiMatKhau.lblNewPassError.Text = string.Empty;
                    frmDoiMatKhau.lblReNPassError.Text = "Nhập xác nhận không khớp!";
                    goto Nhan;
                }
                nguoiDungBLL.ChangePassword(username, newPassword);
                MessageBoxEx.Show("Đổi mật khẩu thành công!", "PASSWORD CHANGED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                return;
        }

        public void Default()
        {
            //True
            btnDangNhap.Enabled = true;
            btnDangNhapContext.Enabled = true;
            btnThoat.Enabled = true;
            btnThoatContext.Enabled = true;
            btnHuongDan.Enabled = true;
            btnThongTin.Enabled = true;

            //False
            btnDangXuat.Enabled = false;
            btnDangXuatContext.Enabled = false;
            btnDoiMatKhau.Enabled = false;
            btnDoiMatKhauContext.Enabled = false;
            btnQLNguoiDung.Enabled = false;
            btnSaoLuu.Enabled = false;
            btnPhucHoi.Enabled = false;

            btnLopHoc.Enabled = false;
            btnKhoiLop.Enabled = false;
            btnHocKy.Enabled = false;
            btnNamHoc.Enabled = false;
            ribbonBarMonHoc.Enabled = false;
            btnMonHoc.Enabled = false;
            btnDiem.Enabled = false;
            btnKetQua.Enabled = false;
            btnHocLuc.Enabled = false;
            btnHanhKiem.Enabled = false;
            btnHocSinh.Enabled = false;
            btnPhanLop.Enabled = false;
            btnTonGiao.Enabled = false;
            btnDanToc.Enabled = false;
            btnNgheNghiep.Enabled = false;
            btnGiaoVien.Enabled = false;
            btnPhanCong.Enabled = false;

            btnKQHKTheoLop.Enabled = false;
            btnKQHKTheoMon.Enabled = false;
            btnKQCNTheoLop.Enabled = false;
            btnKQCNTheoMon.Enabled = false;
            btnDanhSachHocSinh.Enabled = false;
            btnDanhSachGiaoVien.Enabled = false;
            btnDanhSachLopHoc.Enabled = false;

            btnTimKiemHS.Enabled = false;
            btnTimKiemGV.Enabled = false;

            btnSiSo.Enabled = false;
            btnThangDiem.Enabled = false;
            btnDoTuoi.Enabled = false;
            btnTruong.Enabled = false;
        }

        public void IsBGH()
        {
            //False
            btnDangNhap.Enabled = false;
            btnDangNhapContext.Enabled = false;

            //True
            btnDangXuat.Enabled = true;
            btnDangXuatContext.Enabled = true;
            btnDoiMatKhau.Enabled = true;
            btnDoiMatKhauContext.Enabled = true;
            btnQLNguoiDung.Enabled = true;
            btnSaoLuu.Enabled = true;
            btnPhucHoi.Enabled = true;
            btnThoat.Enabled = true;
            btnThoatContext.Enabled = true;

            btnLopHoc.Enabled = true;
            btnKhoiLop.Enabled = true;
            btnHocKy.Enabled = true;
            btnNamHoc.Enabled = true;
            ribbonBarMonHoc.Enabled = true;
            btnMonHoc.Enabled = true;
            btnDiem.Enabled = true;
            btnKetQua.Enabled = true;
            btnHocLuc.Enabled = true;
            btnHanhKiem.Enabled = true;
            btnHocSinh.Enabled = true;
            btnPhanLop.Enabled = true;
            btnTonGiao.Enabled = true;
            btnDanToc.Enabled = true;
            btnNgheNghiep.Enabled = true;
            btnGiaoVien.Enabled = true;
            btnPhanCong.Enabled = true;

            btnKQHKTheoLop.Enabled = true;
            btnKQHKTheoMon.Enabled = true;
            btnKQCNTheoLop.Enabled = true;
            btnKQCNTheoMon.Enabled = true;
            btnDanhSachHocSinh.Enabled = true;
            btnDanhSachGiaoVien.Enabled = true;
            btnDanhSachLopHoc.Enabled = true;

            btnTimKiemHS.Enabled = true;
            btnTimKiemGV.Enabled = true;

            btnSiSo.Enabled = true;
            btnThangDiem.Enabled = true;
            btnDoTuoi.Enabled = true;
            btnTruong.Enabled = true;

            btnHuongDan.Enabled = true;
            btnThongTin.Enabled = true;
        }

        public void IsGiaoVien()
        {
            //True
            btnDangXuat.Enabled = true;
            btnDangXuatContext.Enabled = true;
            btnDoiMatKhau.Enabled = true;
            btnDoiMatKhauContext.Enabled = true;
            btnThoat.Enabled = true;
            btnThoatContext.Enabled = true;

            ribbonBarMonHoc.Enabled = true;
            btnMonHoc.Enabled = true;
            btnDiem.Enabled = true;

            btnKQHKTheoLop.Enabled = true;
            btnKQHKTheoMon.Enabled = true;
            btnKQCNTheoLop.Enabled = true;
            btnKQCNTheoMon.Enabled = true;
            btnDanhSachHocSinh.Enabled = true;
            btnDanhSachGiaoVien.Enabled = true;
            btnDanhSachLopHoc.Enabled = true;

            btnTimKiemHS.Enabled = true;
            btnTimKiemGV.Enabled = true;

            btnHuongDan.Enabled = true;
            btnThongTin.Enabled = true;

            //False
            btnDangNhap.Enabled = false;
            btnDangNhapContext.Enabled = false;
            btnQLNguoiDung.Enabled = false;
            btnSaoLuu.Enabled = false;
            btnPhucHoi.Enabled = false;

            btnLopHoc.Enabled = false;
            btnKhoiLop.Enabled = false;
            btnHocKy.Enabled = false;
            btnNamHoc.Enabled = false;
            btnKetQua.Enabled = false;
            btnHocLuc.Enabled = false;
            btnHanhKiem.Enabled = false;
            btnHocSinh.Enabled = false;
            btnPhanLop.Enabled = false;
            btnTonGiao.Enabled = false;
            btnDanToc.Enabled = false;
            btnNgheNghiep.Enabled = false;
            btnGiaoVien.Enabled = false;
            btnPhanCong.Enabled = false;

            btnSiSo.Enabled = false;
            btnThangDiem.Enabled = false;
            btnDoTuoi.Enabled = false;
            btnTruong.Enabled = false;
        }

        public void IsGiaoVu()
        {
            //True
            btnDangXuat.Enabled = true;
            btnDangXuatContext.Enabled = true;
            btnDoiMatKhau.Enabled = true;
            btnDoiMatKhauContext.Enabled = true;
            btnThoat.Enabled = true;
            btnThoatContext.Enabled = true;

            btnLopHoc.Enabled = true;
            btnKhoiLop.Enabled = true;
            btnHocKy.Enabled = true;
            btnNamHoc.Enabled = true;
            btnKetQua.Enabled = true;
            btnHocLuc.Enabled = true;
            btnHanhKiem.Enabled = true;
            ribbonBarMonHoc.Enabled = true;
            btnMonHoc.Enabled = true;
            btnDiem.Enabled = true;
            btnHocSinh.Enabled = true;
            btnPhanLop.Enabled = true;
            btnTonGiao.Enabled = true;
            btnDanToc.Enabled = true;
            btnNgheNghiep.Enabled = true;

            btnKQHKTheoLop.Enabled = true;
            btnKQHKTheoMon.Enabled = true;
            btnKQCNTheoLop.Enabled = true;
            btnKQCNTheoMon.Enabled = true;
            btnDanhSachHocSinh.Enabled = true;
            btnDanhSachGiaoVien.Enabled = true;
            btnDanhSachLopHoc.Enabled = true;

            btnTimKiemHS.Enabled = true;
            btnTimKiemGV.Enabled = true;

            btnHuongDan.Enabled = true;
            btnThongTin.Enabled = true;

            //False
            btnDangNhap.Enabled = false;
            btnDangNhapContext.Enabled = false;
            btnQLNguoiDung.Enabled = false;
            btnSaoLuu.Enabled = false;
            btnPhucHoi.Enabled = false;

            btnGiaoVien.Enabled = false;
            btnPhanCong.Enabled = false;

            btnSiSo.Enabled = false;
            btnThangDiem.Enabled = false;
            btnDoTuoi.Enabled = false;
            btnTruong.Enabled = false;
        }
    }
}