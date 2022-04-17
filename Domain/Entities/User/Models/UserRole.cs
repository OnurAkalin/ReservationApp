using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Domain.Enumerations;

namespace Domain.Entities;

public class UserRole : IEntity
{
    [Key] [ForeignKey("User")] public Guid UserId { get; set; }
    public User User { get; set; }

    [Key] public Role Role { get; set; }
}