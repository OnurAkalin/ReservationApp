namespace Services;

public interface IDashboardService
{
    Task<DataResult<List<UserResponseDto>>> GetLastActiveUsersAsync();
    Task<DataResult<int>> CalculateIncomeAsync(CalculateIncomeRequestDto requestDto);
    Task<DataResult<WeeklySummaryResponseDto>> GetWeeklySummaryAsync();
    Task<DataResult<List<MonthlyUserSummaryResponseDto>>> GetMonthlyUserSummaryAsync();
}