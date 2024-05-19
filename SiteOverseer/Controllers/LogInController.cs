using Microsoft.AspNetCore.Authentication.Cookies;
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

namespace SiteOverseer.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly SiteDbContext _context;
        private readonly EncryptDecryptService _encryptDecryptService;

        // A mock user repository for demonstration
        private static readonly Dictionary<string, string> UserStore = new Dictionary<string, string>();

        public LogInController(EncryptDecryptService encryptDecryptService, ILogger<LogInController> logger, SiteDbContext context)
        {
            _logger = logger;
            _context = context;
            _encryptDecryptService = encryptDecryptService;
        }


        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity != null && claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "SaleDashboard");
            }
            return RedirectToAction("LogIn", "Home"); // Use RedirectToAction instead of RedirectToActionResult
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<LogInController> logIn(string uc, string p, LogInUser user)
        {
            var userList = await _context.MS_User.ToListAsync();
            if ((!string.IsNullOrEmpty(uc)) && (!string.IsNullOrEmpty(p)))
            {
                try
                {
                    var dbUser = userList.FirstOrDefault(u => u.UserCde.ToLower() == u.UserCde.ToLower() && user.Pwd == user.Pwd);

                    if (dbUser != null)
                    {
                        if (user.Pwd == p)
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

                            }
                            catch (Exception ex)
                            {
                                ViewBag.AlertMessage = ex.Message;
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
                }
             
            }
            else
            {
                ViewBag.AlertMessage = "All fields must be filled!";
            }

        }


    }
}
