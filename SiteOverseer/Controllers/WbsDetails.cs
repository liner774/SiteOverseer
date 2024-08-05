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
    public class WbsDetails : Controller
    {
        private readonly SiteDbContext _context;

        public WbsDetails(SiteDbContext context)
        {
            _context = context;
        }

        // GET: WbsDetails
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.MS_Wbsdetail.ToListAsync());
        }

        // GET: WbsDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var wbsDetail = await _context.MS_Wbsdetail
                .FirstOrDefaultAsync(m => m.WbsdId == id);
            if (wbsDetail == null)
            {
                return NotFound();
            }

            return View(wbsDetail);
        }

        // GET: WbsDetails/Create
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

        // POST: WbsDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WbsdId,WbsId,WbsdCde,CmpyId,UserId,RevdTetime")] WbsDetail wbsDetail)
        {
            if (ModelState.IsValid)
            {
                wbsDetail.RevdTetime = DateTime.Now;
                _context.Add(wbsDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wbsDetail);
        }

        // GET: WbsDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var wbsDetail = await _context.MS_Wbsdetail.FindAsync(id);
            if (wbsDetail == null)
            {
                return NotFound();
            }
            return View(wbsDetail);
        }

        // POST: WbsDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WbsdId,WbsId,WbsdCde,CmpyId,UserId,RevdTetime")] WbsDetail wbsDetail)
        {
            if (id != wbsDetail.WbsdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wbsDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WbsDetailExists(wbsDetail.WbsdId))
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
            return View(wbsDetail);
        }

        // GET: WbsDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var wbsDetail = await _context.MS_Wbsdetail
                .FirstOrDefaultAsync(m => m.WbsdId == id);
            if (wbsDetail == null)
            {
                return NotFound();
            }

            return View(wbsDetail);
        }

        // POST: WbsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wbsDetail = await _context.MS_Wbsdetail.FindAsync(id);
            if (wbsDetail != null)
            {
                _context.MS_Wbsdetail.Remove(wbsDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WbsDetailExists(int id)
        {
            return _context.MS_Wbsdetail.Any(e => e.WbsdId == id);
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
    }
}
