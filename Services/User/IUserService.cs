namespace Services;

public interface IUserService
{
    Task<Result> UpdateUser(UserDto requestDto);
    Task<Result> DeleteUser(Guid id);
}