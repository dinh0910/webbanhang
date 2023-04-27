using System;
using System.Collections.Generic;
using System.Linq;
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
    public class QuyenHansController : Controller
    {
        private readonly webbanhangContext _context;
        private readonly INotyfService _notifyService;

        public QuyenHansController(webbanhangContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/QuyenHans
        public async Task<IActionResult> Index()
        {
              return _context.QuyenHan != null ? 
                          View(await _context.QuyenHan.ToListAsync()) :
                          Problem("Entity set 'webbanhangContext.QuyenHan'  is null.");
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
                _notifyService.Success("Thêm thành công!");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _notifyService.Error("Thêm thất bại!");
            return RedirectToAction(nameof(Index));
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
            else
            {
                _notifyService.Success("Xóa thành công!");
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
