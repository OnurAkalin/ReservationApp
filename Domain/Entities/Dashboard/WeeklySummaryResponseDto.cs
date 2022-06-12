namespace Domain.Entities;

public class WeeklySummaryResponseDto
{
    public int CompletedReservationTotal { get; set; }
    public int CancelledReservationTotal { get; set; }
    public int SiteServiceTotal { get; set; }
    public int UserTotal { get; set; }
    public int MaleTotal { get; set; }
    public int FemaleTotal { get; set; }
    public List<WeeklySummaryDayResponseDto> DaySummary { get; set; }
    public List<WeeklySummarySiteServiceResponseDto> SiteServiceSummary { get; set; }
}

public class WeeklySummarySiteServiceResponseDto
{
    public string SiteServiceName { get; set; }
    public int Total { get; set; }
}

public class WeeklySummaryDayResponseDto
{
    public DayOfWeek Day { get; set; }
    public int CompletedTotal { get; set; }
    public int CancelledTotal { get; set; }
}