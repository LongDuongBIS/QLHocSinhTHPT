using DevComponents.DotNetBar;
using QLHocSinhTHPT.DTO;
using QLHocSinhTHPT.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace QLHocSinhTHPT.Component
{
    public static class Utilities
    {
        public static NguoiDungDTO NguoiDung;
        public static string DatabaseName;
    }

    public class QuyDinh
    {
        public static QuyDinhDTO LayThongTinTruong()
        {
            QuyDinhDTO truong = new QuyDinhDTO();
            DataService ds = new DataService();

            ds.Load(new SqlCommand("SELECT TenTruong, DiaChiTruong " + "FROM QUYDINH"));

            if (ds.Rows.Count > 0)
            {
                truong.TenTruong = ds.Rows[0]["TenTruong"].ToString();
                truong.DiaChiTruong = ds.Rows[0]["DiaChiTruong"].ToString();
            }

            return truong;
        }

        public string ArrayToString(string[] array, int n)
        {
            string str = string.Empty;
            for (int i = 0; i < n; i++)
            {
                if (i != n - 1)
                    str += string.Format("{0};", array[i]);
                else
                    str += array[i];
            }
            return str;
        }

        public bool KiemTraDiem(string diemSo)
        {
            IList<string> gioiHanDiem = new List<string>();

            DataService ds = new DataService();
            ds.Load(new SqlCommand("SELECT ThangDiem " + "FROM QUYDINH"));

            int thangDiem = Convert.ToInt32(ds.Rows[0]["ThangDiem"]);
            float nacDiemTrongGioiHan = 0;

            if (thangDiem == 10)
            {
                for (int i = 0; i <= 1010; i++)
                {
                    gioiHanDiem.Add(nacDiemTrongGioiHan.ToString());
                    nacDiemTrongGioiHan += 0.01F;
                    nacDiemTrongGioiHan = (float)Math.Round(nacDiemTrongGioiHan, 2);
                }

                if (gioiHanDiem.Contains(diemSo) == true)
                    return true;
                return false;
            }
            for (int i = 0; i <= 100; i++)
            {
                gioiHanDiem.Add(nacDiemTrongGioiHan.ToString());
                nacDiemTrongGioiHan += 1;
            }

            if (gioiHanDiem.Contains(diemSo) == true)
                return true;
            return false;
        }

        public bool KiemTraSiSo(int siSo)
        {
            DataService ds = new DataService();
            ds.Load(new SqlCommand("SELECT SiSoCanDuoi, SiSoCanTren " + "FROM QUYDINH"));

            int siSoMin = Convert.ToInt32(ds.Rows[0]["SiSoCanDuoi"]);
            int siSoMax = Convert.ToInt32(ds.Rows[0]["SiSoCanTren"]);

            if (siSo >= siSoMin && siSo <= siSoMax)
                return true;
            return false;
        }

        public bool KiemTraDoTuoi(DateTime ngaySinh)
        {
            DataService ds = new DataService();
            ds.Load(new SqlCommand("SELECT TuoiCanDuoi, TuoiCanTren " + "FROM QUYDINH"));

            int doTuoiMin = Convert.ToInt32(ds.Rows[0]["TuoiCanDuoi"]);
            int doTuoiMax = Convert.ToInt32(ds.Rows[0]["TuoiCanTren"]);

            int doTuoi = DateTime.Today.Year - ngaySinh.Year;

            if (doTuoi >= doTuoiMin && doTuoi <= doTuoiMax)
                return true;
            return false;
        }

        public string LaySTT(int autoNum)
        {
            if (autoNum < 10)
                return "000" + autoNum;
            if (autoNum >= 10 && autoNum < 100)
                return "00" + autoNum;
            if (autoNum >= 100 && autoNum < 1000)
                return "0" + autoNum;
            if (autoNum >= 1000 && autoNum < 10000)
                return "" + autoNum;
            return "";
        }
    }

    public static class ThamSo
    {
        public static frmAbout m_FrmAbout;
        public static frmConnection m_FrmConnection;
        public static frmDanToc m_FrmDanToc;
        public static frmGiaoVien m_FrmGiaoVien;
        public static frmHanhKiem m_FrmHanhKiem;
        public static frmHocKy m_FrmHocKy;
        public static frmHocLuc m_FrmHocLuc;
        public static frmHocSinh m_FrmHocSinh;
        public static frmKetQua m_FrmKetQua;
        public static frmKhoiLop m_FrmKhoiLop;
        public static frmLop m_FrmLop;
        public static frmMain m_FrmMain;
        public static frmMonHoc m_FrmMonHoc;
        public static frmNamHoc m_FrmNamHoc;
        public static frmNhapDiemRieng m_FrmNhapDiemRieng;
        public static frmNhapDiemChung m_FrmNhapDiemChung;
        public static frmXemDiem m_FrmXemDiem;
        public static frmNgheNghiep m_FrmNgheNghiep;
        public static frmPhanCong m_FrmPhanCong;
        public static frmPhanLop m_FrmPhanLop;
        public static frmTonGiao m_FrmTonGiao;
        public static frmLoaiNguoiDung m_FrmLoaiNguoiDung;
        public static frmLoaiDiem m_FrmLoaiDiem;
        public static frmTimKiemGV m_TimKiemGV;
        public static frmTimKiemHS m_TimKiemHS;
        public static frmQuyDinh m_FrmQuyDinh;
        public static frptDanhSachGiaoVien m_FrmDSGiaoVien;
        public static frptDanhSachHocSinh m_FrmDSHocSinh;
        public static frptDanhSachLopHoc m_FrmDSLopHoc;
        public static frptKetQuaCaNam_Lop m_FrmKetQuaCaNam_Lop;
        public static frptKetQuaCaNam_MonHoc m_FrmKetQuaCaNam_MonHoc;
        public static frptKetQuaHocKy_Lop m_FrmKetQuaHocKy_Lop;
        public static frptKetQuaHocKy_MonHoc m_FrmKetQuaHocKy_MonHoc;

        public static void ShowFormLoaiNguoiDung()
        {
            if (m_FrmLoaiNguoiDung == null || m_FrmLoaiNguoiDung.IsDisposed)
            {
                m_FrmLoaiNguoiDung = new frmLoaiNguoiDung();
                m_FrmLoaiNguoiDung.MdiParent = Form.ActiveForm;
                m_FrmLoaiNguoiDung.Show();
            }
            else
                m_FrmLoaiNguoiDung.Activate();
        }

        public static void ShowFormLopHoc()
        {
            if (m_FrmLop == null || m_FrmLop.IsDisposed)
            {
                m_FrmLop = new frmLop();
                m_FrmLop.MdiParent = Form.ActiveForm;
                m_FrmLop.Show();
            }
            else
                m_FrmLop.Activate();
        }

        public static void ShowFormKhoiLop()
        {
            if (m_FrmKhoiLop == null || m_FrmKhoiLop.IsDisposed)
            {
                m_FrmKhoiLop = new frmKhoiLop();
                m_FrmKhoiLop.MdiParent = Form.ActiveForm;
                m_FrmKhoiLop.Show();
            }
            else
                m_FrmKhoiLop.Activate();
        }

        public static void ShowFormHocKy()
        {
            if (m_FrmHocKy == null || m_FrmHocKy.IsDisposed)
            {
                m_FrmHocKy = new frmHocKy();
                m_FrmHocKy.MdiParent = Form.ActiveForm;
                m_FrmHocKy.Show();
            }
            else
                m_FrmHocKy.Activate();
        }

        public static void ShowFormNamHoc()
        {
            if (m_FrmNamHoc == null || m_FrmNamHoc.IsDisposed)
            {
                m_FrmNamHoc = new frmNamHoc();
                m_FrmNamHoc.MdiParent = Form.ActiveForm;
                m_FrmNamHoc.Show();
            }
            else
                m_FrmNamHoc.Activate();
        }

        public static void ShowFormMonHoc()
        {
            if (m_FrmMonHoc == null || m_FrmMonHoc.IsDisposed)
            {
                m_FrmMonHoc = new frmMonHoc();
                m_FrmMonHoc.MdiParent = Form.ActiveForm;
                m_FrmMonHoc.Show();
            }
            else
                m_FrmMonHoc.Activate();
        }

        public static void ShowFormLoaiDiem()
        {
            if (m_FrmLoaiDiem == null || m_FrmLoaiDiem.IsDisposed)
            {
                m_FrmLoaiDiem = new frmLoaiDiem();
                m_FrmLoaiDiem.MdiParent = Form.ActiveForm;
                m_FrmLoaiDiem.Show();
            }
            else
                m_FrmLoaiDiem.Activate();
        }

        public static void ShowFormNhapDiemRieng()
        {
            if (m_FrmNhapDiemRieng == null || m_FrmNhapDiemRieng.IsDisposed)
            {
                m_FrmNhapDiemRieng = new frmNhapDiemRieng();
                m_FrmNhapDiemRieng.MdiParent = Form.ActiveForm;
                m_FrmNhapDiemRieng.Show();
            }
            else
                m_FrmNhapDiemRieng.Activate();
        }

        public static void ShowFormNhapDiemChung()
        {
            if (m_FrmNhapDiemChung == null || m_FrmNhapDiemChung.IsDisposed)
            {
                m_FrmNhapDiemChung = new frmNhapDiemChung();
                m_FrmNhapDiemChung.MdiParent = Form.ActiveForm;
                m_FrmNhapDiemChung.Show();
            }
            else
                m_FrmNhapDiemChung.Activate();
        }

        public static void ShowFormXemDiem()
        {
            if (m_FrmXemDiem == null || m_FrmXemDiem.IsDisposed)
            {
                m_FrmXemDiem = new frmXemDiem();
                m_FrmXemDiem.MdiParent = Form.ActiveForm;
                m_FrmXemDiem.Show();
            }
            else
                m_FrmXemDiem.Activate();
        }

        public static void ShowFormKetQua()
        {
            if (m_FrmKetQua == null || m_FrmKetQua.IsDisposed)
            {
                m_FrmKetQua = new frmKetQua();
                m_FrmKetQua.MdiParent = Form.ActiveForm;
                m_FrmKetQua.Show();
            }
            else
                m_FrmKetQua.Activate();
        }

        public static void ShowFormHocLuc()
        {
            if (m_FrmHocLuc == null || m_FrmHocLuc.IsDisposed)
            {
                m_FrmHocLuc = new frmHocLuc();
                m_FrmHocLuc.MdiParent = Form.ActiveForm;
                m_FrmHocLuc.Show();
            }
            else
                m_FrmHocLuc.Activate();
        }

        public static void ShowFormHanhKiem()
        {
            if (m_FrmHanhKiem == null || m_FrmHanhKiem.IsDisposed)
            {
                m_FrmHanhKiem = new frmHanhKiem();
                m_FrmHanhKiem.MdiParent = Form.ActiveForm;
                m_FrmHanhKiem.Show();
            }
            else
                m_FrmHanhKiem.Activate();
        }

        public static void ShowFormHocSinh()
        {
            if (m_FrmHocSinh == null || m_FrmHocSinh.IsDisposed)
            {
                m_FrmHocSinh = new frmHocSinh();
                m_FrmHocSinh.MdiParent = Form.ActiveForm;
                m_FrmHocSinh.Show();
            }
            else
                m_FrmHocSinh.Activate();
        }

        public static void ShowFormPhanLop()
        {
            if (m_FrmPhanLop == null || m_FrmPhanLop.IsDisposed)
            {
                m_FrmPhanLop = new frmPhanLop();
                m_FrmPhanLop.MdiParent = Form.ActiveForm;
                m_FrmPhanLop.Show();
            }
            else
                m_FrmPhanLop.Activate();
        }

        public static void ShowFormDanToc()
        {
            if (m_FrmDanToc == null || m_FrmDanToc.IsDisposed)
            {
                m_FrmDanToc = new frmDanToc();
                m_FrmDanToc.MdiParent = Form.ActiveForm;
                m_FrmDanToc.Show();
            }
            else
                m_FrmDanToc.Activate();
        }

        public static void ShowFormTonGiao()
        {
            if (m_FrmTonGiao == null || m_FrmTonGiao.IsDisposed)
            {
                m_FrmTonGiao = new frmTonGiao();
                m_FrmTonGiao.MdiParent = Form.ActiveForm;
                m_FrmTonGiao.Show();
            }
            else
                m_FrmTonGiao.Activate();
        }

        public static void ShowFormNgheNghiep()
        {
            if (m_FrmNgheNghiep == null || m_FrmNgheNghiep.IsDisposed)
            {
                m_FrmNgheNghiep = new frmNgheNghiep();
                m_FrmNgheNghiep.MdiParent = Form.ActiveForm;
                m_FrmNgheNghiep.Show();
            }
            else
                m_FrmNgheNghiep.Activate();
        }

        public static void ShowFormGiaoVien()
        {
            if (m_FrmGiaoVien == null || m_FrmGiaoVien.IsDisposed)
            {
                m_FrmGiaoVien = new frmGiaoVien();
                m_FrmGiaoVien.MdiParent = Form.ActiveForm;
                m_FrmGiaoVien.Show();
            }
            else
                m_FrmGiaoVien.Activate();
        }

        public static void ShowFormPhanCong()
        {
            if (m_FrmPhanCong == null || m_FrmPhanCong.IsDisposed)
            {
                m_FrmPhanCong = new frmPhanCong();
                m_FrmPhanCong.MdiParent = Form.ActiveForm;
                m_FrmPhanCong.Show();
            }
            else
                m_FrmPhanCong.Activate();
        }

        public static void ShowFormKQHKTheoLop()
        {
            if (m_FrmKetQuaHocKy_Lop == null || m_FrmKetQuaHocKy_Lop.IsDisposed)
            {
                m_FrmKetQuaHocKy_Lop = new frptKetQuaHocKy_Lop();
                m_FrmKetQuaHocKy_Lop.MdiParent = Form.ActiveForm;
                m_FrmKetQuaHocKy_Lop.Show();
            }
            else
                m_FrmKetQuaHocKy_Lop.Activate();
        }

        public static void ShowFormKQHKTheoMon()
        {
            if (m_FrmKetQuaHocKy_MonHoc == null || m_FrmKetQuaHocKy_MonHoc.IsDisposed)
            {
                m_FrmKetQuaHocKy_MonHoc = new frptKetQuaHocKy_MonHoc();
                m_FrmKetQuaHocKy_MonHoc.MdiParent = Form.ActiveForm;
                m_FrmKetQuaHocKy_MonHoc.Show();
            }
            else
                m_FrmKetQuaHocKy_MonHoc.Activate();
        }

        public static void ShowFormKQCNTheoLop()
        {
            if (m_FrmKetQuaCaNam_Lop == null || m_FrmKetQuaCaNam_Lop.IsDisposed)
            {
                m_FrmKetQuaCaNam_Lop = new frptKetQuaCaNam_Lop();
                m_FrmKetQuaCaNam_Lop.MdiParent = Form.ActiveForm;
                m_FrmKetQuaCaNam_Lop.Show();
            }
            else
                m_FrmKetQuaCaNam_Lop.Activate();
        }

        public static void ShowFormKQCNTheoMon()
        {
            if (m_FrmKetQuaCaNam_MonHoc == null || m_FrmKetQuaCaNam_MonHoc.IsDisposed)
            {
                m_FrmKetQuaCaNam_MonHoc = new frptKetQuaCaNam_MonHoc();
                m_FrmKetQuaCaNam_MonHoc.MdiParent = Form.ActiveForm;
                m_FrmKetQuaCaNam_MonHoc.Show();
            }
            else
                m_FrmKetQuaCaNam_MonHoc.Activate();
        }

        public static void ShowFormDanhSachHocSinh()
        {
            if (m_FrmDSHocSinh == null || m_FrmDSHocSinh.IsDisposed)
            {
                m_FrmDSHocSinh = new frptDanhSachHocSinh();
                m_FrmDSHocSinh.MdiParent = Form.ActiveForm;
                m_FrmDSHocSinh.Show();
            }
            else
                m_FrmDSHocSinh.Activate();
        }

        public static void ShowFormDanhSachGiaoVien()
        {
            if (m_FrmDSGiaoVien == null || m_FrmDSGiaoVien.IsDisposed)
            {
                m_FrmDSGiaoVien = new frptDanhSachGiaoVien();
                m_FrmDSGiaoVien.MdiParent = Form.ActiveForm;
                m_FrmDSGiaoVien.Show();
            }
            else
                m_FrmDSGiaoVien.Activate();
        }

        public static void ShowFormDanhSachLopHoc()
        {
            if (m_FrmDSLopHoc == null || m_FrmDSLopHoc.IsDisposed)
            {
                m_FrmDSLopHoc = new frptDanhSachLopHoc();
                m_FrmDSLopHoc.MdiParent = Form.ActiveForm;
                m_FrmDSLopHoc.Show();
            }
            else
                m_FrmDSLopHoc.Activate();
        }

        public static void ShowFormTimKiemHS()
        {
            if (m_TimKiemHS == null || m_TimKiemHS.IsDisposed)
            {
                m_TimKiemHS = new frmTimKiemHS();
                m_TimKiemHS.MdiParent = Form.ActiveForm;
                m_TimKiemHS.Show();
            }
            else
                m_TimKiemHS.Activate();
        }

        public static void ShowFormTimKiemGV()
        {
            if (m_TimKiemGV == null || m_TimKiemGV.IsDisposed)
            {
                m_TimKiemGV = new frmTimKiemGV();
                m_TimKiemGV.MdiParent = Form.ActiveForm;
                m_TimKiemGV.Show();
            }
            else
                m_TimKiemGV.Activate();
        }

        public static void ShowFormQuyDinh()
        {
            if (m_FrmQuyDinh == null || m_FrmQuyDinh.IsDisposed)
            {
                m_FrmQuyDinh = new frmQuyDinh();
                m_FrmQuyDinh.Show();
            }
            else
                m_FrmQuyDinh.Activate();
        }

        public static void ShowFormKetNoi()
        {
            if (m_FrmConnection == null || m_FrmConnection.IsDisposed)
            {
                m_FrmConnection = new frmConnection();
                m_FrmConnection.Show();
            }
            else
                m_FrmConnection.Activate();
        }

        public static void ShowFormThongTin()
        {
            if (m_FrmAbout == null || m_FrmAbout.IsDisposed)
            {
                m_FrmAbout = new frmAbout();
                m_FrmAbout.Show();
            }
            else
                m_FrmAbout.Activate();
        }
    }

    public class XML
    {
        public static XmlDocument XMLReader(string filename)
        {
            XmlDocument xmlRead = new XmlDocument();
            try
            {
                xmlRead.Load(filename);
            }
            catch
            {
                MessageBoxEx.Show(string.Format("Không đọc được hoặc không tồn tại tập tin cấu hình {0}", filename), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return xmlRead;
        }

        public static void XMLWriter(string filename, string servername, string database, string constatus)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(filename, null);
            xmlWriter.Formatting = Formatting.Indented;

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("\nKhong duoc thay doi noi dung file nay!" + "\nThong so co ban:" + "\n\tcostatus = true : quyen Windows" + "\n\tcostatus = false: quyen SQL Server" + "\n\tservname: ten server" + "\n\tusername: ten dang nhap he thong" + "\n\tpassword: mat khau dang nhap he thong" + "\n\tdatabase: ten co so du lieu\n");
            xmlWriter.WriteStartElement("config");

            xmlWriter.WriteStartElement("constatus");
            xmlWriter.WriteString(constatus);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("servername");
            xmlWriter.WriteString(servername);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("username");
            xmlWriter.WriteString("");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("password");
            xmlWriter.WriteString("");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("database");
            xmlWriter.WriteString(database);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();
        }

        public static void XMLWriter(string filename, string servername, string username, string password, string database, string constatus)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(filename, null);
            xmlWriter.Formatting = Formatting.Indented;

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("\nKhong duoc thay doi noi dung file nay!" + "\nThong so co ban:" + "\n\tconstatus = true : quyen Windows" + "\n\tconstatus = false: quyen SQL Server" + "\n\tservername: ten server" + "\n\tusername: ten dang nhap he thong" + "\n\tpassword: mat khau dang nhap he thong" + "\n\tdatabase: ten co so du lieu\n");
            xmlWriter.WriteStartElement("config");

            xmlWriter.WriteStartElement("constatus");
            xmlWriter.WriteString(constatus);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("servername");
            xmlWriter.WriteString(servername);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("username");
            xmlWriter.WriteString(username);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("password");
            xmlWriter.WriteString(password);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("database");
            xmlWriter.WriteString(database);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();
        }
    }
}