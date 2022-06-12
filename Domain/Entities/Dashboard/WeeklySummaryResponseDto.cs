namespace Domain.Entities;

public class WeeklySummaryResponseDto
{
    public int CompletedReservationTotal { get; set; }
    public int CancelledReservationTotal { get; set; }
    public int SiteServiceTotal { get; set; }
    public int UserTotal { get; set; }
    public int MaleTotal { get; set; }
    public int FemaleTotal { get; set; }
}