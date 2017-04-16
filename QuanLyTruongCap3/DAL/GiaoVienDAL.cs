using QuanLyTruongCap3.Components;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTruongCap3.DAL
{
    public class GiaoVienDAL
    {
        private readonly DataService giaoVienDS = new DataService();

        public DataTable LayDsGiaoVien()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM GIAOVIEN"))
            {
                giaoVienDS.Load(cmd);
            }

            return giaoVienDS;
        }

        public DataTable LayDsGiaoVienForReport()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM MONHOC M " + "INNER JOIN GIAOVIEN G ON G.MaMonHoc = M.MaMonHoc"))
            {
                giaoVienDS.Load(cmd);
            }

            return giaoVienDS;
        }

        public DataRow ThemDongMoi()
        {
            return giaoVienDS.NewRow();
        }

        public void ThemGiaoVien(DataRow row)
        {
            giaoVienDS.Rows.Add(row);
        }

        public bool LuuGiaoVien()
        {
            return giaoVienDS.ExecuteNonQuery() > 0;
        }

        public void LuuGiaoVien(string maGiaoVien, string tenGiaoVien, string diaChi, string dienThoai, string chuyenMon)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO GIAOVIEN " + "VALUES(@maGiaoVien, @tenGiaoVien, @diaChi, @dienThoai, @chuyenMon)"))
            {
                cmd.Parameters.Add("maGiaoVien", SqlDbType.VarChar).Value = maGiaoVien;
                cmd.Parameters.Add("tenGiaoVien", SqlDbType.NVarChar).Value = tenGiaoVien;
                cmd.Parameters.Add("diaChi", SqlDbType.NVarChar).Value = diaChi;
                cmd.Parameters.Add("dienThoai", SqlDbType.VarChar).Value = dienThoai;
                cmd.Parameters.Add("chuyenMon", SqlDbType.VarChar).Value = chuyenMon;

                giaoVienDS.Load(cmd);
            }
        }

        public DataTable TimTheoMa(string id)
        {
            using (var cmd = new SqlCommand("SELECT * " + "FROM GIAOVIEN " + "WHERE MaGiaoVien LIKE '%' + @id + '%'"))
            {
                cmd.Parameters.Add("id", SqlDbType.VarChar).Value = id;

                giaoVienDS.Load(cmd);
            }

            return giaoVienDS;
        }

        public DataTable TimTheoTen(string ten)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM GIAOVIEN " + "WHERE TenGiaoVien LIKE '%' + @ten + '%'"))
            {
                cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;

                giaoVienDS.Load(cmd);
            }

            return giaoVienDS;
        }

        public string TruyVanChung
        {
            get
            {
                return "SELECT G.MaGiaoVien, G.TenGiaoVien, G.DiaChi, G.DienThoai, H.TenMonHoc " + "FROM GIAOVIEN G " + "INNER JOIN MONHOC H ON G.MaMonHoc = H.MaMonHoc";
            }
        }

        public DataTable TimKiemGiaoVien(string hoTen, string theoDChi, string diaChi, string theoCMon, string cMon)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string sql = string.Format("{0} " + "WHERE G.TenGiaoVien LIKE '%' + @hoTen + '%' ", TruyVanChung);
                cmd.Parameters.Add("hoTen", SqlDbType.NVarChar).Value = hoTen;

                if (theoDChi != "NONE")
                {
                    sql += string.Format("{0} " + "G.DiaChi LIKE '%' + @diaChi + '%' ", theoDChi);
                    cmd.Parameters.Add("diaChi", SqlDbType.NVarChar).Value = diaChi;
                }

                if (theoCMon != "NONE")
                {
                    sql += string.Format("{0} " + "H.TenMonHoc = @cMon", theoCMon);
                    cmd.Parameters.Add("cMon", SqlDbType.NVarChar).Value = cMon;
                }

                cmd.CommandText = sql;
                giaoVienDS.Load(cmd);
            }

            return giaoVienDS;
        }
    }
}