using MVC01.BL;
using MVC01.Models;
using MVC01.ViewModels;
namespace MVC01.Repositry
{

    public interface IStudentRepositry: IRepositry<Student>
    {
        public StudentDetailsVM studentDetailsVM(int id);
    }
}
