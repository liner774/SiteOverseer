using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class FacilityTypes : Controller
    {
        private readonly SiteDbContext _context;

        public FacilityTypes(SiteDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            return View(await _context.MS_Facilitytype.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityType = await _context.MS_Facilitytype
                .FirstOrDefaultAsync(m => m.FciltypId == id);
            if (facilityType == null)
            {
                return NotFound();
            }

            return View(facilityType);
        }

   
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FciltypId,FciltypCde,FciltypDesc")] FacilityType facilityType)
        {
            if (ModelState.IsValid)
            {
                facilityType.RevdTetime = DateTime.Now;
               
                _context.Add(facilityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityType);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityType = await _context.MS_Facilitytype.FindAsync(id);
            if (facilityType == null)
            {
                return NotFound();
            }
            return View(facilityType);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FciltypId,FciltypCde,FciltypDesc")] FacilityType facilityType)
        {
            if (id != facilityType.FciltypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facilityType.RevdTetime = DateTime.Now;
                    facilityType.CmpyId = GetCmpyId();
                    facilityType.UserId = GetUserId();
                    _context.Update(facilityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityTypeExists(facilityType.FciltypId))
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
            return View(facilityType);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityType = await _context.MS_Facilitytype
                .FirstOrDefaultAsync(m => m.FciltypId == id);
            if (facilityType == null)
            {
                return NotFound();
            }

            return View(facilityType);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilityType = await _context.MS_Facilitytype.FindAsync(id);
            if (facilityType != null)
            {
                _context.MS_Facilitytype.Remove(facilityType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityTypeExists(int id)
        {
            return _context.MS_Facilitytype.Any(e => e.FciltypId == id);
        }

        #endregion

        #region // Grobal Method//
        protected short GetUserId()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var userId = (short)_context.MS_User
                .Where(u => u.UserCde== userCde)
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
