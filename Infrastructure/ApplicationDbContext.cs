using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        #region Entity Settings

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        builder.Entity<SiteImage>().HasKey(x => new {x.SiteId, x.ImageId});
        builder.Entity<SiteServiceImage>().HasKey(x => new {x.ServiceId, x.ImageId});

        #endregion
        
        base.OnModelCreating(builder);

        #region Models

        #endregion
    }

    #region DbSets
    public DbSet<Site> Sites { get; set; } = null!;
    public DbSet<SiteCustomization> SiteCustomizations { get; set; } = null!;
    public DbSet<SiteService> SiteServices { get; set; } = null!;
    public DbSet<SiteServiceDay> SiteServiceDays { get; set; } = null!;

    public DbSet<Calendar> Calendars { get; set; } = null!;

    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<SiteImage> SiteImages { get; set; } = null!;
    public DbSet<SiteServiceImage> SiteServiceImages { get; set; } = null!;

    #endregion
}