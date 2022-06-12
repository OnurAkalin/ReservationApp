namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<UserResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLastActiveUsers()
        => Ok(await _dashboardService.GetLastActiveUsersAsync());
    
    
    [HttpPost]
    [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateIncome([FromBody] CalculateIncomeRequestDto requestDto)
        => Ok(await _dashboardService.CalculateIncomeAsync(requestDto));
    
    
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<WeeklySummaryResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWeeklySummary()
        => Ok(await _dashboardService.GetWeeklySummaryAsync());
    
    
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<MonthlySummaryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthlyUserSummary()
        => Ok(await _dashboardService.GetMonthlyUserSummaryAsync());
    
    
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<MonthlySummaryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthlyIncomeSummary()
        => Ok(await _dashboardService.GetMonthlyIncomeSummaryAsync());
    
    
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<MonthlySummaryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthlyReservationSummary()
        => Ok(await _dashboardService.GetMonthlyReservationSummaryAsync());
}