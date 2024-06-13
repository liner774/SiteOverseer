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
    public class ProgressPaymentsController : Controller
    {
        private readonly SiteDbContext _context;

        public ProgressPaymentsController(SiteDbContext context)
        {
            _context = context;
        }

        // GET: ProgressPayments
        public async Task<IActionResult> Index()            
        {
            SetLayOutData();
            return View(await _context.MS_Progresspayment.ToListAsync());
        }

        // GET: ProgressPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var progressPayment = await _context.MS_Progresspayment
                .FirstOrDefaultAsync(m => m.Progpayid == id);
            if (progressPayment == null)
            {
                return NotFound();
            }
            progressPayment.CmpyNme = _context.MS_Company.Where(c => c.CmpyId == progressPayment.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            progressPayment.UserNme = _context.MS_User.Where(u => u.UserId == progressPayment.UserId).Select(u => u.UserNme).FirstOrDefault();

            return View(progressPayment);
        }

        // GET: ProgressPayments/Create
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Currcde,Currate")] ProgressPayment progressPayment)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                progressPayment.RevdTetime = DateTime.Now;
                progressPayment.CmpyId = GetCmpyId();
                progressPayment.UserId = GetUserId();
                _context.Add(progressPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(progressPayment);
        }

        // GET: ProgressPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var progressPayment = await _context.MS_Progresspayment.FindAsync(id);
            if (progressPayment == null)
            {
                return NotFound();
            }
            return View(progressPayment);
        }

        // POST: ProgressPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Progpayid,Currcde,Currate,")] ProgressPayment progressPayment)
        {
            SetLayOutData();
            if (id != progressPayment.Progpayid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    progressPayment.RevdTetime = DateTime.Now;
                    progressPayment.CmpyId = GetCmpyId();
                    progressPayment.UserId = GetUserId();
                    _context.Update(progressPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgressPaymentExists(progressPayment.Progpayid))
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
            return View(progressPayment);
        }

        // GET: ProgressPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var progressPayment = await _context.MS_Progresspayment
                .FirstOrDefaultAsync(m => m.Progpayid == id);
            if (progressPayment == null)
            {
                return NotFound();
            }
            progressPayment.CmpyNme = _context.MS_Company.Where(c => c.CmpyId == progressPayment.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            progressPayment.UserNme = _context.MS_User.Where(u => u.UserId == progressPayment.UserId).Select(u => u.UserNme).FirstOrDefault();

            return View(progressPayment);
        }

        // POST: ProgressPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var progressPayment = await _context.MS_Progresspayment.FindAsync(id);
            if (progressPayment != null)
            {
                _context.MS_Progresspayment.Remove(progressPayment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgressPaymentExists(int id)
        {
            return _context.MS_Progresspayment.Any(e => e.Progpayid == id);
        }

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
    }
}
