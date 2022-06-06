namespace Services;

public interface IUserService
{
    Task<DataResult<UserResponseDto>> GetAsync(int id);
    Task<Result> UpdateAsync(UserRequestDto requestDto);
    Task<Result> DeleteAsync(int id);
    Task<Result> AddToRoleAsync(int userId, int roleId);
    Task<Result> DeleteFromRoleAsync(int userId, int roleId);
}