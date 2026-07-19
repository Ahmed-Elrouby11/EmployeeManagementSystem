using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Common;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Exceptions;
using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Application.Interfaces.Services;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<EmployeeService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllWithDepartmentAsync();

        _logger.LogInformation("Getting all employees.");
        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDetailsDto?> GetByIdAsync(int id)
    {
        var employee =
            await _unitOfWork
                .Employees
                .GetByIdWithDepartmentAsync(id);

        if (employee == null)
            throw new NotFoundException("Employee not found");

        _logger.LogInformation($"Getting employee with Id: {id}");

        return _mapper.Map<EmployeeDetailsDto>(employee);
    }
    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);

        await _unitOfWork.Employees.AddAsync(employee);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation($"Employee created with Email: {employee.Email}");

        return _mapper.Map<EmployeeDto>(employee);
    }
    public async Task<EmployeeDto> UpdateAsync(
    int id,
    UpdateEmployeeDto dto)
    {
        var employee =
            await _unitOfWork
                .Employees
                .GetByIdAsync(id);

        if (employee == null)
            throw new NotFoundException("Employee not found");

        _mapper.Map(dto, employee);

        _unitOfWork.Employees.Update(employee);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation($"Employee updated with Id: {employee.Id}");

        return _mapper.Map<EmployeeDto>(employee);
    }
    public async Task DeleteAsync(int id)
    {
        var employee =
            await _unitOfWork
                .Employees
                .GetByIdAsync(id);

        if (employee == null)
            throw new NotFoundException("Employee not found");

        _unitOfWork
            .Employees
            .Delete(employee);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"Employee deleted with Id: {employee.Id}");
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync(
    EmployeeQueryParameters parameters)
    {
        var employees =
            await _unitOfWork.Employees.GetAllAsync(parameters);

        _logger.LogInformation("Getting all employees with query parameters.");

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }
}