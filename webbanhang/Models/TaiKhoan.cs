namespace webbanhang.Models
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }

        public string? TenTaiKhoan { get; set; }

        public string? MatKhau { get; set; }

        public string? HoTen { get; set; }

        public string? Sdt { get; set; }

        public string? DiaChi { get; set; }

        public int QuyenHanID { get; set; }
        public QuyenHan? QuyenHan { get; set; }
    }
}
