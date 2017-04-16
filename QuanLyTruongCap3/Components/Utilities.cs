using DevComponents.DotNetBar;
using QuanLyTruongCap3.DTO;
using QuanLyTruongCap3.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace QuanLyTruongCap3.Components
{
    public static class Utilities
    {

        public static string DatabaseName { get; set; }

        public static NguoiDungDTO NguoiDung { get; set; }
    }

    public class QuyDinh
    {

        public static string ArrayToString(string[] array, int n)
        {
            string str = string.Empty;
            var builder = new System.Text.StringBuilder();
            builder.Append(str);
            for (int i = 0; i < n; i++)
            {
                builder.Append(i != n - 1 ? $"{array[i]};" : array[i]);
            }
            str = builder.ToString();
            return str;
        }

        public static bool KiemTraDiem(string diemSo)
        {
            IList<string> gioiHanDiem = new List<string>();

            using (DataService ds = new DataService())
            {
                using (var sqlCommand = new SqlCommand("SELECT ThangDiem " + "FROM QUYDINH"))
                {
                    ds.Load(sqlCommand);
                }

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

                    if (gioiHanDiem.Contains(diemSo))
                        return true;
                    return false;
                }
                for (int i = 0; i <= 100; i++)
                {
                    gioiHanDiem.Add(nacDiemTrongGioiHan.ToString());
                    nacDiemTrongGioiHan += 1;
                }

                if (gioiHanDiem.Contains(diemSo))
                    return true;
                return false;
            }
        }

        public static bool KiemTraDoTuoi(DateTime ngaySinh)
        {
            using (DataService ds = new DataService())
            {
                using (var sqlCommand = new SqlCommand("SELECT TuoiCanDuoi, TuoiCanTren " + "FROM QUYDINH"))
                {
                    ds.Load(sqlCommand);
                }

                int doTuoiMin = Convert.ToInt32(ds.Rows[0]["TuoiCanDuoi"]);
                int doTuoiMax = Convert.ToInt32(ds.Rows[0]["TuoiCanTren"]);

                int doTuoi = DateTime.Today.Year - ngaySinh.Year;

                if (doTuoi >= doTuoiMin && doTuoi <= doTuoiMax)
                    return true;
                return false;
            }
        }

        public static bool KiemTraSiSo(int siSo)
        {
            using (DataService ds = new DataService())
            {
                using (var sqlCommand = new SqlCommand("SELECT SiSoCanDuoi, SiSoCanTren " + "FROM QUYDINH"))
                {
                    ds.Load(sqlCommand);
                }

                int siSoMin = Convert.ToInt32(ds.Rows[0]["SiSoCanDuoi"]);
                int siSoMax = Convert.ToInt32(ds.Rows[0]["SiSoCanTren"]);

                if (siSo >= siSoMin && siSo <= siSoMax)
                    return true;
                return false;
            }
        }

        public static string LaySTT(int autoNum)
        {
            if (autoNum < 10)
                return $"000{autoNum}";
            if (autoNum >= 10 && autoNum < 100)
                return $"00{autoNum}";
            if (autoNum >= 100 && autoNum < 1000)
                return $"0{autoNum}";
            if (autoNum >= 1000 && autoNum < 10000)
                return $"{autoNum}";
            return string.Empty;
        }
        public static QuyDinhDTO LayThongTinTruong()
        {
            QuyDinhDTO truong = new QuyDinhDTO();
            using (DataService ds = new DataService())
            {
                using (var sqlCommand = new SqlCommand("SELECT TenTruong, DiaChiTruong " + "FROM QUYDINH"))
                {
                    ds.Load(sqlCommand);
                }

                if (ds.Rows.Count > 0)
                {
                    truong.TenTruong = ds.Rows[0]["TenTruong"].ToString();
                    truong.DiaChiTruong = ds.Rows[0]["DiaChiTruong"].ToString();
                }

                return truong;
            }
        }
    }

    public static class ThamSo
    {
        public static frmAbout frmAbout;
        public static frmConnection frmConnection;
        public static frmDanToc frmDanToc;
        public static frptDanhSachGiaoVien frmDSGiaoVien;
        public static frptDanhSachHocSinh frmDSHocSinh;
        public static frptDanhSachLopHoc frmDSLopHoc;
        public static frmGiaoVien frmGiaoVien;
        public static frmHanhKiem frmHanhKiem;
        public static frmHocKy frmHocKy;
        public static frmHocLuc frmHocLuc;
        public static frmHocSinh frmHocSinh;
        public static frmKetQua frmKetQua;
        public static frptKetQuaCaNam_Lop frmKetQuaCaNam_Lop;
        public static frptKetQuaCaNam_MonHoc frmKetQuaCaNam_MonHoc;
        public static frptKetQuaHocKy_Lop frmKetQuaHocKy_Lop;
        public static frptKetQuaHocKy_MonHoc frmKetQuaHocKy_MonHoc;
        public static frmKhoiLop frmKhoiLop;
        public static frmLoaiDiem frmLoaiDiem;
        public static frmLoaiNguoiDung frmLoaiNguoiDung;
        public static frmLop frmLop;
        public static frmMain frmMain;
        public static frmMonHoc frmMonHoc;
        public static frmNamHoc frmNamHoc;
        public static frmNgheNghiep frmNgheNghiep;
        public static frmNhapDiemChung frmNhapDiemChung;
        public static frmNhapDiemRieng frmNhapDiemRieng;
        public static frmPhanCong frmPhanCong;
        public static frmPhanLop frmPhanLop;
        public static frmQuyDinh frmQuyDinh;
        public static frmTimKiemGV frmTimKiemGV;
        public static frmTimKiemHS frmTimKiemHS;
        public static frmTonGiao frmTonGiao;
        public static frmXemDiem frmXemDiem;

        public static void ShowFormDanhSachGiaoVien()
        {
            if (frmDSGiaoVien == null || frmDSGiaoVien.IsDisposed)
            {
                frmDSGiaoVien = new frptDanhSachGiaoVien
                {
                    MdiParent = Form.ActiveForm
                };
                frmDSGiaoVien.Show();
            }
            else
                frmDSGiaoVien.Activate();
        }

        public static void ShowFormDanhSachHocSinh()
        {
            if (frmDSHocSinh == null || frmDSHocSinh.IsDisposed)
            {
                frmDSHocSinh = new frptDanhSachHocSinh
                {
                    MdiParent = Form.ActiveForm
                };
                frmDSHocSinh.Show();
            }
            else
                frmDSHocSinh.Activate();
        }

        public static void ShowFormDanhSachLopHoc()
        {
            if (frmDSLopHoc == null || frmDSLopHoc.IsDisposed)
            {
                frmDSLopHoc = new frptDanhSachLopHoc
                {
                    MdiParent = Form.ActiveForm
                };
                frmDSLopHoc.Show();
            }
            else
                frmDSLopHoc.Activate();
        }

        public static void ShowFormDanToc()
        {
            if (frmDanToc == null || frmDanToc.IsDisposed)
            {
                frmDanToc = new frmDanToc
                {
                    MdiParent = Form.ActiveForm
                };
                frmDanToc.Show();
            }
            else
                frmDanToc.Activate();
        }

        public static void ShowFormGiaoVien()
        {
            if (frmGiaoVien == null || frmGiaoVien.IsDisposed)
            {
                frmGiaoVien = new frmGiaoVien
                {
                    MdiParent = Form.ActiveForm
                };
                frmGiaoVien.Show();
            }
            else
                frmGiaoVien.Activate();
        }

        public static void ShowFormHanhKiem()
        {
            if (frmHanhKiem == null || frmHanhKiem.IsDisposed)
            {
                frmHanhKiem = new frmHanhKiem
                {
                    MdiParent = Form.ActiveForm
                };
                frmHanhKiem.Show();
            }
            else
                frmHanhKiem.Activate();
        }

        public static void ShowFormHocKy()
        {
            if (frmHocKy == null || frmHocKy.IsDisposed)
            {
                frmHocKy = new frmHocKy
                {
                    MdiParent = Form.ActiveForm
                };
                frmHocKy.Show();
            }
            else
                frmHocKy.Activate();
        }

        public static void ShowFormHocLuc()
        {
            if (frmHocLuc == null || frmHocLuc.IsDisposed)
            {
                frmHocLuc = new frmHocLuc
                {
                    MdiParent = Form.ActiveForm
                };
                frmHocLuc.Show();
            }
            else
                frmHocLuc.Activate();
        }

        public static void ShowFormHocSinh()
        {
            if (frmHocSinh == null || frmHocSinh.IsDisposed)
            {
                frmHocSinh = new frmHocSinh
                {
                    MdiParent = Form.ActiveForm
                };
                frmHocSinh.Show();
            }
            else
                frmHocSinh.Activate();
        }

        public static void ShowFormKetNoi()
        {
            if (frmConnection == null || frmConnection.IsDisposed)
            {
                frmConnection = new frmConnection();
                frmConnection.Show();
            }
            else
                frmConnection.Activate();
        }

        public static void ShowFormKetQua()
        {
            if (frmKetQua == null || frmKetQua.IsDisposed)
            {
                frmKetQua = new frmKetQua
                {
                    MdiParent = Form.ActiveForm
                };
                frmKetQua.Show();
            }
            else
                frmKetQua.Activate();
        }

        public static void ShowFormKhoiLop()
        {
            if (frmKhoiLop == null || frmKhoiLop.IsDisposed)
            {
                frmKhoiLop = new frmKhoiLop
                {
                    MdiParent = Form.ActiveForm
                };
                frmKhoiLop.Show();
            }
            else
                frmKhoiLop.Activate();
        }

        public static void ShowFormKQCNTheoLop()
        {
            if (frmKetQuaCaNam_Lop == null || frmKetQuaCaNam_Lop.IsDisposed)
            {
                frmKetQuaCaNam_Lop = new frptKetQuaCaNam_Lop
                {
                    MdiParent = Form.ActiveForm
                };
                frmKetQuaCaNam_Lop.Show();
            }
            else
                frmKetQuaCaNam_Lop.Activate();
        }

        public static void ShowFormKQCNTheoMon()
        {
            if (frmKetQuaCaNam_MonHoc == null || frmKetQuaCaNam_MonHoc.IsDisposed)
            {
                frmKetQuaCaNam_MonHoc = new frptKetQuaCaNam_MonHoc
                {
                    MdiParent = Form.ActiveForm
                };
                frmKetQuaCaNam_MonHoc.Show();
            }
            else
                frmKetQuaCaNam_MonHoc.Activate();
        }

        public static void ShowFormKQHKTheoLop()
        {
            if (frmKetQuaHocKy_Lop == null || frmKetQuaHocKy_Lop.IsDisposed)
            {
                frmKetQuaHocKy_Lop = new frptKetQuaHocKy_Lop
                {
                    MdiParent = Form.ActiveForm
                };
                frmKetQuaHocKy_Lop.Show();
            }
            else
                frmKetQuaHocKy_Lop.Activate();
        }

        public static void ShowFormKQHKTheoMon()
        {
            if (frmKetQuaHocKy_MonHoc == null || frmKetQuaHocKy_MonHoc.IsDisposed)
            {
                frmKetQuaHocKy_MonHoc = new frptKetQuaHocKy_MonHoc
                {
                    MdiParent = Form.ActiveForm
                };
                frmKetQuaHocKy_MonHoc.Show();
            }
            else
                frmKetQuaHocKy_MonHoc.Activate();
        }

        public static void ShowFormLoaiDiem()
        {
            if (frmLoaiDiem == null || frmLoaiDiem.IsDisposed)
            {
                frmLoaiDiem = new frmLoaiDiem
                {
                    MdiParent = Form.ActiveForm
                };
                frmLoaiDiem.Show();
            }
            else
                frmLoaiDiem.Activate();
        }

        public static void ShowFormLoaiNguoiDung()
        {
            if (frmLoaiNguoiDung == null || frmLoaiNguoiDung.IsDisposed)
            {
                frmLoaiNguoiDung = new frmLoaiNguoiDung
                {
                    MdiParent = Form.ActiveForm
                };
                frmLoaiNguoiDung.Show();
            }
            else
                frmLoaiNguoiDung.Activate();
        }

        public static void ShowFormLopHoc()
        {
            if (frmLop == null || frmLop.IsDisposed)
            {
                frmLop = new frmLop
                {
                    MdiParent = Form.ActiveForm
                };
                frmLop.Show();
            }
            else
                frmLop.Activate();
        }

        public static void ShowFormMonHoc()
        {
            if (frmMonHoc == null || frmMonHoc.IsDisposed)
            {
                frmMonHoc = new frmMonHoc
                {
                    MdiParent = Form.ActiveForm
                };
                frmMonHoc.Show();
            }
            else
                frmMonHoc.Activate();
        }

        public static void ShowFormNamHoc()
        {
            if (frmNamHoc == null || frmNamHoc.IsDisposed)
            {
                frmNamHoc = new frmNamHoc
                {
                    MdiParent = Form.ActiveForm
                };
                frmNamHoc.Show();
            }
            else
                frmNamHoc.Activate();
        }

        public static void ShowFormNgheNghiep()
        {
            if (frmNgheNghiep == null || frmNgheNghiep.IsDisposed)
            {
                frmNgheNghiep = new frmNgheNghiep
                {
                    MdiParent = Form.ActiveForm
                };
                frmNgheNghiep.Show();
            }
            else
                frmNgheNghiep.Activate();
        }

        public static void ShowFormNhapDiemChung()
        {
            if (frmNhapDiemChung == null || frmNhapDiemChung.IsDisposed)
            {
                frmNhapDiemChung = new frmNhapDiemChung
                {
                    MdiParent = Form.ActiveForm
                };
                frmNhapDiemChung.Show();
            }
            else
                frmNhapDiemChung.Activate();
        }

        public static void ShowFormNhapDiemRieng()
        {
            if (frmNhapDiemRieng == null || frmNhapDiemRieng.IsDisposed)
            {
                frmNhapDiemRieng = new frmNhapDiemRieng
                {
                    MdiParent = Form.ActiveForm
                };
                frmNhapDiemRieng.Show();
            }
            else
                frmNhapDiemRieng.Activate();
        }

        public static void ShowFormPhanCong()
        {
            if (frmPhanCong == null || frmPhanCong.IsDisposed)
            {
                frmPhanCong = new frmPhanCong
                {
                    MdiParent = Form.ActiveForm
                };
                frmPhanCong.Show();
            }
            else
                frmPhanCong.Activate();
        }

        public static void ShowFormPhanLop()
        {
            if (frmPhanLop == null || frmPhanLop.IsDisposed)
            {
                frmPhanLop = new frmPhanLop
                {
                    MdiParent = Form.ActiveForm
                };
                frmPhanLop.Show();
            }
            else
                frmPhanLop.Activate();
        }

        public static void ShowFormQuyDinh()
        {
            if (frmQuyDinh == null || frmQuyDinh.IsDisposed)
            {
                frmQuyDinh = new frmQuyDinh();
                frmQuyDinh.Show();
            }
            else
                frmQuyDinh.Activate();
        }

        public static void ShowFormThongTin()
        {
            if (frmAbout == null || frmAbout.IsDisposed)
            {
                frmAbout = new frmAbout();
                frmAbout.Show();
            }
            else
                frmAbout.Activate();
        }

        public static void ShowFormTimKiemGV()
        {
            if (frmTimKiemGV == null || frmTimKiemGV.IsDisposed)
            {
                frmTimKiemGV = new frmTimKiemGV
                {
                    MdiParent = Form.ActiveForm
                };
                frmTimKiemGV.Show();
            }
            else
                frmTimKiemGV.Activate();
        }

        public static void ShowFormTimKiemHS()
        {
            if (frmTimKiemHS == null || frmTimKiemHS.IsDisposed)
            {
                frmTimKiemHS = new frmTimKiemHS
                {
                    MdiParent = Form.ActiveForm
                };
                frmTimKiemHS.Show();
            }
            else
                frmTimKiemHS.Activate();
        }

        public static void ShowFormTonGiao()
        {
            if (frmTonGiao == null || frmTonGiao.IsDisposed)
            {
                frmTonGiao = new frmTonGiao
                {
                    MdiParent = Form.ActiveForm
                };
                frmTonGiao.Show();
            }
            else
                frmTonGiao.Activate();
        }

        public static void ShowFormXemDiem()
        {
            if (frmXemDiem == null || frmXemDiem.IsDisposed)
            {
                frmXemDiem = new frmXemDiem
                {
                    MdiParent = Form.ActiveForm
                };
                frmXemDiem.Show();
            }
            else
                frmXemDiem.Activate();
        }
    }

    public static class XML
    {
        public static XmlDocument XMLReader(string filename)
        {
            XmlDocument xmlRead = new XmlDocument();
            try
            {
                xmlRead.Load(filename);
            }
            catch (Exception)
            {
                MessageBoxEx.Show($"Không đọc được hoặc không tồn tại tập tin cấu hình {filename}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return xmlRead;
        }

        public static void XMLWriter(string filename, string servername, string database, string constatus)
        {
            using (XmlTextWriter xmlWriter = new XmlTextWriter(filename, null))
            {
                xmlWriter.Formatting = Formatting.Indented;

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteComment("\nKhong duoc thay doi noi dung file nay!" + "\nThong so co ban:" + "\n\tcostatus = true : quyen Windows" + "\n\tcostatus = false: quyen SQL Server" + "\n\tservname: ten server" + "\n\tusername: ten dang nhap he thong" + "\n\tpassword: mat khau dang nhap he thong" + "\n\tdatabase: ten co so du lieu\n");
                xmlWriter.WriteStartElement("config");

                xmlWriter.WriteStartElement(nameof(constatus));
                xmlWriter.WriteString(constatus);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(nameof(servername));
                xmlWriter.WriteString(servername);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("username");
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("password");
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(nameof(database));
                xmlWriter.WriteString(database);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Close();
            }
        }

        public static void XMLWriter(string filename, string servername, string username, string password, string database, string constatus)
        {
            using (var xmlWriter = new XmlTextWriter(filename, null))
            {
                xmlWriter.Formatting = Formatting.Indented;

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteComment("\nKhong duoc thay doi noi dung file nay!" + "\nThong so co ban:" + "\n\tconstatus = true : quyen Windows" + "\n\tconstatus = false: quyen SQL Server" + "\n\tservername: ten server" + "\n\tusername: ten dang nhap he thong" + "\n\tpassword: mat khau dang nhap he thong" + "\n\tdatabase: ten co so du lieu\n");
                xmlWriter.WriteStartElement("config");

                xmlWriter.WriteStartElement(nameof(constatus));
                xmlWriter.WriteString(constatus);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(nameof(servername));
                xmlWriter.WriteString(servername);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(nameof(username));
                xmlWriter.WriteString(username);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(nameof(password));
                xmlWriter.WriteString(password);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(nameof(database));
                xmlWriter.WriteString(database);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Close();
            }
        }
    }
}