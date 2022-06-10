namespace Domain.Entities;

public class SiteOffTimeRequestDto
{
    public int? Id { get; set; }
    public Day Day { get; set; }
    public bool IsFullDay { get; set; }

    public DateTime? Date { get; set; }
    public DateTime? EndDate { get; set; }
}