using Microsoft.AspNetCore.Mvc;

namespace MVC01.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            var instructors = BL.InstructorBL.GetAllInstructors();
            return View(instructors);
        }

        public IActionResult Details(int id)
        {
            var instructor = BL.InstructorBL.GetInstructorById(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult AddInstructor()
        {
            ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult AddInstructor(Models.Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
                return View(instructor);
            }
            BL.InstructorBL.AddInstructor(instructor);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var instructor = BL.InstructorBL.GetInstructorById(id);
            if (instructor == null) return NotFound();
            ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(int id, Models.Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
                return View(instructor);
            }
            instructor.Ins_Id = id;
            BL.InstructorBL.UpdateInstructor(instructor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var instructor = BL.InstructorBL.GetInstructorById(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var instructor = BL.InstructorBL.GetInstructorById(id);
            if (instructor == null) return NotFound();
            BL.InstructorBL.DeleteInstructor(id);
            return RedirectToAction("Index");
        }


    }
}
