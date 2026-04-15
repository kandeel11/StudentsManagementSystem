using Mapster;
using MVC01.Models;
using MVC01.ViewModels;

namespace MVC01.Configuration
{
    public class MapsterConfiguration
    {
        public static void Register()
        {

            
            TypeAdapterConfig<Student, StudentDetailsVM>.NewConfig()
            .Map(dest => dest.Id, src => src.SSn) 
            .Map(dest => dest.DepartmentName, src => src.Department.Name) 
            .Map(dest => dest.Courses, src => src.Student_Crs);

            TypeAdapterConfig<Student_Crs, CourseVM>.NewConfig()
                .Map(dest => dest.Name, src => src.Course.Name)
                .Map(dest => dest.Degree, src => src.Grade)
                .Map(dest => dest.MinDegree, src => src.Course.Min_Degree);

            TypeAdapterConfig<Student, StudentCourseEnrollmentVM>.NewConfig()
                .Map(dest => dest.Student_Id, src => src.SSn)
                .Map(dest => dest.Student_Name, src => src.Name)
                .Map(dest => dest.Courses, src => src.Student_Crs);
        }
    }
}
