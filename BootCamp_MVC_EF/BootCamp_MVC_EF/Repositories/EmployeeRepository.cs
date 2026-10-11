using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }


        public Task AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            return _context.SaveChangesAsync();
        }

        public Task DeleteEmployeeAsync(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            IEnumerable<EmployeeDto> employees = await _context.Employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                Position = e.Position,
                Department = e.Department,
                Salary = e.Salary
            }).ToListAsync();
            return employees;
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;


        }
        public Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return _context.SaveChangesAsync();

        }
    }
}