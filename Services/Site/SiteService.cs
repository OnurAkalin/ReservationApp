namespace Services;

public class SiteService : BasicService, ISiteService
{
    public SiteService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
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
            .ToListAsync();

        var mappedData = _mapper.Map<List<SiteResponseDto>>(sites);

        return new SuccessDataResult<List<SiteResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<SiteResponseDto>> GetSiteAsync(int id)
    {
        var site = await _dbContext.Sites
            .AsNoTracking()
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

    #endregion

    #region Site Service

    public async Task<Result> InsertSiteServiceAsync(SiteServiceRequestDto requestDto)
    {
        var siteService = _mapper.Map<Domain.Entities.SiteService>(requestDto);
        siteService.SiteId = _currentSiteId;
        siteService.CreateDate = DateTime.Now;
        siteService.CreateUser = _currentUserId;

        await _dbContext.SiteServices.AddAsync(siteService);
        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.Error);
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

        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.Error);
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
    
}