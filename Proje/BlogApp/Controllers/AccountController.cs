using BlogApp.Data;
using BlogApp.Models.Concrete;
using BlogApp.ModelView;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using BlogApp.Utils;

namespace BlogApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogContext _context;

        public AccountController(BlogContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            HttpContext.Session.SetString("AdminName", "Hazal İlik");
            var sessionAdminName = HttpContext.Session.GetString("AdminName");

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30),
                HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append("AdminName", "", cookieOptions);
            var cookieAdminName = Request.Cookies["AdminName"];

            ViewBag.SessionAdminName = sessionAdminName;
            ViewBag.CookieAdminName = cookieAdminName;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new ModelViews());
        }

        [HttpPost]
        public async Task<IActionResult> Login(ModelViews model)
        {
            // Validasyon kontrolü: Sadece email/password dolu mu diye bakıyoruz
            var email = model.UserRegister?.Email;
            var password = model.UserRegister?.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Email ve şifre alanları boş olamaz.");
                return View(model);
            }

            // 1. ADIM: Önce User tablosunda Email ile kullanıcıyı bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // 2. ADIM: Kullanıcı varsa Şifreyi Doğrula (VerifyPassword)
            if (user != null && PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName ?? user.Email),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()), // User Id'yi claim olarak tutmak iyidir
                    new(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }

            // 3. ADIM: Admin tablosunda Email ile ara
            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == email);

            // 4. ADIM: Admin varsa Şifreyi Doğrula
            if (admin != null && PasswordHasher.VerifyPassword(password, admin.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, admin.Name ?? admin.Email),
                    new(ClaimTypes.Email, admin.Email),
                    new(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new(ClaimTypes.Role, "Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz email veya şifre.");
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new ModelViews());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(ModelViews model)
        {
            // Sadece UserRegister alanlarını validate et
            if (model.UserRegister == null) return View("Register", model);

            if (string.IsNullOrEmpty(model.UserRegister.Email) || string.IsNullOrEmpty(model.UserRegister.Password))
            {
                ModelState.AddModelError("UserRegister.Email", "Alanlar boş olamaz.");
                return View("Register", model);
            }

            if (await _context.Users.AnyAsync(u => u.Email == model.UserRegister.Email))
            {
                ModelState.AddModelError("UserRegister.Email", "Bu e-posta adresi zaten kayıtlı.");
                return View("Register", model);
            }

            // Şifreyi Hashle
            var hashedPassword = PasswordHasher.HashPassword(model.UserRegister.Password);

            var newUser = new User
            {
                Email = model.UserRegister.Email,
                PasswordHash = hashedPassword, // Hashlenmiş şifre
                UserName = model.UserRegister.UserName,
                CreatedAt = DateTime.Now
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(ModelViews model)
        {
            if (model.AdminRegister == null) return View("Register", model);

            // Basit validasyon
            if (string.IsNullOrEmpty(model.AdminRegister.Email) || string.IsNullOrEmpty(model.AdminRegister.Password))
            {
                ModelState.AddModelError("AdminRegister.Email", "Alanlar boş olamaz.");
                return View("Register", model);
            }

            if (await _context.Admin.AnyAsync(a => a.Email == model.AdminRegister.Email))
            {
                ModelState.AddModelError("AdminRegister.Email", "Bu e-posta adresi zaten kayıtlı.");
                return View("Register", model);
            }

            // Şifreyi Hashle
            var hashedPassword = PasswordHasher.HashPassword(model.AdminRegister.Password);

            var newAdmin = new Admin
            {
                Email = model.AdminRegister.Email,
                PasswordHash = hashedPassword, // Hashlenmiş şifre
                Name = model.AdminRegister.Name,
                CreatedAt = DateTime.Now
            };
            _context.Admin.Add(newAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}