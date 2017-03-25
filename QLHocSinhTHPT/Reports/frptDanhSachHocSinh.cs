using DevComponents.DotNetBar;
using Microsoft.Reporting.WinForms;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Component;
using QLHocSinhTHPT.DTO;
using System;
using System.Collections.Generic;

namespace QLHocSinhTHPT.Reports
{
    public partial class frptDanhSachHocSinh : Office2007Form
    {
        public frptDanhSachHocSinh()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frptDanhSachHocSinh_Load(object sender, EventArgs e)
        {
            IList<ReportParameter> param = new List<ReportParameter>();
            QuyDinhDTO truong = QuyDinh.LayThongTinTruong();
            param.Add(new ReportParameter("TenTruong", truong.TenTruong));
            param.Add(new ReportParameter("DiaChiTruong", truong.DiaChiTruong));
            param.Add(new ReportParameter("NgayLap", string.Format("{0}/{1}/{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)));
            this.reportViewerDSHS.LocalReport.SetParameters(param);

            IList<HocSinhDTO> hocsinh = HocSinhBLL.LayDsHocSinh();
            this.bSDSHocSinh.DataSource = hocsinh;

            this.reportViewerDSHS.RefreshReport();
        }
    }
}