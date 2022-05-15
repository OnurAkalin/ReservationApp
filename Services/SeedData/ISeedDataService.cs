namespace Services;

public interface ISeedDataService
{
    Task<Result> SeedAdminSiteAndUser();
}