using Domain.Constants;

namespace Services;

public class UserService : BasicService , IUserService
{
    private readonly UserManager<User> _userManager;
    
    protected UserService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        UserManager<User> userManager
    )
        : base(logger, mapper, dbContext)
    {
        _userManager = userManager;
    }

    public async Task<Result> UpdateUser(UserDto requestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => 
                x.Id == requestDto.Id);

        if (user == null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        _mapper.Map(requestDto, user);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> DeleteUser(Guid id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }
        
        // Delete action

        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }
}