using System;

namespace Domain.Entities
{
    public class SiteCustomization : EntityBaseSite<Guid>
    {
        public string ComponentName { get; set; }
        public string CssContent { get; set; }
    }
}