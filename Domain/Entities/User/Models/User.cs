using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Guid? CreateUser { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid? ModifyUser { get; set; }
    public DateTime? ModifyDate { get; set; }

    [ForeignKey("Site")] public int SiteId { get; set; }
    public Site Site { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}