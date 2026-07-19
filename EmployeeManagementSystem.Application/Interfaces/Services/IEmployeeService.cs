using EmployeeManagementSystem.Application.DTOs.Common;
using EmployeeManagementSystem.Application.DTOs.Employee;

namespace EmployeeManagementSystem.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();

    Task<EmployeeDetailsDto?> GetByIdAsync(int id);

    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);

    Task<EmployeeDto> UpdateAsync(int id, UpdateEmployeeDto dto);

    Task DeleteAsync(int id);
    Task<IEnumerable<EmployeeDto>> GetAllAsync(
    EmployeeQueryParameters parameters);
}