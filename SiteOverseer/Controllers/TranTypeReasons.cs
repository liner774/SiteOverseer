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
    public class TranTypeReasons : Controller
    {
        private readonly SiteDbContext _context;

        public TranTypeReasons(SiteDbContext context)
        {
            _context = context;
        }

        #region // Main methods //
        public async Task<IActionResult> Index()
        {
            return View(await _context.MS_Trantypereason.ToListAsync());
        }

        
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeReason = await _context.MS_Trantypereason
                .FirstOrDefaultAsync(m => m.TranReasonId == id);
            if (tranTypeReason == null)
            {
                return NotFound();
            }

            return View(tranTypeReason);
        }


        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TranReasonDesc")] TranTypeReason tranTypeReason)
        {
            if (ModelState.IsValid)
            { 
                tranTypeReason.RevDtetime = DateTime.Now;
                tranTypeReason.CmpyId = 1; //default
                tranTypeReason.UserId = 1; //default
                _context.Add(tranTypeReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tranTypeReason);
        }

        public async Task<IActionResult> Edit(short? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            var tranTypeReason = await _context.MS_Trantypereason.FindAsync(id);
            if (tranTypeReason == null)
            {
                return NotFound();

            }

            tranTypeReason.RevDtetime = DateTime.Now;
            return View(tranTypeReason);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("TranReasonId,TranReasonDesc")] TranTypeReason tranTypeReason)
        {
            if (id != tranTypeReason.TranReasonId)
            {
                
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tranTypeReason.RevDtetime = DateTime.Now;
                    tranTypeReason.UserId = 1; //default
                    tranTypeReason.CmpyId = 1; //default
                    _context.Update(tranTypeReason);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranTypeReasonExists(tranTypeReason.TranReasonId))
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

            
            return View(tranTypeReason);
        }

        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeReason = await _context.MS_Trantypereason
                .FirstOrDefaultAsync(m => m.TranReasonId == id);
            if (tranTypeReason == null)
            {
                return NotFound();
            }

            return View(tranTypeReason);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var tranTypeReason = await _context.MS_Trantypereason.FindAsync(id);
            if (tranTypeReason != null)
            {
                _context.MS_Trantypereason.Remove(tranTypeReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranTypeReasonExists(short id)
        {
            return _context.MS_Trantypereason.Any(e => e.TranReasonId == id);
        }

        #endregion 
    }
}
