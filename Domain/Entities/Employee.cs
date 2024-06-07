namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Employee_Name { get; set; } = null!;
    public int Employee_Salary { get; set; }
    public int Employee_Age { get; set; }
    public string Profile_Image { get; set; } = null!;
}
