using Microsoft.EntityFrameworkCore;
using MVC01.Context;
using MVC01.Models;
using MVC01.Repositry;

namespace MVC01.BL
{

    public class InstructorRepositry : IInstructorRepositry
    {
        Context.MVCContext db;
        public InstructorRepositry(MVCContext _db)
        {
            db = _db;
        }

        public  List<Instructor> GetAll()
        {
            return db.Instructors.ToList();
        }

        public  bool Add(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
            return true;
        }
        public  bool Update(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
            return true;
        }
        public  bool Delete(int id)
        {
            var instructor = db.Instructors.Find(id);
            if (instructor != null)
            {
                db.Instructors.Remove(instructor);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public  Instructor GetById(int id)
        {
            return db.Instructors
                .Include(i => i.Department)
                .Include(i => i.Instructor_Crs)
                .ThenInclude(ic => ic.Course)
                .FirstOrDefault(i => i.Ins_Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        
    }
}