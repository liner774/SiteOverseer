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
            
            foreach (var facilityName in facilityNameList)
            {
               facilityName.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityName.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
               facilityName.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityName.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
               facilityName.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityName.WbsdId).Select(gp => gp.WbsdCde).FirstOrDefault();
               facilityName.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityName.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
               facilityName.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == facilityName.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
            }
         
            return View(facilityNameList);
            
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
            facilityTask.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityTask.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
            facilityTask.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == facilityTask.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
            facilityTask.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityTask.WbsdId).Select(gp => gp.WbsdCde).FirstOrDefault();
            facilityTask.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityTask.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            facilityTask.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityTask.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
            facilityTask.Company = _context.MS_Company.Where(c => c.CmpyId == facilityTask.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            facilityTask.User = _context.MS_User.Where(u => u.UserId == facilityTask.UserId).Select(u => u.UserNme).FirstOrDefault();
           
            return View(facilityTask);
        }




        public IActionResult Create()
        {
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");            
            ViewData["WbsCdeList"] = new SelectList(_context.MS_Wbs.ToList(), "WbsId", "WbsCde");
            return View();

        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FciltskId,FcilId,WbsdId,Budget,CntorId,SelectionTyp,WorkstartDte,WorkendDte,AwardedValue,ProgpayId,AllowSubmitExpense,TaskCompleteFlg,Remark")] FacilityTask facilityTask)
        {
            if (ModelState.IsValid)
            {
                facilityTask.RevdTetime = DateTime.Now;
                facilityTask.CmpyId = GetCmpyId();
                facilityTask.UserId = GetUserId();
                _context.Add(facilityTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");            
            ViewData["WbsCdeList"] = new SelectList(_context.MS_Wbs.ToList(), "WbsId", "WbsCde");
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

            var wbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityTask.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
            facilityTask.WbsId = wbsId;

            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            ViewData["WbsCdeList"] = new SelectList(_context.MS_Wbs.ToList(), "WbsId", "WbsCde");
            ViewData["WbsDtlList"] = new SelectList(_context.MS_Wbsdetail.Where(d => d.WbsId == wbsId).ToList(),"WbsdId","WbsdCde");
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
                    facilityTask.CmpyId = GetCmpyId();
                    facilityTask.UserId = GetUserId();
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
            ViewData["WbsCdeList"] = new SelectList(_context.MS_Wbs.ToList(), "WbsId", "WbsCde");
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
            facilityTask.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityTask.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
            facilityTask.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == facilityTask.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
            facilityTask.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityTask.WbsdId).Select(gp => gp.WbsdCde).FirstOrDefault();
            facilityTask.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityTask.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            facilityTask.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityTask.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
            facilityTask.Company = _context.MS_Company.Where(c => c.CmpyId == facilityTask.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            facilityTask.User = _context.MS_User.Where(u => u.UserId == facilityTask.UserId).Select(u => u.UserNme).FirstOrDefault();

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

        [HttpGet]
        public IActionResult GetWbsDetails(int wbsId)
        {
            
            var wbsDetails = _context.MS_Wbsdetail
                .Where(w => w.WbsId == wbsId)
                .Select(w => new { value = w.WbsdId, text = w.WbsdCde })
                .ToList();

            return Json(wbsDetails);
        }

    }
}
