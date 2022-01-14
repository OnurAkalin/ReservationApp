namespace Infrastructure.SeedData;

public static partial class ApplicationDbInitializer
{
    public static void SeedData(ApplicationDbContext dbContext)
    {
        SeedAdminSite(dbContext);
        SeedTestSites(dbContext);
        SeedAdminUsers(dbContext);
    }
}