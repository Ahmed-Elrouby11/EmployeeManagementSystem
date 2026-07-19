using EmployeeManagementSystem.Domain.Common;

namespace EmployeeManagementSystem.Domain.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }

    public string? PhotoUrl { get; set; }

    // Foreign Key
    public int DepartmentId { get; set; }

    // Navigation Property
    public Department Department { get; set; } = null!;
}