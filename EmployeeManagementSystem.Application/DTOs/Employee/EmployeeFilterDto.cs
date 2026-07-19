using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.DTOs.Employee
{
    public class EmployeeFilterDto
    {
        public int? DepartmentId { get; set; }

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }
    }
}
