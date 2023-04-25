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
    public class DonViTinhsController : Controller
    {
        private readonly webbanhangContext _context;
        private readonly INotyfService _notifyService;

        public DonViTinhsController(webbanhangContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/DonViTinhs
        public async Task<IActionResult> Index()
        {
              return _context.DonViTinh != null ? 
                          View(await _context.DonViTinh.ToListAsync()) :
                          Problem("Entity set 'webbanhangContext.DonViTinh'  is null.");
        }

        // POST: Admin/DonViTinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonViTinhID,DonVi")] DonViTinh donViTinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donViTinh);
                _notifyService.Success("Thêm thành công!");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _notifyService.Error("Thêm thất bại!");
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/DonViTinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DonViTinh == null)
            {
                return NotFound();
            }

            var donViTinh = await _context.DonViTinh.FindAsync(id);
            if (donViTinh == null)
            {
                return NotFound();
            }
            return View(donViTinh);
        }

        // POST: Admin/DonViTinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonViTinhID,DonVi")] DonViTinh donViTinh)
        {
            if (id != donViTinh.DonViTinhID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donViTinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonViTinhExists(donViTinh.DonViTinhID))
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
            return View(donViTinh);
        }

        // GET: Admin/DonViTinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DonViTinh == null)
            {
                return NotFound();
            }

            var donViTinh = await _context.DonViTinh
                .FirstOrDefaultAsync(m => m.DonViTinhID == id);
            if (donViTinh == null)
            {
                return NotFound();
            }
            else
            {
                _notifyService.Success("Xóa thành công!");
                _context.DonViTinh.Remove(donViTinh);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonViTinhExists(int id)
        {
          return (_context.DonViTinh?.Any(e => e.DonViTinhID == id)).GetValueOrDefault();
        }
    }
}
