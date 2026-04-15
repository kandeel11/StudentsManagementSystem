using Microsoft.AspNetCore.Mvc;
using MVC01.Repositry;

namespace MVC01.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepositry courseBL;
        public CourseController(ICourseRepositry _courseBL)
        {
            courseBL = _courseBL;
        }
        public IActionResult Index()
        {
                var courses = courseBL.GetAll();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = courseBL.GetById(id);
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

            if (courseBL.Add(course))
            {

                courseBL.Save();
                return RedirectToAction("Index");
            }
            return View(course);
        }
        public IActionResult TestError()
        {
            throw new Exception("Test");
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
            var course = courseBL.GetById(id);
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
            if (courseBL.Update(course))
            {
                courseBL.Save();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public IActionResult DeleteCourse(int id)
        {
            var course = courseBL.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult ConfirmDeleteCourse(int id)
        {
            if (courseBL.Delete(id))
            {
                courseBL.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}