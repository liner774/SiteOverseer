using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Data;
using SiteOverseer.Models;

namespace SiteOverseer.Controllers
{
    [Authorize]
    public class TranTypeReasons : Controller
    {
        private readonly SiteDbContext _context;

        public TranTypeReasons(SiteDbContext context)
        {
            _context = context;
        }

        #region // Main methods //
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.MS_Trantypereason.ToListAsync());
        }

        
        public async Task<IActionResult> Details(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeReason = await _context.MS_Trantypereason
                .FirstOrDefaultAsync(m => m.TranReasonId == id);
            if (tranTypeReason == null)
            {
                return NotFound();
            }

            tranTypeReason.Company = _context.MS_Company.Where(c => c.CmpyId == tranTypeReason.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            tranTypeReason.User = _context.MS_User.Where(u => u.UserId == tranTypeReason.UserId).Select(u => u.UserNme).FirstOrDefault();
            return View(tranTypeReason);
        }


        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TranReasonDesc")] TranTypeReason tranTypeReason)
        {
            if (ModelState.IsValid)
            { 
                tranTypeReason.RevDtetime = DateTime.Now;
                tranTypeReason.CmpyId = GetCmpyId();
                tranTypeReason.UserId = GetUserId();
                _context.Add(tranTypeReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tranTypeReason);
        }

        public async Task<IActionResult> Edit(short? id)
        {

            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeReason = await _context.MS_Trantypereason.FindAsync(id);
            if (tranTypeReason == null)
            {
                return NotFound();

            }

            tranTypeReason.RevDtetime = DateTime.Now;
            return View(tranTypeReason);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("TranReasonId,TranReasonDesc")] TranTypeReason tranTypeReason)
        {
            if (id != tranTypeReason.TranReasonId)
            {
                
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tranTypeReason.RevDtetime = DateTime.Now;
                    tranTypeReason.CmpyId = GetCmpyId();
                    tranTypeReason.UserId = GetUserId();
                    _context.Update(tranTypeReason);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranTypeReasonExists(tranTypeReason.TranReasonId))
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

            
            return View(tranTypeReason);
        }

        public async Task<IActionResult> Delete(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeReason = await _context.MS_Trantypereason
                .FirstOrDefaultAsync(m => m.TranReasonId == id);
            if (tranTypeReason == null)
            {
                return NotFound();
            }

            return View(tranTypeReason);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var tranTypeReason = await _context.MS_Trantypereason.FindAsync(id);
            if (tranTypeReason != null)
            {
                _context.MS_Trantypereason.Remove(tranTypeReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranTypeReasonExists(short id)
        {
            return _context.MS_Trantypereason.Any(e => e.TranReasonId == id);
        }

        #endregion

        #region // Global Methods (Important)  //
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
        protected void SetLayOutData()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userCde != null)
            {
                var userName = _context.MS_User
                    .Where(u => u.UserCde == userCde)
                    .Select(u => u.UserNme)
                    .FirstOrDefault();

                ViewData["Username"] = userName;
            }
        }
        #endregion
    }
}
