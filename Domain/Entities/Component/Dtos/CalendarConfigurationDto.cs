namespace Domain.Entities;

public class CalendarConfigurationDto
{
    public string Id { get; set; }
    public int HourDuration { get; set; }
    public int HourSegmentHeight { get; set; }
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
    public List<int> ExcludeDays { get; set; }
    public List<int> WeekendDays { get; set; }
    public CalendarConfigThemeDto Theme { get; set; }
    public int DayStartHour { get; set; }
    public int DayStartMinute { get; set; }
    public int DayEndHour { get; set; }
    public int DayEndMinute { get; set; }
    public string Font { get; set; }
}

public class CalendarConfigThemeDto
{
    public string Name { get; set; }
    public string Primary { get; set; }
    public string Secondary { get; set; }
}