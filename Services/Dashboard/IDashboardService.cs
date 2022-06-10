using Domain.Entities.Dashboard;

namespace Services;

public interface IDashboardService
{
    Task<DataResult<List<UserResponseDto>>> GetLastActiveUsersAsync();
    Task<DataResult<int>> CalculateIncome(CalculateIncomeRequestDto requestDto);
}