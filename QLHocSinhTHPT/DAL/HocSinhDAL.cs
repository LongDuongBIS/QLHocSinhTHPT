using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class HocSinhDAL
    {
        private readonly DataService hocSinhDS = new DataService();

        public DataTable LayDsHocSinh()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCSINH"))
            {
                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public DataTable LayDsHocSinhTheoLop(string namHoc, string lop)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT PL.MaHocSinh, HS.HoTen " + "FROM HOCSINH HS " + "INNER JOIN PHANLOP PL ON HS.MaHocSinh = PL.MaHocSinh " + "INNER JOIN LOP L ON L.MaLop = PL.MaLop " + "WHERE PL.MaLop = @lop AND PL.MaNamHoc = @namHoc"))
            {
                cmd.Parameters.Add("lop", SqlDbType.VarChar).Value = lop;
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;

                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public DataTable LayDsHocSinhTheoLop(string namHoc, string khoiLop, string lop)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT PL.MaHocSinh, HS.HoTen, L.TenLop " + "FROM HOCSINH HS " + "INNER JOIN PHANLOP PL ON HS.MaHocSinh = PL.MaHocSinh " + "INNER JOIN LOP L ON L.MaLop = PL.MaLop " + "INNER JOIN KHOILOP KL ON KL.MaKhoiLop = PL.MaKhoiLop " + "WHERE PL.MaLop = @lop AND PL.MaNamHoc = @namHoc AND PL.MaKhoiLop = @khoiLop"))
            {
                cmd.Parameters.Add("lop", SqlDbType.VarChar).Value = lop;
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;
                cmd.Parameters.Add("khoiLop", SqlDbType.VarChar).Value = khoiLop;

                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public DataTable LayDsHocSinhTheoNamHoc(string namHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT PL.MaHocSinh, HS.HoTen, L.TenLop " + "FROM HOCSINH HS " + "INNER JOIN PHANLOP PL ON HS.MaHocSinh = PL.MaHocSinh " + "INNER JOIN LOP L ON L.MaLop = PL.MaLop " + "WHERE PL.MaNamHoc = @namHoc"))
            {
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;

                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public void LuuHSVaoBangPhanLop(string maNamHoc, string maKhoiLop, string maLop, string maHS)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO PHANLOP " + "VALUES(@maNamHoc, @maKhoiLop, @maLop, @maHS)"))
            {
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maKhoiLop", SqlDbType.VarChar).Value = maKhoiLop;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maHS", SqlDbType.VarChar).Value = maHS;

                hocSinhDS.Load(cmd);
            }
        }

        public void XoaHSKhoiBangPhanLop(string maNamHoc, string maKhoiLop, string maLop, string maHS)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM PHANLOP " + "WHERE MaNamHoc = @maNamHoc AND MaKhoiLop = @maKhoiLop AND MaLop = @maLop AND MaHocSinh = @maHS"))
            {
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("maKhoiLop", SqlDbType.VarChar).Value = maKhoiLop;
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("maHS", SqlDbType.VarChar).Value = maHS;

                hocSinhDS.Load(cmd);
            }
        }

        public DataTable LayDsHocSinhForReport()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM NGHENGHIEP NNC, NGHENGHIEP NNM, HOCSINH HS, DANTOC DT, TONGIAO TG " + "WHERE HS.MaNNghiepCha = NNC.MaNghe AND HS.MaNNghiepMe = NNM.MaNghe AND HS.MaDanToc = DT.MaDanToc AND HS.MaTonGiao = TG.MaTonGiao"))
            {
                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public DataRow ThemDongMoi()
        {
            return hocSinhDS.NewRow();
        }

        public void ThemHocSinh(DataRow row)
        {
            hocSinhDS.Rows.Add(row);
        }

        public bool LuuHocSinh()
        {
            return hocSinhDS.ExecuteNonQuery() > 0;
        }

        public void LuuHocSinh(string maHocSinh, string hoTen, bool gioiTinh, DateTime ngaySinh, string noiSinh, string maDanToc, string maTonGiao, string hoTenCha, string maNgheCha, string hoTenMe, string maNgheMe)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO HOCSINH " + "VALUES(@maHocSinh, @hoTen, @gioiTinh, @ngaySinh, @noiSinh, @maDanToc, @maTonGiao, @hoTenCha, @maNgheCha, @hoTenMe, @maNgheMe)"))
            {
                cmd.Parameters.Add("maHocSinh", SqlDbType.VarChar).Value = maHocSinh;
                cmd.Parameters.Add("hoTen", SqlDbType.NVarChar).Value = hoTen;
                cmd.Parameters.Add("gioiTinh", SqlDbType.Bit).Value = gioiTinh;
                cmd.Parameters.Add("ngaySinh", SqlDbType.DateTime).Value = ngaySinh;
                cmd.Parameters.Add("noiSinh", SqlDbType.NVarChar).Value = noiSinh;
                cmd.Parameters.Add("maDanToc", SqlDbType.VarChar).Value = maDanToc;
                cmd.Parameters.Add("maTonGiao", SqlDbType.VarChar).Value = maTonGiao;
                cmd.Parameters.Add("hoTenCha", SqlDbType.NVarChar).Value = hoTenCha;
                cmd.Parameters.Add("maNgheCha", SqlDbType.VarChar).Value = maNgheCha;
                cmd.Parameters.Add("hoTenMe", SqlDbType.NVarChar).Value = hoTenMe;
                cmd.Parameters.Add("maNgheMe", SqlDbType.VarChar).Value = maNgheMe;

                hocSinhDS.Load(cmd);
            }
        }

        public DataTable TimTheoMa(string id)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCSINH " + "WHERE MaHocSinh LIKE '%' + @id + '%'"))
            {
                cmd.Parameters.Add("id", SqlDbType.VarChar).Value = id;

                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public DataTable TimTheoTen(string ten)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM HOCSINH " + "WHERE HoTen LIKE '%' + @ten + '%'"))
            {
                cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;

                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }

        public string TruyVanChung
        {
            get
            {
                return "SELECT HS.MaHocSinh, HS.HoTen, HS.GioiTinh, HS.NgaySinh, HS.NoiSinh, DT.TenDanToc, TG.TenTonGiao " + "FROM HOCSINH HS " + "INNER JOIN DANTOC DT ON HS.MaDanToc = DT.MaDanToc " + "INNER JOIN TONGIAO TG ON HS.MaTonGiao = TG.MaTonGiao";
            }
        }

        public DataTable TimKiemHocSinh(string hoTen, string theoNSinh, string noiSinh, string theoDToc, string danToc, string theoTGiao, string tonGiao)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string sql = string.Format("{0} " + "WHERE HS.HoTen LIKE '%' + @hoTen + '%' ", TruyVanChung);
                cmd.Parameters.Add("hoTen", SqlDbType.NVarChar).Value = hoTen;

                if (theoNSinh != "NONE")
                {
                    sql += string.Format("{0} " + "HS.NoiSinh LIKE '%' + @noiSinh + '%' ", theoNSinh);
                    cmd.Parameters.Add("noiSinh", SqlDbType.NVarChar).Value = noiSinh;
                }

                if (theoDToc != "NONE")
                {
                    sql += string.Format("{0} " + "DT.TenDanToc = @danToc ", theoDToc);
                    cmd.Parameters.Add("danToc", SqlDbType.NVarChar).Value = danToc;
                }

                if (theoTGiao != "NONE")
                {
                    sql += string.Format("{0} " + "TG.TenTonGiao = @tonGiao", theoTGiao);
                    cmd.Parameters.Add("tonGiao", SqlDbType.NVarChar).Value = tonGiao;
                }

                cmd.CommandText = sql;
                hocSinhDS.Load(cmd);
            }

            return hocSinhDS;
        }
    }
}