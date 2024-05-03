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
    public class FacilityTasks : Controller
    {
        private readonly SiteDbContext _context;

        public FacilityTasks(SiteDbContext context)
        {
            _context = context;
        }

        // GET: FacilityTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.MS_Facilitytask.ToListAsync());
        }

        // GET: FacilityTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityTask = await _context.MS_Facilitytask
                .FirstOrDefaultAsync(m => m.FciltskId == id);
            if (facilityTask == null)
            {
                return NotFound();
            }

            return View(facilityTask);
        }

        // GET: FacilityTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacilityTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FciltskId,FcilId,WbsdId,Budget,CntorId,SelectionTyp,WorkstartDte,WorkendDte,AwardedValue,ProgpayId,AllowSubmitExpense,TaskCompleteFlg,Remark,CmpyId,UserId,RevdTetime")] FacilityTask facilityTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilityTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityTask);
        }

        // GET: FacilityTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityTask = await _context.MS_Facilitytask.FindAsync(id);
            if (facilityTask == null)
            {
                return NotFound();
            }
            return View(facilityTask);
        }

        // POST: FacilityTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FciltskId,FcilId,WbsdId,Budget,CntorId,SelectionTyp,WorkstartDte,WorkendDte,AwardedValue,ProgpayId,AllowSubmitExpense,TaskCompleteFlg,Remark,CmpyId,UserId,RevdTetime")] FacilityTask facilityTask)
        {
            if (id != facilityTask.FciltskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilityTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityTaskExists(facilityTask.FciltskId))
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
            return View(facilityTask);
        }

        // GET: FacilityTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityTask = await _context.MS_Facilitytask
                .FirstOrDefaultAsync(m => m.FciltskId == id);
            if (facilityTask == null)
            {
                return NotFound();
            }

            return View(facilityTask);
        }

        // POST: FacilityTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilityTask = await _context.MS_Facilitytask.FindAsync(id);
            if (facilityTask != null)
            {
                _context.MS_Facilitytask.Remove(facilityTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityTaskExists(int id)
        {
            return _context.MS_Facilitytask.Any(e => e.FciltskId == id);
        }
    }
}
