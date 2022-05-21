﻿namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SeedDataController : ControllerBase
{
    private readonly ISeedDataService _seedDataService;

    public SeedDataController(ISeedDataService seedDataService)
    {
        _seedDataService = seedDataService;
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SeedBaseData()
        => Ok(await _seedDataService.SeedBaseData());
    
}