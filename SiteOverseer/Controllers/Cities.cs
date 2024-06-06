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
    public class Cities : Controller
    {
        private readonly SiteDbContext _context;

        public Cities(SiteDbContext context)
        {
            _context = context;
        }

        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.MS_City.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();  
            }

            var city = await _context.MS_City
                .FirstOrDefaultAsync(m => m.CityId == id);


            if (city == null)
            {
                return NotFound();
            }

            city.Company = _context.MS_Company.Where(c => c.CmpyId == city.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            city.User = _context.MS_User.Where(u => u.UserId == city.UserId).Select(u => u.UserNme).FirstOrDefault();
            return View(city);
        }


        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,CityCode,CityName")] City city)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                city.CmpyId = GetCmpyId();
                city.UserId = GetUserId();
                city.RevDtetime = DateTime.Now;
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.MS_City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,CityCode,CityName")] City city)
        {
            SetLayOutData();
            if (id != city.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    city.CmpyId = GetCmpyId();
                    city.UserId = GetUserId();
                    city.RevDtetime = DateTime.Now;
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.CityId))
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
            return View(city);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.MS_City
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SetLayOutData();
            var city = await _context.MS_City.FindAsync(id);
            if (city != null)
            {
                _context.MS_City.Remove(city);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            SetLayOutData();
            return _context.MS_City.Any(e => e.CityId == id);
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
