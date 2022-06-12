using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SiteServiceDay : EntityBase<int>
{
    public DayOfWeek Day { get; set; }

    [ForeignKey("SiteService")] public int SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }
}