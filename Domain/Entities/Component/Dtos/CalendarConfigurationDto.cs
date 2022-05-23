namespace Domain.Entities;

public class CalendarConfigurationDto
{
    public string Id { get; set; }
    public string HourDuration { get; set; }
    public string HourSegmentHeight { get; set; }
    public string Precision { get; set; }
    public string Locale { get; set; }
    public string MonthViewColumnHeader { get; set; }
    public string MonthViewDayNumber { get; set; }
    public string MonthViewTitle { get; set; }
    public string WeekViewColumnHeader { get; set; }
    public string WeekViewColumnSubHeader { get; set; }
    public string WeekViewHour { get; set; }
    public string DayViewHour { get; set; }
    public string DayViewTitle { get; set; }
    public string ExcludeDays { get; set; }
    public string WeekendDays { get; set; }
    public string Theme { get; set; }
}