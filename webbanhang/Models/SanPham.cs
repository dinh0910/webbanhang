using System.ComponentModel.DataAnnotations;

namespace webbanhang.Models
{
    public class SanPham
    {
        public int SanPhamID { get; set; }

        public int DanhMucID { get; set; }
        public DanhMuc? DanhMuc { get; set; }

        public int ThuongHieuID { get; set; }
        public ThuongHieu? ThuongHieu { get; set; }

        public string? TenSanPham { get; set; }

        public string? HinhAnh { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int DonGia { get; set; }

        public int Sale { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int ThanhTien { get; set; }

        public int SoLuong { get; set; }
    }
}
