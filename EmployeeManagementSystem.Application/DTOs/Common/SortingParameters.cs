using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.DTOs.Common
{
    public class SortingParameters
    {
        public string? SortBy { get; set; } = "Id";

        public bool Descending { get; set; } = false;
    }
}
