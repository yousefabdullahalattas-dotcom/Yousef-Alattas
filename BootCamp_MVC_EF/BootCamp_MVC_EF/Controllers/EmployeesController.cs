using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepository.GetAllEmployeesAsync());
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = new EmployeeUpdateDto
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Position = employee.Position,
                Department = employee.Department,
                Salary = employee.Salary
            };

            return View(employeeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeUpdateDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeDTO.Id);
                if (employee == null)
                {
                    return NotFound();
                }

                employee.EmployeeName = employeeDTO.EmployeeName;
                employee.Position = employeeDTO.Position;
                employee.Department = employeeDTO.Department;
                employee.Salary = employeeDTO.Salary;

                await _employeeRepository.UpdateEmployeeAsync(employee);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employeeDTO);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDto employeeDTO)
        {
            if (ModelState.IsValid)
            {

                var employee = new Employee
                {
                    EmployeeName = employeeDTO.EmployeeName,
                    Position = employeeDTO.Position,
                    Department = employeeDTO.Department,
                    Salary = employeeDTO.Salary
                };

                await _employeeRepository.AddEmployeeAsync(employee);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(employeeDTO);

        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = new EmployeeUpdateDto
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Position = employee.Position,
                Department = employee.Department,
                Salary = employee.Salary
            };

            return View(employeeDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployeeAsync(employee.Id);

            return RedirectToAction("Index");
        }

    }
}