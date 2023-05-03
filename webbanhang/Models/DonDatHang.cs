using System.ComponentModel.DataAnnotations;

namespace webbanhang.Models
{
    public class DonDatHang
    {
        public int DonDatHangID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayLap { get; set; }

        public string? HoTen { get; set; }

        public string? Sdt { get; set; }

        public string? DiaChi { get; set; }

        public string? GhiChu { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int TongTien { get; set; }


    }
}
