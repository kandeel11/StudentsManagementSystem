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
    }
}