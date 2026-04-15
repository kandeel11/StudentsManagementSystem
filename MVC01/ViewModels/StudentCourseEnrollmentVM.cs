using MVC01.Models;

namespace MVC01.ViewModels
{
    public class StudentCourseEnrollmentVM
    {
        public int Student_Id { get; set; } 
        public string? Student_Name { get; set; }
       public List<CourseVM> Courses { get; set; } = new List<CourseVM>();
    }
}
