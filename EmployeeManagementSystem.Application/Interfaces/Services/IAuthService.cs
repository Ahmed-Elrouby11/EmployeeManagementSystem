using EmployeeManagementSystem.Application.DTOs.Auth;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);

    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}