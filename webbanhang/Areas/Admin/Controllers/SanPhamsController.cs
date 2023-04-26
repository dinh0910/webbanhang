using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create([Bind("SanPhamID,DanhMucID,ThuongHieuID,TenSanPham,HinhAnh,DonGia,Sale,ThanhTien,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
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
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "DanhMucID", sanPham.DanhMucID);
            ViewData["ThuongHieuID"] = new SelectList(_context.ThuongHieu, "ThuongHieuID", "ThuongHieuID", sanPham.ThuongHieuID);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SanPhamID,DanhMucID,ThuongHieuID,TenSanPham,HinhAnh,DonGia,Sale,ThanhTien,SoLuong")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "DanhMucID", sanPham.DanhMucID);
            ViewData["ThuongHieuID"] = new SelectList(_context.ThuongHieu, "ThuongHieuID", "ThuongHieuID", sanPham.ThuongHieuID);
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
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }


    }
}
