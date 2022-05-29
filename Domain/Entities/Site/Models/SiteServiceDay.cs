using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SiteServiceDay : EntityBase<int>
{
    public Day Day { get; set; }
    public bool IsHoliday { get; set; }
    public DateTime? Date { get; set; }

    [ForeignKey("SiteService")] public int SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }
}