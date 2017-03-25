using System.Data;
using System.Data.SqlClient;

namespace QLHocSinhTHPT.DAL
{
    public class MonHocDAL
    {
        private readonly DataService monHocDS = new DataService();

        public DataTable LayDsMonHoc()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM MONHOC"))
            {
                monHocDS.Load(cmd);
            }

            return monHocDS;
        }

        public DataTable LayDsMonHoc(string namHoc, string lop)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MH.MaMonHoc, MH.TenMonHoc, MH.HeSo " + "FROM MONHOC MH, PHANCONG PC " + "WHERE MH.MaMonHoc = PC.MaMonHoc AND PC.MaNamHoc = @namHoc AND PC.MaLop = @lop"))
            {
                cmd.Parameters.Add("namHoc", SqlDbType.VarChar).Value = namHoc;
                cmd.Parameters.Add("lop", SqlDbType.VarChar).Value = lop;

                monHocDS.Load(cmd);
            }

            return monHocDS;
        }

        public DataRow ThemDongMoi()
        {
            return monHocDS.NewRow();
        }

        public void ThemMonHoc(DataRow row)
        {
            monHocDS.Rows.Add(row);
        }

        public bool LuuMonHoc()
        {
            return monHocDS.ExecuteNonQuery() > 0;
        }
    }
}