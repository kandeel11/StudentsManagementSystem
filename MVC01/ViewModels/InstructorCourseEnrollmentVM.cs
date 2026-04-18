using System.Collections.Generic;

namespace MVC01.ViewModels
{
    public class InstructorCourseEnrollmentVM
    {
        public int Instructor_Id { get; set; } 
        public string? Instructor_Name { get; set; }
        public List<InstructorCourseVM> Courses { get; set; } = new List<InstructorCourseVM>();
    }
}