namespace Domain.Entities;

public class SiteOffTimes : EntityBaseSite<int>
{
    public bool IsFullDay { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}