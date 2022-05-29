using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SiteImage : EntityBase<int>
{
    [ForeignKey("Site")] public int SiteId { get; set; }
    public Site Site { get; set; }

    [ForeignKey("Image")] public int ImageId { get; set; }
    public Image Image { get; set; }
}