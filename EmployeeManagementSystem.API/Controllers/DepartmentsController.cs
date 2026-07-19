using Asp.Versioning;
using EmployeeManagementSystem.Application.DTOs.Department;
using EmployeeManagementSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Authorize(Policy = "ViewDepartments")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }
        [Authorize(Policy = "ViewDepartments")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();
            return Ok(department);
        }
        [Authorize(Policy = "ManageDepartments")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            var department = await _departmentService.CreateAsync(createDepartmentDto);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }
        [Authorize(Policy = "ManageDepartments")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();
            await _departmentService.UpdateAsync(id, updateDepartmentDto);
            return NoContent();
        }
        [Authorize(Policy = "ManageDepartments")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();
            await _departmentService.DeleteAsync(id);
            return NoContent();
        }
        [Authorize(Policy = "ViewDepartments")]
        [HttpGet("{id}/employees/count")]
        public async Task<IActionResult> GetEmployeeCount(int id)
        {
            var result = await _departmentService.GetEmployeeCountAsync(id);

            return Ok(result);
        }

        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetEmployees(int id)
        {
            var employees = await _departmentService.GetEmployeeNamesAsync(id);

            return Ok(employees);
        }
    }
}
