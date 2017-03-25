using DevComponents.DotNetBar;
using System;
using System.Windows.Forms;

namespace QLHocSinhTHPT.Component
{
    public partial class frmDoiMatKhau : Office2007Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            txtNewPassword.Focus();
            this.DialogResult = DialogResult.OK;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtReNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}