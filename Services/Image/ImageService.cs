namespace Services;

public class ImageService : BasicService, IImageService
{
    public ImageService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
    }

    public async Task<DataResult<int>> UploadImage(IFormFile image)
    {
        var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        
        var imageEntity = new Image
        {
            ImageTitle = image.FileName,
            ImageData = memoryStream.ToArray()
        };
        
        await _dbContext.Images.AddAsync(imageEntity);
        await _dbContext.SaveChangesAsync();

        return new SuccessDataResult<int>(imageEntity.Id,UiMessages.Success);
    }

    public async Task<DataResult<string>> GetImage(int id)
    {
        var image = await _dbContext.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (image is null)
        {
            return new ErrorDataResult<string>(UiMessages.NotFoundData);
        }
        
        var imageData = $"data:image/jpg;base64,{Convert.ToBase64String(image.ImageData)}";

        return new SuccessDataResult<string>(imageData,UiMessages.Success);
    }
}