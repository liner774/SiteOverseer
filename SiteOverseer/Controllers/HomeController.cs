using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Data;
using SiteOverseer.Models;
using System.Diagnostics;

namespace SiteOverseer.Controllers
{
    [Authorize]
    
    public class HomeController : Controller
    {
        private readonly SiteDbContext _context;

        public HomeController(SiteDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            SetLayOutData();

            var progressData = (from p in _context.PMS_Facilityprogress
                                join ft in _context.MS_Facilitytask on p.FcilTskId equals ft.FciltskId
                                join f in _context.MS_Facility on ft.FcilId equals f.FcilId
                                orderby ft.WorkendDte descending
                                select new
                                {
                                    progId = p.ProgId,
                                    progPercent = p.ProgPercent,
                                    fciltskId = p.FcilTskId,
                                    fcilId = ft.FcilId,
                                    fcilnme = f.FcilNme,
                                    workendDte = ft.WorkendDte,
                                    workstartDte = ft.WorkstartDte
                                }).Take(5).ToList();

            return View(progressData);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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


    }
}
