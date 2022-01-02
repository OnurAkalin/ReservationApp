using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class EntityBaseSite<T> : EntityBase<T>
    {
        [ForeignKey("ReservationSite")] public Guid SiteId { get; set; }
        public ReservationSite ReservationSite { get; set; }
    }
}