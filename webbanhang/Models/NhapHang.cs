using System.ComponentModel.DataAnnotations;

namespace webbanhang.Models
{
    public class NhapHang
    {
        public int NhapHangID { get; set; }

        public int TaiKhoanID { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }

        public int NhaCungCapID { get; set; }
        public NhaCungCap? NhaCungCap { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayLap { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int TongTien { get; set; }
    }
}
