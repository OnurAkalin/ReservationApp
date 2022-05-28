﻿using Domain.Enumerations;

namespace Domain.Entities;

public class SiteServiceResponseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Duration { get; set; }
    public bool BreakAfter { get; set; }
    public DateTime? BreakAfterDuration { get; set; }
    public int? Price { get; set; }
    public Currency? Currency { get; set; }
}