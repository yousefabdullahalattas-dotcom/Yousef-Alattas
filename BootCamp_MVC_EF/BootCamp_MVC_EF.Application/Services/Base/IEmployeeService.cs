using BootCamp_MVC_EF.Dtos;

namespace BootCamp_MVC_EF.Services.Base
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();

        Task<EmployeeUpdateDto?> GetEmployeeById(int id);

        Task AddEmployee(EmployeeCreateDto employeeDTO);

        Task UpdateEmployee(EmployeeUpdateDto employeeDTO);

        Task DeleteEmployee(int id);
    }
}
