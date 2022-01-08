using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class EntityBaseSite<T> : EntityBase<T>
    {
        [ForeignKey("Site")] public Guid SiteId { get; set; }
        public Site Site { get; set; }
    }
}