namespace Domain.Entities;

public class ReservationRequestDto
{
    public int? Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool AllDay { get; set; }
    public string CssClass { get; set; }
    public string Color { get; set; }
    public ReservationResizableDto Resizable { get; set; }
    public ReservationActionDto Actions { get; set; }
    public bool Draggable { get; set; }
    public ReservationMetaRequestDto Meta { get; set; }
}

public class ReservationMetaRequestDto
{
    public bool IsCancelled { get; set; }
    public int SiteServiceId { get; set; }
    public int? UserId { get; set; }
    public string UserMessage { get; set; }
}