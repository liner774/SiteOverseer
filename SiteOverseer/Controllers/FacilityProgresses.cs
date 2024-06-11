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
    public class FacilityProgresses : Controller
    {
        private readonly SiteDbContext _context;

        public FacilityProgresses(SiteDbContext context)
        {
            _context = context;
        }

        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            var progList = await _context.PMS_Facilityprogress.ToListAsync();

            foreach (var prog in progList)
            {
                prog.FcilId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.FcilId).FirstOrDefault();
                prog.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == prog.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
                prog.FcilCde = _context.MS_Facility.Where(gp => gp.FcilId == prog.FcilId).Select(gp => gp.FcilCde).FirstOrDefault();
                prog.CntorId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.CntorId).FirstOrDefault();
                prog.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == prog.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
                prog.WbsdId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WbsdId).FirstOrDefault();
                prog.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == prog.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
                prog.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsId == prog.WbsId).Select(gp => gp.WbsdCde).FirstOrDefault();
                prog.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == prog.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
                prog.WorkstartDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WorkstartDte).FirstOrDefault();
                prog.WorkendDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WorkendDte).FirstOrDefault();
                prog.Budget = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.Budget).FirstOrDefault();
                
            }

            return View(progList);
        }

       
        public async Task<IActionResult> Details(long? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facilityProgress = await _context.PMS_Facilityprogress
                .FirstOrDefaultAsync(m => m.ProgId == id);
            if (facilityProgress == null)
            {
                return NotFound();
            }
            facilityProgress.Company = _context.MS_Company.Where(c => c.CmpyId == facilityProgress.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            facilityProgress.User = _context.MS_User.Where(u => u.UserId == facilityProgress.UserId).Select(u => u.UserNme).FirstOrDefault();

            return View(facilityProgress);
        }

        
        public IActionResult Create()
        {
            SetLayOutData();
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacilityProgress facilityProgress, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        facilityProgress.ImageData = memoryStream.ToArray();
                        facilityProgress.ImageName = image.FileName;
                        facilityProgress.ImageContentType = image.ContentType;
                    }
                

                facilityProgress.RevDteTime = DateTime.Now;
                facilityProgress.Longitude = 0; // default
                facilityProgress.Latitude = 0; // default
                facilityProgress.UserId = GetUserId();
                facilityProgress.CmpyId = GetCmpyId();
                _context.Add(facilityProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityProgress);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facilityProgress = await _context.PMS_Facilityprogress.FindAsync(id);
            if (facilityProgress == null)
            {
                return NotFound();
            }
            #region // facility task call //
            facilityProgress.FcilId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.FcilId).FirstOrDefault();
            facilityProgress.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityProgress.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            facilityProgress.FcilCde = _context.MS_Facility.Where(gp => gp.FcilId == facilityProgress.FcilId).Select(gp => gp.FcilCde).FirstOrDefault();
            facilityProgress.CntorId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.CntorId).FirstOrDefault();
            facilityProgress.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityProgress.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
            facilityProgress.WbsdId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.WbsdId).FirstOrDefault();
            facilityProgress.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityProgress.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
            facilityProgress.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsId == facilityProgress.WbsId).Select(gp => gp.WbsdCde).FirstOrDefault();
            facilityProgress.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == facilityProgress.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
            facilityProgress.WorkstartDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.WorkstartDte).FirstOrDefault();
            facilityProgress.WorkendDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.WorkendDte).FirstOrDefault();
            facilityProgress.Budget = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.Budget).FirstOrDefault();
            facilityProgress.SelectionTyp = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.SelectionTyp).FirstOrDefault();
            facilityProgress.AwardedValue = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.AwardedValue).FirstOrDefault();
            facilityProgress.ProgpayId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.ProgpayId).FirstOrDefault();
            facilityProgress.AllowSubmitExpense = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.AllowSubmitExpense).FirstOrDefault();
            facilityProgress.TaskCompleteFlg = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.TaskCompleteFlg).FirstOrDefault();
            facilityProgress.Remark = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.Remark).FirstOrDefault();
            facilityProgress.FC_Id = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.CmpyId).FirstOrDefault();
            facilityProgress.FU_Id = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.UserId).FirstOrDefault();
            facilityProgress.FCompany = _context.MS_Company.Where(gp => gp.CmpyId == facilityProgress.FC_Id).Select(gp => gp.CmpyNme).FirstOrDefault();
            facilityProgress.FUser = _context.MS_User.Where(gp => gp.UserId == facilityProgress.FU_Id).Select(gp => gp.UserNme).FirstOrDefault();
            #endregion

            return View(facilityProgress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, FacilityProgress facilityProgress, List<IFormFile> images)
        {
            if (id != facilityProgress.ProgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (images.Count > 0)
                    {
                        foreach (var image in images)
                        {
                            if (image != null && image.Length > 0)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    await image.CopyToAsync(memoryStream);
                                    facilityProgress.ImageData = memoryStream.ToArray();
                                    facilityProgress.ImageName = image.FileName;
                                    facilityProgress.ImageContentType = image.ContentType;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Retain existing image data if no new image is uploaded
                        var existingFacilityProgress = await _context.PMS_Facilityprogress.AsNoTracking().FirstOrDefaultAsync(fp => fp.ProgId == id);
                        facilityProgress.ImageData = existingFacilityProgress.ImageData;
                        facilityProgress.ImageName = existingFacilityProgress.ImageName;
                        facilityProgress.ImageContentType = existingFacilityProgress.ImageContentType;
                    }

                    facilityProgress.RevDteTime = DateTime.Now;
                    facilityProgress.Longitude = 0; // default
                    facilityProgress.Latitude = 0; // default
                    facilityProgress.UserId = GetUserId();
                    facilityProgress.CmpyId = GetCmpyId();
                    _context.Update(facilityProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityProgressExists(facilityProgress.ProgId))
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
            return View(facilityProgress);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facilityProgress = await _context.PMS_Facilityprogress
                .FirstOrDefaultAsync(m => m.ProgId == id);
            if (facilityProgress == null)
            {
                return NotFound();
            }

            return View(facilityProgress);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var facilityProgress = await _context.PMS_Facilityprogress.FindAsync(id);
            if (facilityProgress != null)
            {
                _context.PMS_Facilityprogress.Remove(facilityProgress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityProgressExists(long id)
        {
            return _context.PMS_Facilityprogress.Any(e => e.ProgId == id);
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
