namespace Domain.Entities;

public class ReservationMetaDto
{
    public int SiteServiceId { get; set; }
    public int? UserId { get; set; }
    public string UserMessage { get; set; }
}