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

        public IActionResult Details(int id)
        {
            var course = BL.CourseBL.GetCourseById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Models.Course course)
        {
            if(!ModelState.IsValid)
            {
                return View(course);
            }

            if (BL.CourseBL.AddCourse(course))
            {
                return RedirectToAction("Index");
            }
            return View(course);
        }
        public IActionResult ValidateCourseDegree(int Total_Degree, int Min_Degree) 
        {
            if (Total_Degree <= Min_Degree)
            {
                return Json($"Total Degree must be greater than Min Degree.");
            }
            return Json(true);
        }

        public IActionResult EditCourse(int id)
        {
            var course = BL.CourseBL.GetCourseById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult EditCourse(int id, Models.Course course)
        {
            
            if (course == null || string.IsNullOrEmpty(course.Name))
            {
                return View(course);
            }
            course.Crs_Id = id; 
            if (BL.CourseBL.UpdateCourse(course))
            {
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public IActionResult DeleteCourse(int id)
        {
            var course = BL.CourseBL.GetCourseById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult ConfirmDeleteCourse(int id)
        {
            if (BL.CourseBL.DeleteCourse(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}