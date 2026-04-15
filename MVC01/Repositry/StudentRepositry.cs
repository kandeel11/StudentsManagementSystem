using Mapster;
using MVC01.Context;
using MVC01.Models;
using MVC01.Repositry;
using MVC01.ViewModels;

namespace MVC01.BL
{
    public class StudentRepositry: IStudentRepositry
    {
        Context.MVCContext db;
        public StudentRepositry(MVCContext _db)
        {
            db = _db;
        }
        public List<Student> GetAll()
        {
            return db.Students.ToList();
        }
        public Student GetById(int id)
        {
            return db.Students.Find(id);
        }

        public bool Add(Student student)
        {
            db.Students.Add(student);
            return true;
        }
        public StudentCourseEnrollmentVM studentCourseEnrollmentVM(int id)
        {
            var studentCourseEnrollment = db.Students
                .Where(s => s.SSn == id)
                .ProjectToType<StudentCourseEnrollmentVM>()
                .FirstOrDefault();
            return studentCourseEnrollment;
        }
        public StudentDetailsVM studentDetailsVM(int id)
        {
            var studentDetails = db.Students
                .Where(s => s.SSn == id)
                .ProjectToType<StudentDetailsVM>()
                .FirstOrDefault();
            return studentDetails;
        }

        public bool Update(Student student)
        {
            try
            {
                var existing = db.Students.Find(student.SSn);
                if (existing != null)
                {
                    existing.Name = student.Name;
                    existing.Address = student.Address;
                    existing.Phone = student.Phone;
                    existing.Email = student.Email;
                    existing.DeptId = student.DeptId;
                    if (student.Image != null)
                    {
                        existing.Image = student.Image;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var existing = db.Students.Find(id);
                if (existing != null)
                {
                    db.Students.Remove(existing);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
