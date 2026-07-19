using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department?> GetByIdAsync(int id);

        Task AddAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);

        Task<int> GetEmployeeCountAsync(int departmentId);

        Task<IEnumerable<DepartmentEmployeeDto>> GetEmployeeNamesAsync(int departmentId);
    }
}
