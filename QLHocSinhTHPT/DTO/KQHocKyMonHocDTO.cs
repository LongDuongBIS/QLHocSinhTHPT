//Hoàn chỉnh
namespace QLHocSinhTHPT.DTO
{
    public class KQHocKyMonHocDTO
    {
        public HocSinhDTO HocSinh { get; set; }
        public LopDTO Lop { get; set; }
        public MonHocDTO MonHoc { get; set; }
        public HocKyDTO HocKy { get; set; }
        public NamHocDTO NamHoc { get; set; }
        public float DTBKiemTra { get; set; }
        public float DTBMonHocKy { get; set; }
    }
}