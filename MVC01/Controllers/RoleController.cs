using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC01.ViewModels;

namespace MVC01.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleVM role)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
                if (roleExist)
                {
                    ModelState.AddModelError("", "Role already exists");
                    return View(role);
                }
                var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));

                if (result.Succeeded)
                {


                    return View(role);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View(role);
                    }
                }
            }
            return View();

        }
    }

}