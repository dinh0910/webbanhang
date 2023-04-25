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
    public class ChiTietDHsController : Controller
    {
        private readonly webbanhangContext _context;

        public ChiTietDHsController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/ChiTietDHs
        public async Task<IActionResult> Index()
        {
            var webbanhangContext = _context.ChiTietDH.Include(c => c.DonDatHang).Include(c => c.SanPham);
            return View(await webbanhangContext.ToListAsync());
        }

        // GET: Admin/ChiTietDHs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChiTietDH == null)
            {
                return NotFound();
            }

            var chiTietDH = await _context.ChiTietDH
                .Include(c => c.DonDatHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.ChiTietDHID == id);
            if (chiTietDH == null)
            {
                return NotFound();
            }

            return View(chiTietDH);
        }

        // GET: Admin/ChiTietDHs/Create
        public IActionResult Create()
        {
            ViewData["DonDatHangID"] = new SelectList(_context.DonDatHang, "DonDatHangID", "DonDatHangID");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: Admin/ChiTietDHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietDHID,DonDatHangID,SanPhamID,DonGia,SoLuong,ThanhTien")] ChiTietDH chiTietDH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonDatHangID"] = new SelectList(_context.DonDatHang, "DonDatHangID", "DonDatHangID", chiTietDH.DonDatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietDH.SanPhamID);
            return View(chiTietDH);
        }

        // GET: Admin/ChiTietDHs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChiTietDH == null)
            {
                return NotFound();
            }

            var chiTietDH = await _context.ChiTietDH.FindAsync(id);
            if (chiTietDH == null)
            {
                return NotFound();
            }
            ViewData["DonDatHangID"] = new SelectList(_context.DonDatHang, "DonDatHangID", "DonDatHangID", chiTietDH.DonDatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietDH.SanPhamID);
            return View(chiTietDH);
        }

        // POST: Admin/ChiTietDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietDHID,DonDatHangID,SanPhamID,DonGia,SoLuong,ThanhTien")] ChiTietDH chiTietDH)
        {
            if (id != chiTietDH.ChiTietDHID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDHExists(chiTietDH.ChiTietDHID))
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
            ViewData["DonDatHangID"] = new SelectList(_context.DonDatHang, "DonDatHangID", "DonDatHangID", chiTietDH.DonDatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", chiTietDH.SanPhamID);
            return View(chiTietDH);
        }

        // GET: Admin/ChiTietDHs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChiTietDH == null)
            {
                return NotFound();
            }

            var chiTietDH = await _context.ChiTietDH
                .Include(c => c.DonDatHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.ChiTietDHID == id);
            if (chiTietDH == null)
            {
                return NotFound();
            }

            return View(chiTietDH);
        }

        // POST: Admin/ChiTietDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChiTietDH == null)
            {
                return Problem("Entity set 'webbanhangContext.ChiTietDH'  is null.");
            }
            var chiTietDH = await _context.ChiTietDH.FindAsync(id);
            if (chiTietDH != null)
            {
                _context.ChiTietDH.Remove(chiTietDH);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDHExists(int id)
        {
          return (_context.ChiTietDH?.Any(e => e.ChiTietDHID == id)).GetValueOrDefault();
        }
    }
}
