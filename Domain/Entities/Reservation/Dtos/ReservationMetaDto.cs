namespace Domain.Entities;

public class ReservationMetaDto
{
    public int? UserId { get; set; }
    public int SiteId { get; set; }
    public int SiteServiceId { get; set; }
    public string UserMessage { get; set; }
}