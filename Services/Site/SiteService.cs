namespace Services;

public class SiteService : BasicService, ISiteService
{
    private readonly IImageService _imageService;

    public SiteService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IImageService imageService
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _imageService = imageService;
    }

    #region Site

    public async Task<Result> InsertSiteAsync(SiteRequestDto requestDto)
    {
        var checkCodeExists = await _dbContext.Sites
            .AnyAsync(x => x.Code.Equals(requestDto.Code));

        if (checkCodeExists)
        {
            return new ErrorResult(UiMessages.SiteCodeAlreadyExists);
        }

        var site = _mapper.Map<Site>(requestDto);
        site.CreateDate = DateTime.Now;
        site.CreateUser = _currentUserId;

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> UpdateSiteAsync(SiteRequestDto requestDto)
    {
        var checkCodeExists = await _dbContext.Sites
            .AnyAsync(x => x.Code.Equals(requestDto.Code)
                           && !x.Id.Equals(requestDto.Id));

        if (checkCodeExists)
        {
            return new ErrorResult(UiMessages.SiteCodeAlreadyExists);
        }

        var site = await _dbContext.Sites
            .FirstOrDefaultAsync(x => x.Id == requestDto.Id);

        if (site is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, site);
        site.ModifyDate = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<SiteResponseDto>>> ListSiteAsync()
    {
        var sites = await _dbContext.Sites
            .AsNoTracking()
            .Include(x => x.SiteImages).ThenInclude(x => x.Image)
            .ToListAsync();

        var mappedData = _mapper.Map<List<SiteResponseDto>>(sites);

        return new SuccessDataResult<List<SiteResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<SiteResponseDto>> GetSiteAsync(int id)
    {
        var site = await _dbContext.Sites
            .AsNoTracking()
            .Include(x => x.SiteImages).ThenInclude(x => x.Image)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (site is null)
        {
            return new ErrorDataResult<SiteResponseDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<SiteResponseDto>(site);

        return new SuccessDataResult<SiteResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteSiteAsync(int id)
    {
        var site = await _dbContext.Sites
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (site is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _dbContext.Remove(site);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> UploadSiteImageAsync(ImageRequestDto requestDto)
    {
        var result = await _imageService.UploadToFileAsync(requestDto.Image);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        var siteImage = new SiteImage
        {
            SiteId = _currentSiteId,
            ImageId = result.Data
        };

        await _dbContext.SiteImages.AddAsync(siteImage);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    #endregion

    #region Site Off-Time

    public async Task<Result> InsertSiteOffTimeAsync(SiteOffTimeRequestDto requestDto)
    {
        var siteOffTime = _mapper.Map<SiteOffTime>(requestDto);
        siteOffTime.SiteId = _currentSiteId;

        await _dbContext.SiteOfTimes.AddAsync(siteOffTime);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> UpdateSiteOffTimeAsync(SiteOffTimeRequestDto requestDto)
    {
        var siteOffTime = await _dbContext.SiteOfTimes
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (siteOffTime is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, siteOffTime);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<SiteOffTimeResponseDto>>> ListSiteOffTimeAsync()
    {
        var siteOffTimes = await _dbContext.SiteOfTimes
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var mappedData = _mapper.Map<List<SiteOffTimeResponseDto>>(siteOffTimes);

        return new SuccessDataResult<List<SiteOffTimeResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<SiteOffTimeResponseDto>> GetSiteOffTimeAsync(int id)
    {
        var siteOffTime = await _dbContext.SiteOfTimes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (siteOffTime is null)
        {
            return new ErrorDataResult<SiteOffTimeResponseDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<SiteOffTimeResponseDto>(siteOffTime);

        return new SuccessDataResult<SiteOffTimeResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteSiteOffTimeAsync(int id)
    {
        var siteOffTime = await _dbContext.SiteOfTimes
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (siteOffTime is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _dbContext.Remove(siteOffTime);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    #endregion

    #region Site Service

    public async Task<Result> InsertSiteServiceAsync(SiteServiceRequestDto requestDto)
    {
        var siteService = _mapper.Map<Domain.Entities.SiteService>(requestDto);
        siteService.SiteId = _currentSiteId;
        siteService.CreateDate = DateTime.Now;
        siteService.CreateUser = _currentUserId;

        await _dbContext.SiteServices.AddAsync(siteService);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> UpdateSiteServiceAsync(SiteServiceRequestDto requestDto)
    {
        var siteService = await _dbContext.SiteServices
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (siteService is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, siteService);
        siteService.ModifyDate = DateTime.Now;
        siteService.ModifyUser = _currentUserId;

        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<SiteServiceResponseDto>>> ListSiteServiceAsync()
    {
        var siteServices = await _dbContext.SiteServices
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var mappedData = _mapper.Map<List<SiteServiceResponseDto>>(siteServices);

        return new SuccessDataResult<List<SiteServiceResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<SiteServiceResponseDto>> GetSiteServiceAsync(int id)
    {
        var siteService = await _dbContext.SiteServices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (siteService is null)
        {
            return new ErrorDataResult<SiteServiceResponseDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<SiteServiceResponseDto>(siteService);

        return new SuccessDataResult<SiteServiceResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteSiteServiceAsync(int id)
    {
        var siteService = await _dbContext.SiteServices
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (siteService is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _dbContext.SiteServices.Remove(siteService);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    #endregion

    #region Site Service Day

    public async Task<Result> InsertSiteServiceDayAsync(SiteServiceDayRequestDto requestDto)
    {
        var siteServiceDays = _mapper.Map<SiteServiceDay>(requestDto);

        await _dbContext.SiteServiceDays.AddAsync(siteServiceDays);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> UpdateSiteServiceDayAsync(SiteServiceDayRequestDto requestDto)
    {
        var siteServiceDay = await _dbContext.SiteServiceDays
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (siteServiceDay is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, siteServiceDay);

        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<SiteServiceDayResponseDto>>> ListSiteServiceDayAsync()
    {
        var siteServiceDays = await _dbContext.SiteServiceDays
            .AsNoTracking()
            .Include(x => x.SiteService)
            .Where(x => x.SiteService.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var mappedData = _mapper.Map<List<SiteServiceDayResponseDto>>(siteServiceDays);

        return new SuccessDataResult<List<SiteServiceDayResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<SiteServiceDayResponseDto>> GetSiteServiceDayAsync(int id)
    {
        var siteServiceDay = await _dbContext.SiteServiceDays
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (siteServiceDay is null)
        {
            return new ErrorDataResult<SiteServiceDayResponseDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<SiteServiceDayResponseDto>(siteServiceDay);

        return new SuccessDataResult<SiteServiceDayResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteSiteServiceDayAsync(int id)
    {
        var siteServiceDay = await _dbContext.SiteServiceDays
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (siteServiceDay is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _dbContext.SiteServiceDays.Remove(siteServiceDay);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    #endregion
}