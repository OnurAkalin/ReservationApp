namespace Domain.Entities;

public class EmployeeRequestDto
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Code { get; set; }
}