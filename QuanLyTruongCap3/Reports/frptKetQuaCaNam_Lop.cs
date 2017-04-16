using DevComponents.DotNetBar;
using Microsoft.Reporting.WinForms;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;

namespace QuanLyTruongCap3.Reports
{
    public partial class frptKetQuaCaNam_Lop : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();
        private LopBLL lopBLL = new LopBLL();

        public frptKetQuaCaNam_Lop()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frptKetQuaCaNam_Lop_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
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
            IList<KQCaNamTongHopDTO> ketqua = BLL.KQCaNamTongHopBLL.LayDsKQCaNamTongHop(cmbLop.SelectedValue.ToString(),
                                                                                      cmbNamHoc.SelectedValue.ToString());

            IList<ReportParameter> param = new List<ReportParameter>();
            QuyDinhDTO truong = QuyDinh.LayThongTinTruong();
            param.Add(new ReportParameter("TenTruong", truong.TenTruong));
            param.Add(new ReportParameter("DiaChiTruong", truong.DiaChiTruong));
            param.Add(new ReportParameter("NgayLap", string.Format("{0}/{1}/{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)));
            param.Add(new ReportParameter("NamHoc", cmbNamHoc.Text));
            param.Add(new ReportParameter("Lop", cmbLop.Text));
            this.reportViewerKQCNTH.LocalReport.SetParameters(param);

            this.bSKQCNTH.DataSource = ketqua;
            this.reportViewerKQCNTH.RefreshReport();
        }
    }
}