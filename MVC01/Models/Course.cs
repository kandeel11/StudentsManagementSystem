using Microsoft.AspNetCore.Mvc;
using MVC01.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC01.Models
{
    public class Course
    {
        [Key]
        public int Crs_Id { get; set; }
        [Unique(ErrorMessage = "Course name must be unique.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Remote(action: "ValidateCourseDegree", controller: "Course",AdditionalFields = "Min_Degree", ErrorMessage = "Course code must be unique.")]
        public int Total_Degree { get; set; } = 0;
        public int Min_Degree { get; set; }=0;
        public string? Topics { get; set; } 
        public ICollection<Student_Crs>? Student_Crs { get; set; } = new HashSet<Student_Crs>();
        public ICollection<Instructor_Crs>? Instructor_Crs { get; set; } = new HashSet<Instructor_Crs>();
    }
}
