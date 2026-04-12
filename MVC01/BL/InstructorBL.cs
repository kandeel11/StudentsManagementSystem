using Microsoft.EntityFrameworkCore;
using MVC01.Models;

namespace MVC01.BL
{
    public class InstructorBL
    {
        static Context.MVCContext db = new Context.MVCContext();

        public static List<Instructor> GetAllInstructors()
        {
            return db.Instructors.ToList();
        }

        public static void AddInstructor(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }
        public static void UpdateInstructor(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
        public static void DeleteInstructor(int id)
        {
            var instructor = db.Instructors.Find(id);
            if (instructor != null)
            {
                db.Instructors.Remove(instructor);
                db.SaveChanges();
            }
        }
        public static Instructor GetInstructorById(int id)
        {
            return db.Instructors
                .Include(i => i.Department)
                .Include(i => i.Instructor_Crs)
                .ThenInclude(ic => ic.Course)
                .FirstOrDefault(i => i.Ins_Id == id);
        }
    }
}