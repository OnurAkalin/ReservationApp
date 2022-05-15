using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserDto
{
    [Required] public Guid Id { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string PhoneNumber { get; set; }
}