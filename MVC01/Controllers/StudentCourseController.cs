using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC01.BL;
using MVC01.Models;
using MVC01.ViewModels;

namespace MVC01.Controllers
{
    public class StudentCourseController : Controller
    {
        MVC01.Context.MVCContext _context;
        public StudentCourseController(MVC01.Context.MVCContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int id=3)
        {
            var studentcourse = new StudentRepositry(_context).studentCourseEnrollmentVM(id);
            ViewBag.students = new StudentRepositry(_context).GetAll();
            return View(studentcourse);
        }

        public IActionResult Assign(int id) // Student ID
        {

            return View();
        }

        [HttpPost]
        public IActionResult Assign(StudentCourseEnrollmentVM model)
        {
            if (ModelState.IsValid)
            {
                // 1. Remove old enrollments for this student
                var oldEntries = _context.Student_Crs.Where(sc => sc.StudentId == model.Student_Id);
                _context.Student_Crs.RemoveRange(oldEntries);

                // 2. Add only selected courses
                foreach (var item in model.Courses.Where(c => c.IsSelected))
                {
                    _context.Student_Crs.Add(new Student_Crs
                    {
                        StudentId = model.Student_Id,
                        CourseId = item.courseId,
                        Grade = item.Degree
                    });
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Student");
            }
            return View(model);
        }
    }
}
