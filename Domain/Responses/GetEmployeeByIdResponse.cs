using Domain.Entities;

namespace Domain.Responses;

public class GetEmployeeByIdResponse
{
    public string Status { get; set; } = null!;
    public Employee? Data { get; set; }
    public string Message { get; set; } = null!;
}
