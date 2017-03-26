using DevComponents.DotNetBar;
using QLHocSinhTHPT.BLL;
using QLHocSinhTHPT.Components;
using QLHocSinhTHPT.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLHocSinhTHPT
{
    public partial class frmNhapDiemChung : Office2007Form
    {
        private NamHocBLL namHocBLL = new NamHocBLL();
        private LopBLL lopBLL = new LopBLL();
        private HocKyBLL hocKyBLL = new HocKyBLL();
        private MonHocBLL monHocBLL = new MonHocBLL();
        private DiemBLL diemBLL = new DiemBLL();
        private LoaiDiemBLL loaiDiemBLL = new LoaiDiemBLL();
        private HocSinhBLL hocSinhBLL = new HocSinhBLL();
        private KQHocKyMonHocBLL kqHocKyMonHocBLL = new KQHocKyMonHocBLL();
        private KQHocKyTongHopBLL kqHocKyTongHopBLL = new KQHocKyTongHopBLL();
        private KQCaNamMonHocBLL kqCaNamMonHocBLL = new KQCaNamMonHocBLL();
        private KQCaNamTongHopBLL kqCaNamTongHopBLL = new KQCaNamTongHopBLL();

        private DiemDAL diemDAL = new DiemDAL();
        private QuyDinh quyDinh = new QuyDinh();
        private int[,] STT;

        public frmNhapDiemChung()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }

        private void frmNhapDiemChung_Load(object sender, EventArgs e)
        {
            namHocBLL.HienThiComboBox(cmbNamHoc);
            hocKyBLL.HienThiComboBox(cmbHocKy);
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);

            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);

            namHocBLL.HienThiComboBox(cmbNamHocSD);
            hocKyBLL.HienThiComboBox(cmbHocKySD);
            if (cmbNamHocSD.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHocSD.SelectedValue.ToString(), cmbLopSD);

            if (cmbNamHocSD.SelectedValue != null && cmbLopSD.SelectedValue != null)
                monHocBLL.HienThiComboBox(cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString(), cmbMonHocSD);
        }

        public bool KiemTraDiemTruocKhiLuu(string loaiDiem)
        {
            foreach (DataGridViewRow row in dGVDiem.Rows)
            {
                if (row.Cells[loaiDiem].Value != null)
                {
                    string chuoiDiemChuaXuLy = row.Cells[loaiDiem].Value.ToString();
                    string diemDaXuLy = null;

                    int count = 0;
                    for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                    {
                        if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                            count++;
                        else
                        {
                            if (i == chuoiDiemChuaXuLy.Length - 1)
                            {
                                i++;
                                count++;
                            }

                            diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                            if (count != 0 && quyDinh.KiemTraDiem(diemDaXuLy) == false)
                            {
                                MessageBoxEx.Show(string.Format("Điểm của học sinh {0} không hợp lệ!", row.Cells["colHoTen"].Value), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }

                            diemDaXuLy = null;
                            count = 0;
                        }
                    }
                }
            }
            return true;
        }

        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            if (KiemTraDiemTruocKhiLuu("colDiemMieng") == true && KiemTraDiemTruocKhiLuu("colDiem15Phut") == true && KiemTraDiemTruocKhiLuu("colDiem45Phut") == true && KiemTraDiemTruocKhiLuu("colDiemThi") == true)
            {
                if (buttonItemNhapDuLieu.Checked == true && STT == null)
                {
                    int rowcount = 0;

                    foreach (DataGridViewRow row in dGVDiem.Rows)
                    {
                        rowcount++;

                        if (row.Cells["colDiemMieng"].Value != null)
                        {
                            string chuoiDiemChuaXuLy = row.Cells["colDiemMieng"].Value.ToString();
                            string diemDaXuLy = null;

                            int count = 0;
                            for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                            {
                                if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                                    count++;
                                else
                                {
                                    if (i == chuoiDiemChuaXuLy.Length - 1)
                                    {
                                        i++;
                                        count++;
                                    }

                                    diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                                    if (diemDaXuLy != null && diemDaXuLy != " " && quyDinh.KiemTraDiem(diemDaXuLy))
                                        diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), "LD0001", float.Parse(diemDaXuLy));

                                    diemDaXuLy = null;
                                    count = 0;
                                }
                            }
                        }

                        if (row.Cells["colDiem15Phut"].Value != null)
                        {
                            string chuoiDiemChuaXuLy = row.Cells["colDiem15Phut"].Value.ToString();
                            string diemDaXuLy = null;

                            int count = 0;
                            for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                            {
                                if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                                    count++;
                                else
                                {
                                    if (i == chuoiDiemChuaXuLy.Length - 1)
                                    {
                                        i++;
                                        count++;
                                    }

                                    diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                                    if (diemDaXuLy != null && diemDaXuLy != " " && quyDinh.KiemTraDiem(diemDaXuLy))
                                        diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), "LD0002", float.Parse(diemDaXuLy));

                                    diemDaXuLy = null;
                                    count = 0;
                                }
                            }
                        }

                        if (row.Cells["colDiem45Phut"].Value != null)
                        {
                            string chuoiDiemChuaXuLy = row.Cells["colDiem45Phut"].Value.ToString();
                            string diemDaXuLy = null;

                            int count = 0;
                            for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                            {
                                if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                                    count++;
                                else
                                {
                                    if (i == chuoiDiemChuaXuLy.Length - 1)
                                    {
                                        i++;
                                        count++;
                                    }

                                    diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                                    if (diemDaXuLy != null && diemDaXuLy != " " && quyDinh.KiemTraDiem(diemDaXuLy))
                                        diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), "LD0003", float.Parse(diemDaXuLy));

                                    diemDaXuLy = null;
                                    count = 0;
                                }
                            }
                        }

                        if (row.Cells["colDiemThi"].Value != null)
                        {
                            string diemThi = row.Cells["colDiemThi"].Value.ToString();
                            if (quyDinh.KiemTraDiem(diemThi))
                                diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), "LD0004", float.Parse(diemThi));
                        }

                        if (rowcount <= dGVDiem.Rows.Count)
                        {
                            kqHocKyMonHocBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString());

                            kqCaNamMonHocBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString());

                            kqHocKyTongHopBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLop.SelectedValue.ToString(), cmbHocKy.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString());

                            kqCaNamTongHopBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLop.SelectedValue.ToString(), cmbNamHoc.SelectedValue.ToString());
                        }
                    }
                    MessageBoxEx.Show("Đã lưu thành công vào bảng điểm!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (buttonItemCapNhatDuLieu.Checked == true || STT != null)
                {
                    int rowcount = 0;

                    foreach (DataGridViewRow row in dGVDiem.Rows)
                    {
                        rowcount++;

                        if (row.Cells["colDiemMieng"].Value != null)
                        {
                            string chuoiDiemChuaXuLy = row.Cells["colDiemMieng"].Value.ToString();
                            string diemDaXuLy = null;

                            int count = 0;
                            for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                            {
                                if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                                    count++;
                                else
                                {
                                    if (i == chuoiDiemChuaXuLy.Length - 1)
                                    {
                                        i++;
                                        count++;
                                    }

                                    diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                                    if (diemDaXuLy != null && diemDaXuLy != " " && quyDinh.KiemTraDiem(diemDaXuLy))
                                        diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString(), "LD0001", float.Parse(diemDaXuLy));

                                    diemDaXuLy = null;
                                    count = 0;
                                }
                            }
                        }

                        if (row.Cells["colDiem15Phut"].Value != null)
                        {
                            string chuoiDiemChuaXuLy = row.Cells["colDiem15Phut"].Value.ToString();
                            string diemDaXuLy = null;

                            int count = 0;
                            for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                            {
                                if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                                    count++;
                                else
                                {
                                    if (i == chuoiDiemChuaXuLy.Length - 1)
                                    {
                                        i++;
                                        count++;
                                    }

                                    diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                                    if (diemDaXuLy != null && diemDaXuLy != " " && quyDinh.KiemTraDiem(diemDaXuLy))
                                        diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString(), "LD0002", float.Parse(diemDaXuLy));

                                    diemDaXuLy = null;
                                    count = 0;
                                }
                            }
                        }

                        if (row.Cells["colDiem45Phut"].Value != null)
                        {
                            string chuoiDiemChuaXuLy = row.Cells["colDiem45Phut"].Value.ToString();
                            string diemDaXuLy = null;

                            int count = 0;
                            for (int i = 0; i < chuoiDiemChuaXuLy.Length; i++)
                            {
                                if (chuoiDiemChuaXuLy[i] != ';' && i != chuoiDiemChuaXuLy.Length - 1)
                                    count++;
                                else
                                {
                                    if (i == chuoiDiemChuaXuLy.Length - 1)
                                    {
                                        i++;
                                        count++;
                                    }

                                    diemDaXuLy = chuoiDiemChuaXuLy.Substring(i - count, count);

                                    if (diemDaXuLy != null && diemDaXuLy != " " && quyDinh.KiemTraDiem(diemDaXuLy))
                                        diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString(), "LD0003", float.Parse(diemDaXuLy));

                                    diemDaXuLy = null;
                                    count = 0;
                                }
                            }
                        }

                        if (row.Cells["colDiemThi"].Value != null)
                        {
                            string diemThi = row.Cells["colDiemThi"].Value.ToString();
                            if (quyDinh.KiemTraDiem(diemThi))
                                diemBLL.LuuDiem(row.Cells["colMaHocSinh"].Value.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString(), "LD0004", float.Parse(diemThi));
                        }

                        if (rowcount <= dGVDiem.Rows.Count)
                        {
                            kqHocKyMonHocBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLopSD.SelectedValue.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString());

                            kqCaNamMonHocBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLopSD.SelectedValue.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString());

                            kqHocKyTongHopBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLopSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString());

                            kqCaNamTongHopBLL.LuuKetQua(row.Cells["colMaHocSinh"].Value.ToString(), cmbLopSD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString());
                        }

                        if (STT != null)
                            for (int i = 1; i < 60; i++)
                                for (int j = 1; j < 20; j++)
                                {
                                    int id = STT[i, j];
                                    if (id > 0)
                                        diemBLL.XoaDiem(id);
                                    else
                                        break;
                                }
                    }
                    MessageBoxEx.Show("Cập nhật thành công!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormXemDiem();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dGVNhapDiemChung_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnThemNamHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormNamHoc();
            namHocBLL.HienThiComboBox(cmbNamHoc);
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormLopHoc();
            lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
        }

        private void btnThemHocKy_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormHocKy();
            hocKyBLL.HienThiComboBox(cmbHocKy);
        }

        private void btnThemMonHoc_Click(object sender, EventArgs e)
        {
            ThamSo.ShowFormMonHoc();
            monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
        }

        private void btnHienThiDanhSach_Click(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null && cmbHocKy.SelectedValue != null && cmbMonHoc.SelectedValue != null)
                hocSinhBLL.HienThiDsHocSinhTheoLop(dGVDiem, bindingNavigatorDiem, cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString());
        }

        private void btnHienThiDanhSachSD_Click(object sender, EventArgs e)
        {
            STT = new int[60, 20];

            if (cmbNamHocSD.SelectedValue != null && cmbLopSD.SelectedValue != null && cmbHocKySD.SelectedValue != null && cmbMonHocSD.SelectedValue != null)
                hocSinhBLL.HienThiDsHocSinhTheoLop(dGVDiem, bindingNavigatorDiem, cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString());

            int countRowHocSinh = 0;
            foreach (DataGridViewRow rowHocSinh in dGVDiem.Rows)
            {
                countRowHocSinh++;

                string[] diemMieng = new string[10];
                string[] diem15Phut = new string[10];
                string[] diem45Phut = new string[10];
                string diemThi = string.Empty;

                int soDiemMieng = 0;
                int soDiem15Phut = 0;
                int soDiem45Phut = 0;

                DataTable dt = diemDAL.LayDsDiem(rowHocSinh.Cells["colMaHocSinh"].Value.ToString(), cmbMonHocSD.SelectedValue.ToString(), cmbHocKySD.SelectedValue.ToString(), cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString());

                int countRowDiem = 0;
                foreach (DataRow rowDiem in dt.Rows)
                {
                    countRowDiem++;

                    STT[countRowHocSinh, countRowDiem] = int.Parse(rowDiem["STT"].ToString());
                    switch (rowDiem["MaLoai"].ToString())
                    {
                        case "LD0001":
                            diemMieng[soDiemMieng++] = rowDiem["Diem"].ToString();
                            break;

                        case "LD0002":
                            diem15Phut[soDiem15Phut++] = rowDiem["Diem"].ToString();
                            break;

                        case "LD0003":
                            diem45Phut[soDiem45Phut++] = rowDiem["Diem"].ToString();
                            break;

                        case "LD0004":
                            diemThi = rowDiem["Diem"].ToString();
                            break;
                    }
                }

                rowHocSinh.Cells["colDiemMieng"].Value = quyDinh.ArrayToString(diemMieng, soDiemMieng);
                rowHocSinh.Cells["colDiem15Phut"].Value = quyDinh.ArrayToString(diem15Phut, soDiem15Phut);
                rowHocSinh.Cells["colDiem45Phut"].Value = quyDinh.ArrayToString(diem45Phut, soDiem45Phut);
                rowHocSinh.Cells["colDiemThi"].Value = diemThi;
            }
        }

        //Lấy thông tin lớp theo từng năm học
        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }

        //Lấy môn học theo từng lớp
        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
                monHocBLL.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop.SelectedValue.ToString(), cmbMonHoc);
            cmbMonHoc.DataBindings.Clear();
        }

        //Lấy thông tin lớp theo từng năm học
        private void cmbNamHocSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHocSD.SelectedValue != null)
                lopBLL.HienThiComboBox(cmbNamHocSD.SelectedValue.ToString(), cmbLopSD);
            cmbLopSD.DataBindings.Clear();
        }

        //Lấy môn học theo từng lớp
        private void cmbLopSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHocSD.SelectedValue != null && cmbLopSD.SelectedValue != null)
                monHocBLL.HienThiComboBox(cmbNamHocSD.SelectedValue.ToString(), cmbLopSD.SelectedValue.ToString(), cmbMonHocSD);
            cmbMonHocSD.DataBindings.Clear();
        }
    }
}