using System;
using System.Collections.Generic;
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
        public decimal? AveragePrice { get; set; }

        public List<Calendar> Calendars { get; set; }
    }
}