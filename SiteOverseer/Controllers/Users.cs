﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Common.EncryptDecryptService;
using SiteOverseer.Data;
using SiteOverseer.Models;

namespace SiteOverseer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Users : Controller
    {
        private readonly SiteDbContext _context;
        private readonly EncryptDecryptService _encryptDecryptService;

        // for github push
        public Users(SiteDbContext context)
        {
            _context = context;
            _encryptDecryptService = new EncryptDecryptService();
        }


        #region //Main Method//
        public async Task<IActionResult> Index()
        {
            SetLayOutData();
            //Testing 
            return View(await _context.MS_User.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            SetLayOutData();
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
            SetLayOutData();
            ViewData["Positions"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpNme", "MnugrpNme");
            ViewData["Companies"] = new SelectList(_context.MS_Company.ToList(), "CmpyId", "CmpyNme");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserCde,UserNme,CmpyId,Position,Gender,Password,ConfirmPassword")] User user)
        {
            SetLayOutData();


            if (ModelState.IsValid && user.Password == user.ConfirmPassword)
            {
                user.Password ??= "User@123";
                string encodedString = _encryptDecryptService.EncryptString(user.Password);
                user.Pwd = Encoding.UTF8.GetBytes(encodedString);
                user.RevdTetime = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                ModelState.AddModelError("NewPassword", "The new password and confirm password do not match.");
                ModelState.AddModelError("ConfirmPassword", "The new password and confirm password do not match.");
                ViewData["Positions"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpNme", "MnugrpNme");
                ViewData["Companies"] = new SelectList(_context.MS_Company.ToList(), "CmpyId", "CmpyNme");
                return View(user);
            }                        
            
        }



        public async Task<IActionResult> Edit(int? id)
        {
            SetLayOutData();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.MS_User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["Positions"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpNme", "MnugrpNme");
            ViewData["Companies"] = new SelectList(_context.MS_Company.ToList(), "CmpyId", "CmpyNme");
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserCde,UserNme,CmpyId,Position,Gender")] User user)
        {
            SetLayOutData();

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldUser = _context.MS_User.Where(u => u.UserId == user.UserId).FirstOrDefault(); // to get old password again
                    if (oldUser != null)
                    {
                        oldUser.UserNme = user.UserNme;
                        oldUser.CmpyId = user.CmpyId;
                        oldUser.Position = user.Position;
                        oldUser.Gender = user.Gender;
                        oldUser.RevdTetime = DateTime.Now;
                        _context.Update(oldUser);
                        await _context.SaveChangesAsync();
                    }
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

            ViewData["Positions"] = new SelectList(_context.MS_Menugp.ToList(), "MnugrpNme", "MnugrpNme");
            ViewData["Companies"] = new SelectList(_context.MS_Company.ToList(), "CmpyId", "CmpyNme");
            return View(user);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            SetLayOutData();
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
            SetLayOutData();
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


        #region // Global Methods //

        protected short GetUserId()
        {
            var userCde = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var userId = (short)_context.MS_User
                .Where(u => u.UserCde == userCde)
                .Select(u => u.UserId)
                .FirstOrDefault();

            return userId;
        }

        #endregion

    }
}
