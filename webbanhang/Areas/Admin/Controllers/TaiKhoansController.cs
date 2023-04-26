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
    public class TaiKhoansController : Controller
    {
        private readonly webbanhangContext _context;

        public TaiKhoansController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/TaiKhoans
        public async Task<IActionResult> Index()
        {
            var webbanhangContext = _context.TaiKhoan.Include(t => t.QuyenHan);
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "Ten");
            return View(await webbanhangContext.ToListAsync());
        }

        // GET: Admin/TaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaiKhoan == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan
                .Include(t => t.QuyenHan)
                .FirstOrDefaultAsync(m => m.TaiKhoanID == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public IActionResult Create()
        {
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "QuyenHanID");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoanID,TenTaiKhoan,MatKhau,QuyenHanID")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "QuyenHanID", taiKhoan.QuyenHanID);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaiKhoan == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "QuyenHanID", taiKhoan.QuyenHanID);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaiKhoanID,TenTaiKhoan,MatKhau,QuyenHanID")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanID))
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
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "QuyenHanID", taiKhoan.QuyenHanID);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaiKhoan == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan
                .Include(t => t.QuyenHan)
                .FirstOrDefaultAsync(m => m.TaiKhoanID == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaiKhoan == null)
            {
                return Problem("Entity set 'webbanhangContext.TaiKhoan'  is null.");
            }
            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoan.Remove(taiKhoan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
          return (_context.TaiKhoan?.Any(e => e.TaiKhoanID == id)).GetValueOrDefault();
        }
    }
}
