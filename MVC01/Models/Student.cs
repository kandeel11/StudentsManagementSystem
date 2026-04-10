using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC01.Models
{

    public class Student
    {
        [Key]
        public int SSn { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        public string? Image { get; set; }

        public ICollection<Student_Crs>? Student_Crs { get; set; } = new HashSet<Student_Crs>();
        [ForeignKey("Department")]
        public  int? DeptId { get; set; }
        public Department? Department { get; set; }
    }
}
