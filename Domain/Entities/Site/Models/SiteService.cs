using Core.Entities;
using Domain.Enumerations;

namespace Domain.Entities;

public class SiteService : EntityBaseSiteWithAudit<int>, IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
        
    public int Duration { get; set; } // Minute

    public bool BreakAfter { get; set; } // After service break times. Optional. True if exists.
    public int? BreakAfterDuration { get; set; } // If BreakAfter property is true. This field keeps break time duration in minute

    public int? Price { get; set; } // Optional
    public Currency? Currency { get; set; }

    // RELATIONS //
    public List<Reservation> Reservations { get; set; }
    public List<SiteServiceDay> SiteServiceDays { get; set; }
    public List<SiteServiceImage> SiteServiceImages { get; set; }
}