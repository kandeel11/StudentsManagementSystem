using Mapster;
using MVC01.BL;
using MVC01.Models;
using MVC01.ViewModels;

namespace MVC01.Repositry
{
    public class StudentCourseEnrollmentRepositry : IRepositry<StudentCourseEnrollmentVM>
    {
        MVC01.Context.MVCContext db;
        public StudentCourseEnrollmentRepositry(MVC01.Context.MVCContext _db)
        {
            db = _db;
        }
        public bool Add(StudentCourseEnrollmentVM entity)
        {
            db.Add(entity);
            return true;

        }

        public bool Delete(int id)
        {
            var existing = db.Student_Crs.Find(id);
            if (existing != null)
            {
                db.Student_Crs.Remove(existing);
                return true;
            }
            return false;
        }

        public List<StudentCourseEnrollmentVM> GetAll()
        {
            var studentCourseEnrollments = db.Students
                .Select(s => new StudentCourseEnrollmentVM
                {
                    Student_Id = s.SSn,
                    Student_Name = s.Name,
                    Courses = s.Student_Crs.Select(sc => new CourseVM
                    {
                        courseId = sc.CourseId,
                        Name = sc.Course.Name,
                        IsSelected = true, 
                        Degree = sc.Grade
                    }).ToList()
                }).ToList();
            return studentCourseEnrollments;
        }

        public StudentCourseEnrollmentVM GetById(int id)
        {
            var studentCourseEnrollment=db.Students.Where(s=>s.SSn==id).ProjectToType<StudentCourseEnrollmentVM>().FirstOrDefault();
            
            return studentCourseEnrollment;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool Update(StudentCourseEnrollmentVM entity)
        {
            var existing = db.Students.Find(entity.Student_Id);
            if (existing != null)
            {
                existing.Name = entity.Student_Name;
                // Update other student properties if needed
                // Update course enrollments
                var existingEnrollments = db.Student_Crs.Where(sc => sc.StudentId == entity.Student_Id).ToList();
                db.Student_Crs.RemoveRange(existingEnrollments);
                foreach (var course in entity.Courses.Where(c => c.IsSelected))
                {
                    db.Student_Crs.Add(new Student_Crs
                    {
                        StudentId = entity.Student_Id,
                        CourseId = course.courseId,
                        Grade = course.Degree
                    });
                }
                return true;
            }
            return false;
        }
    }
}
