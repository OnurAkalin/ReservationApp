namespace Services;

public interface ISeedDataService
{
    Task<Result> SeedBaseData();
}