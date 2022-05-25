namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class CalendarController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public CalendarController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(ReservationRequestDto requestDto)
        => Ok(await _reservationService.InsertAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(ReservationRequestDto requestDto)
        => Ok(await _reservationService.UpdateAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<ReservationResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _reservationService.GetAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<ReservationResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _reservationService.ListAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _reservationService.DeleteAsync(id));
}