namespace Services;

public interface ISiteService
{
    #region Site

    Task<Result> InsertSiteAsync(SiteRequestDto requestDto);
    Task<Result> UpdateSiteAsync(SiteRequestDto requestDto);
    Task<DataResult<List<SiteResponseDto>>> ListSiteAsync();
    Task<DataResult<SiteResponseDto>> GetSiteAsync(int id);
    Task<Result> DeleteSiteAsync(int id);
    Task<Result> UploadSiteImageAsync(ImageRequestDto requestDto);

    #endregion

    #region Site Service

    Task<Result> InsertSiteServiceAsync(SiteServiceRequestDto requestDto);
    Task<Result> UpdateSiteServiceAsync(SiteServiceRequestDto requestDto);
    Task<DataResult<List<SiteServiceResponseDto>>> ListSiteServiceAsync();
    Task<DataResult<SiteServiceResponseDto>> GetSiteServiceAsync(int id);
    Task<Result> DeleteSiteServiceAsync(int id);
    Task<Result> UploadSiteServiceImageAsync(ImageRequestDto requestDto);

    #endregion

    #region Site Service Day

    Task<Result> InsertSiteServiceDayAsync(SiteServiceDayRequestDto requestDto);
    Task<Result> UpdateSiteServiceDayAsync(SiteServiceDayRequestDto requestDto);
    Task<DataResult<List<SiteServiceDayResponseDto>>> ListSiteServiceDayAsync();
    Task<DataResult<SiteServiceDayResponseDto>> GetSiteServiceDayAsync(int id);
    Task<Result> DeleteSiteServiceDayAsync(int id);

    #endregion
}