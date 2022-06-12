namespace Services;

public interface IDashboardService
{
    Task<DataResult<List<UserResponseDto>>> GetLastActiveUsersAsync();
    Task<DataResult<int>> CalculateIncomeAsync(CalculateIncomeRequestDto requestDto);
    Task<DataResult<WeeklySummaryResponseDto>> GetWeeklySummaryAsync();
    Task<DataResult<List<MonthlySummaryResponseDto>>> GetMonthlyUserSummaryAsync();
    Task<DataResult<List<MonthlySummaryResponseDto>>> GetMonthlyIncomeSummaryAsync();
    Task<DataResult<List<MonthlySummaryResponseDto>>> GetMonthlyReservationSummaryAsync();
}