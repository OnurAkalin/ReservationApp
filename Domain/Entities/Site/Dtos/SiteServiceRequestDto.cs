﻿namespace Domain.Entities;

public class SiteServiceRequestDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Duration { get; set; }
    public bool BreakAfter { get; set; }
    public DateTime? BreakAfterDuration { get; set; }
    public int? Price { get; set; }
    public Currency? Currency { get; set; }
    public string Color { get; set; }
}