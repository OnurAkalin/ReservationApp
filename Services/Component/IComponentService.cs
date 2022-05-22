namespace Services;

public interface IComponentService
{
    Task<Result> SetLoginAsync(List<LoginComponentDto> requestDto);
    Task<DataResult<List<LoginComponentDto>>> GetLoginAsync();
}