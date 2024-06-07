using Business.Interfaces;
using Business.Services;
using DAL.Interfaces;
using Domain.Entities;
using Moq;

namespace Test.Services;


[TestFixture]
public class EmployeeServiceTest
{
    private Mock<IEmployeeRepository> _mockRepository;
    private IEmployeeService _employeeService;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _employeeService = new EmployeeService(_mockRepository.Object);
    }

    [Test]
    public async Task GetAllEmployeesAsync_ReturnsListOfEmployees()
    {
        //Arrange
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Employee_Name = "Employee One", Employee_Age = 25, Employee_Salary = 10000, Profile_Image = "" },
            new Employee { Id = 2, Employee_Name = "Employee Two", Employee_Age = 30, Employee_Salary = 20000, Profile_Image = "" }
        };
        _mockRepository.Setup(r => r.GetAllEmployessAsync()).ReturnsAsync(employees);

        //Act
        var result = await _employeeService.GetAllEmployeesAsync();

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(employees.Count));
    }

    [Test]
    public async Task GetEmployeeById_ReturnsEmployee()
    {
        //Arrange
        var employee = new Employee { Id = 1, Employee_Name = "Employee One", Employee_Age = 25, Employee_Salary = 10000, Profile_Image = "" };
        _mockRepository.Setup(r => r.GetEmployeeByIdAsync(employee.Id)).ReturnsAsync(employee);

        //Act
        var result = await _employeeService.GetEmployeeByIdAsync(1);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Employee_Name, Is.EqualTo(employee.Employee_Name));
    }

    [Test]
    public async Task GetEmployeeById_ReturnsNull()
    {
        //Arrange
        Employee? employee = null;
        var employeeId = 77;
        _mockRepository.Setup(r => r.GetEmployeeByIdAsync(employeeId)).ReturnsAsync(employee);

        //Act
        var result = await _employeeService.GetEmployeeByIdAsync(employeeId);

        //Assert
        Assert.That(result, Is.Null);
    }
}
