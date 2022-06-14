using Microsoft.AspNetCore.Hosting;

namespace Services;

public class ImageService : BasicService, IImageService
{
    private readonly IWebHostEnvironment _environment;

    public ImageService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _environment = environment;
    }

    public async Task<DataResult<int>> UploadToFileAsync(IFormFile image)
    {
        if (image is null)
        {
            return new ErrorDataResult<int>(UiMessages.EmptyRequest);
        }

        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Images");
        var formattedImageName = image.FileName;
        var imagePath = Path.Combine(uploadsFolder, formattedImageName);

        await using var fileStream = new FileStream(imagePath, FileMode.Create);
        await image.CopyToAsync(fileStream);

        var imageEntity = new Image
        {
            Title = image.FileName,
            Path = "/Images/" + $"{formattedImageName}"
        };

        await _dbContext.Images.AddAsync(imageEntity);
        await _dbContext.SaveChangesAsync();

        return new SuccessDataResult<int>(imageEntity.Id, UiMessages.Success);
    }

    public async Task<DataResult<string>> GetImagePathAsync(int id)
    {
        var imagePath = await _dbContext.Images
            .AsNoTracking()
            .Where(x => x.Id.Equals(id))
            .Select(x => x.Path)
            .FirstOrDefaultAsync();

        if (imagePath is null)
        {
            return new ErrorDataResult<string>(UiMessages.NotFoundData);
        }

        return new SuccessDataResult<string>(imagePath, UiMessages.Success);
    }
    
    public async Task<DataResult<List<string>>> ListImagePathsAsync()
    {
        var images = await _dbContext.Images
            .AsNoTracking()
            .Select(x => x.Path)
            .ToListAsync();

        return new SuccessDataResult<List<string>>(images, UiMessages.Success);
    }

    public async Task<DataResult<int>> UploadToDatabaseAsync(IFormFile image)
    {
        if (image is null)
        {
            return new ErrorDataResult<int>(UiMessages.EmptyRequest);
        }

        await using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);

        var formattedImageName = image.FileName;

        var imageEntity = new Image
        {
            Title = image.FileName,
            Path = "/Images/" + $"{formattedImageName}",
            Data = memoryStream.ToArray()
        };

        await _dbContext.Images.AddAsync(imageEntity);
        await _dbContext.SaveChangesAsync();

        return new SuccessDataResult<int>(imageEntity.Id, UiMessages.Success);
    }

    public async Task<DataResult<string>> GetBase64ImageAsync(int id)
    {
        var image = await _dbContext.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (image is null)
        {
            return new ErrorDataResult<string>(UiMessages.NotFoundData);
        }

        var imageData = $"data:image/jpg;base64,{Convert.ToBase64String(image.Data)}";

        return new SuccessDataResult<string>(imageData, UiMessages.Success);
    }
}