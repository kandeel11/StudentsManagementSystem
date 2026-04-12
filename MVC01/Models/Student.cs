using MVC01.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC01.Models
{

    public class Student
    {
        [Key]
        public int SSn { get; set; }
        
        [MaxLength(15),MinLength(4),Required(ErrorMessage ="The name must be in more that 4 charachter and less that 15 chrachter ")]
        [Unique(ErrorMessage = "Name must be unique.")]
        public string? Name { get; set; }
        [MaxLength(50)]
        [AddAlexCairoIsma]
        public string? Address { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        [RegularExpression(@"\w+\.(jpg||png)$", ErrorMessage = "Invalid image format.")]
        public string? Image { get; set; }
        [Range(12, 22, ErrorMessage = "Age must be between 12 and 22.")]
        public int Age { get; set; }

        public ICollection<Student_Crs>? Student_Crs { get; set; } = new HashSet<Student_Crs>();
        [ForeignKey("Department")]
        public  int? DeptId { get; set; }
        public Department? Department { get; set; }
    }
}
