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
    public class DonDatHangsController : Controller
    {
        private readonly webbanhangContext _context;

        public DonDatHangsController(webbanhangContext context)
        {
            _context = context;
        }

        // GET: Admin/DonDatHangs
        public async Task<IActionResult> Index()
        {
              return _context.DonDatHang != null ? 
                          View(await _context.DonDatHang.ToListAsync()) :
                          Problem("Entity set 'webbanhangContext.DonDatHang'  is null.");
        }

        // GET: Admin/DonDatHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DonDatHang == null)
            {
                return NotFound();
            }

            var donDatHang = await _context.DonDatHang
                .FirstOrDefaultAsync(m => m.DonDatHangID == id);
            if (donDatHang == null)
            {
                return NotFound();
            }

            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DonDatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonDatHangID,NgayLap,HoTen,Sdt,DiaChi,GhiChu,TongTien")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donDatHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DonDatHang == null)
            {
                return NotFound();
            }

            var donDatHang = await _context.DonDatHang.FindAsync(id);
            if (donDatHang == null)
            {
                return NotFound();
            }
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonDatHangID,NgayLap,HoTen,Sdt,DiaChi,GhiChu,TongTien")] DonDatHang donDatHang)
        {
            if (id != donDatHang.DonDatHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donDatHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonDatHangExists(donDatHang.DonDatHangID))
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
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DonDatHang == null)
            {
                return NotFound();
            }

            var donDatHang = await _context.DonDatHang
                .FirstOrDefaultAsync(m => m.DonDatHangID == id);
            if (donDatHang == null)
            {
                return NotFound();
            }

            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DonDatHang == null)
            {
                return Problem("Entity set 'webbanhangContext.DonDatHang'  is null.");
            }
            var donDatHang = await _context.DonDatHang.FindAsync(id);
            if (donDatHang != null)
            {
                _context.DonDatHang.Remove(donDatHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonDatHangExists(int id)
        {
          return (_context.DonDatHang?.Any(e => e.DonDatHangID == id)).GetValueOrDefault();
        }
    }
}
