//Hoàn chỉnh
namespace QLHocSinhTHPT.DTO
{
    public class DiemDTO
    {
        public DiemDTO()
        {
            HocSinh = new HocSinhDTO();
            MonHoc = new MonHocDTO();
            HocKy = new HocKyDTO();
            NamHoc = new NamHocDTO();
            LoaiDiem = new LoaiDiemDTO();
            Lop = new LopDTO();
            Diem = 0;
        }

        public HocSinhDTO HocSinh { get; set; }
        public MonHocDTO MonHoc { get; set; }
        public HocKyDTO HocKy { get; set; }
        public NamHocDTO NamHoc { get; set; }
        public LopDTO Lop { get; set; }
        public LoaiDiemDTO LoaiDiem { get; set; }
        public float Diem { get; set; }
    }
}