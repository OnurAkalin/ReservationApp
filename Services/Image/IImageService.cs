namespace Services;

public interface IImageService
{
    Task<DataResult<int>> UploadToFileAsync(IFormFile image);
    Task<DataResult<string>> GetImagePathAsync(int id);
    Task<DataResult<List<string>>> ListImagePathsAsync();
    Task<DataResult<int>> UploadToDatabaseAsync(IFormFile image);
    Task<DataResult<string>> GetBase64ImageAsync(int id);
}