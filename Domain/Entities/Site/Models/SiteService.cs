using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities
{
    public class SiteService : EntityBaseSiteWithAudit<Guid>, IEntity
    {
        public string Description { get; set; }
        public DateTime AverageDuration { get; set; }

        // After service break times. Optional
        public bool BreakAfter { get; set; }
        public DateTime? BreakAfterDuration { get; set; }
        
        [Column(TypeName = "money")]
        public decimal? AveragePrice { get; set; }
        
        [ForeignKey("Logo")] public int LogoId { get; set; }
        public Logo Logo { get; set; }

        public List<Calendar> Calendars { get; set; }
        public List<SiteServiceDay> SiteServiceDays { get; set; }
    }
}