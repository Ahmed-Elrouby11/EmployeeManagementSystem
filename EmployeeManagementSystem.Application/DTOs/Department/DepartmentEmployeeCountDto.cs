using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.DTOs.Department
{
    public class DepartmentEmployeeCountDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public int EmployeeCount { get; set; }
    }
}
