using Microsoft.AspNetCore.Mvc;
using MVC01.Repositry;

namespace MVC01.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepositry instructorBL;
        IDepartmentRepositry departmentBL;

        public InstructorController(IInstructorRepositry _instructorBL, IDepartmentRepositry _departmentBL)
        {
            instructorBL = _instructorBL;
            departmentBL = _departmentBL;
        }
        public IActionResult Index()
        {
            var instructors = instructorBL.GetAll();
            return View(instructors);
        }

        public IActionResult Details(int id)
        {
            var instructor = instructorBL.GetById(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult AddInstructor()
        {
            ViewBag.Departments =departmentBL.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult AddInstructor(Models.Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = departmentBL.GetAll();
                return View(instructor);
            }
            instructorBL.Add(instructor);
            instructorBL.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var instructor = instructorBL.GetById(id);
            if (instructor == null) return NotFound();
            ViewBag.Departments = departmentBL.GetAll();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(int id, Models.Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = departmentBL.GetAll();
                return View(instructor);
            }
            instructor.Ins_Id = id;
            instructorBL.Update(instructor);
            instructorBL.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var instructor = instructorBL.GetById(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var instructor =instructorBL.GetById(id);
            if (instructor == null) return NotFound();
            instructorBL.Delete(id);
            instructorBL.Save();
            return RedirectToAction("Index");
        }


    }
}
