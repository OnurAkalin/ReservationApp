namespace Domain.Entities;

public class CalendarConfigurationDto
{
    public string Id { get; set; }
    public int HourDuration { get; set; }
    public int HourSegmentHeight { get; set; }
    public string Precision { get; set; }
    public string Locale { get; set; }
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