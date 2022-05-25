﻿namespace API.Controllers;

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
    public async Task<IActionResult> Insert(ReservationMainDto requestDto)
        => Ok(await _reservationService.InsertAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(ReservationMainDto requestDto)
        => Ok(await _reservationService.UpdateAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<ReservationMainDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _reservationService.GetAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<ReservationMainDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _reservationService.ListAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _reservationService.DeleteAsync(id));
}