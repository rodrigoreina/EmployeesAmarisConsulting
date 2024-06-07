using DAL.Interfaces;
using Domain.Entities;
using Domain.Responses;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace DAL.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private const string UrlGetAllEmployees = "v1/employees";
    private const string UrlGetEmployeeById = "v1/employee";

    public EmployeeRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployessAsync()
    {
        string? httpClientName = _configuration["EmployeeHttpClientName"];
        using HttpClient client = _httpClientFactory.CreateClient(httpClientName ?? "");

        GetAllEmployeesResponse? response = await client.GetFromJsonAsync<GetAllEmployeesResponse>($"{UrlGetAllEmployees}");

        if (response != null)
            return response.Data;

        return [];
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        string? httpClientName = _configuration["EmployeeHttpClientName"];
        using HttpClient client = _httpClientFactory.CreateClient(httpClientName ?? "");

        GetEmployeeByIdResponse? response = await client.GetFromJsonAsync<GetEmployeeByIdResponse>($"{UrlGetEmployeeById}/{id}");

        if (response != null)
            return response.Data;

        return null;
    }
}
