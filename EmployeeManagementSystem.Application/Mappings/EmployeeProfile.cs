using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        // CreateEmployeeDto -> Employee
        CreateMap<CreateEmployeeDto, Employee>();

        // UpdateEmployeeDto -> Employee
        CreateMap<UpdateEmployeeDto, Employee>();

        // Employee -> EmployeeDto
        CreateMap<Employee, EmployeeDto>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"))

            .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(src =>
                    src.Department.Name));

        // Employee -> EmployeeDetailsDto
        CreateMap<Employee, EmployeeDetailsDto>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"))

            .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(src =>
                    src.Department.Name));
    }
}