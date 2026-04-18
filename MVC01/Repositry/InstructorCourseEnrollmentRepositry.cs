using System.Collections.Generic;
using System.Linq;
using MVC01.BL;
using MVC01.Models;
using MVC01.ViewModels;

namespace MVC01.Repositry
{
    public class InstructorCourseEnrollmentRepositry : IRepositry<InstructorCourseEnrollmentVM>
    {
        MVC01.Context.MVCContext db;
        public InstructorCourseEnrollmentRepositry(MVC01.Context.MVCContext _db)
        {
            db = _db;
        }

        public bool Add(InstructorCourseEnrollmentVM entity)
        {
            return true;
        }

        public bool Delete(int id)
        {
            var existing = db.Instructors.Find(id);
            if (existing != null)
            {
                var related = db.Instructor_Crs.Where(x => x.Ins_Id == id).ToList();
                db.Instructor_Crs.RemoveRange(related);
                return true;
            }
            return false;
        }

        public List<InstructorCourseEnrollmentVM> GetAll()
        {
            return db.Instructors
                .Select(i => new InstructorCourseEnrollmentVM
                {
                    Instructor_Id = i.Ins_Id,
                    Instructor_Name = i.Name,
                    Courses = i.Instructor_Crs.Select(ic => new InstructorCourseVM
                    {
                        CourseId = ic.Crs_Id,
                        Name = ic.Course.Name,
                        IsSelected = true
                    }).ToList()
                }).ToList();
        }

        public InstructorCourseEnrollmentVM GetById(int id)
        {
            return db.Instructors
                .Where(i => i.Ins_Id == id)
                .Select(i => new InstructorCourseEnrollmentVM
                {
                    Instructor_Id = i.Ins_Id,
                    Instructor_Name = i.Name,
                    Courses = i.Instructor_Crs.Select(ic => new InstructorCourseVM
                    {
                        CourseId = ic.Crs_Id,
                        Name = ic.Course.Name,
                        IsSelected = true
                    }).ToList()
                }).FirstOrDefault();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool Update(InstructorCourseEnrollmentVM entity)
        {
            var existing = db.Instructors.Find(entity.Instructor_Id);
            if (existing != null)
            {
                existing.Name = entity.Instructor_Name;

                var existingEnrollments = db.Instructor_Crs.Where(ic => ic.Ins_Id == entity.Instructor_Id).ToList();
                db.Instructor_Crs.RemoveRange(existingEnrollments);

                foreach (var course in entity.Courses.Where(c => c.IsSelected))
                {
                    db.Instructor_Crs.Add(new Instructor_Crs
                    {
                        Ins_Id = entity.Instructor_Id,
                        Crs_Id = course.CourseId,
                        Hours = 0 // Ignore degrees/hours
                    });
                }
                return true;
            }
            return false;
        }
    }
}