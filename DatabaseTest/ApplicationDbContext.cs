using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseTest;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;" +
                                    "Initial Catalog=ReservationApp;" +
                                    "User ID=SA;" +
                                    "Password=<Reservation-1234>;" +
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

        #endregion

        #region Models

        builder.Entity<ReservationSite>(entity => { entity.ToTable("ReservationSites"); });
        builder.Entity<UserRole>(entity => { entity.ToTable("UserRoles"); });
        builder.Entity<User>(entity => { entity.ToTable("Users"); });
        builder.Entity<SiteService>(entity => { entity.ToTable("SiteServices"); });
        builder.Entity<Calendar>(entity => { entity.ToTable("Calendars"); });
        builder.Entity<SiteCustomization>(entity => { entity.ToTable("SiteCustomizations"); });

        #endregion
    }

    #region DbSets

    public DbSet<ReservationSite> ReservationSites { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<SiteService> SiteServices { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<SiteCustomization> SiteCustomizations { get; set; }

    #endregion
}