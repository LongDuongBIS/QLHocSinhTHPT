using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class LopDAL : System.IDisposable
    {
        private readonly DataService lopDS = new DataService();

        public void Dispose()
        {
            lopDS.Dispose();
            System.GC.SuppressFinalize(this);
        }

        public DataTable LayDsLop()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOP"))
            {
                lopDS.Load(cmd);
            }

            return lopDS;
        }

        public DataTable LayDsLop(string namHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOP " + "WHERE MaNamHoc = @namHoc"))
            {
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;

                lopDS.Load(cmd);
            }

            return lopDS;
        }

        public DataTable LayDsLop(string khoiLop, string namHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOP " + "WHERE MaKhoiLop = @khoiLop AND MaNamHoc = @namHoc"))
            {
                cmd.Parameters.Add("khoiLop", SqlDbType.VarChar).Value = khoiLop;
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;

                lopDS.Load(cmd);
            }

            return lopDS;
        }

        public DataTable LayDsLopForReport()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM GIAOVIEN GV " + "INNER JOIN LOP L ON L.MaGiaoVien = GV.MaGiaoVien " + "INNER JOIN KHOILOP KL ON L.MaKhoiLop = KL.MaKhoiLop " + "INNER JOIN NAMHOC NH ON L.MaNamHoc = NH.MaNamHoc"))
            {
                lopDS.Load(cmd);
            }

            return lopDS;
        }

        public DataTable LayDsLopForReport(string namHoc)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM GIAOVIEN GV " + "INNER JOIN LOP L ON L.MaGiaoVien = GV.MaGiaoVien " + "INNER JOIN KHOILOP KL ON L.MaKhoiLop = KL.MaKhoiLop " + "INNER JOIN NAMHOC NH ON L.MaNamHoc = NH.MaNamHoc " + "WHERE L.MaNamHoc = @namHoc"))
            {
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;

                lopDS.Load(cmd);
            }

            return lopDS;
        }

        public bool LuuLop()
        {
            return lopDS.ExecuteNonQuery() > 0;
        }

        public void LuuLop(string maLop, string tenLop, string maKhoiLop, string maNamHoc, int siSo, string maGiaoVien)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO LOP " + "VALUES(@maLop, @tenLop, @maKhoiLop, @maNamHoc, @siSo, @maGiaoVien)"))
            {
                cmd.Parameters.Add("maLop", SqlDbType.VarChar).Value = maLop;
                cmd.Parameters.Add("tenLop", SqlDbType.NVarChar).Value = tenLop;
                cmd.Parameters.Add("maKhoiLop", SqlDbType.VarChar).Value = maKhoiLop;
                cmd.Parameters.Add("maNamHoc", SqlDbType.VarChar).Value = maNamHoc;
                cmd.Parameters.Add("siSo", SqlDbType.Int).Value = siSo;
                cmd.Parameters.Add("maGiaoVien", SqlDbType.VarChar).Value = maGiaoVien;

                lopDS.Load(cmd);
            }
        }

        public DataRow ThemDongMoi()
        {
            return lopDS.NewRow();
        }

        public void ThemLop(DataRow row)
        {
            lopDS.Rows.Add(row);
        }

        public DataTable TimTheoMa(string id)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOP " + "WHERE MaLop LIKE '%' + @id + '%'"))
            {
                cmd.Parameters.Add("id", SqlDbType.VarChar).Value = id;

                lopDS.Load(cmd);
            }

            return lopDS;
        }

        public DataTable TimTheoTen(string ten)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM LOP " + "WHERE TenLop LIKE '%' + @ten + '%'"))
            {
                cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;

                lopDS.Load(cmd);
            }

            return lopDS;
        }
    }
}