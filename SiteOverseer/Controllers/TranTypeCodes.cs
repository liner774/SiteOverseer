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
    public class TranTypeCodes : Controller
    {
        private readonly SiteDbContext _context;

        public TranTypeCodes(SiteDbContext context)
        {
            _context = context;
        }

        // GET: TranTypeCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MS_Trantypecode.ToListAsync());
        }

        // GET: TranTypeCodes/Details/5
        public async Task<IActionResult> Details(short? id)
        {
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

        // GET: TranTypeCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TranTypeCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrantypId,TrantypCde,TranNature,ContractorFlg,RequireClaim,CmpyId,UserId,RevDtetime")] TranTypeCode tranTypeCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tranTypeCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tranTypeCode);
        }

        // GET: TranTypeCodes/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranTypeCode = await _context.MS_Trantypecode.FindAsync(id);
            if (tranTypeCode == null)
            {
                return NotFound();
            }
            return View(tranTypeCode);
        }

        // POST: TranTypeCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("TrantypId,TrantypCde,TranNature,ContractorFlg,RequireClaim,CmpyId,UserId,RevDtetime")] TranTypeCode tranTypeCode)
        {
            if (id != tranTypeCode.TrantypId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: TranTypeCodes/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
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

        // POST: TranTypeCodes/Delete/5
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
    }
}
