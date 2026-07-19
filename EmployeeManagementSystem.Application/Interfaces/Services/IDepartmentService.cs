using EmployeeManagementSystem.Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto?> GetByIdAsync(int id);

        Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto);

        Task UpdateAsync(int id, UpdateDepartmentDto dto);

        Task DeleteAsync(int id);

        Task<DepartmentEmployeeCountDto> GetEmployeeCountAsync(int departmentId);

        Task<IEnumerable<DepartmentEmployeeDto>> GetEmployeeNamesAsync(int departmentId);
    }
}
