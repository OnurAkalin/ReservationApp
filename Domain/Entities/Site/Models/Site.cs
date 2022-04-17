using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Domain.Entities;

public class Site : EntityBaseWithAudit<Guid>, IEntity
{
    [MaxLength(10)] public string Code { get; set; }
    [MaxLength(20)] public string PhoneNumber { get; set; }
    [MaxLength(40)] public string Email { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // RELATIONS //
    public List<SiteCustomization> SiteCustomizations { get; set; } = new List<SiteCustomization>();
    public List<SiteImage> SiteImages { get; set; } = new List<SiteImage>();
}