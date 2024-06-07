using Domain.Entities;

namespace DAL.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployessAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
}
