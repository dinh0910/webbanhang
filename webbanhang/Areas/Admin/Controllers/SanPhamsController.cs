using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webbanhang.Data;
using webbanhang.Models;

namespace webbanhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly webbanhangContext _context;
        private readonly INotyfService _notifyService;

        public SanPhamsController(webbanhangContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/SanPhams
        public async Task<IActionResult> Index()
        {
            var webbanhangContext = _context.SanPham.Include(s => s.DanhMuc).Include(s => s.ThuongHieu);
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "TenDanhMuc");
            ViewData["ThuongHieuID"] = new SelectList(_context.ThuongHieu, "ThuongHieuID", "TenThuongHieu");
            return View(await webbanhangContext.ToListAsync());
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("SanPhamID,DanhMucID,ThuongHieuID,TenSanPham,HinhAnh,DonGia,Sale,ThanhTien,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.HinhAnh = Upload(file);
                _context.Add(sanPham);
                _notifyService.Success("Thêm thành công!");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            ViewData["ThuongHieuID"] = new SelectList(_context.ThuongHieu, "ThuongHieuID", "TenThuongHieu", sanPham.ThuongHieuID);
            _notifyService.Error("Thêm thất bại!");
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "TenDanhMuc");
            ViewData["ThuongHieuID"] = new SelectList(_context.ThuongHieu, "ThuongHieuID", "TenThuongHieu");
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("SanPhamID,DanhMucID,ThuongHieuID,TenSanPham,HinhAnh,DonGia,Sale,ThanhTien,SoLuong")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sanPham.HinhAnh = Upload(file);
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.SanPhamID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "TenDanhMuc", sanPham.DanhMucID);
            ViewData["ThuongHieuID"] = new SelectList(_context.ThuongHieu, "ThuongHieuID", "TenThuongHieu", sanPham.ThuongHieuID);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.DanhMuc)
                .Include(s => s.ThuongHieu)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            else
            {
                _notifyService.Success("Xóa thành công!");
                _context.SanPham.Remove(sanPham);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
          return (_context.SanPham?.Any(e => e.SanPhamID == id)).GetValueOrDefault();
        }

        // GET: Admin/SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.DanhMuc)
                .Include(s => s.ThuongHieu)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);

            ViewBag.thongso = _context.ThongSo;
            ViewBag.hinhanh = _context.HinhAnh;
            ViewBag.mota = _context.MoTa;

            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("ThongSoID,SanPhamID,TenThongSo,NoiDung")] ThongSo thongSo,
            IFormFile file, [Bind("HinhAnhID,SanPhamID,Anh,Active")] HinhAnh hinhAnh, [Bind("MoTaID,SanPhamID,NoiDungMoTa")] MoTa moTa)
        {
            if(thongSo.TenThongSo != null || thongSo.NoiDung != null)
            {
                _context.Update(thongSo);
                await _context.SaveChangesAsync();
            }
            else if (moTa.NoiDungMoTa != null)
            {
                _context.Update(moTa);
                await _context.SaveChangesAsync();
            }
            else
            {
                hinhAnh.Anh = Upload(file);
                _context.Update(hinhAnh);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }
        public async Task<IActionResult> DeleteThongSo(int? id)
        {
            var ts = await _context.ThongSo
                .FirstOrDefaultAsync(m => m.SanPhamID == id);

            _context.ThongSo.Remove(ts);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }

        public async Task<IActionResult> DeleteMoTa(int? id)
        {
            var tt = await _context.MoTa
                .FirstOrDefaultAsync(m => m.MoTaID == id);

            _context.MoTa.Remove(tt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }

        public async Task<IActionResult> DeleteHinhAnh(int? id)
        {
            var tt = await _context.HinhAnh
                .FirstOrDefaultAsync(m => m.HinhAnhID == id);

            _context.HinhAnh.Remove(tt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }


        public string Upload(IFormFile file)
        {
            string fn = null;

            if (file != null)
            {
                // Phát sinh tên mới cho file để tránh trùng tên
                fn = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\products\\{fn}"; // đường dẫn lưu file
                // upload file lên đường dẫn chỉ định
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return fn;
        }

        public const string CARTIMPORT = "addcart";

        public async Task<IActionResult> Stored()
        {
            if (HttpContext.Session.GetInt32("_TaiKhoanID") != null)
            {
                var webbanlaptopContext = _context.NhapHang.Include(n => n.NhaCungCap).Include(n => n.TaiKhoan);
                return View(await webbanlaptopContext.ToListAsync());
            }
            return RedirectToAction("Login", "Home");
        }

        public async Task<IActionResult> DetailImport(int? id)
        {
            var webbanlaptopContext = _context.ChiTietNH
                .Include(c => c.NhapHang)
                .Include(c => c.SanPham)
                .Include(c => c.DonViTinhs)
                .Where(m => m.NhapHangID == id);

            ViewBag.nh = _context.NhapHang.Include(s => s.TaiKhoan).Include(s => s.NhaCungCap).FirstOrDefault(n => n.NhapHangID == id);

            return View(await webbanlaptopContext.ToListAsync());
        }

        List<ImportItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTIMPORT);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<ImportItem>>(jsoncart);
            }
            return new List<ImportItem>();
        }

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<ImportItem> list)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(list);
            session.SetString("addcart", jsoncart);
        }

        // Xóa session giỏ hàng
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("addcart");
        }

        public async Task<IActionResult> AddToCart([Bind("SanPhamID,DonViTinhID,SoLuong")] ImportItem importItem)
        {
            var product = await _context.SanPham
                .FirstOrDefaultAsync(m => m.SanPhamID == importItem.SanPhamID);
            var dvt = await _context.DonViTinh
                .FirstOrDefaultAsync(m => m.DonViTinhID == importItem.DonViTinhID);
            var cart = GetCartItems();
            var item = cart.Find(p => p.SanPham.SanPhamID == importItem.SanPhamID);
            if (item != null)
            {
                item.SoLuong += importItem.SoLuong;
            }
            else
            {
                cart.Add(new ImportItem() { SanPham = product, DonViTinh = dvt, SoLuong = importItem.SoLuong });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(ViewImport));
        }

        [Route("/updateitem", Name = "updateitem")]
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
            return RedirectToAction(nameof(ViewImport));
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
            return RedirectToAction(nameof(ViewImport));
        }

        [Route("/viewimport", Name = "import")]
        public IActionResult ViewImport()
        {
            ViewData["NhaCungCapID"] = new SelectList(_context.NhaCungCap, "NhaCungCapID", "Ten");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "TenSanPham");
            ViewData["DonViTinhID"] = new SelectList(_context.DonViTinh, "DonViTinhID", "DonVi");
            return View(GetCartItems());
        }

        public async Task<IActionResult> CreateBill(int NhaCungCapID)
        {
            // lưu hóa đơn
            var bill = new NhapHang();
            bill.NgayLap = DateTime.Now;
            bill.NhaCungCapID = NhaCungCapID;
            bill.TaiKhoanID = (int)HttpContext.Session.GetInt32("_TaiKhoanID");

            _context.Add(bill);
            await _context.SaveChangesAsync();

            var cart = GetCartItems();
            int amount = 0;
            int soLuong = 0;
            //chi tiết hóa đơn
            foreach (var i in cart)
            {
                var b = new ChiTietNH();
                b.NhapHangID = bill.NhapHangID;
                b.SanPhamID = i.SanPham.SanPhamID;
                b.DonViTinhID = i.DonViTinh.DonViTinhID;
                b.DonGia = i.SanPham.ThanhTien;
                b.SoLuong = i.SoLuong;
                amount = i.SanPham.ThanhTien * i.SoLuong;
                b.ThanhTien = amount;

                var sp = _context.SanPham.FirstOrDefault(s => s.SanPhamID == b.SanPhamID);
                sp.SoLuong += i.SoLuong;
                bill.TongTien += amount;
                _context.Add(b);
            }
            await _context.SaveChangesAsync();
            ClearCart();
            return RedirectToAction(nameof(Stored));
        }
    }
}
