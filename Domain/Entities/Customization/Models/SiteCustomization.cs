using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Domain.Entities;

public class SiteCustomization : EntityBaseSite<Guid> , IEntity
{
    [MaxLength(50)] public string ComponentName { get; set; }
    public string CssContent { get; set; }
}