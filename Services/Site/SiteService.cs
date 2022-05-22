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

    public async Task<Result> InsertAsync(SiteRequestDto requestDto)
    {
        var checkCodeExists = await _dbContext.Sites
            .AnyAsync(x => x.Code.Equals(requestDto.Code));

        if (checkCodeExists)
        {
            return new ErrorResult(UiMessages.SiteCodeAlreadyExists);
        }

        var site = _mapper.Map<Site>(requestDto);
        site.CreateDate = DateTime.Now;

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> UpdateAsync(SiteRequestDto requestDto)
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

    public async Task<DataResult<List<SiteResponseDto>>> ListAsync()
    {
        var sites = await _dbContext.Sites
            .AsNoTracking()
            .ToListAsync();

        var mappedData = _mapper.Map<List<SiteResponseDto>>(sites);

        return new SuccessDataResult<List<SiteResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<SiteResponseDto>> GetAsync(int id)
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

    public async Task<Result> DeleteAsync(int id)
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
}