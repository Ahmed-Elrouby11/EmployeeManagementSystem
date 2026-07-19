using Asp.Versioning;
using EmployeeManagementSystem.Application.DTOs.Common;
using EmployeeManagementSystem.Application.DTOs.Employee;
using EmployeeManagementSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(
        IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            Message = "You are authenticated!"
        });
    }

    [Authorize(Policy = "ViewEmployees")]
    [HttpGet]
    public async Task<IActionResult> GetAll(
      [FromQuery] EmployeeQueryParameters parameters)
    {
        var result =
            await _employeeService.GetAllAsync(parameters);

        return Ok(result);
    }

    [Authorize(Policy = "ViewEmployees")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDetailsDto>> GetById(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [Authorize(Policy = "ManageEmployees")]
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeDto createEmployeeDto)
    {
        var employee = await _employeeService.CreateAsync(createEmployeeDto);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }
    [Authorize(Policy = "ManageEmployees")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> Update(int id, UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = await _employeeService.UpdateAsync(id, updateEmployeeDto);
        if (employee == null)
            return NotFound();
        return Ok(employee);
    }
    [Authorize(Policy = "ManageEmployees")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.DeleteAsync(id);
        return NoContent();
    }

}