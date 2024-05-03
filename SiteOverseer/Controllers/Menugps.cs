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
    public class Menugps : Controller
    {
        private readonly SiteDbContext _context;

        public Menugps(SiteDbContext context)
        {
            _context = context;
        }

        // GET: Menugps
        public async Task<IActionResult> Index()
        {
            return View(await _context.MS_Menugp.ToListAsync());
        }

        // GET: Menugps/Details/5
        public async Task<IActionResult> Details(short? id)
        {
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

        // GET: Menugps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menugps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MnugrpId,MnugrpNme,RevdTetime")] Menugp menugp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menugp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menugp);
        }

        // GET: Menugps/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
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

        // POST: Menugps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("MnugrpId,MnugrpNme,RevdTetime")] Menugp menugp)
        {
            if (id != menugp.MnugrpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: Menugps/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
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

        // POST: Menugps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
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
            return _context.MS_Menugp.Any(e => e.MnugrpId == id);
        }
    }
}
