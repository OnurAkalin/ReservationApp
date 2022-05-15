using Domain.Constants;

namespace Services;

public class AccountService : BasicService, IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ITokenService _tokenService;

    public AccountService
    (
        Logger logger,
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<Role> roleManager,
        ApplicationDbContext dbContext,
        ITokenService tokenService
    )
        : base(logger, mapper, dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<Result> RegisterAsync(RegisterRequestDto requestDto)
    {
        var tempSiteId = Guid.Empty;
        var userName = tempSiteId + "_" + requestDto.Email;

        var checkUserExist = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x =>
                x.Email == requestDto.Email
                || x.UserName == userName);

        if (checkUserExist)
        {
            return new ErrorResult(UiMessages.UserAlreadyExist);
        }

        var user = _mapper.Map<User>(requestDto);
        user.UserName = userName;

        var createUserResult = await _userManager.CreateAsync(user, requestDto.Password);

        if (!createUserResult.Succeeded)
        {
            return new ErrorResult(UiMessages.InvalidCredentials);
        }

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<TokenResponseDto>> LoginAsync(LoginRequestDto requestDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == requestDto.Email);

        if (user is null)
        {
            return new ErrorDataResult<TokenResponseDto>(UiMessages.UserNotFound);
        }

        var passwordIsCorrect = await _signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);

        if (!passwordIsCorrect.Succeeded)
        {
            return new ErrorDataResult<TokenResponseDto>(UiMessages.InvalidCredentials);
        }

        var token = await _tokenService.GenerateTokenAsync(user);

        return new SuccessDataResult<TokenResponseDto>(token, UiMessages.Authorized);
    }

    public async Task<Result> ChangePassword(ChangePasswordRequestDto requestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == requestDto.Id);

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        var result = await _userManager.ChangePasswordAsync(user, requestDto.OldPassword, requestDto.NewPassword);

        if (!result.Succeeded)
        {
            return new ErrorResult(UiMessages.InvalidPassword);
        }

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<Result> ChangeEmail(ChangeEmailRequestDto requestDto)
    {
        var checkEmailExists = await _dbContext.Users
            .AnyAsync(x => x.Email.Equals(requestDto.Email)
                           && !x.Id.Equals(requestDto.Id));

        if (checkEmailExists)
        {
            return new ErrorResult(UiMessages.UserWithEmailAlreadyExist);
        }

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        await _userManager.SetEmailAsync(user, requestDto.Email);
        var userName = Guid.Empty + "_" + requestDto.Email;
        await _userManager.SetUserNameAsync(user, userName);

        return new SuccessResult(UiMessages.Success);
    }
}