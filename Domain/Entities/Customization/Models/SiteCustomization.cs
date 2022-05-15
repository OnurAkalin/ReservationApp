using Core.Entities;

namespace Domain.Entities;

public class SiteCustomization : EntityBaseSite<int> , IEntity
{
    public string ComponentName { get; set; }
    public string CssContent { get; set; }
}