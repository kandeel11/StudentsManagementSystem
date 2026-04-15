using Microsoft.AspNetCore.Mvc;
using MVC01.Repositry;

namespace MVC01.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepositry departmentBL;
        public DepartmentController(IDepartmentRepositry _departmentBL)
        {
            departmentBL = _departmentBL;
        }
        public IActionResult Index()
        {
            var departments = departmentBL.GetAll();
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var dept = departmentBL.GetById(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDepartment(Models.Department department)
        {
            if (department == null || department.Name == null && department.Location == null || department.PhoneNumber == null) return View(department);
            if (departmentBL.Add(department))
            {
                departmentBL.Save();
                return RedirectToAction("Index");

            }
            return View(department);
        }

        public IActionResult EditDepartment(int id)
        {
            var dept = departmentBL.GetById(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        public IActionResult EditDepartment(int id, Models.Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Name)) return View(department);
            department.DeptId = id;
            if (departmentBL.Update(department))
            {
                departmentBL.Save();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult DeleteDepartment(int id)
        {
            var dept = departmentBL.GetById(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        public IActionResult ConfirmDeleteDepartment(int id)
        {
            if (departmentBL.Delete(id))
            {
                departmentBL.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
