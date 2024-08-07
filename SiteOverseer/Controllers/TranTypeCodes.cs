﻿using System;
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
    [Authorize(Roles = "Administrator")]
    public class TranTypeCodes : Controller
    {
        private readonly SiteDbContext _context;

        public TranTypeCodes(SiteDbContext context)
        {
            _context = context;
        }
        #region // Main Methods //
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            return View(await _context.MS_Trantypecode.ToListAsync());
        }

 
        public async Task<IActionResult> Details(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeCode = await _context.MS_Trantypecode
                .FirstOrDefaultAsync(m => m.TrantypId == id);
            if (tranTypeCode == null)
            {
                return NotFound();
            }

            tranTypeCode.Company = _context.MS_Company.Where(c => c.CmpyId == tranTypeCode.CmpyId).Select(c => c.CmpyNme).FirstOrDefault();
            tranTypeCode.User = _context.MS_User.Where(u => u.UserId == tranTypeCode.UserId).Select(u => u.UserNme).FirstOrDefault();
            return View(tranTypeCode);
        }

         
        public IActionResult Create()
        {
            SetLayOutData();
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrantypId,TrantypCde,TranNature,ContractorFlg,RequireClaim")] TranTypeCode tranTypeCode)
        {
            if (ModelState.IsValid)
            {
                tranTypeCode.RevDtetime = DateTime.Now;
                tranTypeCode.CmpyId = GetCmpyId();
                tranTypeCode.UserId = GetUserId();
                _context.Add(tranTypeCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tranTypeCode);
        }
 
        public async Task<IActionResult> Edit(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeCode = await _context.MS_Trantypecode.FindAsync(id);
            if (tranTypeCode == null)
            {
                return NotFound();
            }
            tranTypeCode.RevDtetime = DateTime.Now;
            return View(tranTypeCode);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("TrantypId,TrantypCde,TranNature")] TranTypeCode tranTypeCode)
        {
            if (id != tranTypeCode.TrantypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tranTypeCode.RevDtetime = DateTime.Now;
                    tranTypeCode.CmpyId = GetCmpyId();
                    tranTypeCode.UserId = GetUserId();
                    _context.Update(tranTypeCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranTypeCodeExists(tranTypeCode.TrantypId))
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
            return View(tranTypeCode);
        }


        public async Task<IActionResult> Delete(short? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeCode = await _context.MS_Trantypecode
                .FirstOrDefaultAsync(m => m.TrantypId == id);
            if (tranTypeCode == null)
            {
                return NotFound();
            }

            return View(tranTypeCode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var tranTypeCode = await _context.MS_Trantypecode.FindAsync(id);
            if (tranTypeCode != null)
            {
                _context.MS_Trantypecode.Remove(tranTypeCode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranTypeCodeExists(short id)
        {
            return _context.MS_Trantypecode.Any(e => e.TrantypId == id);
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
        #endregion
    }
}
