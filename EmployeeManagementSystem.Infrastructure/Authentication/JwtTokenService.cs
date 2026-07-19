using EmployeeManagementSystem.Application.DTOs.Auth;
using EmployeeManagementSystem.Application.Exceptions;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public JwtTokenService(
        IOptions<JwtSettings> options,
        UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork)
    {
        _jwtSettings = options.Value;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> GenerateTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(
            _jwtSettings.DurationInMinutes);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var accessToken = await GenerateAccessToken(user, expiresAt);

        // Generate Refresh Token
        var refreshToken = GenerateRefreshToken();

        // Save Refresh Token
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            IsRevoked = false
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

        await _unitOfWork.SaveChangesAsync();

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration = expiresAt,
            Email = user.Email!,
            FullName = $"{user.FirstName} {user.LastName}"
        };
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    private async Task<string> GenerateAccessToken(
    ApplicationUser user,
    DateTime expiresAt)
    {
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(
    RefreshTokenRequestDto dto)
    {
        var existingToken = await _unitOfWork.RefreshTokens
            .GetByTokenAsync(dto.RefreshToken);

        if (existingToken == null)
            throw new UnauthorizedException("Invalid refresh token.");

        if (existingToken.IsRevoked)
            throw new UnauthorizedException("Refresh token has been revoked.");

        if (existingToken.ExpiresAt <= DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token has expired.");

        // Revoke old token
        existingToken.IsRevoked = true;
        existingToken.RevokedAt = DateTime.UtcNow;

        await _unitOfWork.RefreshTokens.UpdateAsync(existingToken);

        // Create new refresh token
        var newRefreshToken = new RefreshToken
        {
            Token = GenerateRefreshToken(),
            UserId = existingToken.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            IsRevoked = false
        };

        await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);

        await _unitOfWork.SaveChangesAsync();

        var expiresAt = DateTime.UtcNow.AddMinutes(
            _jwtSettings.DurationInMinutes);

        var accessToken = await GenerateAccessToken(
            existingToken.User,
            expiresAt);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
            AccessTokenExpiration = expiresAt,
            Email = existingToken.User.Email!,
            FullName =
                $"{existingToken.User.FirstName} {existingToken.User.LastName}"
        };
    }
}