using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities;

public class SiteService : EntityBaseSiteWithAudit<Guid>, IEntity
{
    [MaxLength(50)] public string Name { get; set; }
    public string Description { get; set; }
        
    public DateTime Duration { get; set; }

    public bool BreakAfter { get; set; } // After service break times. Optional. True if exists.
    public DateTime? BreakAfterDuration { get; set; } // If BreakAfter property is true. This field keeps break time duration

    [Column(TypeName = "money")] public decimal? ServicePrice { get; set; } // Optional

    // RELATIONS //
    public List<Calendar> Calendars { get; set; } = new List<Calendar>();
    public List<SiteServiceDay> SiteServiceDays { get; set; } = new List<SiteServiceDay>();
    public List<SiteServiceImage> SiteServiceImages { get; set; } = new List<SiteServiceImage>();
}