using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using QLHocSinhTHPT.DAL;
using QLHocSinhTHPT.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT.BLL
{
    public class LopBLL
    {
        private readonly LopDAL lopDAL = new LopDAL();

        public void HienThiComboBox(ComboBox comboBox)
        {
            comboBox.DataSource = lopDAL.LayDsLop();
            comboBox.DisplayMember = "TenLop";
            comboBox.ValueMember = "MaLop";
        }

        public void HienThiComboBox(string namHoc, ComboBox comboBox)
        {
            comboBox.DataSource = lopDAL.LayDsLop(namHoc);
            comboBox.DisplayMember = "TenLop";
            comboBox.ValueMember = "MaLop";
        }

        public void HienThiComboBox(string khoiLop, string namHoc, ComboBox comboBox)
        {
            comboBox.DataSource = lopDAL.LayDsLop(khoiLop, namHoc);
            comboBox.DisplayMember = "TenLop";
            comboBox.ValueMember = "MaLop";
        }

        public void HienThiDataGridViewComboBoxColumn(DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = lopDAL.LayDsLop();
            cmbColumn.DisplayMember = "TenLop";
            cmbColumn.ValueMember = "MaLop";
            cmbColumn.DataPropertyName = "MaLop";
            cmbColumn.HeaderText = "Lớp";
        }

        public void HienThiDataGridViewComboBoxColumn(string namHoc, DataGridViewComboBoxColumn cmbColumn)
        {
            cmbColumn.DataSource = lopDAL.LayDsLop(namHoc);
            cmbColumn.DisplayMember = "TenLop";
            cmbColumn.ValueMember = "MaLop";
            cmbColumn.DataPropertyName = "MaLop";
            cmbColumn.HeaderText = "Lớp";
        }

        public void HienThi(DataGridView dGV, BindingNavigator bN)
        {
            BindingSource bS = new BindingSource();

            bS.DataSource = lopDAL.LayDsLop();
            bN.BindingSource = bS;
            dGV.DataSource = bS;
        }

        public void HienThi(DataGridViewX dGV, BindingNavigator bN, TextBoxX txtMaLop, TextBoxX txtTenLop, ComboBoxEx cmbKhoiLop, ComboBoxEx cmbNamHoc, IntegerInput iniSiSo, ComboBoxEx cmbGiaoVien)
        {
            BindingSource bS = new BindingSource();
            bS.DataSource = lopDAL.LayDsLop();

            bN.BindingSource = bS;
            dGV.DataSource = bS;

            txtMaLop.DataBindings.Clear();
            txtMaLop.DataBindings.Add("Text", bS, "MaLop");

            txtTenLop.DataBindings.Clear();
            txtTenLop.DataBindings.Add("Text", bS, "TenLop");

            cmbKhoiLop.DataBindings.Clear();
            cmbKhoiLop.DataBindings.Add("SelectedValue", bS, "MaKhoiLop");

            cmbNamHoc.DataBindings.Clear();
            cmbNamHoc.DataBindings.Add("SelectedValue", bS, "MaNamHoc");

            iniSiSo.DataBindings.Clear();
            iniSiSo.DataBindings.Add("Text", bS, "SiSo");

            cmbGiaoVien.DataBindings.Clear();
            cmbGiaoVien.DataBindings.Add("SelectedValue", bS, "MaGiaoVien");
        }

        public static IList<LopDTO> LayDsLop()
        {
            DataTable m_DT = new LopDAL().LayDsLopForReport();

            IList<LopDTO> dS = new List<LopDTO>();

            foreach (DataRow Row in m_DT.Rows)
            {
                LopDTO lopDTO = new LopDTO();

                GiaoVienDTO giaoVienDTO = new GiaoVienDTO();
                giaoVienDTO.MaGiaoVien = Convert.ToString(Row["MaGiaoVien"]);
                giaoVienDTO.TenGiaoVien = Convert.ToString(Row["TenGiaoVien"]);

                KhoiLopDTO khoiLopDTO = new KhoiLopDTO();
                khoiLopDTO.MaKhoiLop = Convert.ToString(Row["MaKhoiLop"]);
                khoiLopDTO.TenKhoiLop = Convert.ToString(Row["TenKhoiLop"]);

                NamHocDTO namHocDTO = new NamHocDTO();
                namHocDTO.MaNamHoc = Convert.ToString(Row["MaNamHoc"]);
                namHocDTO.TenNamHoc = Convert.ToString(Row["TenNamHoc"]);

                lopDTO.MaLop = Convert.ToString(Row["MaLop"]);
                lopDTO.TenLop = Convert.ToString(Row["TenLop"]);
                lopDTO.KhoiLop = khoiLopDTO;
                lopDTO.NamHoc = namHocDTO;
                lopDTO.SiSo = Convert.ToInt32(Row["SiSo"]);
                lopDTO.GiaoVien = giaoVienDTO;

                dS.Add(lopDTO);
            }
            return dS;
        }

        public static IList<LopDTO> LayDsLop(string namHoc)
        {
            DataTable dt = new LopDAL().LayDsLopForReport(namHoc);

            IList<LopDTO> dS = new List<LopDTO>();

            foreach (DataRow Row in dt.Rows)
            {
                LopDTO lopDTO = new LopDTO();

                GiaoVienDTO giaoVienDTO = new GiaoVienDTO();
                giaoVienDTO.MaGiaoVien = Convert.ToString(Row["MaGiaoVien"]);
                giaoVienDTO.TenGiaoVien = Convert.ToString(Row["TenGiaoVien"]);

                KhoiLopDTO khoiLopDTO = new KhoiLopDTO();
                khoiLopDTO.MaKhoiLop = Convert.ToString(Row["MaKhoiLop"]);
                khoiLopDTO.TenKhoiLop = Convert.ToString(Row["TenKhoiLop"]);

                NamHocDTO namHocDTO = new NamHocDTO();
                namHocDTO.MaNamHoc = Convert.ToString(Row["MaNamHoc"]);
                namHocDTO.TenNamHoc = Convert.ToString(Row["TenNamHoc"]);

                lopDTO.MaLop = Convert.ToString(Row["MaLop"]);
                lopDTO.TenLop = Convert.ToString(Row["TenLop"]);
                lopDTO.KhoiLop = khoiLopDTO;
                lopDTO.NamHoc = namHocDTO;
                lopDTO.SiSo = Convert.ToInt32(Row["SiSo"]);
                lopDTO.GiaoVien = giaoVienDTO;

                dS.Add(lopDTO);
            }
            return dS;
        }

        public DataRow ThemDongMoi()
        {
            return lopDAL.ThemDongMoi();
        }

        public void ThemLop(DataRow row)
        {
            lopDAL.ThemLop(row);
        }

        public bool LuuLop()
        {
            return lopDAL.LuuLop();
        }

        public void LuuLop(string maLop, string tenLop, string maKhoiLop, string maNamHoc, int siSo, string maGiaoVien)
        {
            lopDAL.LuuLop(maLop, tenLop, maKhoiLop, maNamHoc, siSo, maGiaoVien);
        }

        public void TimTheoMa(string maLop)
        {
            lopDAL.TimTheoMa(maLop);
        }

        public void TimTheoTen(string tenLop)
        {
            lopDAL.TimTheoTen(tenLop);
        }
    }
}