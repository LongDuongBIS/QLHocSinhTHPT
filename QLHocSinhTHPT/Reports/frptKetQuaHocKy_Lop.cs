using DevComponents.DotNetBar;
using Microsoft.Reporting.WinForms;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using QLHocSinhTHPT.DTO;
using System;
using System.Collections.Generic;

namespace QLHocSinhTHPT.Reports
{
    public partial class frptKetQuaHocKy_Lop : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();
        private HocKyBLL hocKyBLL = new HocKyBLL();
        private LopBLL lopBLL = new LopBLL();

        public frptKetQuaHocKy_Lop()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frptKetQuaHocKy_Lop_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
            hocKyBLL.HienThiComboBox(cmbHocKy);
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
        }

        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            IList<KQHocKyTongHopDTO> ketqua = KQHocKyTongHopBLL.LayDsKQHocKyTongHop(cmbLop.SelectedValue.ToString(),
                                                                                     cmbHocKy.SelectedValue.ToString(),
                                                                                     cmbNamHoc.SelectedValue.ToString());

            IList<ReportParameter> param = new List<ReportParameter>();
            QuyDinhDTO truong = QuyDinh.LayThongTinTruong();
            param.Add(new ReportParameter("TenTruong", truong.TenTruong));
            param.Add(new ReportParameter("DiaChiTruong", truong.DiaChiTruong));
            param.Add(new ReportParameter("NgayLap", string.Format("{0}/{1}/{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)));
            param.Add(new ReportParameter("NamHoc", cmbNamHoc.Text));
            param.Add(new ReportParameter("HocKy", cmbHocKy.Text));
            param.Add(new ReportParameter("Lop", cmbLop.Text));
            this.reportViewerKQHKTH.LocalReport.SetParameters(param);

            this.bSKQHKTH.DataSource = ketqua;
            this.reportViewerKQHKTH.RefreshReport();
        }
    }
}