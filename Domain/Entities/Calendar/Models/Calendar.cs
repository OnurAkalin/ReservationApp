using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Domain.Enumerations;

namespace Domain.Entities;

public class Calendar : EntityBaseSiteWithAudit<Guid>, IEntity
{
    public Day? Day { get; set; } // This property may helpful for FE.
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
        
    public string UserMessage { get; set; } // User message or note for reservation.

    // FOREIGN KEYS //
        
    [ForeignKey("SiteService")] public Guid SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }

    [ForeignKey("User")] public Guid UserId { get; set; }
    public User User { get; set; }
}