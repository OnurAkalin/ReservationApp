namespace Services;

public interface IUserService
{
    Task<DataResult<UserDto>> GetUserAsync(int id);
    Task<Result> UpdateUserAsync(UserDto requestDto);
    Task<Result> DeleteUserAsync(int id);
    Task<Result> AddToRoleAsync(int userId , int roleId);
    Task<Result> DeleteFromRoleAsync(int userId, int roleId);

}