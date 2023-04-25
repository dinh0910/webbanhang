namespace webbanhang.Models
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }

        public string? TenTaiKhoan { get; set; }

        public string? MatKhau { get; set; }

        public int QuyenHanID { get; set; }
        public QuyenHan? QuyenHan { get; set; }
    }
}
