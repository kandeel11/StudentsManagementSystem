using Mapster;
using MVC01.ViewModels;

namespace MVC01.BL
{
    public class StudentBL
    {
        Context.MVCContext db = new Context.MVCContext();
        public List<Models.Student> GetAllStudents()
        {
            return db.Students.ToList();
        }
        public Models.Student GetStudentById(int id)
        {
            return db.Students.Find(id);
        }

        public bool AddStudent(Models.Student student)
        {
            db.Students.Add(student);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public StudentDetailsVM studentDetailsVM(int id)
        {
            var studentDetails = db.Students
                .Where(s => s.SSn == id)
                .ProjectToType<StudentDetailsVM>()
                .FirstOrDefault();
            return studentDetails;
        }

        public bool UpdateStudent(Models.Student student)
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

        public bool DeleteStudent(int id)
        {
            try
            {
                var existing = db.Students.Find(id);
                if (existing != null)
                {
                    db.Students.Remove(existing);
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
