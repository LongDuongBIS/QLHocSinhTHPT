using DevComponents.DotNetBar.Controls;
using QuanLyTruongCap3.DAL;
using QuanLyTruongCap3.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLyTruongCap3.BLL
{
    public class GiaoVienBLL
    {
        private readonly GiaoVienDAL giaoVienDAL = new GiaoVienDAL();

        public static IList<GiaoVienDTO> LayDsGiaoVien()
        {
            DataTable dt = new GiaoVienDAL().LayDsGiaoVienForReport();

            IList<GiaoVienDTO> ds = new List<GiaoVienDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                GiaoVienDTO giaoVienDTO = new GiaoVienDTO();

                MonHocDTO monHocDTO = new MonHocDTO();
                monHocDTO.MaMonHoc = Convert.ToString(Row["MaMonHoc"]);
                monHocDTO.TenMonHoc = Convert.ToString(Row["TenMonHoc"]);
                monHocDTO.SoTiet = Convert.ToInt32(Row["SoTiet"]);
                monHocDTO.HeSo = Convert.ToInt32(Row["HeSo"]);

                giaoVienDTO.MaGiaoVien = Convert.ToString(Row["MaGiaoVien"]);
                giaoVienDTO.TenGiaoVien = Convert.ToString(Row["TenGiaoVien"]);
                giaoVienDTO.DiaChi = Convert.ToString(Row["DiaChi"]);
                giaoVienDTO.DienThoai = Convert.ToString(Row["DienThoai"]);
                giaoVienDTO.MonHoc = monHocDTO;

                ds.Add(giaoVienDTO);
            }
            return ds;
        }

        public void HienThi(DataGridView dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = giaoVienDAL.LayDsGiaoVien();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN, TextBoxX txtMaGiaoVien, TextBoxX txtTenGiaoVien, TextBoxX txtDiaChi, TextBoxX txtDienThoai, ComboBoxEx cmbMonHoc)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = giaoVienDAL.LayDsGiaoVien();

            txtMaGiaoVien.DataBindings.Clear();
            txtMaGiaoVien.DataBindings.Add("Text", bS, "MaGiaoVien");

            txtTenGiaoVien.DataBindings.Clear();
            txtTenGiaoVien.DataBindings.Add("Text", bS, "TenGiaoVien");

            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", bS, "DiaChi");

            txtDienThoai.DataBindings.Clear();
            txtDienThoai.DataBindings.Add("Text", bS, "DienThoai");

            cmbMonHoc.DataBindings.Clear();
            cmbMonHoc.DataBindings.Add("SelectedValue", bS, "MaMonHoc");

            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThiComboBox(ComboBox comboBox)
        {
            comboBox.DataSource = giaoVienDAL.LayDsGiaoVien();
            comboBox.DisplayMember = "TenGiaoVien";
            comboBox.ValueMember = "MaGiaoVien";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = giaoVienDAL.LayDsGiaoVien();
            cmbColumn.DisplayMember = "TenGiaoVien";
            cmbColumn.ValueMember = "MaGiaoVien";
            cmbColumn.DataPropertyName = "MaGiaoVien";
            cmbColumn.HeaderText = "Giáo viên";
        }

        public bool LuuGiaoVien()
        {
            return giaoVienDAL.LuuGiaoVien();
        }

        public void LuuGiaoVien(string maGiaoVien, string tenGiaoVien, string diaChi, string dienThoai, string chuyenMon)
        {
            giaoVienDAL.LuuGiaoVien(maGiaoVien, tenGiaoVien, diaChi, dienThoai, chuyenMon);
        }

        public DataRow ThemDongMoi()
        {
            return giaoVienDAL.ThemDongMoi();
        }

        public void ThemGiaoVien(DataRow row)
        {
            giaoVienDAL.ThemGiaoVien(row);
        }

        public void TimKiemGiaoVien(TextBoxX txtHoTen, ComboBoxEx cmbTheoDChi, TextBoxX txtDiaChi, ComboBoxEx cmbTheoCMon, ComboBoxEx cmbCMon, DataGridViewX dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = giaoVienDAL.TimKiemGiaoVien(txtHoTen.Text, cmbTheoDChi.Text, txtDiaChi.Text, cmbTheoCMon.Text, cmbCMon.Text);

            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void TimTheoMa(string maGiaoVien)
        {
            giaoVienDAL.TimTheoMa(maGiaoVien);
        }

        public void TimTheoTen(string tenGiaoVien)
        {
            giaoVienDAL.TimTheoTen(tenGiaoVien);
        }
    }
}