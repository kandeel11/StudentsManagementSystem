using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC01.Models;
using MVC01.ViewModels;
using System.Security.Claims;

namespace MVC01.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM model, IFormFile ImageUrl)
        {
            if(ImageUrl != null && ImageUrl.Length > 0)
            {
                string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                using (var memoryStream = new MemoryStream())
                {
                    await ImageUrl.CopyToAsync(memoryStream);
                    string imagePath = Path.Combine("wwwroot/images", imageFileName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        await memoryStream.CopyToAsync(fileStream);
                    }
                    model.ImageUrl = "/images/" + imageFileName; // Store the relative path to the image
                    
                }
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    Address = model.Address,
                    ImageUrl=model.ImageUrl
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("Image", user.ImageUrl ?? ""),
                        new Claim("Address", user.Address ?? "")
                    };
                    await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
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
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Account.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}