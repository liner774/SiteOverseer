using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
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
                prog.CntorId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.CntorId).FirstOrDefault() ?? 0;
                prog.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == prog.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
                prog.WbsdId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WbsdId).FirstOrDefault();
                prog.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == prog.WbsdId).Select(gp => gp.WbsdCde).FirstOrDefault();
                prog.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == prog.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
                prog.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == prog.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
                prog.WorkstartDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WorkstartDte).FirstOrDefault();
                prog.WorkendDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WorkendDte).FirstOrDefault();
                prog.Budget = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.Budget).FirstOrDefault();
                prog.TaskCompleteFlg = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.TaskCompleteFlg).FirstOrDefault();
            }

            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");

            return View(progList);



        }

        [HttpPost]
        public IActionResult Filter(int? facilityId, int? contractorId, DateTime? startDateFrom, DateTime? startDateTo, bool taskComplete)
        {
            var facilityProgresses = _context.PMS_Facilityprogress
                .Join(_context.MS_Facilitytask,
                fp => fp.FcilTskId,
                ft => ft.FciltskId,
                (fp, ft) => new { fp, ft })
                .AsQueryable();

            if (facilityId.HasValue)
            {
                facilityProgresses = facilityProgresses.Where(f => f.ft.FcilId == facilityId.Value);
            }

            if (contractorId.HasValue)
            {
                facilityProgresses = facilityProgresses.Where(f => f.ft.CntorId == contractorId.Value);
            }

            if (startDateFrom.HasValue)
            {
                facilityProgresses = facilityProgresses.Where(f => f.ft.WorkstartDte >= startDateFrom.Value);
            }

            if (startDateTo.HasValue)
            {
                facilityProgresses = facilityProgresses.Where(f => f.ft.WorkstartDte <= startDateTo.Value);
            }

            if (taskComplete)
            {
                facilityProgresses = facilityProgresses.Where(f => f.ft.TaskCompleteFlg == taskComplete);
            }

            var progList = facilityProgresses
                .Select(f => f.fp)
                .ToList();

            foreach (var prog in progList)
            {
                prog.FcilId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.FcilId).FirstOrDefault();
                prog.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == prog.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
                prog.FcilCde = _context.MS_Facility.Where(gp => gp.FcilId == prog.FcilId).Select(gp => gp.FcilCde).FirstOrDefault();
                prog.CntorId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.CntorId).FirstOrDefault() ?? 0;
                prog.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == prog.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
                prog.WbsdId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WbsdId).FirstOrDefault();
                prog.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == prog.WbsdId).Select(gp => gp.WbsdCde).FirstOrDefault();
                prog.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == prog.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
                prog.WbsCde = _context.MS_Wbs.Where(gp => gp.WbsId == prog.WbsId).Select(gp => gp.WbsCde).FirstOrDefault();
                prog.WorkstartDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WorkstartDte).FirstOrDefault();
                prog.WorkendDte = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.WorkendDte).FirstOrDefault();
                prog.Budget = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.Budget).FirstOrDefault();
                prog.TaskCompleteFlg = _context.MS_Facilitytask.Where(gp => gp.FciltskId == prog.FcilTskId).Select(gp => gp.TaskCompleteFlg).FirstOrDefault();
            }

            return PartialView("_FilteredTable", progList);
        }

        public async Task<IActionResult> Details(long? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var facilityProgress = await _context.PMS_Facilityprogress
            .Include(fp => fp.Images)
            .Include(fp => fp.ProgressHistory)
            .FirstOrDefaultAsync(fp => fp.ProgId == id);


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
            var facilityProgress = new FacilityProgress
            {
                SubmitDte = DateTime.Now // Set default value
            };

            SetLayOutData();

            var facilityTaskList = (from ft in _context.MS_Facilitytask
                                    join f in _context.MS_Facility on ft.FcilId equals f.FcilId
                                    join wd in _context.MS_Wbsdetail on ft.WbsdId equals wd.WbsdId
                                    select new
                                    {
                                        FciltskId = ft.FciltskId,
                                        FcilCde = f.FcilCde,
                                        FcilNme = f.FcilNme,
                                        WbsdCde = wd.WbsdCde 
                                    }).ToList();

            var selectList = facilityTaskList.Select(ft => new
            {
                FciltskId = ft.FciltskId,
                Display = $" {ft.FcilNme} - {ft.WbsdCde}" 
            }).ToList();
            ViewBag.FciltskidList = new SelectList(selectList, "FciltskId", "Display");

            return View(facilityProgress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacilityProgress facilityProgress, List<IFormFile> images)
        {
            ModelState.Remove("ProgpayId");
            ModelState.Remove("CntorNme");
            ModelState.Remove("CntorId");
            ModelState.Remove("WbsdId");
            ModelState.Remove("FcilId");
            if (ModelState.IsValid)
            {
                try
                {
                    facilityProgress.RevDteTime = DateTime.Now;
                    facilityProgress.Longitude = 0; // default
                    facilityProgress.Latitude = 0; // default
                    facilityProgress.UserId = GetUserId();
                    facilityProgress.CmpyId = GetCmpyId();
                    _context.Add(facilityProgress);
                    await _context.SaveChangesAsync();

                    if (images != null && images.Count > 0)
                    {
                        facilityProgress.Images = new List<FacilityProgressImage>();
                        foreach (var image in images)
                        {
                            if (image != null && image.Length > 0)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    await image.CopyToAsync(memoryStream);
                                    var facilityProgressImage = new FacilityProgressImage
                                    {
                                        FacilityProgressId = facilityProgress.ProgId,
                                        ImageData = memoryStream.ToArray(),
                                        ImageName = image.FileName,
                                        ImageContentType = image.ContentType
                                    };
                                    _context.Add(facilityProgressImage);
                                }
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
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

            SetLayOutData();
            var facilityTaskList = (from ft in _context.MS_Facilitytask
                                    join f in _context.MS_Facility on ft.FcilId equals f.FcilId
                                    select new
                                    {
                                        FciltskId = ft.FciltskId,
                                        FcilCde = f.FcilCde,
                                        FcilNme = f.FcilNme
                                    }).ToList();

            var selectList = facilityTaskList.Select(ft => new
            {
                FciltskId = ft.FciltskId,
                Display = $"{ft.FcilCde} | {ft.FcilNme}"
            }).ToList();
            ViewBag.FciltskidList = new SelectList(selectList, "FciltskId", "Display");

            return View(facilityProgress);
        }


        public async Task<IActionResult> Edit(long id)
        {
            var facilityProgress = await _context.PMS_Facilityprogress
                .Include(fp => fp.Images)
                .FirstOrDefaultAsync(fp => fp.ProgId == id);

            if (facilityProgress == null)
            {
                return NotFound();
            }
            #region // facility task call //
            facilityProgress.FcilId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.FcilId).FirstOrDefault();
            facilityProgress.FcilNme = _context.MS_Facility.Where(gp => gp.FcilId == facilityProgress.FcilId).Select(gp => gp.FcilNme).FirstOrDefault();
            facilityProgress.FcilCde = _context.MS_Facility.Where(gp => gp.FcilId == facilityProgress.FcilId).Select(gp => gp.FcilCde).FirstOrDefault();
            facilityProgress.CntorId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.CntorId).FirstOrDefault() ?? 0;
            facilityProgress.CntorNme = _context.MS_Contractor.Where(gp => gp.CntorId == facilityProgress.CntorId).Select(gp => gp.CntorNme).FirstOrDefault();
            facilityProgress.WbsdId = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.WbsdId).FirstOrDefault();
            facilityProgress.WbsdCde = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityProgress.WbsdId).Select(gp => gp.WbsdCde).FirstOrDefault();
            facilityProgress.WbsId = _context.MS_Wbsdetail.Where(gp => gp.WbsdId == facilityProgress.WbsdId).Select(gp => gp.WbsId).FirstOrDefault();
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
            facilityProgress.RevdTetime = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.RevdTetime).FirstOrDefault();
            facilityProgress.FC_Id = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.CmpyId).FirstOrDefault();
            facilityProgress.FU_Id = _context.MS_Facilitytask.Where(gp => gp.FciltskId == facilityProgress.FcilTskId).Select(gp => gp.UserId).FirstOrDefault();
            facilityProgress.FCompany = _context.MS_Company.Where(gp => gp.CmpyId == facilityProgress.FC_Id).Select(gp => gp.CmpyNme).FirstOrDefault();
            facilityProgress.FUser = _context.MS_User.Where(gp => gp.UserId == facilityProgress.FU_Id).Select(gp => gp.UserNme).FirstOrDefault();

            #endregion


            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            ViewData["WbsCdeList"] = new SelectList(_context.MS_Wbs.ToList(), "WbsId", "WbsCde");
            ViewData["WbsDtlList"] = new SelectList(_context.MS_Wbsdetail.Where(d => d.WbsId == facilityProgress.WbsId).ToList(), "WbsdId", "WbsdCde");
            ViewData["PayList"] = new SelectList(_context.MS_Progresspayment.ToList(), "Progpayid", "Currcde");

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
            ModelState.Remove("SubmitDte");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProgress = await _context.PMS_Facilityprogress
                                                         .Include(fp => fp.Images)
                                                         .FirstOrDefaultAsync(fp => fp.ProgId == id);

                    if (existingProgress == null)
                    {
                        return NotFound();
                    }

                    // Check if ProgPercent has changed
                    if (existingProgress.ProgPercent != facilityProgress.ProgPercent)
                    {
                        // Create a history entry for each image
                        if (existingProgress.Images != null && existingProgress.Images.Any())
                        {
                            foreach (var image in existingProgress.Images)
                            {
                                var historyEntry = new FacilityProgressHistory
                                {
                                    FacilityProgressId = existingProgress.ProgId,
                                    ProgPercent = existingProgress.ProgPercent ?? 0,
                                    UpdatedAt = DateTime.Now,
                                    ImageData = image.ImageData,
                                    ImageName = image.ImageName,
                                    ImageContentType = image.ImageContentType
                                };
                                _context.FacilityProgressHistory.Add(historyEntry);
                            }
                        }
                    }

                    /*
                                       if (existingProgress.ProgPercent != facilityProgress.ProgPercent)
                                       {
                                           // Serialize images
                                           string serializedImageData = string.Empty;
                                           string serializedImageNames = string.Empty;
                                           string serializedImageContentTypes = string.Empty;

                                           if (existingProgress.Images != null && existingProgress.Images.Any())
                                           {
                                               foreach (var image in existingProgress.Images)
                                               {
                                                   serializedImageData += Convert.ToBase64String(image.ImageData) + ";";
                                                   serializedImageNames += image.ImageName + ";";
                                                   serializedImageContentTypes += image.ImageContentType + ";";
                                               }
                                           }

                                           // Create a new history entry before updating
                                           var historyEntry = new FacilityProgressHistory
                                           {
                                               FacilityProgressId = existingProgress.ProgId,
                                               ProgPercent = existingProgress.ProgPercent ?? 0,
                                               UpdatedAt = DateTime.Now,
                                               ImageData = serializedImageData.Length > 0 ? Encoding.UTF8.GetBytes(serializedImageData) : null,
                                               ImageName = serializedImageNames,
                                               ImageContentType = serializedImageContentTypes
                                           };
                                           _context.FacilityProgressHistory.Add(historyEntry);
                                       }
                   */
                    #region // Existing Progress Save //
                    existingProgress.Longitude = facilityProgress.Longitude;
                    existingProgress.Latitude = facilityProgress.Latitude;
                    existingProgress.ProgPercent = facilityProgress.ProgPercent;

                    #endregion

                    // Handle image upload
                    if (images != null && images.Count > 0)
                    {
                        existingProgress.Images.Clear();

                        foreach (var image in images)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await image.CopyToAsync(memoryStream);
                                existingProgress.Images.Add(new FacilityProgressImage
                                {
                                    ImageData = memoryStream.ToArray(),
                                    ImageContentType = image.ContentType,
                                    ImageName = image.FileName
                                });
                            }
                        }
                    }

                    _context.PMS_Facilityprogress.Update(existingProgress);
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


                // Update facility task
                var facilityTask = await _context.MS_Facilitytask.FirstOrDefaultAsync(ft => ft.FciltskId == facilityProgress.FcilTskId);

                facilityTask.FcilId = facilityProgress.FcilId;
                facilityTask.WbsdId = facilityProgress.WbsdId;
                facilityTask.Budget = facilityProgress.Budget;
                facilityTask.CntorId = facilityProgress.CntorId;
                facilityTask.SelectionTyp = facilityProgress.SelectionTyp;
                facilityTask.WorkstartDte = facilityProgress.WorkstartDte;
                facilityTask.WorkendDte = facilityProgress.WorkendDte;
                facilityTask.AwardedValue = facilityProgress.AwardedValue;
                facilityTask.ProgpayId = facilityProgress.ProgpayId;
                facilityTask.AllowSubmitExpense = facilityProgress.AllowSubmitExpense;
                facilityTask.TaskCompleteFlg = facilityProgress.TaskCompleteFlg;
                facilityTask.Remark = facilityProgress.Remark;



                _context.Update(facilityTask);
                await _context.SaveChangesAsync();




                return RedirectToAction(nameof(Index));
            }
            var facilityProg = await _context.PMS_Facilityprogress
                .Include(fp => fp.Images)
                .FirstOrDefaultAsync(fp => fp.ProgId == id);
            facilityProgress.Images = facilityProg.Images;
            ViewData["FcliNmeList"] = new SelectList(_context.MS_Facility.ToList(), "FcilId", "FcilNme");
            ViewData["CntorNmeList"] = new SelectList(_context.MS_Contractor.ToList(), "CntorId", "CntorNme");
            ViewData["WbsCdeList"] = new SelectList(_context.MS_Wbs.ToList(), "WbsId", "WbsCde");
            ViewData["PayList"] = new SelectList(_context.MS_Progresspayment.ToList(), "Progpayid", "Currcde");
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
                var user = _context.MS_User
                    .Where(u => u.UserCde == userCde)
                    .Select(u => new { u.UserNme, u.Position })
                    .FirstOrDefault();

                if (user != null)
                {
                    ViewData["Username"] = user.UserNme;
                    ViewData["Position"] = user.Position;
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetWbsDetails(int wbsId)
        {
            var wbsDetails = await _context.MS_Wbsdetail
                .Where(w => w.WbsId == wbsId)
                .Select(w => new { value = w.WbsdId, text = w.WbsdCde })
                .ToListAsync();

            return Json(wbsDetails);
        }
        #endregion
    }
}
