using Core.Entities;

namespace Domain.Entities;

public class SiteService : EntityBaseSiteWithAudit<int>, IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
        
    public DateTime Duration { get; set; }

    public bool BreakAfter { get; set; } // After service break times. Optional. True if exists.
    public DateTime? BreakAfterDuration { get; set; } // If BreakAfter property is true. This field keeps break time duration

    public int Price { get; set; } // Optional

    // RELATIONS //
    public List<Reservation> Calendars { get; set; }
    public List<SiteServiceDay> SiteServiceDays { get; set; }
    public List<SiteServiceImage> SiteServiceImages { get; set; }
}