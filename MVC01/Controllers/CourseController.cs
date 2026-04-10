using Microsoft.AspNetCore.Mvc;

namespace MVC01.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            var courses =BL.CourseBL.GetAllCourses();
            return View(courses);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Models.Course course)
        {
            if (course == null || string.IsNullOrEmpty(course.Name)) 
            {
                return View(course);
            }

            if (BL.CourseBL.AddCourse(course))
            {
                return RedirectToAction("Index");
            }
            return View(course);
        }
    }
}