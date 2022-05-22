namespace Services;

public interface ISiteService
{
    Task<Result> InsertAsync(SiteRequestDto requestDto);
    Task<Result> UpdateAsync(SiteRequestDto requestDto);
    Task<DataResult<List<SiteResponseDto>>> ListAsync();
    Task<DataResult<SiteResponseDto>> GetAsync(int id);
    Task<Result> DeleteAsync(int id);
}