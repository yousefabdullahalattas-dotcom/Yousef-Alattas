using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;
        public EmployeesController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Employee> employees = _db.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employee);
        }

        //Edit------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var employee = _db.Employees.Find(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employee);
        }


        //Delete-----------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var employee = _db.Employees.Find(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }



}