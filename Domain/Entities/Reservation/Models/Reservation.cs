using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Domain.Enumerations;

namespace Domain.Entities;

public class Reservation : EntityBaseSiteWithAudit<int>, IEntity
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public string UserMessage { get; set; } // User message or note for reservation.

    // FOREIGN KEYS //
    
    [ForeignKey("SiteService")] public int SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public User User { get; set; }
}