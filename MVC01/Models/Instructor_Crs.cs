using System.ComponentModel.DataAnnotations.Schema;

namespace MVC01.Models
{
    public class Instructor_Crs
    {
        [ForeignKey("Instructor")]
        public int Ins_Id { get; set; }
        public Instructor? Instructor { get; set; }
        [ForeignKey("Course")]
        public int Crs_Id { get; set; }
        public Course? Course { get; set; }

        public int Hours { get; set; }

    }
}
