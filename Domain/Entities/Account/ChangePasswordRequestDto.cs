namespace Domain.Entities;

public class ChangePasswordRequestDto
{
    public Guid Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}