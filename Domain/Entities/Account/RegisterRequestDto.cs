using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class RegisterRequestDto
{
    [Required] public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Password { get; set; }
}