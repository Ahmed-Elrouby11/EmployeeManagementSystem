namespace EmployeeManagementSystem.Application.DTOs.Employee;

public class UpdateEmployeeDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }
}