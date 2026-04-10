using MVC01.Models;

namespace MVC01.ViewModels
{
    public class StudentDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public string Image { get; set; }

        public List<CourseVM> Courses { get; set; } = new List<CourseVM>();


    }
}
