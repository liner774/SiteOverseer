﻿using System;
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
    [Authorize(Roles = "Administrator")]
    public class Menugps : Controller
    {
        private readonly SiteDbContext _context;

        // Test by Ko Kg
        public Menugps(SiteDbContext context)
        {
            _context = context;
        }

        #region // Main methods  //

        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.MS_Menugp.ToListAsync());
        }

        public async Task<IActionResult> Details(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.MS_Menugp
                .FirstOrDefaultAsync(m => m.MnugrpId == id);
            if (menugp == null)
            {
                return NotFound();
            }

            return View(menugp);
        }

        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MnugrpNme")] Menugp menugp)
        {
            SetLayOutData();
            if (ModelState.IsValid)
            {
                menugp.RevdTetime = DateTime.Now;
                _context.Add(menugp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menugp);
        }


        public async Task<IActionResult> Edit(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.MS_Menugp.FindAsync(id);
            if (menugp == null)
            {
                return NotFound();
            }
            return View(menugp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("MnugrpId,MnugrpNme")] Menugp menugp)
        {
            SetLayOutData();
            if (id != menugp.MnugrpId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    menugp.RevdTetime = DateTime.Now;
                    _context.Update(menugp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenugpExists(menugp.MnugrpId))
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
            return View(menugp);
        }

        public async Task<IActionResult> Delete(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var menugp = await _context.MS_Menugp
                .FirstOrDefaultAsync(m => m.MnugrpId == id);
            if (menugp == null)
            {
                return NotFound();
            }

            return View(menugp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            SetLayOutData();
            var menugp = await _context.MS_Menugp.FindAsync(id);
            if (menugp != null)
            {
                _context.MS_Menugp.Remove(menugp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenugpExists(short id)
        {
            SetLayOutData();
            return _context.MS_Menugp.Any(e => e.MnugrpId == id);
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

        #endregion


        #region//Grobal Method//
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

    }
}
