﻿using Domain.Enumerations;

namespace Domain.Entities;

public class CalendarRequestDto
{
    public int? Id { get; set; }
    public Day? Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
        
    public string UserMessage { get; set; }
    public int SiteServiceId { get; set; }
    public int? UserId { get; set; }
}