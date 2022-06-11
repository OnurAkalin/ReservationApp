namespace Domain.Entities;

public class ReservationResponseDto
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
    public ReservationMetaResponseDto Meta { get; set; }
}

public class ReservationMetaResponseDto
{
    public SiteServiceResponseDto SiteService { get; set; }
    public UserResponseDto User { get; set; }
    public string UserMessage { get; set; }
    public bool IsCancelled { get; set; }
}