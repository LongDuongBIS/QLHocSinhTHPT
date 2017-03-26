using System.Data;
using System.Data.SqlClient;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT.DAL
{
    public class QuyDinhDAL
    {
        private readonly DataService quyDinhDS = new DataService();

        public DataTable LayDsQuyDinh()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * " + "FROM QUYDINH"))
            {
                quyDinhDS.Load(cmd);
            }

            return quyDinhDS;
        }

        public void CapNhatQuyDinhSiSo(int siSoCanDuoi, int siSoCanTren)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE QUYDINH " + "SET SiSoCanDuoi = @siSoCanDuoi, SiSoCanTren = @siSoCanTren"))
            {
                cmd.Parameters.Add("siSoCanDuoi", SqlDbType.Int).Value = siSoCanDuoi;
                cmd.Parameters.Add("siSoCanTren", SqlDbType.Int).Value = siSoCanTren;

                quyDinhDS.Load(cmd);
            }
        }

        public void CapNhatQuyDinhDoTuoi(int tuoiCanDuoi, int tuoiCanTren)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE QUYDINH " + "SET TuoiCanDuoi = @tuoiCanDuoi, TuoiCanTren = @tuoiCanTren"))
            {
                cmd.Parameters.Add("tuoiCanDuoi", SqlDbType.Int).Value = tuoiCanDuoi;
                cmd.Parameters.Add("tuoiCanTren", SqlDbType.Int).Value = tuoiCanTren;

                quyDinhDS.Load(cmd);
            }
        }

        public void CapNhatQuyDinhTruong(string tenTruong, string diaChiTruong)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE QUYDINH " + "SET TenTruong = @tenTruong, DiaChiTruong = @diaChiTruong"))
            {
                cmd.Parameters.Add("tenTruong", SqlDbType.NVarChar).Value = tenTruong;
                cmd.Parameters.Add("diaChiTruong", SqlDbType.NVarChar).Value = diaChiTruong;

                quyDinhDS.Load(cmd);
            }
        }

        public void CapNhatQuyDinhThangDiem(int thangDiem)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE QUYDINH " + "SET ThangDiem = @thangDiem"))
            {
                cmd.Parameters.Add("thangDiem", SqlDbType.Int).Value = thangDiem;

                quyDinhDS.Load(cmd);
            }
        }
    }
}