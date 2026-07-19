using EmployeeManagementSystem.Application.DTOs.Common;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Infrastructure.Persistence.Context;
using EmployeeManagementSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository
    : GenericRepository<Employee>,
      IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Employee>>
        GetEmployeesByDepartmentAsync(int departmentId)
    {
        return await _context.Employees
            .Where(e => e.DepartmentId == departmentId)
            .ToListAsync();
    }
    public async Task<IEnumerable<Employee>> GetAllWithDepartmentAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdWithDepartmentAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(
        EmployeeQueryParameters parameters)
    {
        IQueryable<Employee> query =_context.Employees
         .Include(e => e.Department);

        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            query = query.Where(e =>
                e.FirstName.Contains(parameters.Search) ||
                e.LastName.Contains(parameters.Search) ||
                e.Email.Contains(parameters.Search));
        }
        if (parameters.DepartmentId.HasValue)
        {
            query = query.Where(e =>
                e.DepartmentId == parameters.DepartmentId);
        }
        query = parameters.SortBy?.ToLower() switch
        {
            "firstname" => parameters.Descending
                ? query.OrderByDescending(e => e.FirstName)
                : query.OrderBy(e => e.FirstName),

            "lastname" => parameters.Descending
                ? query.OrderByDescending(e => e.LastName)
                : query.OrderBy(e => e.LastName),

            "email" => parameters.Descending
                ? query.OrderByDescending(e => e.Email)
                : query.OrderBy(e => e.Email),

            _ => parameters.Descending
                ? query.OrderByDescending(e => e.Id)
                : query.OrderBy(e => e.Id)
        };
        query = query
        .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        .Take(parameters.PageSize);

        return await query.ToListAsync();
    }

}