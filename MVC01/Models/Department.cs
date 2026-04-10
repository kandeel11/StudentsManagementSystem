using System.ComponentModel.DataAnnotations;

namespace MVC01.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Student>? Students { get; set; } = new HashSet<Student>();
        public ICollection<Instructor>? Instructors { get; set; } = new HashSet<Instructor>();

    }
}
