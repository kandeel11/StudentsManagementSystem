namespace MVC01.ViewModels
{
    public class CourseVM
    {
        public int courseId { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public bool IsSelected { get; set; } // Add this for checkboxes
    }
}
