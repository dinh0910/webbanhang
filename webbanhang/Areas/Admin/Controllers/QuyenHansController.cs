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
    public class QuyenHansController : Controller
    {
        private readonly webbanhangContext _context;

        public QuyenHansController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/QuyenHans
        public async Task<IActionResult> Index()
        {
              return _context.QuyenHan != null ? 
                          View(await _context.QuyenHan.ToListAsync()) :
                          Problem("Entity set 'webbanhangContext.QuyenHan'  is null.");
        }

        // GET: Admin/QuyenHans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuyenHan == null)
            {
                return NotFound();
            }

            var quyenHan = await _context.QuyenHan
                .FirstOrDefaultAsync(m => m.QuyenHanID == id);
            if (quyenHan == null)
            {
                return NotFound();
            }

            return View(quyenHan);
        }

        // GET: Admin/QuyenHans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuyenHans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuyenHanID,Ten")] QuyenHan quyenHan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quyenHan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quyenHan);
        }

        // GET: Admin/QuyenHans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QuyenHan == null)
            {
                return NotFound();
            }

            var quyenHan = await _context.QuyenHan.FindAsync(id);
            if (quyenHan == null)
            {
                return NotFound();
            }
            return View(quyenHan);
        }

        // POST: Admin/QuyenHans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuyenHanID,Ten")] QuyenHan quyenHan)
        {
            if (id != quyenHan.QuyenHanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quyenHan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuyenHanExists(quyenHan.QuyenHanID))
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
            return View(quyenHan);
        }

        // GET: Admin/QuyenHans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuyenHan == null)
            {
                return NotFound();
            }

            var quyenHan = await _context.QuyenHan
                .FirstOrDefaultAsync(m => m.QuyenHanID == id);
            if (quyenHan == null)
            {
                return NotFound();
            }

            return View(quyenHan);
        }

        // POST: Admin/QuyenHans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuyenHan == null)
            {
                return Problem("Entity set 'webbanhangContext.QuyenHan'  is null.");
            }
            var quyenHan = await _context.QuyenHan.FindAsync(id);
            if (quyenHan != null)
            {
                _context.QuyenHan.Remove(quyenHan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuyenHanExists(int id)
        {
          return (_context.QuyenHan?.Any(e => e.QuyenHanID == id)).GetValueOrDefault();
        }
    }
}
