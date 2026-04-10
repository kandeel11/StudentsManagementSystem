using Microsoft.AspNetCore.Mvc;

namespace MVC01.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            var departments = BL.DepartmentBL.GetAllDepartments();
            return View(departments);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDepartment(Models.Department department)
        {
            if(department == null || department.Name == null && department.Location== null || department.PhoneNumber == null)return View(department);
            if ( BL.DepartmentBL.AddDepartment(department))
                return RedirectToAction("Index");
            return View(department);
        }

        public IActionResult EditDepartment(int id)
        {
            var dept = BL.DepartmentBL.GetDepartmentById(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        public IActionResult EditDepartment(Models.Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Name)) return View(department);
            if (BL.DepartmentBL.UpdateDepartment(department))
                return RedirectToAction("Index");
            return View(department);
        }

        public IActionResult DeleteDepartment(int id)
        {
            var dept = BL.DepartmentBL.GetDepartmentById(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        public IActionResult ConfirmDeleteDepartment(int id)
        {
            if (BL.DepartmentBL.DeleteDepartment(id))
                return RedirectToAction("Index");
            return RedirectToAction("Index"); 
        }
    }
}
