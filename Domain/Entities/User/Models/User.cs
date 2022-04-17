using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Domain.Entities;

public class User : EntityBaseSiteWithAudit<Guid>, IEntity
{
    [MaxLength(60)] public string UserName { get; set; }
    [MaxLength(30)] public string Name { get; set; }
    [MaxLength(30)] public string Surname { get; set; }
    [MaxLength(20)] public string PhoneNumber { get; set; }
    [MaxLength(40)] public string Email { get; set; }
    [MaxLength(200)] public string PasswordHash { get; set; }

    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public List<Calendar> Calendars { get; set; } = new List<Calendar>();
}