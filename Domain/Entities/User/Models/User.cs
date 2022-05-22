using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public DateTime RegisterDate { get; set; }

    [ForeignKey("Site")] public int SiteId { get; set; }
    public Site Site { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}