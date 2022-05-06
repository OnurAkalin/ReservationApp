namespace Domain.Entities;

public class TokenResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public Guid UserId { get; set; }
    public string? UserFullName { get; set; }
    public List<string> UserRoles { get; set; } = null!;
}