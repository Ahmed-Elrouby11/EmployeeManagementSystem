using EmployeeManagementSystem.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection
        AddInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddScoped<
            IEmployeeRepository,
            EmployeeRepository>();

        services.AddScoped<
            IDepartmentRepository,
            DepartmentRepository>();

        services.AddScoped<
            IUnitOfWork,
            UnitOfWork>();

        return services;
    }
}