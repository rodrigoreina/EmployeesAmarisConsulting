using Domain.DTOs;

namespace Business.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
    Task<EmployeeDTO?> GetEmployeeByIdAsync(int id);
}
