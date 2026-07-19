using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Application.DTOs.Common
{
    public class EmployeeQueryParameters
    {
        // Pagination
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 100 ? 100 : value;
        }

        // Search
        public string? Search { get; set; }

        // Filter
        public int? DepartmentId { get; set; }

        // Sort
        public string? SortBy { get; set; } = "Id";

        public bool Descending { get; set; }
    }
}
