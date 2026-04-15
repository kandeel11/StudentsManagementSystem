using Microsoft.EntityFrameworkCore;
using MVC01.Context;
using MVC01.Models;
using MVC01.Repositry;

namespace MVC01.BL
{
    public class DepartmentRepositry : IDepartmentRepositry
    {
        Context.MVCContext db;
        public DepartmentRepositry(MVCContext _db)
        {
            db = _db;
        }
        public List<Models.Department> GetAll()
        {
            return db.Departments.Include(s => s.Students).Include(s => s.Instructors).ToList();
        }
        public bool Add(Models.Department department)
        {
            try
            {
                db.Departments.Add(department);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Models.Department GetById(int id)
        {
            return db.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .FirstOrDefault(d => d.DeptId == id);
        }

        public bool Update(Models.Department department)
        {
            try
            {
                var existing = db.Departments.FirstOrDefault(d => d.DeptId == department.DeptId);
                if (existing != null)
                {
                    existing.Name = department.Name;
                    existing.Location = department.Location;
                    existing.PhoneNumber = department.PhoneNumber;
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
                var dept = db.Departments.FirstOrDefault(d => d.DeptId == id);
                if (dept != null)
                {
                    db.Departments.Remove(dept);
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
            throw new NotImplementedException();
        }
    }
}

