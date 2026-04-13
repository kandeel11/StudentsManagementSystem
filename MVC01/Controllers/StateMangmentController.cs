using Microsoft.AspNetCore.Mvc;

namespace MVC01.Controllers
{
    public class StateMangmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Querystring()
        {
            HttpContext.Request.Query.TryGetValue("name", out var nameValue);
                HttpContext.Request.Query.TryGetValue("password", out var passwordValue);
                return Content($"Name: {nameValue}, Password: {passwordValue}");
        }
        public IActionResult HiddenField()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HiddenField(string name, int password,string Hidden)
        {
            return Content($"Name: {name}, Password: {password}, Hidden: {Hidden}");

        }
        

        public IActionResult SetSession(string name, int password)
        {
            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetInt32("Password", password);

            return Content("Session values set successfully.");
        }

        public IActionResult GetSession()
        {
            return Content($"Name: {HttpContext.Session.GetString("Name")}, Password: {HttpContext.Session.GetInt32("Password")}");
        }

        public IActionResult SetCookie(string name, int password)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                Secure = true
            };
            Response.Cookies.Append("Name", name, options);
            Response.Cookies.Append("Password", password.ToString(), options);
            return Content("Cookie values set successfully.");
        }
        public IActionResult GetCookie()
        {
            var name = Request.Cookies["Name"];
            var password = Request.Cookies["Password"];
            return Content($"Name: {name}, Password: {password}");
        }

        public IActionResult SetTempData(string name, int password)
        {
            TempData["Name"] = name;
            TempData["Password"] = password;
            return Content("TempData values set successfully.");
        }

        public IActionResult GetTempData() {
            var name = TempData["Name"] as string;
            var password = TempData["Password"] as int?;
            return Content($"Name: {name}, Password: {password}");
        }


    }
}
