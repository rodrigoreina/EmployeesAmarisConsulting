using Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class EmployeeViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive number greater than 0.")]

    public int? EmployeeId { get; set; }

    public IEnumerable<EmployeeDTO> Employees { get; set; } = [];
}
