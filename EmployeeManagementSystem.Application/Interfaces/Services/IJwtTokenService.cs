using EmployeeManagementSystem.Application.DTOs.Auth;
using EmployeeManagementSystem.Domain.Entities;

public interface IJwtTokenService
{
    Task<LoginResponseDto> GenerateTokenAsync(
        ApplicationUser user);

    Task<LoginResponseDto> RefreshTokenAsync(
        RefreshTokenRequestDto dto);
    string GenerateRefreshToken();
}