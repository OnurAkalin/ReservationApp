﻿namespace Domain.Entities;

public class SiteRequestDto : IDto
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}