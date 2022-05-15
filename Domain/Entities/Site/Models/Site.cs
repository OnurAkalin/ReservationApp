using Core.Entities;

namespace Domain.Entities;

public class Site : EntityBaseWithAudit<int>, IEntity
{
    public string Code { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // RELATIONS //
    public List<SiteCustomization> SiteCustomizations { get; set; }
    public List<SiteImage> SiteImages { get; set; }
}