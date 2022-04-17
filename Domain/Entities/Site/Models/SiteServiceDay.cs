using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerations;

namespace Domain.Entities;

public class SiteServiceDay : EntityBase<Guid>
{
    public Day Day { get; set; }

    [ForeignKey("SiteService")] public Guid SiteServiceId { get; set; }
    public SiteService SiteService { get; set; }
}