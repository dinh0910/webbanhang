using System.ComponentModel.DataAnnotations;

namespace webbanhang.Models
{
    public class ChiTietDH
    {
        public int ChiTietDHID { get; set; }

        public int DonDatHangID { get; set; }
        public DonDatHang? DonDatHang { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int ThanhTien { get; set; }
    }
}
