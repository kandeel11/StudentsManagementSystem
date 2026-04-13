using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MVC01.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            BL.StudentBL studentBL = new BL.StudentBL();
            var students = studentBL.GetAllStudents();
            return View(students);
        }
        public IActionResult Details(int id)
        {
            BL.StudentBL studentBL = new BL.StudentBL();
            var student = studentBL.studentDetailsVM(id);
            if (student == null) { return NotFound(); }
            else
            {
                return View(student);
            }
        }
        public IActionResult AddStudent()
        {
            ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Models.Student student, IFormFile Image)
        {
           // if (student == null || string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Phone))
            if(!ModelState.IsValid)
            {
                ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
                return View("AddStudent", student);
            }
            if (Image != null && Image.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                StringBuilder Sp = new StringBuilder();
                Sp.Append(Guid.NewGuid().ToString());
                var fileName = Sp.Append(Path.GetExtension(Image.FileName)).ToString();
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
                student.Image = fileName;
            }
            BL.StudentBL studentBL = new BL.StudentBL();
            bool isAdded = studentBL.AddStudent(student);
            if (isAdded)
            {
                TempData["SuccessMessage"] = "Student added successfully!";
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
                TempData["ErrorMessage"] = "Failed to add student. Please try again.";
                return View(student);
            }
        }
        public IActionResult EditStudent(int id) 
        {
            BL.StudentBL studentBL = new BL.StudentBL();
            var student = studentBL.GetStudentById(id);
            if (student == null) return NotFound();
            ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(int id,Models.Student student, IFormFile Image)
        {
            //if (student == null || string.IsNullOrEmpty(student.Name) || string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.Phone))
            

            if (Image != null && Image.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                var fileName = Image.FileName;
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
                student.Image = fileName;
            }
            student.SSn = id;
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
                return View(student);
            }
            BL.StudentBL studentBL = new BL.StudentBL();
            if (studentBL.UpdateStudent(student))
            {
                return RedirectToAction("Index");
            }
            ViewBag.Departments = BL.DepartmentBL.GetAllDepartments();
            return View(student);


        }

        public IActionResult DeleteStudent(int id)
        {
            BL.StudentBL studentBL = new BL.StudentBL();
            var student = studentBL.GetStudentById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult ConfirmDeleteStudent(int id)
        {
            BL.StudentBL studentBL = new BL.StudentBL();
            if (studentBL.DeleteStudent(id))
                return RedirectToAction("Index");
            return RedirectToAction("Index");
        }
    }
}
