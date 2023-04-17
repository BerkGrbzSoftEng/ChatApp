using System.Security.Claims;
using ChatAppMVC.Context;
using ChatAppMVC.Data;
using ChatAppMVC.Hub;
using ChatAppMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatAppMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ChatAppContext _context;

        public UserController(ChatAppContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> IsLogin(LoginVM model)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.Username
                                                                     && x.Password == model.Password);

            var claims = new List<Claim>
            {
                new Claim("User", user.UserName), 
                new Claim("Fullname",user.Name+" "+user.Surname),
                new Claim(ClaimTypes.Role, "Member")
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            if (user != null)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)); 
                return Json(new { Message = "Yönlendiriliyorsunuz...", Header = "Basarili", result = true });
            }
            else
            {
                return Json(new { Message = "Kullanici Adi veya Sifre Hatali", result = false });
            }
        }


        [HttpPost]
        public async Task<JsonResult> Register(RegisterVM model)
        {

            var user = new User
            {
                LastLogin = DateTime.Now,
                Name = model.Name,
                Password = model.Password,
                Surname = model.Surname,
                UserName = model.Username
            };
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Json(new { result = true, Message = "Kayit İslemi Basariyla Gerceklestirildi", Header = "Basarili" });
        }

    }
}
