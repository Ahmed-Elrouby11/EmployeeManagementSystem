namespace EmployeeManagementSystem.Application.DTOs.Employee;

public class EmployeeDetailsDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }

    public string? PhotoUrl { get; set; }

    public string DepartmentName { get; set; } = string.Empty;
}