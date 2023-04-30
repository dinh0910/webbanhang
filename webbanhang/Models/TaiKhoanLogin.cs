namespace webbanhang.Models
{
    public class TaiKhoanLogin
    {
        public string? TenTaiKhoan { get; set; }

        public string MatKhau { get; set; }

        internal static Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
