using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories.Base;
using BootCamp_MVC_EF.Services.Base;

namespace BootCamp_MVC_EF.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _unitOfWork.EmployeeRepository
                .GetAllEmployeesAsync();

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                Position = e.Position,
                Department = e.Department,
                Salary = e.Salary
            });
        }

        public async Task<EmployeeUpdateDto?> GetEmployeeById(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository
                .GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            return new EmployeeUpdateDto
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Position = employee.Position,
                Department = employee.Department,
                Salary = employee.Salary
            };
        }

        public async Task AddEmployee(EmployeeCreateDto employeeDTO)
        {
            var employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                Position = employeeDTO.Position,
                Department = employeeDTO.Department,
                Salary = employeeDTO.Salary
            };

            await _unitOfWork.EmployeeRepository
                .AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployee(EmployeeUpdateDto employeeDTO)
        {
            var employee = await _unitOfWork.EmployeeRepository
                .GetEmployeeByIdAsync(employeeDTO.Id);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            employee.EmployeeName = employeeDTO.EmployeeName;
            employee.Position = employeeDTO.Position;
            employee.Department = employeeDTO.Department;
            employee.Salary = employeeDTO.Salary;

            await _unitOfWork.EmployeeRepository
                .UpdateEmployeeAsync(employee);
        }


        public async Task DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository
                .GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            await _unitOfWork.EmployeeRepository
                .DeleteEmployeeAsync(id);
        }
    }
}
