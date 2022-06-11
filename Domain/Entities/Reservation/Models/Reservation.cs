using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Reservation : EntityBaseSiteWithAudit<int>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool AllDay { get; set; }
    public string CssClass { get; set; }
    public string Color { get; set; }
    
    // Resizable //
    public bool BeforeStart { get; set; }
    public bool AfterEnd { get; set; }
    
    // Action //
    public bool Editable { get; set; }
    public bool Deletable { get; set; }

    public bool Draggable { get; set; }
    
    // Meta //
    public bool IsCancelled { get; set; }
    public string UserMessage { get; set; } // User message or note for reservation.

    // FOREIGN KEYS //
    
    [ForeignKey("SiteService")] public int SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public User User { get; set; }
}