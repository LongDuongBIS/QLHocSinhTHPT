using DevComponents.DotNetBar.Controls;
using DevComponents.Editors.DateTimeAdv;
using QuanLyTruongCap3.DAL;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class HocSinhBLL
    {
        private readonly HocSinhDAL hocSinhDAL = new HocSinhDAL();

        public void HienThiComboBox(ComboBoxEx comboBox)
        {
            comboBox.DataSource = hocSinhDAL.LayDsHocSinh();
            comboBox.DisplayMember = "HoTen";
            comboBox.ValueMember = "MaHocSinh";
        }

        public void HienThiComboBox(string namHoc, string lop, ComboBoxEx comboBox)
        {
            comboBox.DataSource = hocSinhDAL.LayDsHocSinhTheoLop(namHoc, lop);
            comboBox.DisplayMember = "HoTen";
            comboBox.ValueMember = "MaHocSinh";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = hocSinhDAL.LayDsHocSinh();
            cmbColumn.DisplayMember = "HoTen";
            cmbColumn.ValueMember = "MaHocSinh";
            cmbColumn.DataPropertyName = "MaHocSinh";
            cmbColumn.HeaderText = "Học sinh";
        }

        public void HienThi(DataGridView dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = hocSinhDAL.LayDsHocSinh();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN, TextBoxX txtMaHocSinh, TextBoxX txtTenHocSinh, TextBoxX txtGioiTinh, CheckBoxX ckbGTinhNam, CheckBoxX ckbGTinhNu, DateTimeInput dtpNgaySinh, TextBoxX txtNoiSinh, ComboBoxEx cmbDanToc, ComboBoxEx cmbTonGiao, TextBoxX txtHoTenCha, ComboBoxEx cmbNgheNghiepCha, TextBoxX txtHoTenMe, ComboBoxEx cmbNgheNghiepMe)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = hocSinhDAL.LayDsHocSinh();

            DataTable dt = hocSinhDAL.LayDsHocSinh();
            bool gioiTinh = Convert.ToBoolean(dt.Rows[0]["GioiTinh"]);

            if (gioiTinh)
                ckbGTinhNu.Checked = true;
            else
                ckbGTinhNam.Checked = true;

            txtMaHocSinh.DataBindings.Clear();
            txtMaHocSinh.DataBindings.Add("Text", bS, "MaHocSinh");

            txtTenHocSinh.DataBindings.Clear();
            txtTenHocSinh.DataBindings.Add("Text", bS, "HoTen");

            txtGioiTinh.DataBindings.Clear();
            txtGioiTinh.DataBindings.Add("Text", bS, "GioiTinh");

            dtpNgaySinh.DataBindings.Clear();
            dtpNgaySinh.DataBindings.Add("Value", bS, "NgaySinh");

            txtNoiSinh.DataBindings.Clear();
            txtNoiSinh.DataBindings.Add("Text", bS, "NoiSinh");

            cmbDanToc.DataBindings.Clear();
            cmbDanToc.DataBindings.Add("SelectedValue", bS, "MaDanToc");

            cmbTonGiao.DataBindings.Clear();
            cmbTonGiao.DataBindings.Add("SelectedValue", bS, "MaTonGiao");

            txtHoTenCha.DataBindings.Clear();
            txtHoTenCha.DataBindings.Add("Text", bS, "HoTenCha");

            cmbNgheNghiepCha.DataBindings.Clear();
            cmbNgheNghiepCha.DataBindings.Add("SelectedValue", bS, "MaNNghiepCha");

            txtHoTenMe.DataBindings.Clear();
            txtHoTenMe.DataBindings.Add("Text", bS, "HoTenMe");

            cmbNgheNghiepMe.DataBindings.Clear();
            cmbNgheNghiepMe.DataBindings.Add("SelectedValue", bS, "MaNNghiepMe");

            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiDsHocSinhTheoLop(DataGridViewX dGV, BindingNavigator bN, string namHoc, string lop)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = hocSinhDAL.LayDsHocSinhTheoLop(namHoc, lop);

            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiDsHocSinhTheoLop(string namHoc, string khoiLop, string lop, ListViewEx lV)
        {
            DataTable dt = hocSinhDAL.LayDsHocSinhTheoLop(namHoc, khoiLop, lop);

            lV.Items.Clear();
            foreach (DataRow Row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Row["MaHocSinh"].ToString();
                item.SubItems.Add(Row["HoTen"].ToString());

                lV.Items.Add(item);
            }
        }

        public DataTable HienThiDsHocSinhTheoNamHoc(string namHoc)
        {
            return hocSinhDAL.LayDsHocSinhTheoNamHoc(namHoc);
        }

        public void XoaHSKhoiBangPhanLop(string namHocCu, string khoiLopCu, string lopCu, ListViewEx hocSinh)
        {
            foreach (ListViewItem item in hocSinh.Items)
            {
                hocSinhDAL.XoaHSKhoiBangPhanLop(namHocCu, khoiLopCu, lopCu, item.SubItems[0].Text);
            }
        }

        public void LuuHSVaoBangPhanLop(string namHocMoi, string khoiLopMoi, string lopMoi, ListViewEx hocSinh)
        {
            foreach (ListViewItem item in hocSinh.Items)
            {
                hocSinhDAL.LuuHSVaoBangPhanLop(namHocMoi, khoiLopMoi, lopMoi, item.SubItems[0].Text);
            }
        }

        public static IList<HocSinhDTO> LayDsHocSinh()
        {
            DataTable dt = new HocSinhDAL().LayDsHocSinhForReport();

            IList<HocSinhDTO> dS = new List<HocSinhDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                HocSinhDTO hocSinhDTO = new HocSinhDTO();

                NgheNghiepDTO ngheNghiepDTO = new NgheNghiepDTO();
                ngheNghiepDTO.MaNghe = Convert.ToString(Row["MaNghe"]);
                ngheNghiepDTO.TenNghe = Convert.ToString(Row["TenNghe"]);

                DanTocDTO danTocDTO = new DanTocDTO();
                danTocDTO.MaDanToc = Convert.ToString(Row["MaDanToc"]);
                danTocDTO.TenDanToc = Convert.ToString(Row["TenDanToc"]);

                TonGiaoDTO tonGiaoDTO = new TonGiaoDTO();
                tonGiaoDTO.MaTonGiao = Convert.ToString(Row["MaTonGiao"]);
                tonGiaoDTO.TenTonGiao = Convert.ToString(Row["TenTonGiao"]);

                hocSinhDTO.MaHocSinh = Convert.ToString(Row["MaHocSinh"]);
                hocSinhDTO.HoTen = Convert.ToString(Row["HoTen"]);
                hocSinhDTO.GioiTinh = Convert.ToBoolean(Row["GioiTinh"]);
                hocSinhDTO.NgaySinh = Convert.ToDateTime(Row["NgaySinh"]);
                hocSinhDTO.NoiSinh = Convert.ToString(Row["NoiSinh"]);
                hocSinhDTO.DanToc = danTocDTO;
                hocSinhDTO.TonGiao = tonGiaoDTO;
                hocSinhDTO.HoTenCha = Convert.ToString(Row["HoTenCha"]);
                hocSinhDTO.NNghiepCha = ngheNghiepDTO;
                hocSinhDTO.HoTenMe = Convert.ToString(Row["HoTenMe"]);
                hocSinhDTO.NNghiepMe = ngheNghiepDTO;

                dS.Add(hocSinhDTO);
            }
            return dS;
        }

        public DataRow ThemDongMoi()
        {
            return hocSinhDAL.ThemDongMoi();
        }

        public void ThemHocSinh(DataRow row)
        {
            hocSinhDAL.ThemHocSinh(row);
        }

        public bool LuuHocSinh()
        {
            return hocSinhDAL.LuuHocSinh();
        }

        public void LuuHocSinh(string maHocSinh, string hoTen, bool gioiTinh, DateTime ngaySinh, string noiSinh, string maDanToc, string maTonGiao, string hoTenCha, string maNgheCha, string hoTenMe, string maNgheMe)
        {
            hocSinhDAL.LuuHocSinh(maHocSinh, hoTen, gioiTinh, ngaySinh, noiSinh, maDanToc, maTonGiao, hoTenCha, maNgheCha, hoTenMe, maNgheMe);
        }

        public void TimKiemHocSinh(TextBoxX txtHoTen, ComboBoxEx cmbTheoNSinh, TextBoxX txtNoiSinh, ComboBoxEx cmbTheoDToc, ComboBoxEx cmbDanToc, ComboBoxEx cmbTheoTGiao, ComboBoxEx cmbTonGiao, DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = hocSinhDAL.TimKiemHocSinh(txtHoTen.Text, cmbTheoNSinh.Text, txtNoiSinh.Text, cmbTheoDToc.Text, cmbDanToc.Text, cmbTheoTGiao.Text, cmbTonGiao.Text);

            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void TimTheoMa(string maHocSinh)
        {
            hocSinhDAL.TimTheoMa(maHocSinh);
        }

        public void TimTheoTen(string tenHocSinh)
        {
            hocSinhDAL.TimTheoTen(tenHocSinh);
        }
    }
}