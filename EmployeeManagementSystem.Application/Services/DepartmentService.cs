using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Application.Exceptions;
using EmployeeManagementSystem.Application.Interfaces.Services;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            _logger.LogInformation("Getting all departments.");
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException("Department not found");
            _logger.LogInformation($"Getting department with Id: {id}");
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Department created with Name: {department.Name}");
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException("Department not found");
            _mapper.Map(dto, department);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Department updated with Id: {id}");
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException("Department not found");
            await _unitOfWork.Departments.DeleteAsync(department);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Department deleted with Id: {id}");
        }

        public async Task<DepartmentEmployeeCountDto> GetEmployeeCountAsync(int departmentId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(departmentId);

            if (department is null)
                throw new Exception("Department not found.");

            var count = await _unitOfWork.Departments
                .GetEmployeeCountAsync(departmentId);

            return new DepartmentEmployeeCountDto
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,
                EmployeeCount = count
            };
        }

        public async Task<IEnumerable<DepartmentEmployeeDto>> GetEmployeeNamesAsync(int departmentId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(departmentId);

            if (department is null)
                throw new Exception("Department not found.");

            return await _unitOfWork.Departments
                .GetEmployeeNamesAsync(departmentId);
        }
    }
}
