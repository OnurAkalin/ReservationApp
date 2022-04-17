using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Domain.Entities;

public class SiteImage : IEntity
{
    [Key] [ForeignKey("Site")] public Guid SiteId { get; set; }
    public Site Site { get; set; }

    [Key] [ForeignKey("Image")] public int ImageId { get; set; }
    public Image Image { get; set; }
}