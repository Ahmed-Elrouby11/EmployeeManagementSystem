using EmployeeManagementSystem.Application.DTOs.Common;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Interfaces.Repositories;

public interface IEmployeeRepository
    : IGenericRepository<Employee>
{
    Task<IEnumerable<Employee>>
        GetEmployeesByDepartmentAsync(int departmentId);

    Task<IEnumerable<Employee>> GetAllWithDepartmentAsync();
    Task<Employee?> GetByIdWithDepartmentAsync(int id);

    Task<IEnumerable<Employee>> GetAllAsync(EmployeeQueryParameters parameters);
}