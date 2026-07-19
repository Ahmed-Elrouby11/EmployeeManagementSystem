
using EmployeeManagementSystem.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IEmployeeRepository Employees { get; }

    IDepartmentRepository Departments { get; }

    IRefreshTokenRepository RefreshTokens { get; }

    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}