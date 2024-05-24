using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Common.EncryptDecryptService;
using SiteOverseer.Controllers;
using SiteOverseer.Data;
using SiteOverseer.Models;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

public class ChangePasswords : Controller
{
    private readonly EncryptDecryptService _encryptDecryptService;
    private readonly SiteDbContext _context;
    public ChangePasswords(SiteDbContext context)
    {
        _context = context;
        _encryptDecryptService = new EncryptDecryptService();

    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> ChangePassword(int? id)
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

        ChangePassword changePassword = new ChangePassword
        {
            UserId = user.UserId,
            UserCde = user.UserCde,
            UserNme = user.UserNme
        };

        return View(changePassword);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ChangePassword(int id, [Bind("UserId,UserCde,UserNme,Password,ConfirmPassword,NewPassword")] ChangePassword model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var oldUser = _context.MS_User.Where(u => u.UserId == model.UserId).FirstOrDefault();
                if (oldUser != null && oldUser.Pwd != null)
                {
                    var stringOldPwd = Encoding.UTF8.GetString(oldUser.Pwd);
                    var decryptedPwd = _encryptDecryptService.DecryptString(stringOldPwd);

                    if (model.Password == decryptedPwd) // check old password correct or not
                    {
                        if (model.NewPassword == model.ConfirmPassword)// check new password and confirm password are same or not
                        {
                            var encryptedPwd = _encryptDecryptService.EncryptString(model.NewPassword);
                            oldUser.Pwd = Encoding.UTF8.GetBytes(encryptedPwd);
                            _context.Update(oldUser);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            ModelState.AddModelError("ConfirmPassword", "The new password and confirmation password do not match.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Old password does not match.");
                        return View(model);
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return View(model);
        }
        return View(model);
    }


    //private string HashPassword(string password)
    //{
    //    using (var sha256 = SHA256.Create())
    //    {
    //        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    //    }
    //}

}
