using Domain.Constants;

namespace Services;

public class RoleService : BasicService, IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService
    (
        Logger logger,
        IMapper mapper,
        RoleManager<Role> roleManager,
        ApplicationDbContext dbContext
    )
        : base(logger, mapper, dbContext)
    {
        _roleManager = roleManager;
    }

    public async Task<Result> CreateRoleAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new Role {Name = roleName});

        if (result.Succeeded)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.UnknownError);
    }

    public async Task<Result> UpdateRoleAsync(int id, string roleName)
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

    public async Task<DataResult<List<RoleResponseDto>>> GetRolesAsync()
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
}