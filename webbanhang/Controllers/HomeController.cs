using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using webbanhang.Data;
using webbanhang.Libs;
using webbanhang.Models;

namespace webbanhang.Controllers
{
    public class HomeController : Controller
    {
        private readonly webbanhangContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notifyService;

        public const string SessionTK = "_TaiKhoanIDU";
        public const string SessionHoten = "_HoTenU";
        public const string SessionTenDN = "_TenTaiKhoanU";
        public const string SessionMK = "_MatKhauU";
        public const string SessionEmail = "_EmailU";
        public const string SessionSDT = "_SDTU";
        public const string SessionDiaChi = "_DiaChiU";

        public HomeController(ILogger<HomeController> logger, webbanhangContext context, INotyfService notyfService)
        {
            _logger = logger;
            _context = context;
            _notifyService = notyfService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(TaiKhoanLogin TaiKhoan)
        {
            if (ModelState.IsValid)
            {
                string mahoamatkhau = SHA1.ComputeHash(TaiKhoan.MatKhau);
                var taiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(r => r.TenTaiKhoan == TaiKhoan.TenTaiKhoan
                                                                            && r.MatKhau == mahoamatkhau
                                                                            && r.QuyenHanID == 3);
                if (taiKhoan == null)
                {
                    _notifyService.Error("Đăng nhập không thành công!");
                }
                else
                {
                    // Đăng ký SESSION
                    HttpContext.Session.SetInt32(SessionTK, (int)taiKhoan.TaiKhoanID);
                    HttpContext.Session.SetString(SessionTenDN, taiKhoan.TenTaiKhoan);
                    HttpContext.Session.SetString(SessionMK, taiKhoan.MatKhau);
                    //HttpContext.Session.SetString(SessionHoten, taiKhoan.HoTen);
                    //HttpContext.Session.SetString(SessionEmail, taiKhoan.Email);
                    //HttpContext.Session.SetString(SessionSDT, taiKhoan.SoDienThoai);
                    //HttpContext.Session.SetString(SessionDiaChi, taiKhoan.DiaChi);

                    _notifyService.Success("Đăng nhập thành công!");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(TaiKhoan);
        }

        public async Task<IActionResult> Index()
        {
            var sp = _context.SanPham.Include(s => s.DanhMuc).Include(s => s.ThuongHieu);
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View(await sp.ToListAsync());
        }

        public IActionResult Login()
        {
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View();
        }
        
        public IActionResult SignUp()
        {
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View();
        }

        public async Task<IActionResult> Category(int? id)
        {
            var category = _context.SanPham
                            .Include(s => s.DanhMuc)
                            .Include(s => s.ThuongHieu)
                            .Where(s => s.DanhMucID == id);
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            ViewBag.dm = _context.DanhMuc.FirstOrDefault(d => d.DanhMucID == id);
            return View(await category.ToListAsync());
        }

        public async Task<IActionResult> Details()
        {
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View();
        }

        public async Task<IActionResult> TradeMark(int? id)
        {
            var trademark = _context.SanPham
                            .Include(s => s.DanhMuc)
                            .Include(s => s.ThuongHieu)
                            .Where(s => s.ThuongHieuID == id);
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            ViewBag.th = _context.ThuongHieu.FirstOrDefault(t => t.ThuongHieuID == id);
            return View(await trademark.ToListAsync());
        }

        public const string CARTKEY = "shopcart";

        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        void SaveCartSession(List<CartItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString(CARTKEY, jsoncart);
        }

        // Xóa session giỏ hàng
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.SanPham
                .FirstOrDefaultAsync(m => m.SanPhamID == id);
            if (product == null)
            {
                _notifyService.Error("Sản phẩm không tồn tại.");
            }
            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.SanPhamID == id);
            if (item != null)
            {
                item.SoLuong++;
            }
            else
            {
                cart.Add(new CartItem() { SanPham = product, SoLuong = 1 });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        public async Task<IActionResult> UpdateItem(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.SanPhamID == id);
            if (quantity == 0)
            {
                cart.Remove(item);
            }
            item.SoLuong = quantity;
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.SanPhamID == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewCart));
        }

        public IActionResult ViewCart()
        {
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View(GetCartItems());
        }

        public IActionResult CheckOut()
        {
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View(GetCartItems());
        }

        public async Task<IActionResult> CreateBill(string Ten, string SoDienThoai, string DiaChi, string GhiChu)
        {
            // lưu hóa đơn
            var bill = new DonDatHang();
            bill.NgayLap = DateTime.Now;
            bill.HoTen = Ten;
            bill.Sdt = SoDienThoai;
            bill.DiaChi = DiaChi;
            bill.GhiChu = GhiChu;

            _context.Add(bill);
            await _context.SaveChangesAsync();

            var cart = GetCartItems();
            int amount = 0;
            int soLuong = 0;

            //chi tiết hóa đơn
            foreach (var i in cart)
            {
                var b = new ChiTietDH();
                b.DonDatHangID = bill.DonDatHangID;
                b.SanPhamID = i.SanPham.SanPhamID;
                b.DonGia = i.SanPham.ThanhTien;
                b.SoLuong = i.SoLuong;
                amount = i.SanPham.ThanhTien * i.SoLuong;
                b.ThanhTien = amount;

                var sp = _context.SanPham.FirstOrDefault(s => s.SanPhamID == b.SanPhamID);
                sp.SoLuong -= i.SoLuong;
                bill.TongTien += amount;
                _context.Add(b);
            }
            await _context.SaveChangesAsync();
            ClearCart();
            return RedirectToAction(nameof(Message));
        }

        public IActionResult Message()
        {
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.thuonghieu = _context.ThuongHieu;
            return View();
        }
    }
}