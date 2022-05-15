using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerations;

namespace Domain.Entities;

public class SiteServiceDay : EntityBase<int>
{
    public Day Day { get; set; }

    [ForeignKey("SiteService")] public int SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }
}