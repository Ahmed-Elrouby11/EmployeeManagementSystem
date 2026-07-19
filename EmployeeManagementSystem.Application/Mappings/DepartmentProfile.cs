using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // CreateDepartmentDto -> Department
            CreateMap<CreateDepartmentDto, Department>();

            // UpdateDepartmentDto -> Department
            CreateMap<UpdateDepartmentDto, Department>();

            // Department -> DepartmentDto
            CreateMap<Department, DepartmentDto>();
        }
    }
}
