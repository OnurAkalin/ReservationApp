using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities;

public class SiteImage : EntityBase<int>,  IEntity
{
    [ForeignKey("Site")] public int SiteId { get; set; }
    public Site Site { get; set; }

    [ForeignKey("Image")] public int ImageId { get; set; }
    public Image Image { get; set; }
}