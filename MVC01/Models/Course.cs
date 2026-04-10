using System.ComponentModel.DataAnnotations;

namespace MVC01.Models
{
    public class Course
    {
        [Key]
        public int Crs_Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public int Total_Degree { get; set; } = 0;
        public int Min_Degree { get; set; }=0;
        public string? Topics { get; set; } 
        public ICollection<Student_Crs>? Student_Crs { get; set; } = new HashSet<Student_Crs>();
        public ICollection<Instructor_Crs>? Instructor_Crs { get; set; } = new HashSet<Instructor_Crs>();
    }
}
