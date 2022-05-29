namespace Domain.Entities;

public class ReservationActionDto : IDto
{
    public bool Editable { get; set; }
    public bool Deletable { get; set; }
}