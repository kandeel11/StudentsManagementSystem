using Microsoft.EntityFrameworkCore;
using MVC01.Models;

namespace MVC01.BL
{
    public class CourseBL
    {
        static Context.MVCContext db = new Context.MVCContext();

        public static List<Course> GetAllCourses()
        {
            return db.Courses.ToList();
        }

        public static bool AddCourse(Course course)
        {
            try
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Course GetCourseById(int id)
        {
            return db.Courses
                .Include(c => c.Student_Crs)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefault(c => c.Crs_Id == id);
        }

        public static bool UpdateCourse(Course course)
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
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteCourse(int id)
        {
            try
            {
                var existing = db.Courses.FirstOrDefault(c => c.Crs_Id == id);
                if (existing != null)
                {
                    db.Courses.Remove(existing);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}