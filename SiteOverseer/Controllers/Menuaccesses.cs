﻿using System;
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
    public class Menuaccesses : Controller
    {
        private readonly SiteDbContext _context;

        public Menuaccesses(SiteDbContext context)
        {
            _context = context;
        }


        #region //Main Method//

     
        public async Task<IActionResult> Index()
        {
            var menuAccessList = await _context.MS_Menuaccess.ToListAsync();

            foreach (var menuAccess in menuAccessList)
            {
                menuAccess.MnugrpNme = _context.MS_Menugp.Where(gp => gp.MnugrpId == menuAccess.MnugrpId).Select(gp => gp.MnugrpNme).FirstOrDefault();
            }

            return View(menuAccessList);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuaccess = await _context.MS_Menuaccess .FirstOrDefaultAsync(m => m.AccessId == id);
            if (menuaccess == null)
            {
                return NotFound();
            }
            menuaccess.MnugrpNme = _context.MS_Menugp.Where(gp => gp.MnugrpId == menuaccess.MnugrpId).Select(gp => gp.MnugrpNme).FirstOrDefault();

            return View(menuaccess);
        }

        
        public IActionResult Create()
        {
            ViewData["MenuGpList"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MnugrpId,BtnNme")] Menuaccess menuaccess)
        {
            if (ModelState.IsValid)
            {
                menuaccess.RevdTetime = DateTime.Now;
                
                _context.Add(menuaccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewData["MenuGpList"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpId", "MnugrpNme");

            return View(menuaccess);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuaccess = await _context.MS_Menuaccess.FindAsync(id);
            if (menuaccess == null)
            {
                return NotFound();
            }
            ViewData["MenuGpList"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpId", "MnugrpNme");
            return View(menuaccess);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessId,MnugrpId,BtnNme")] Menuaccess menuaccess)
        {
            if (id != menuaccess.AccessId)
               
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    menuaccess.RevdTetime = DateTime.Now;
                    _context.Update(menuaccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuaccessExists(menuaccess.AccessId))
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
           

            return View(menuaccess);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuaccess = await _context.MS_Menuaccess
                .FirstOrDefaultAsync(m => m.AccessId == id);

            if (menuaccess == null)
            {
                return NotFound();
            }

            menuaccess.MnugrpNme = _context.MS_Menugp.Where(gp => gp.MnugrpId == menuaccess.MnugrpId).Select(gp => gp.MnugrpNme).FirstOrDefault();

            return View(menuaccess);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var menuaccess = await _context.MS_Menuaccess.FindAsync(id);
            if (menuaccess != null)
            {
                menuaccess.MnugrpNme = _context.MS_Menugp.Where(gp => gp.MnugrpId == menuaccess.MnugrpId).Select(gp => gp.MnugrpNme).FirstOrDefault();

                _context.MS_Menuaccess.Remove(menuaccess);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuaccessExists(int id)
        {

            return _context.MS_Menuaccess.Any(e => e.AccessId == id);
        }
        #endregion
    }
}
