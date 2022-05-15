using Domain.Constants;

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
        UserManager<User> userManager,
        RoleManager<Role> roleManager
    )
        : base(logger, mapper, dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<DataResult<UserDto>> GetUserAsync(int id)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (user is null)
        {
            return new ErrorDataResult<UserDto>(UiMessages.UserNotFound);
        }

        var mappedData = _mapper.Map<UserDto>(user);

        return new SuccessDataResult<UserDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> UpdateUserAsync(UserDto requestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        _mapper.Map(requestDto, user);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> DeleteUserAsync(int id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        // Delete action

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