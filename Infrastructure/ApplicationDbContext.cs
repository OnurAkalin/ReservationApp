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

        builder.Entity<Role>()
            .Ignore(x => x.ConcurrencyStamp);
    }

    #region DbSets

    public DbSet<Site> Sites { get; set; }
    public DbSet<SiteService> SiteServices { get; set; }
    public DbSet<SiteServiceDay> SiteServiceDays { get; set; }
    public DbSet<SiteOffTime> SiteOfTimes { get; set; }
    
    public DbSet<Component> Components { get; set; }
    
    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<Image> Images { get; set; }
    public DbSet<SiteImage> SiteImages { get; set; }

    #endregion
}