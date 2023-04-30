using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using webbanhang.Data;
using webbanhang.Libs;
using webbanhang.Models;

namespace webbanhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly webbanhangContext _context;
        private readonly INotyfService _notifyService;

        public const string SessionTKAdmin = "_TaiKhoanID";
        public const string SessionHotenA = "_HoTen";
        public const string SessionTenDNA = "_TenTaiKhoan";
        public const string SessionEmailA = "_Email";
        public const string SessionSDTA = "_SDT";
        public const string SessionDiaChiA = "_DiaChi";
        public const string SessionQuyenA = "_QuyenID";

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
                                                                            && (r.QuyenHanID == 1 || r.QuyenHanID == 2));

                if (taiKhoan == null)
                {
                    _notifyService.Error("Đăng nhập không thành công!");
                }
                else
                {
                    // Đăng ký SESSION
                    HttpContext.Session.SetInt32(SessionTKAdmin, (int)taiKhoan.TaiKhoanID);
                    HttpContext.Session.SetString(SessionTenDNA, taiKhoan.TenTaiKhoan);
                    //HttpContext.Session.SetString(SessionHoten, taiKhoan.HoTen);
                    //HttpContext.Session.SetString(SessionEmail, taiKhoan.Email);
                    //HttpContext.Session.SetString(SessionSDT, taiKhoan.SoDienThoai);
                    //HttpContext.Session.SetString(SessionDiaChi, taiKhoan.DiaChi);
                    _notifyService.Success("Đăng nhập thành công!");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login", "Home");
        }

        [Route("/admin")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("_TaiKhoanID") == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("/admin/home")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("_TaiKhoanID") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("_TaiKhoanID");
            //HttpContext.Session.Remove("_Hoten");
            HttpContext.Session.Remove("_TenTaiKhoan");
            //HttpContext.Session.Remove("_Quyen");
            //HttpContext.Session.Remove("_HinhAnh");
            //HttpContext.Session.Remove("_Email");

            return RedirectToAction("Index", "Home");
        }
    }
}