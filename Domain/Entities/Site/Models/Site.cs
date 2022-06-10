namespace Domain.Entities;

public class Site : EntityBaseWithAudit<int>
{
    public string Code { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // RELATIONS //
    public List<SiteService> SiteServices { get; set; }
    public List<SiteOffTime> SiteOffTimes { get; set; }
    public List<SiteImage> SiteImages { get; set; }
}