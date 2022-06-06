namespace Services;

public class UserService : BasicService, IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        RoleManager<Role> roleManager
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<DataResult<UserResponseDto>> GetAsync(int id)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (user is null)
        {
            return new ErrorDataResult<UserResponseDto>(UiMessages.UserNotFound);
        }

        var mappedData = _mapper.Map<UserResponseDto>(user);

        return new SuccessDataResult<UserResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> UpdateAsync(UserRequestDto requestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        _mapper.Map(requestDto, user);
        user.ModifyDate = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> AddToRoleAsync(int userId, int roleId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(userId));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role is null)
        {
            return new ErrorResult(UiMessages.RoleNotFound);
        }

        await _userManager.AddToRoleAsync(user, role.Name);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> DeleteFromRoleAsync(int userId, int roleId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(userId));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role is null)
        {
            return new ErrorResult(UiMessages.RoleNotFound);
        }

        await _userManager.RemoveFromRoleAsync(user, role.Name);

        return new SuccessResult(UiMessages.Success);
    }
}