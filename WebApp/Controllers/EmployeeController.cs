using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new EmployeeViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(EmployeeViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (model.EmployeeId.HasValue)
                {
                    var employee = await _employeeService.GetEmployeeByIdAsync(model.EmployeeId.Value);
                    if (employee != null)
                    {
                        model.Employees = [employee];
                        return View(model);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee Not Found.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var employees = await _employeeService.GetAllEmployeesAsync();
                    model.Employees = employees;
                    return View(model);
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    TempData["ErrorMessage"] = "Please wait a moment to perform another search.";
                else
                    TempData["ErrorMessage"] = "There is a problem with the API.";
                _logger.LogError(ex.Message, ex);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "There was a problem searching for employees.";
                _logger.LogError(ex.Message, ex);
            }
        }
        return View(model);
    }
}
