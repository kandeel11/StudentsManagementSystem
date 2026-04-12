using Microsoft.EntityFrameworkCore;

namespace MVC01.BL
{
    public class DepartmentBL
    {
        static Context.MVCContext db = new Context.MVCContext();
        public static List<Models.Department> GetAllDepartments()
        {
            return db.Departments.Include(s => s.Students).Include(s => s.Instructors).ToList();

        }
        public static bool AddDepartment(Models.Department department)
        {
            try
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Models.Department GetDepartmentById(int id)
        {
            return db.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .FirstOrDefault(d => d.DeptId == id);
        }

        public static bool UpdateDepartment(Models.Department department)
        {
            try
            {
                var existing = db.Departments.FirstOrDefault(d => d.DeptId == department.DeptId);
                if (existing != null)
                {
                    existing.Name = department.Name;
                    existing.Location = department.Location;
                    existing.PhoneNumber = department.PhoneNumber;
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

        public static bool DeleteDepartment(int id)
        {
            try
            {
                var dept = db.Departments.FirstOrDefault(d => d.DeptId == id);
                if (dept != null)
                {
                    db.Departments.Remove(dept);
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

