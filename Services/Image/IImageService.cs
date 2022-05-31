namespace Services;

public interface IImageService
{
    Task<DataResult<int>> UploadImage(IFormFile image);
    Task<DataResult<string>> GetImage(int id);
}