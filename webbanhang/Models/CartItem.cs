namespace webbanhang.Models
{
    public class CartItem
    {
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public int SoLuong { get; set; }
    }

    public class ImportItem
    {
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public int DonViTinhID { get; set; }
        public DonViTinh DonViTinh { get; set; }

        public int SoLuong { get; set; }
    }

    public class CartLove
    {
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
