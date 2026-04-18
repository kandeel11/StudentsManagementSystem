using System.ComponentModel.DataAnnotations;

namespace MVC01.ViewModels
{
    public class CourseVM : IValidatableObject
    {
        public int courseId { get; set; }
        public string Name { get; set; }

        [Range(0, 1000, ErrorMessage = "Degree must be greater than or equal to 0.")]
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int TotalDegree { get; set; }
        public bool IsSelected { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsSelected && Degree > TotalDegree)
            {
                yield return new ValidationResult($"Degree for {Name} cannot exceed the total degree of {TotalDegree}.", new[] { nameof(Degree) });
            }
        }
    }
}
