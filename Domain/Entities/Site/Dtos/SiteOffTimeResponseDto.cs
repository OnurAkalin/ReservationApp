namespace Domain.Entities;

public class SiteOffTimeResponseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsFullDay { get; set; }

    public DateTime? EndDate { get; set; }
}