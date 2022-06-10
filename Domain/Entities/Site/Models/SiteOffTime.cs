namespace Domain.Entities;

public class SiteOffTime : EntityBaseSite<int>
{
    public DateTime Date { get; set; }
    public bool IsFullDay { get; set; }

    public DateTime? EndDate { get; set; }
}