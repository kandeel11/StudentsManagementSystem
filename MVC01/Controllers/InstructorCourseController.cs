using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC01.BL;
using MVC01.Repositry;
using MVC01.ViewModels;

namespace MVC01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorCourseController : Controller
    {
        private readonly IRepositry<InstructorCourseEnrollmentVM> _repository;
        private readonly IInstructorRepositry _instructorRepositry;
        private readonly ICourseRepositry _courseRepositry;

        public InstructorCourseController(IRepositry<InstructorCourseEnrollmentVM> repository, IInstructorRepositry instructorRepositry, ICourseRepositry courseRepositry)
        {
            _repository = repository;
            _instructorRepositry = instructorRepositry;
            _courseRepositry = courseRepositry;
        }

        public IActionResult Index(int id)
        {
            var instructorCourse = _repository.GetById(id);
            ViewBag.Instructors = _instructorRepositry.GetAll();
            return View(instructorCourse);
        }

        [HttpGet]
        public IActionResult Assign(int id) 
        {
            var instructor = _instructorRepositry.GetById(id);
            if (instructor == null) return NotFound();

            var mappedCourses = _repository.GetById(id)?.Courses ?? new System.Collections.Generic.List<InstructorCourseVM>();
            var allCourses = _courseRepositry.GetAll();

            var model = new InstructorCourseEnrollmentVM
            {
                Instructor_Id = instructor.Ins_Id,
                Instructor_Name = instructor.Name,
                Courses = allCourses.Select(c => new InstructorCourseVM
                {
                    CourseId = c.Crs_Id,
                    Name = c.Name,
                    IsSelected = mappedCourses.Any(mc => mc.CourseId == c.Crs_Id)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Assign(InstructorCourseEnrollmentVM model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(model);
                _repository.Save();
                return RedirectToAction("Index", "Instructor");
            }
            return View(model);
        }
    }
}