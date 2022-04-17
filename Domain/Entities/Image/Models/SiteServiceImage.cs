using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities;

public class SiteServiceImage : IEntity
{
    [Key] [ForeignKey("SiteService")] public Guid ServiceId { get; set; }
    public SiteService SiteService { get; set; }

    [Key] [ForeignKey("Image")] public int ImageId { get; set; }
    public Image Image { get; set; }
}