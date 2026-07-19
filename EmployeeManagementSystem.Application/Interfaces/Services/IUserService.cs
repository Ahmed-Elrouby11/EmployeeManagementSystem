public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();

    Task<UserDto> GetUserByIdAsync(string id);

    Task ChangeRoleAsync(
        string userId,
        string role);
}