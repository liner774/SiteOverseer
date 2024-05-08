using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Data;
using SiteOverseer.Models;

namespace SiteOverseer.Controllers
{
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
            return View(await _context.MS_Facility.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
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

            return View(facility);
        }

 
        public IActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FcilId,FcilCde,FcilNme,Address,Township,CityId,FciltypId,Contractval,ApproveDte,FcilstartDte")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                facility.RevdTetime = DateTime.Now;
                facility.CmpyId = 1; //default
                facility.UserId = 1; //default
                _context.Add(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.MS_Facility.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
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
                    facility.CmpyId = 1; //default
                    facility.UserId = 1; //default
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
    }
}
