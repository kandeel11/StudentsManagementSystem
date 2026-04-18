using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC01.Models;
using MVC01.ViewModels;
using System.Data;
using System.Security.Claims;

namespace MVC01.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {

            if (!User.IsInRole("Admin"))
            {
                if(User?.Identity?.IsAuthenticated ?? false)
                {
                    TempData["ErrorMessage"] = "You do not have permission to access this page.";
                    return RedirectToAction("Index", "Home");
                }

                var roles = _roleManager.Roles.Where(r => r.Name != "Admin").ToList();
                ViewBag.Roles = roles;
            }
            else
            {
                ViewBag.Roles = _roleManager.Roles.ToList();
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM model, IFormFile ImageUrl)
        {

            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                using (var stream = new FileStream(Path.Combine("wwwroot/images", imageFileName), FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }
                model.ImageUrl = imageFileName;
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    Address = model.Address,
                    ImageUrl = model.ImageUrl
                };


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                        TempData["SuccessMessage"] = $" account created successfully! For {user.UserName}";
                        return RedirectToAction("Index", "Home");
                    }
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("Image", user.ImageUrl ?? ""),
                        new Claim("Address", user.Address ?? "")
                    };
                    await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
                    TempData["SuccessMessage"] = $" account created successfully! Welcome {user.UserName}";
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                TempData["SuccessMessage"] = $"You are already logged in!";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                TempData["SuccessMessage"] = $"You are already logged in!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Invalid Account.");
                        return View(model);
                    }
                    bool isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (isPasswordValid)
                    {
                        List<Claim> claims = new List<Claim>
                    {
                        new Claim("Image", user.ImageUrl ?? ""),
                        new Claim("Address", user.Address ?? "")
                    };
                        await _signInManager.SignInWithClaimsAsync(user, isPersistent: model.RememberMe, claims);
                        TempData["SuccessMessage"] = $"Login successful! Welcome {user.UserName}";
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid Account.");
                }
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Profile()
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var nameClaim = claimsIdentity?.FindFirst(ClaimTypes.Name);
            var imageClaim = claimsIdentity?.FindFirst("Image");
            var addressClaim = claimsIdentity?.FindFirst("Address");
            var model = new ProfileVM
            {
                UserName = nameClaim?.Value,
                ImageUrl = imageClaim?.Value,
                Address = addressClaim?.Value
            };
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}