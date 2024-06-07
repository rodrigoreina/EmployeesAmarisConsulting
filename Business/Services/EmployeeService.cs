using Business.Interfaces;
using DAL.Interfaces;
using Domain.DTOs;

namespace Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
    {
        var result = await _employeeRepository.GetAllEmployessAsync();
        return result.Select(e => new EmployeeDTO()
        {
            Id = e.Id,
            Employee_Name = e.Employee_Name,
            Employee_Salary = e.Employee_Salary,
            Employee_Annual_Salary = CalculateAnnualSalary(e.Employee_Salary),
            Employee_Age = e.Employee_Age,
            Profile_Image = e.Profile_Image,
        }).ToList();
    }

    public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id)
    {
        var result = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (result == null)
            return null;

        return new EmployeeDTO()
        {
            Id = result.Id,
            Employee_Name = result.Employee_Name,
            Employee_Salary = result.Employee_Salary,
            Employee_Annual_Salary = CalculateAnnualSalary(result.Employee_Salary),
            Employee_Age = result.Employee_Age,
            Profile_Image = result.Profile_Image,
        };
    }

    private int CalculateAnnualSalary(int salary) => salary * 12;
}
