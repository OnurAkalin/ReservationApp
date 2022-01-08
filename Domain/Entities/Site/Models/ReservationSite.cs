using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities
{
    public class ReservationSite : EntityBaseWithAudit<Guid>, IEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [ForeignKey("Logo")] public int LogoId { get; set; }
        public Logo Logo { get; set; }

        public List<SiteCustomization> SiteCustomizations { get; set; }
    }
}