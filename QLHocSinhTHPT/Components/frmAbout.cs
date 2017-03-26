using DevComponents.DotNetBar;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmAbout : Office2007Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.lbl08Email.Links.Add(0, 19, "mailto:huulongduong@gmail.com");
            this.lbl10Website.Links.Add(0, 23, "https://www.facebook.com/LongDuongBIS/");
        }

        private void lbl08Email_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strURL = Convert.ToString(e.Link.LinkData);
            if (strURL.StartsWith("mailto:", StringComparison.CurrentCulture))
                Process.Start(string.Format("{0}?subject=Hello", strURL));
        }

        private void lbl10Website_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strURL = Convert.ToString(e.Link.LinkData);
            if (strURL.StartsWith("http://", StringComparison.CurrentCulture))
                Process.Start(strURL);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}