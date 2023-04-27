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
    public class TaiKhoansController : Controller
    {
        private readonly webbanhangContext _context;
        private readonly INotyfService _notifyService;

        public TaiKhoansController(webbanhangContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/TaiKhoans
        public async Task<IActionResult> Index()
        {
            var webbanhangContext = _context.TaiKhoan.Include(t => t.QuyenHan);
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "Ten");
            return View(await webbanhangContext.ToListAsync());
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
                _notifyService.Success("Thêm thành công!");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuyenHanID"] = new SelectList(_context.QuyenHan, "QuyenHanID", "QuyenHanID", taiKhoan.QuyenHanID);
            _notifyService.Error("Thêm thất bại!");
            return RedirectToAction(nameof(Index));
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
            else
            {
                _notifyService.Success("Xóa thành công!");
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
