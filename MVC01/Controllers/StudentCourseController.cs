using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC01.BL;
using MVC01.Models;
using MVC01.Repositry;
using MVC01.ViewModels;

namespace MVC01.Controllers
{
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


            return View();
        }

        //public IActionResult Assign(StudentCourseEnrollmentVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // 1. Remove old enrollments for this student
        //        var oldEntries = _context.Student_Crs.Where(sc => sc.StudentId == model.Student_Id);
        //        _context.Student_Crs.RemoveRange(oldEntries);

        //        // 2. Add only selected courses
        //        foreach (var item in model.Courses.Where(c => c.IsSelected))
        //        {
        //            _context.Student_Crs.Add(new Student_Crs
        //            {
        //                StudentId = model.Student_Id,
        //                CourseId = item.courseId,
        //                Grade = item.Degree
        //            });
        //        }

        //        _context.SaveChanges();
        //        return RedirectToAction("Index", "Student");
        //    }
        //    return View(model);
        //}
    }
}
