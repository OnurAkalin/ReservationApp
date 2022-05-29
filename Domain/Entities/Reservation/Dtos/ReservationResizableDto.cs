namespace Domain.Entities;

public class ReservationResizableDto : IDto
{
    public bool BeforeStart { get; set; }
    public bool AfterEnd { get; set; }
}