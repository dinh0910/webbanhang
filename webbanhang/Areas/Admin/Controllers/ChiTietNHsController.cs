using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webbanhang.Data;
using webbanhang.Models;

namespace webbanhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietNHsController : Controller
    {
        private readonly webbanhangContext _context;

        public ChiTietNHsController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/ChiTietNHs
        public async Task<IActionResult> Index()
        {
            var webbanhangContext = _context.ChiTietNH.Include(c => c.DonViTinhs).Include(c => c.NhapHang).Include(c => c.SanPham);
            return View(await webbanhangContext.ToListAsync());
        }

        // GET: Admin/ChiTietNHs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChiTietNH == null)
            {
                return NotFound();
            }

            var chiTietNH = await _context.ChiTietNH
                .Include(c => c.DonViTinhs)
                .Include(c => c.NhapHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.ChiTietNHID == id);
            if (chiTietNH == null)
            {
                return NotFound();
            }

            return View(chiTietNH);
        }

        // GET: Admin/ChiTietNHs/Create
        public IActionResult Create()
        {
            ViewData["DonViTinhID"] = new SelectList(_context.Set<DonViTinh>(), "DonViTinhID", "DonViTinhID");
            ViewData["NhapHangID"] = new SelectList(_context.NhapHang, "NhapHangID", "NhapHangID");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: Admin/ChiTietNHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietNHID,NhapHangID,SanPhamID,DonViTinhID,DonGia,SoLuong,ThanhTien")] ChiTietNH chiTietNH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietNH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonViTinhID"] = new SelectList(_context.Set<DonViTinh>(), "DonViTinhID", "DonViTinhID", chiTietNH.DonViTinhID);
            ViewData["NhapHangID"] = new SelectList(_context.NhapHang, "NhapHangID", "NhapHangID", chiTietNH.NhapHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietNH.SanPhamID);
            return View(chiTietNH);
        }

        // GET: Admin/ChiTietNHs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChiTietNH == null)
            {
                return NotFound();
            }

            var chiTietNH = await _context.ChiTietNH.FindAsync(id);
            if (chiTietNH == null)
            {
                return NotFound();
            }
            ViewData["DonViTinhID"] = new SelectList(_context.Set<DonViTinh>(), "DonViTinhID", "DonViTinhID", chiTietNH.DonViTinhID);
            ViewData["NhapHangID"] = new SelectList(_context.NhapHang, "NhapHangID", "NhapHangID", chiTietNH.NhapHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietNH.SanPhamID);
            return View(chiTietNH);
        }

        // POST: Admin/ChiTietNHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietNHID,NhapHangID,SanPhamID,DonViTinhID,DonGia,SoLuong,ThanhTien")] ChiTietNH chiTietNH)
        {
            if (id != chiTietNH.ChiTietNHID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietNH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietNHExists(chiTietNH.ChiTietNHID))
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
            ViewData["DonViTinhID"] = new SelectList(_context.Set<DonViTinh>(), "DonViTinhID", "DonViTinhID", chiTietNH.DonViTinhID);
            ViewData["NhapHangID"] = new SelectList(_context.NhapHang, "NhapHangID", "NhapHangID", chiTietNH.NhapHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietNH.SanPhamID);
            return View(chiTietNH);
        }

        // GET: Admin/ChiTietNHs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChiTietNH == null)
            {
                return NotFound();
            }

            var chiTietNH = await _context.ChiTietNH
                .Include(c => c.DonViTinhs)
                .Include(c => c.NhapHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.ChiTietNHID == id);
            if (chiTietNH == null)
            {
                return NotFound();
            }

            return View(chiTietNH);
        }

        // POST: Admin/ChiTietNHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChiTietNH == null)
            {
                return Problem("Entity set 'webbanhangContext.ChiTietNH'  is null.");
            }
            var chiTietNH = await _context.ChiTietNH.FindAsync(id);
            if (chiTietNH != null)
            {
                _context.ChiTietNH.Remove(chiTietNH);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietNHExists(int id)
        {
          return (_context.ChiTietNH?.Any(e => e.ChiTietNHID == id)).GetValueOrDefault();
        }
    }
}
