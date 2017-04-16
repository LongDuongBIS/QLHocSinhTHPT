//Hoàn chỉnh
namespace QuanLyTruongCap3.DTO
{
    public class LopDTO
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public KhoiLopDTO KhoiLop { get; set; }
        public NamHocDTO NamHoc { get; set; }
        public int SiSo { get; set; }
        public GiaoVienDTO GiaoVien { get; set; }
    }
}