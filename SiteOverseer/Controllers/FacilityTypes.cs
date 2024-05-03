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
    public class FacilityTypes : Controller
    {
        private readonly SiteDbContext _context;

        public FacilityTypes(SiteDbContext context)
        {
            _context = context;
        }

        // GET: FacilityTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MS_Facilitytype.ToListAsync());
        }

        // GET: FacilityTypes/Details/5
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

        // GET: FacilityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacilityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FciltypId,FciltypCde,FciltypDesc,CmpyId,UserId,RevdTetime")] FacilityType facilityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityType);
        }

        // GET: FacilityTypes/Edit/5
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

        // POST: FacilityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FciltypId,FciltypCde,FciltypDesc,CmpyId,UserId,RevdTetime")] FacilityType facilityType)
        {
            if (id != facilityType.FciltypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: FacilityTypes/Delete/5
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

        // POST: FacilityTypes/Delete/5
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
    }
}
