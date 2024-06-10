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
    public class Facilities : Controller
    {
        private readonly SiteDbContext _context;

        public Facilities(SiteDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            var facilityList = await _context.MS_Facility.ToListAsync();

            foreach (var facility in facilityList)
            {
                facility.CityName = _context.MS_City.Where(gp => gp.CityId == facility.CityId).Select(gp => gp.CityName).FirstOrDefault();
                facility.FciltypCde = _context.MS_Facilitytype.Where(gp => gp.FciltypId == facility.FciltypId).Select(gp => gp.FciltypCde).FirstOrDefault();
            }

            return View(facilityList);
        }


        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.MS_Facility
                .FirstOrDefaultAsync(m => m.FcilId == id);
            if (facility == null)
            {
                return NotFound();
            }

            facility.Company = _context.MS_Company.Where(c => c.CmpyId == facility.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            facility.User = _context.MS_User.Where(u => u.UserId == facility.UserId).Select(u => u.UserNme).FirstOrDefault();
            facility.CityName = _context.MS_City.Where(ft => ft.CityId == facility.CityId).Select(ft => ft.CityName).FirstOrDefault();
            facility.FciltypCde = _context.MS_Facilitytype.Where(ft => ft.FciltypId == facility.FciltypId).Select(ft => ft.FciltypCde).FirstOrDefault();

            return View(facility);
        }

 
        public IActionResult Create()
        {
            SetLayOutData();
            ViewData["CityList"] = new SelectList(_context.MS_City.ToList(), "CityId", "CityName");
            ViewData["FcilTypList"] = new SelectList(_context.MS_Facilitytype.ToList(), "FciltypId", "FciltypCde");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FcilId,FcilCde,FcilNme,Address,Township,CityId,FciltypId,Contractval,ApproveDte,FcilstartDte")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                facility.RevdTetime = DateTime.Now;
                facility.CmpyId = GetCmpyId(); 
                facility.UserId = GetUserId(); 
                _context.Add(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityList"] = new SelectList(_context.MS_City.ToList(), "CityId", "CityName");
            ViewData["FcilTypList"] = new SelectList(_context.MS_Facilitytype.ToList(), "FciltypId", "FciltypCde");
            return View(facility);
        }

 
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.MS_Facility.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            ViewData["CityList"] = new SelectList(_context.MS_City.ToList(), "CityId", "CityName");
            ViewData["FcilTypList"] = new SelectList(_context.MS_Facilitytype.ToList(), "FciltypId", "FciltypCde");
            return View(facility);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FcilId,FcilCde,FcilNme,Address,Township,CityId,FciltypId,Contractval,ApproveDte,FcilstartDte")] Facility facility)
        {
            if (id != facility.FcilId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facility.RevdTetime = DateTime.Now;
                    facility.CmpyId = GetCmpyId(); 
                    facility.UserId = GetUserId();
                    _context.Update(facility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityExists(facility.FcilId))
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
            return View(facility);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.MS_Facility
                .FirstOrDefaultAsync(m => m.FcilId == id);
            if (facility == null)
            {
                return NotFound();
            }

            facility.CityName = _context.MS_City.Where(ft => ft.CityId == facility.CityId).Select(ft => ft.CityName).FirstOrDefault();
            facility.FciltypCde = _context.MS_Facilitytype.Where(ft => ft.FciltypId == facility.FciltypId).Select(ft => ft.FciltypCde).FirstOrDefault();

            return View(facility);
        }
 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _context.MS_Facility.FindAsync(id);
            if (facility != null)
            {
                _context.MS_Facility.Remove(facility);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityExists(int id)
        {
            return _context.MS_Facility.Any(e => e.FcilId == id);
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
