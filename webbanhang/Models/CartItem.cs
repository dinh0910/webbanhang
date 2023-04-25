namespace webbanhang.Models
{
    public class CartItem
    {
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public int SoLuong { get; set; }
    }
}
