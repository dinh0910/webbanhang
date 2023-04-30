using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using webbanhang.Data;
using webbanhang.Models;

namespace webbanhang.Controllers
{
    public class HomeController : Controller
    {
        private readonly webbanhangContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notifyService;



        public HomeController(ILogger<HomeController> logger, webbanhangContext context, INotyfService notyfService)
        {
            _logger = logger;
            _context = context;
            _notifyService = notyfService;
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
    }
}