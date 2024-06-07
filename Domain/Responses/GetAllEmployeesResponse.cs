using Domain.Entities;

namespace Domain.Responses;

public class GetAllEmployeesResponse
{
    public string Status { get; set; } = null!;
    public IEnumerable<Employee> Data { get; set; } = [];
    public string Message { get; set; } = null!;
}
