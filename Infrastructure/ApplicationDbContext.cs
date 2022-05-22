using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .Ignore(x => x.AccessFailedCount)
            .Ignore(x => x.LockoutEnabled)
            .Ignore(x => x.LockoutEnd)
            .Ignore(x => x.TwoFactorEnabled)
            .Ignore(x => x.PhoneNumberConfirmed)
            .Ignore(x => x.EmailConfirmed);
    }

    #region DbSets

    public DbSet<Site> Sites { get; set; }
    public DbSet<SiteService> SiteServices { get; set; }
    public DbSet<SiteServiceDay> SiteServiceDays { get; set; }

    public DbSet<Component> Components { get; set; }

    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<Image> Images { get; set; }
    public DbSet<SiteImage> SiteImages { get; set; }
    public DbSet<SiteServiceImage> SiteServiceImages { get; set; }

    #endregion
}