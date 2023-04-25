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
    public class ThuongHieusController : Controller
    {
        private readonly webbanhangContext _context;

        public ThuongHieusController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/ThuongHieus
        public async Task<IActionResult> Index()
        {
              return _context.ThuongHieu != null ? 
                          View(await _context.ThuongHieu.ToListAsync()) :
                          Problem("Entity set 'webbanhangContext.ThuongHieu'  is null.");
        }

        // GET: Admin/ThuongHieus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThuongHieu == null)
            {
                return NotFound();
            }

            var thuongHieu = await _context.ThuongHieu
                .FirstOrDefaultAsync(m => m.ThuongHieuID == id);
            if (thuongHieu == null)
            {
                return NotFound();
            }

            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThuongHieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThuongHieuID,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuongHieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThuongHieu == null)
            {
                return NotFound();
            }

            var thuongHieu = await _context.ThuongHieu.FindAsync(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThuongHieuID,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.ThuongHieuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuongHieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuongHieuExists(thuongHieu.ThuongHieuID))
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
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThuongHieu == null)
            {
                return NotFound();
            }

            var thuongHieu = await _context.ThuongHieu
                .FirstOrDefaultAsync(m => m.ThuongHieuID == id);
            if (thuongHieu == null)
            {
                return NotFound();
            }

            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThuongHieu == null)
            {
                return Problem("Entity set 'webbanhangContext.ThuongHieu'  is null.");
            }
            var thuongHieu = await _context.ThuongHieu.FindAsync(id);
            if (thuongHieu != null)
            {
                _context.ThuongHieu.Remove(thuongHieu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThuongHieuExists(int id)
        {
          return (_context.ThuongHieu?.Any(e => e.ThuongHieuID == id)).GetValueOrDefault();
        }
    }
}
