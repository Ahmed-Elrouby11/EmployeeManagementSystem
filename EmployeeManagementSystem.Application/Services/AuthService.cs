using EmployeeManagementSystem.Application.DTOs.Auth;
using EmployeeManagementSystem.Application.Exceptions;
using EmployeeManagementSystem.Application.Interfaces.Services;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    public AuthService(
        UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingUser =
            await _userManager.FindByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new ConflictException("Email already exists.");

        var user = new ApplicationUser
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result =
            await _userManager.CreateAsync(
                user,
                dto.Password);

        if (!result.Succeeded)
        {
            throw new ValidationException(
                string.Join(", ",
                    result.Errors.Select(e => e.Description)));
        }
        await _userManager.AddToRoleAsync(user, "Employee");
    }
    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user =
            await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            throw new UnauthorizedException("Invalid email or password.");

        var isPasswordValid =
            await _userManager.CheckPasswordAsync(
                user,
                dto.Password);

        if (!isPasswordValid)
            throw new UnauthorizedException("Invalid email or password.");

        return await _jwtTokenService.GenerateTokenAsync(user);
    }
}