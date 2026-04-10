using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC01.Models
{
    public class Instructor
    {
        [Key]
        public int Ins_Id { get; set; }
        public string Name { get; set; }

        public double? salary { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public ICollection<Instructor_Crs>? Instructor_Crs { get; set; } = new HashSet<Instructor_Crs>();
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Department? Department { get; set; }
    }
}
