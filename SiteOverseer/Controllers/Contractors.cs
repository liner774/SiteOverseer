using System;
using System.Collections.Generic;
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
    public class Contractors : Controller
    {
        private readonly SiteDbContext _context;

        public Contractors(SiteDbContext context)
        {
            _context = context;
        }

        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            var facilityCodeList = await _context.MS_Contractor.ToListAsync();
            foreach (var facilitCode in facilityCodeList)
            {
                facilitCode.FciltypCde = _context.MS_Facilitytype.Where(gp => gp.FciltypId == facilitCode.FciltypId).Select(gp => gp.FciltypCde).FirstOrDefault();
            }
            return View(facilityCodeList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.MS_Contractor
                .FirstOrDefaultAsync(m => m.CntorId == id);

            if (contractor == null)
            {
                return NotFound();
            }

            contractor.FciltypCde = _context.MS_Facilitytype.Where(ft => ft.FciltypId == contractor.FciltypId).Select(ft => ft.FciltypCde).FirstOrDefault();
            contractor.Company = _context.MS_Company.Where(c => c.CmpyId == contractor.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            contractor.User = _context.MS_User.Where(u => u.UserId == contractor.UserId).Select(u => u.UserNme).FirstOrDefault();


            return View(contractor);
        }


        public IActionResult Create()
        {
            ViewData["FcliList"] = new SelectList(_context.MS_Facilitytype.ToList(), "FciltypId", "FciltypCde");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CntorId,CntorNme,FciltypId,ProgpayId,Establisheddte,BadStatus,Remark")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                contractor.RevDtetime = DateTime.Now;
                contractor.CmpyId = GetCmpyId(); //default
                contractor.UserId = GetUserId(); //default
                _context.Add(contractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FcliList"] = new SelectList(_context.MS_Facilitytype.ToList(), "FciltypId", "FciltypCde");
            return View(contractor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.MS_Contractor.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            ViewData["FcliList"] = new SelectList(_context.MS_Facilitytype.ToList(), "FciltypId", "FciltypCde");
            return View(contractor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CntorId,CntorNme,FciltypId,ProgpayId,Establisheddte,BadStatus")] Contractor contractor)
        {
            if (id != contractor.CntorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contractor.RevDtetime = DateTime.Now;
                    contractor.CmpyId = GetCmpyId(); //default
                    contractor.UserId = GetUserId(); //default
                    _context.Update(contractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractor.CntorId))
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
            return View(contractor);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.MS_Contractor
                .FirstOrDefaultAsync(m => m.CntorId == id);
            if (contractor == null)
            {
                return NotFound();
            }

            contractor.FciltypCde = await _context.MS_Facilitytype
                .Where(ft => ft.FciltypId == contractor.FciltypId)
                .Select(ft => ft.FciltypCde)
                .FirstOrDefaultAsync();

            return View(contractor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractor = await _context.MS_Contractor.FindAsync(id);
            if (contractor != null)
            {
                _context.MS_Contractor.Remove(contractor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractorExists(int id)
        {
            return _context.MS_Contractor.Any(e => e.CntorId == id);
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
        #endregion
    }
}
