namespace Domain.Entities;

public class TokenResponseDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public int SiteId { get; set; }
    public List<string> UserRoles { get; set; }
}