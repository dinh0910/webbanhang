using System.ComponentModel.DataAnnotations;

namespace webbanhang.Models
{
    public class ChiTietNH
    {
        public int ChiTietNHID { get; set; }

        public int NhapHangID { get; set; }
        public NhapHang? NhapHang { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public int DonViTinhID { get; set; }
        public DonViTinh? DonViTinhs { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int ThanhTien { get; set; }
    }
}
