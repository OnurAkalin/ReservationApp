namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
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


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<ReservationRequestDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _reservationService.GetAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<ReservationRequestDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _reservationService.ListAsync());


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Cancel([FromQuery] int id)
        => Ok(await _reservationService.CancelAsync(id));
}