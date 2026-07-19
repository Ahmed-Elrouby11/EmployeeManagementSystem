using EmployeeManagementSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
[Authorize(Policy = "ManageEmployees")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(
        IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _userService.GetAllUsersAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
    string id)
    {
        return Ok(
            await _userService.GetUserByIdAsync(id));
    }
    [HttpPut("{id}/role")]
    public async Task<IActionResult> ChangeRole(
    string id,
    ChangeRoleDto dto)
    {
        await _userService.ChangeRoleAsync(
            id,
            dto.Role);

        return NoContent();
    }
}