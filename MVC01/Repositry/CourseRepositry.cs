using Microsoft.EntityFrameworkCore;
using MVC01.Models;
using MVC01.Repositry;

namespace MVC01.BL
{
    public class CourseRepositry:ICourseRepositry
    {
        Context.MVCContext db;
        public CourseRepositry(Context.MVCContext _db)
        {
            db = _db;
        }

        public  List<Course> GetAll()
        {
            return db.Courses.ToList();
        }

        public  bool Add(Course course)
        {
            try
            {
                db.Courses.Add(course);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  Course GetCourseById(int id)
        {
            return db.Courses
                .Include(c => c.Student_Crs)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefault(c => c.Crs_Id == id);
        }

        public  bool Update(Course course)
        {
            try
            {
                var existing = db.Courses.FirstOrDefault(c => c.Crs_Id == course.Crs_Id);
                if (existing != null)
                {
                    existing.Name = course.Name;
                    existing.Description = course.Description;
                    existing.Total_Degree = course.Total_Degree;
                    existing.Min_Degree = course.Min_Degree;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  bool Delete(int id)
        {
            try
            {
                var existing = db.Courses.FirstOrDefault(c => c.Crs_Id == id);
                if (existing != null)
                {
                    db.Courses.Remove(existing);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  void Save()
        {
            db.SaveChanges();
        }

        public Course GetById(int id)
        {
            return db.Courses
                .Include(c => c.Student_Crs)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefault(c => c.Crs_Id == id);

        }
    }
}