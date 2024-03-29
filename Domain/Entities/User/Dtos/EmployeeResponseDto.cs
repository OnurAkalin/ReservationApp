﻿namespace Domain.Entities;

public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime? LastLoginDate { get; set; }
}