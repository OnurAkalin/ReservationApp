namespace Services;

public class RoleService : BasicService, IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        RoleManager<Role> roleManager
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _roleManager = roleManager;
    }

    public async Task<Result> CreateAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new Role {Name = roleName});

        if (result.Succeeded)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.UnknownError);
    }

    public async Task<Result> UpdateAsync(int id, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        role.Name = roleName;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.UnknownError);
    }

    public async Task<DataResult<List<RoleResponseDto>>> ListAsync()
    {
        var roles = await _dbContext.Roles
            .AsNoTracking()
            .ToListAsync();

        if (!roles.Any())
        {
            return new ErrorDataResult<List<RoleResponseDto>>(message: UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<List<RoleResponseDto>>(roles);

        return new SuccessDataResult<List<RoleResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var role = await _dbContext.Roles
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (role is null)
        {
            return new ErrorResult(UiMessages.Error);
        }

        await _roleManager.DeleteAsync(role);

        return new SuccessResult(UiMessages.Success);
    }
}