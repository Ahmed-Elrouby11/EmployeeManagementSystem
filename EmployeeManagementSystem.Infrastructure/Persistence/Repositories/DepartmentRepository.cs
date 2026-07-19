using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Infrastructure.Persistence.Context;
using EmployeeManagementSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

public class DepartmentRepository
    : GenericRepository<Department>,
      IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Department department)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetEmployeeCountAsync(int departmentId)
    {
        return await _context.Employees
            .CountAsync(e => e.DepartmentId == departmentId);
    }

    public async Task<IEnumerable<DepartmentEmployeeDto>> GetEmployeeNamesAsync(int departmentId)
    {
        return await _context.Employees
            .Where(e => e.DepartmentId == departmentId)
            .Select(e => new DepartmentEmployeeDto
            {
                Id = e.Id,
                FullName = e.FirstName + " " + e.LastName
            })
            .ToListAsync();
    }

}