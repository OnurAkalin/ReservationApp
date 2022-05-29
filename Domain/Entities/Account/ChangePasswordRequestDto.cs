namespace Domain.Entities;

public class ChangePasswordRequestDto : IDto
{
    public int Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}