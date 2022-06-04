namespace Domain.Entities;

public class SiteResponseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public List<ImageResponseDto> Images { get; set; }
}