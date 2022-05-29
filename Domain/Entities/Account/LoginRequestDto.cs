namespace Domain.Entities;

public class LoginRequestDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}