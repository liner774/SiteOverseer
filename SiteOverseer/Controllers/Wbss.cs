using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Data;
using SiteOverseer.Models;

namespace SiteOverseer.Controllers
{
    public class Wbss : Controller
    {
        private readonly SiteDbContext _context;

        public Wbss(SiteDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //
       
        public async Task<IActionResult> Index()
        {
            var WbsdCodeList = await _context.MS_Wbs.ToListAsync();
            foreach (var WbsdCode in WbsdCodeList)
            {
                WbsdCode.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsId == WbsdCode.WbsId).Select(gp => gp.WbsdCde).FirstOrDefault();
            }
            return View(WbsdCodeList);

        }

   
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wbs = await _context.MS_Wbs
                .FirstOrDefaultAsync(m => m.WbsId == id);
            if (wbs == null)
            {
                return NotFound();
            }
            wbs.WbsdCde = _context.MS_Wbsdetail.Where(ft => ft.WbsId == wbs.WbsId).Select(ft => ft.WbsdCde).FirstOrDefault();
            return View(wbs);
        }

        
        public IActionResult Create()
        {
            ViewData["WbsdList"] = new SelectList(_context.MS_Wbsdetail.ToList());
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WbsId,WbsCde")] Wbs wbs)
        {
            if (ModelState.IsValid)
            {
                wbs.RevdTetime = DateTime.Now;
                wbs.UserId = GetUserId();
                wbs.CmpyId = GetCmpyId();
                _context.Add(wbs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WbsdList"] = new SelectList(_context.MS_Wbsdetail.ToList());
            return View(wbs);
        }
 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wbs = await _context.MS_Wbs.FindAsync(id);
            if (wbs == null)
            {
                return NotFound();
            }
            ViewData["WbsdList"] = new SelectList(_context.MS_Wbsdetail.ToList());
            return View(wbs);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WbsId,WbsCde")] Wbs wbs)
        {
            if (id != wbs.WbsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    wbs.RevdTetime = DateTime.Now;
                    wbs.UserId = GetUserId();
                    wbs.CmpyId = GetCmpyId();
                    _context.Update(wbs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WbsExists(wbs.WbsId))
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
            return View(wbs);
        }
 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wbs = await _context.MS_Wbs
                .FirstOrDefaultAsync(m => m.WbsId == id);
            if (wbs == null)
            {
                return NotFound();
            }

            return View(wbs);
        }
 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wbs = await _context.MS_Wbs.FindAsync(id);
            if (wbs != null)
            {
                _context.MS_Wbs.Remove(wbs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WbsExists(int id)
        {
            return _context.MS_Wbs.Any(e => e.WbsId == id);
        }

        #endregion

        #region // Global Methods (Important) //
        protected short GetUserId()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var userId = (short)_context.MS_User
                .Where(u => u.UserCde == userCde)
                .Select(u => u.UserId)
                .FirstOrDefault();

            return userId;
        }
        protected short GetCmpyId()
        {
            var cmpyId = _context.MS_User
                .Where(u => u.UserId == GetUserId())
                .Select(u => u.CmpyId)
                .FirstOrDefault();

            return cmpyId;
        }
        #endregion
    }
}
