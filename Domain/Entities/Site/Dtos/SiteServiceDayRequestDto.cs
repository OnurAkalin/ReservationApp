namespace Domain.Entities;

public class SiteServiceDayRequestDto
{
    public int? Id { get; set; }
    public Day Day { get; set; }
    public bool IsHoliday { get; set; }
    public int SiteServiceId { get; set; }
}