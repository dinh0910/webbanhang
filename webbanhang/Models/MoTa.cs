namespace webbanhang.Models
{
    public class MoTa
    {
        public int MoTaID { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public string? NoiDungMoTa { get; set; }
    }
}
