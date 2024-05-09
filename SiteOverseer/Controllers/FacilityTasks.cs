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

        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
           
            var facilityNameList = await _context.MS_Facilitytask.ToListAsync();
            var contractorNameList = await _context.MS_Facilitytask.ToListAsync();


            foreach (var facilityName in facilityNameList)
            {
               facilityName.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityName.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            }
            
            foreach (var contractorName in contractorNameList)
            {
                contractorName.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == contractorName.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
            }

            return View(facilityNameList);
            return View(contractorNameList);
            
 

        }

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

            facilityTask.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityTask.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            facilityTask.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityTask.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();

            return View(facilityTask);



        }




        public IActionResult Create()
        {
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            return View();

        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FciltskId,FcilId,WbsdId,Budget,CntorId,SelectionTyp,WorkstartDte,WorkendDte,AwardedValue,ProgpayId,AllowSubmitExpense,TaskCompleteFlg,Remark")] FacilityTask facilityTask)
        {
            if (ModelState.IsValid)
            {
                facilityTask.RevdTetime = DateTime.Now;
                facilityTask.CmpyId = 1; //default
                facilityTask.UserId = 1; //default
                _context.Add(facilityTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            return View(facilityTask);
        }

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
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            return View(facilityTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FciltskId,FcilId,WbsdId,Budget,CntorId,SelectionTyp,WorkstartDte,WorkendDte,AwardedValue,ProgpayId,AllowSubmitExpense,TaskCompleteFlg,Remark")] FacilityTask facilityTask)
        {
            if (id != facilityTask.FciltskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facilityTask.RevdTetime = DateTime.Now;
                    facilityTask.CmpyId = 1; //default
                    facilityTask.UserId = 1; //default
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
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            return View(facilityTask);
        }

 
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
            facilityTask.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityTask.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            facilityTask.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityTask.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
            return View(facilityTask);
        }
 
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
#endregion
    }
}
