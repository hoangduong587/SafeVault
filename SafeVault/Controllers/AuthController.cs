using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeVault.Data;
using SafeVault.Helpers;
using SafeVault.Models;

namespace SafeVault.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        [HttpGet]
public IActionResult GenerateAdminHash()
{
    string hash = HashingHelper.HashPassword("Test123456");
    return Content(hash);
}

        public AuthController(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // ============================
        // LOGIN (GET)
        // ============================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ============================
        // LOGIN (POST)
        // ============================
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // 1. XSS sanitization
            model.Username = SecurityHelpers.SanitizeXSS(model.Username);
            model.Password = SecurityHelpers.SanitizeXSS(model.Password);

            // 2. Username validation
            if (!ValidationHelpers.ValidateUsername(model.Username, out string userError))
            {
                ModelState.AddModelError("Username", userError);
                return View(model);
            }

            // 3. Password validation
            if (!ValidationHelpers.ValidatePassword(model.Password, out string passError))
            {
                ModelState.AddModelError("Password", passError);
                return View(model);
            }

            // 4. Database lookup
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            // 5. Hash verification
            bool isPasswordValid = HashingHelper.VerifyPassword(model.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            // 6. JWT generation
            string token = JWTHelpers.GenerateToken(
                username: user.Username,
                role: user.Role,
                secretKey: _config["Jwt:SecretKey"],
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expirationMinutes: 60
            );

            // 7. Return token (JSON)
            return Json(new
            {
                success = true,
                token = token,
                username = user.Username,
                role = user.Role
            });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Client should delete the JWT token.
            // Server redirects to Login page.
            return RedirectToAction("Login", "Auth");
        }

        // ============================
        // REGISTER (GET)
        // ============================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        // ============================
        // REGISTER (POST)
        // ============================
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // 1. XSS sanitization
            model.Username = SecurityHelpers.SanitizeXSS(model.Username);
            model.Password = SecurityHelpers.SanitizeXSS(model.Password);

            // 2. Username validation
            if (!ValidationHelpers.ValidateUsername(model.Username, out string userError))
            {
                ModelState.AddModelError("Username", userError);
                return View(model);
            }

            // 3. Password validation
            if (!ValidationHelpers.ValidatePassword(model.Password, out string passError))
            {
                ModelState.AddModelError("Password", passError);
                return View(model);
            }

            // 4. Check if username already exists
            bool exists = await _db.Users.AnyAsync(u => u.Username == model.Username);
            if (exists)
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(model);
            }

            // 5. Hash password
            string hashedPassword = HashingHelper.HashPassword(model.Password);

            // 6. Create user entity
            var user = new User
            {
                Username = model.Username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            // 7. Save to database
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // 8. Return success JSON
            return Json(new
            {
                success = true,
                message = "Registration successful. You can now log in."
            });

            
        }
    }
}