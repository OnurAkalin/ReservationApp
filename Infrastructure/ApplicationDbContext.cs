using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;" +
                                    "Database=ReservationApp;" +
                                    "User Id=SA;" +
                                    "password=<PassSql-1234>;" +
                                    "MultipleActiveResultSets=true");

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Entity Settings

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        builder.Entity<UserRole>().HasKey(x => new {x.UserId, x.Role});
        builder.Entity<SiteImage>().HasKey(x => new {x.SiteId, x.ImageId});
        builder.Entity<SiteServiceImage>().HasKey(x => new {x.ServiceId, x.ImageId});

        #endregion

        #region Models

        builder.Entity<User>(entity => { entity.ToTable("Users"); });
        builder.Entity<UserRole>(entity => { entity.ToTable("UserRoles"); });

        builder.Entity<Site>(entity => { entity.ToTable("Sites"); });
        builder.Entity<SiteCustomization>(entity => { entity.ToTable("SiteCustomizations"); });
        builder.Entity<SiteService>(entity => { entity.ToTable("SiteServices"); });
        builder.Entity<SiteServiceDay>(entity => { entity.ToTable("SiteServiceDays"); });

        builder.Entity<Calendar>(entity => { entity.ToTable("Calendars"); });

        builder.Entity<Image>(entity => { entity.ToTable("Images"); });
        builder.Entity<SiteImage>(entity => { entity.ToTable("SiteImages"); });
        builder.Entity<SiteServiceImage>(entity => { entity.ToTable("SiteServiceImages"); });

        #endregion
    }

    #region DbSets

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Site> Sites { get; set; }
    public DbSet<SiteCustomization> SiteCustomizations { get; set; }
    public DbSet<SiteService> SiteServices { get; set; }
    public DbSet<SiteServiceDay> SiteServiceDays { get; set; }

    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<Image> Images { get; set; }
    public DbSet<SiteImage> SiteImages { get; set; }
    public DbSet<SiteServiceImage> SiteServiceImages { get; set; }

    #endregion
}