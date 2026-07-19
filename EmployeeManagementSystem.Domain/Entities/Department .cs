using EmployeeManagementSystem.Domain.Common;

namespace EmployeeManagementSystem.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Navigation Property
    public ICollection<Employee> Employees { get; set; }
        = new List<Employee>();
}