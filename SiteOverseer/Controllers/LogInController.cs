﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SiteOverseer.Common;
using SiteOverseer.Common.EncryptDecryptService;
using SiteOverseer.Data;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SiteOverseer.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace SiteOverseer.Controllers
{
    public class LogInController : Controller
    {
        private readonly SiteDbContext _context;
        private readonly EncryptDecryptService _encryptDecryptService;

        public LogInController(SiteDbContext context)
        {
            _context = context;
            _encryptDecryptService = new EncryptDecryptService();
        }


        #region // Main Methods //


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LogInUser user)
        {
            if ((!string.IsNullOrEmpty(user.UserCde)) && (!string.IsNullOrEmpty(user.Pwd)))
            {
                var encryptedPwd = _encryptDecryptService.EncryptString(user.Pwd);
                try
                {
                    var dbUser = _context.MS_User.FirstOrDefault(u => u.UserCde.ToLower() == user.UserCde.ToLower());

                    if (dbUser != null && dbUser.Pwd != null)
                    {

                        string strbytes = Encoding.UTF8.GetString(dbUser.Pwd);

                        var decryptedPwd = _encryptDecryptService.DecryptString(strbytes);

                        //if (user.Pwd != null)
                        if (user.Pwd == decryptedPwd)
                        {
                            try
                            {
                                var claims = new List<Claim>() {
                                new (ClaimTypes.NameIdentifier,user.UserCde)
                                };

                                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                var properties = new AuthenticationProperties()
                                {
                                    AllowRefresh = true
                                };

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                                var puser = await _context.MS_User
                                .Where(x => x.UserCde == user.UserCde)
                                .Select(x => x.UserCde)
                                .FirstOrDefaultAsync() ?? "";

                                return RedirectToAction("Index", "Home");

                            }
                            catch (Exception ex)
                            {
                                ViewBag.AlertMessage = ex.Message;
                                return View(user);
                            }
                        }
                        else
                        {
                            ViewBag.AlertMessage = "Authentication failed. Please check your credentials.";
                        }

                    }
                    else
                    {
                        ViewBag.AlertMessage = "Authentication failed. Please check your credentials.";
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.AlertMessage = ex.Message;
                    return View(user);
                }

            }
            else
            {
                ViewBag.AlertMessage = "All fields must be filled!";
            }

            return View(user);

        }

        #endregion


    }
}
