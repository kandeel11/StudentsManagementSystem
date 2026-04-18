using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC01.BL;
using MVC01.Models;
using MVC01.Repositry;
using MVC01.ViewModels;

namespace MVC01.Controllers
{
    [Authorize(Roles = "Admin")]

    public class StudentCourseController : Controller
    {
        private readonly IRepositry<StudentCourseEnrollmentVM> _repository;
        private readonly IStudentRepositry studentRepositry;
        private readonly ICourseRepositry courseRepositry;

        public StudentCourseController(IRepositry<StudentCourseEnrollmentVM> repository, IStudentRepositry studentRepositry, ICourseRepositry courseRepositry)
        {
            _repository = repository;
            this.studentRepositry = studentRepositry;
            this.courseRepositry = courseRepositry;
        }

        public IActionResult Index(int id)
        {
            //    if(id == null)
            //    {

            //        return View();
            //    }
            //if (id == null) return NotFound();
            var studentcourse = _repository.GetById(id);
            ViewBag.Students = studentRepositry.GetAll();
            return View(studentcourse);
        }
        [HttpGet]
        public IActionResult Assign(int id) // Student ID
        {
            var student = studentRepositry.GetById(id);
            if (student == null) return NotFound();

            var mappedCourses = _repository.GetById(id)?.Courses ?? new List<CourseVM>();
            var allCourses = courseRepositry.GetAll();

            var model = new StudentCourseEnrollmentVM
            {
                Student_Id = student.SSn,
                Student_Name = student.Name,
                Courses = allCourses.Select(c => new CourseVM
                {
                    courseId = c.Crs_Id,
                    Name = c.Name,
                    TotalDegree = c.Total_Degree,
                    MinDegree = c.Min_Degree,
                    IsSelected = mappedCourses.Any(mc => mc.courseId == c.Crs_Id),
                    Degree = mappedCourses.FirstOrDefault(mc => mc.courseId == c.Crs_Id)?.Degree ?? 0
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Assign(StudentCourseEnrollmentVM model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(model);
                _repository.Save();
                return RedirectToAction("Index", "Student");
            }
            return View(model);
        }
    }
}
