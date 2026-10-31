using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployees();

            return View(employees);
        }


        public IActionResult Create()
        {
            return View(new EmployeeCreateDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployee(employeeDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(employeeDTO);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeById(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee =
                    await _employeeService.GetEmployeeById(employeeDTO.Id);

                if (existingEmployee == null)
                {
                    return NotFound();
                }

                await _employeeService.UpdateEmployee(employeeDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(employeeDTO);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeById(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeById(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployee(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}
