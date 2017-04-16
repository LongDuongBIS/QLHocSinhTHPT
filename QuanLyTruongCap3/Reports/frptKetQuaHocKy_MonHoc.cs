using DevComponents.DotNetBar;
using Microsoft.Reporting.WinForms;
using QuanLyTruongCap3.BLL;
using QuanLyTruongCap3.Components;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;

namespace QuanLyTruongCap3.Reports
{
    public partial class frptKetQuaHocKy_MonHoc : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();
        private HocKyBLL hocKyBLL = new HocKyBLL();
        private LopBLL lopBLL = new LopBLL();
        private MonHocBLL monHocBLL = new MonHocBLL();

        public frptKetQuaHocKy_MonHoc()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frptKetQuaHocKy_MonHoc_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
            hocKyBLL.HienThiComboBox(cmbHocKy);
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
        }

        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }

        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
            cmbMonHoc.DataBindings.Clear();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            IList<KQHocKyMonHocDTO> ketqua = KQHocKyMonHocBLL.LayDsKQHocKyMonHoc(cmbLop.SelectedValue.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString());

            IList<ReportParameter> param = new List<ReportParameter>();
            QuyDinhDTO truong = QuyDinh.LayThongTinTruong();
            param.Add(new ReportParameter("TenTruong", truong.TenTruong));
            param.Add(new ReportParameter("DiaChiTruong", truong.DiaChiTruong));
            param.Add(new ReportParameter("NgayLap", string.Format("{0}/{1}/{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)));
            param.Add(new ReportParameter("NamHoc", cmbNamHoc.Text));
            param.Add(new ReportParameter("HocKy", cmbHocKy.Text));
            param.Add(new ReportParameter("Lop", cmbLop.Text));
            param.Add(new ReportParameter("MonHoc", cmbMonHoc.Text));
            this.reportViewerKQHKMH.LocalReport.SetParameters(param);

            this.bSKQHKMH.DataSource = ketqua;
            this.reportViewerKQHKMH.RefreshReport();
        }
    }
}