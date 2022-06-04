using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SiteServiceImage : EntityBase<int>
{
    [ForeignKey("SiteService")] public int ServiceId { get; set; }
    public SiteService SiteService { get; set; }

    [ForeignKey("Image")] public int ImageId { get; set; }
    public Image Image { get; set; }
}