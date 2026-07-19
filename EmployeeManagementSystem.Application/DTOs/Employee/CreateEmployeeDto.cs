namespace EmployeeManagementSystem.Application.DTOs.Employee;

public class CreateEmployeeDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }

    public int DepartmentId { get; set; }
}