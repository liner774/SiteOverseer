using System;
using System.Collections.Generic;
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
    
    public class Users : Controller
    {
        private readonly SiteDbContext _context;

        public Users(SiteDbContext context)
        {
            _context = context;
        }

        #region //Main Method//
        public async Task<IActionResult> Index()
        {
            //Testing 
            return View(await _context.MS_User.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.MS_User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserCde,UserNme,Pwd,Position,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                user.RevdTetime = DateTime.Now;
                user.UserId = 1;
               
                user.CmpyId = 1;

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

            return View(user);
        }
        protected short GetUserId()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var userId = (short)_context.MS_User
                .Where(u => u.UserCde== userCde)
                .Select(u => u.UserId)
                .FirstOrDefault();

            return userId;
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.MS_User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserCde,UserNme,Position,Gender,Pwd")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.RevdTetime = DateTime.Now;
                    user.CmpyId = 1; //default
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.MS_User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.MS_User.FindAsync(id);
            if (user != null)
            {
                _context.MS_User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.MS_User.Any(e => e.UserId == id);
        }
        #endregion

    }
}
