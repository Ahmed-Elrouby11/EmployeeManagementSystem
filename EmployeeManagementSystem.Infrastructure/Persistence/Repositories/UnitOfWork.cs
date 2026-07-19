using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public IEmployeeRepository Employees { get; }

    public IDepartmentRepository Departments { get; }

    public IRefreshTokenRepository RefreshTokens { get; }

    public UnitOfWork(
        AppDbContext context,
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _context = context;
        Employees = employeeRepository;
        Departments = departmentRepository;
        RefreshTokens = refreshTokenRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
        }
    }
}