using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Domain.Enumerations;

namespace Domain.Entities
{
    public class Calendar : EntityBaseSiteWithAudit<Guid>, IEntity
    {
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey("SiteService")] public Guid SiteServiceId { get; set; }
        public SiteService SiteService { get; set; }

        [ForeignKey("User")] public Guid UserId { get; set; }
        public User User { get; set; }
    }
}