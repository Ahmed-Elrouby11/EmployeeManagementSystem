using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Infrastructure.Persistence.Context;
using EmployeeManagementSystem.Infrastructure.Persistence.Repositories;
using EmployeeManagementSystem.Application.Interfaces.Repositories;

public class DepartmentRepository
    : GenericRepository<Department>,
      IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context)
        : base(context)
    {
    }
}