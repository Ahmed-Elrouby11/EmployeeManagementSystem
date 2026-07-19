using EmployeeManagementSystem.Domain.Entities;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);

    Task AddAsync(RefreshToken refreshToken);

    Task UpdateAsync(RefreshToken refreshToken);
}