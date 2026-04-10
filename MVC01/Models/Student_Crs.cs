using System.ComponentModel.DataAnnotations.Schema;

namespace MVC01.Models
{
    public class Student_Crs
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public int Grade { get; set; }
    }
}
