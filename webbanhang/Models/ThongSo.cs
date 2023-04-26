namespace webbanhang.Models
{
    public class ThongSo
    {
        public int ThongSoID { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public string? TenThongSo { get; set; }

        public string? NoiDung { get; set; }
    }
}
