namespace Services;

public interface IDashboardService
{
    Task<DataResult<List<UserResponseDto>>> GetLastActiveUsersAsync();
}