using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities;

public class SiteServiceImage : EntityBase<int>, IEntity
{
    [ForeignKey("SiteService")] public int ServiceId { get; set; }
    public SiteService SiteService { get; set; }

    [ForeignKey("Image")] public int ImageId { get; set; }
    public Image Image { get; set; }
}