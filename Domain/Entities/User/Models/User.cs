using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    //[ForeignKey("Site")] public Guid SiteId { get; set; }
    //public Site Site { get; set; } = null!;

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Calendar> Calendars { get; set; } = null!;

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}