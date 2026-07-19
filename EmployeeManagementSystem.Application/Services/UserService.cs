using EmployeeManagementSystem.Application.Interfaces.Services;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var result = new List<UserDto>();

        foreach (var user in users)
        {
            var roles =
                await _userManager.GetRolesAsync(user);

            result.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                FullName =
                    $"{user.FirstName} {user.LastName}",
                Roles = roles
            });
        }

        return result;
    }
    public async Task<UserDto> GetUserByIdAsync(
    string id)
    {
        var user =
            await _userManager.FindByIdAsync(id);

        if (user == null)
            throw new Exception("User not found");

        var roles =
            await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FullName =
                $"{user.FirstName} {user.LastName}",
            Roles = roles
        };
    }
    public async Task ChangeRoleAsync(
    string userId,
    string role)
    {
        var user =
            await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new Exception("User not found");

        var currentRoles =
            await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(
            user,
            currentRoles);

        await _userManager.AddToRoleAsync(
            user,
            role);
    }

}