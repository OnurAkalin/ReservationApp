using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost(Name = ("CreateUserWithOTP"))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool) , StatusCodes.Status400BadRequest)]
    public bool CreateUserWithOtp([FromBody] bool data)
    {
        var result = data;
        return result;
    }

    [HttpPost(Name = ("Login"))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool) , StatusCodes.Status400BadRequest)]
    public bool Login([FromBody] bool data)
    {
        var result = data;
        return result;
    }

    [HttpPost(Name = ("ForgotPassword"))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool) , StatusCodes.Status400BadRequest)]
    public bool ForgotPassword([FromBody] bool data)
    {
        var result = data;
        return result;
    }

    [HttpPost(Name = ("ChangePassword"))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool) , StatusCodes.Status400BadRequest)]
    public bool ChangePassword([FromBody] bool data)
    {
        var result = data;
        return result;
    }
}