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
    public class NhapHangsController : Controller
    {
        private readonly webbanhangContext _context;

        public NhapHangsController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/NhapHangs
        public async Task<IActionResult> Index()
        {
            var webbanhangContext = _context.NhapHang.Include(n => n.NhaCungCap).Include(n => n.TaiKhoan);
            return View(await webbanhangContext.ToListAsync());
        }

        // GET: Admin/NhapHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhapHang == null)
            {
                return NotFound();
            }

            var nhapHang = await _context.NhapHang
                .Include(n => n.NhaCungCap)
                .Include(n => n.TaiKhoan)
                .FirstOrDefaultAsync(m => m.NhapHangID == id);
            if (nhapHang == null)
            {
                return NotFound();
            }

            return View(nhapHang);
        }

        // GET: Admin/NhapHangs/Create
        public IActionResult Create()
        {
            ViewData["NhaCungCapID"] = new SelectList(_context.Set<NhaCungCap>(), "NhaCungCapID", "NhaCungCapID");
            ViewData["TaiKhoanID"] = new SelectList(_context.TaiKhoan, "TaiKhoanID", "TaiKhoanID");
            return View();
        }

        // POST: Admin/NhapHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhapHangID,TaiKhoanID,NhaCungCapID,NgayLap,TongTien")] NhapHang nhapHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhapHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhaCungCapID"] = new SelectList(_context.Set<NhaCungCap>(), "NhaCungCapID", "NhaCungCapID", nhapHang.NhaCungCapID);
            ViewData["TaiKhoanID"] = new SelectList(_context.TaiKhoan, "TaiKhoanID", "TaiKhoanID", nhapHang.TaiKhoanID);
            return View(nhapHang);
        }

        // GET: Admin/NhapHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhapHang == null)
            {
                return NotFound();
            }

            var nhapHang = await _context.NhapHang.FindAsync(id);
            if (nhapHang == null)
            {
                return NotFound();
            }
            ViewData["NhaCungCapID"] = new SelectList(_context.Set<NhaCungCap>(), "NhaCungCapID", "NhaCungCapID", nhapHang.NhaCungCapID);
            ViewData["TaiKhoanID"] = new SelectList(_context.TaiKhoan, "TaiKhoanID", "TaiKhoanID", nhapHang.TaiKhoanID);
            return View(nhapHang);
        }

        // POST: Admin/NhapHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhapHangID,TaiKhoanID,NhaCungCapID,NgayLap,TongTien")] NhapHang nhapHang)
        {
            if (id != nhapHang.NhapHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhapHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhapHangExists(nhapHang.NhapHangID))
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
            ViewData["NhaCungCapID"] = new SelectList(_context.Set<NhaCungCap>(), "NhaCungCapID", "NhaCungCapID", nhapHang.NhaCungCapID);
            ViewData["TaiKhoanID"] = new SelectList(_context.TaiKhoan, "TaiKhoanID", "TaiKhoanID", nhapHang.TaiKhoanID);
            return View(nhapHang);
        }

        // GET: Admin/NhapHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhapHang == null)
            {
                return NotFound();
            }

            var nhapHang = await _context.NhapHang
                .Include(n => n.NhaCungCap)
                .Include(n => n.TaiKhoan)
                .FirstOrDefaultAsync(m => m.NhapHangID == id);
            if (nhapHang == null)
            {
                return NotFound();
            }

            return View(nhapHang);
        }

        // POST: Admin/NhapHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NhapHang == null)
            {
                return Problem("Entity set 'webbanhangContext.NhapHang'  is null.");
            }
            var nhapHang = await _context.NhapHang.FindAsync(id);
            if (nhapHang != null)
            {
                _context.NhapHang.Remove(nhapHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhapHangExists(int id)
        {
          return (_context.NhapHang?.Any(e => e.NhapHangID == id)).GetValueOrDefault();
        }
    }
}
