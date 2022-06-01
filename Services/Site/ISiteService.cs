namespace Services;

public interface ISiteService
{
    Task<Result> InsertSiteAsync(SiteRequestDto requestDto);
    Task<Result> UpdateSiteAsync(SiteRequestDto requestDto);
    Task<DataResult<List<SiteResponseDto>>> ListSiteAsync();
    Task<DataResult<SiteResponseDto>> GetSiteAsync(int id);
    Task<Result> DeleteSiteAsync(int id);
    Task<Result> UploadSiteImageAsync(ImageRequestDto requestDto);

    Task<Result> InsertSiteServiceAsync(SiteServiceRequestDto requestDto);
    Task<Result> UpdateSiteServiceAsync(SiteServiceRequestDto requestDto);
    Task<DataResult<List<SiteServiceResponseDto>>> ListSiteServiceAsync();
    Task<DataResult<SiteServiceResponseDto>> GetSiteServiceAsync(int id);
    Task<Result> DeleteSiteServiceAsync(int id);
    Task<Result> UploadSiteServiceImageAsync(ImageRequestDto requestDto);
}