using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public abstract class EntityBaseSite<T> : EntityBase<T>
{
    [ForeignKey("Site")] public int SiteId { get; set; }
    public Site Site { get; set; }
}