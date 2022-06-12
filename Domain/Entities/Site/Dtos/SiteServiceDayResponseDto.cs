namespace Domain.Entities;

public class SiteServiceDayResponseDto
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public int SiteServiceId { get; set; }
}