namespace Services;

public class AccountService : BasicService, IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Result> RegisterAsync(RegisterRequestDto requestDto)
    {
        var site = await _dbContext.Sites
            .FirstOrDefaultAsync(x => x.Id.Equals(_currentSiteId));

        if (site is null)
        {
            return new ErrorResult(UiMessages.UnselectedSite);
        }

        var userName = site.Id + "_" + requestDto.Email;

        var checkUserExist = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.UserName.Equals(userName));

        if (checkUserExist)
        {
            return new ErrorResult(UiMessages.UserAlreadyExist);
        }

        var user = _mapper.Map<User>(requestDto);
        user.SiteId = _currentSiteId;
        user.UserName = userName;
        user.CreateDate = DateTime.Now;

        var createUserResult = await _userManager.CreateAsync(user, requestDto.Password);

        if (!createUserResult.Succeeded)
        {
            return new ErrorResult(UiMessages.InvalidCredentials);
        }

        await _userManager.AddToRoleAsync(user, UserRoles.Customer);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<TokenResponseDto>> LoginAsync(LoginRequestDto requestDto)
    {
        var site = await _dbContext.Sites
            .FirstOrDefaultAsync(x => x.Id.Equals(_currentSiteId));

        if (site is null)
        {
            return new ErrorDataResult<TokenResponseDto>(UiMessages.UnselectedSite);
        }

        var userName = site.Id + "_" + requestDto.Email;

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

        if (user is null)
        {
            return new ErrorDataResult<TokenResponseDto>(UiMessages.UserNotFound);
        }

        var passwordIsCorrect = await _signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);

        if (!passwordIsCorrect.Succeeded)
        {
            return new ErrorDataResult<TokenResponseDto>(UiMessages.InvalidCredentials);
        }

        var token = await _tokenService.GenerateAsync(user);

        user.LastLoginDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        return new SuccessDataResult<TokenResponseDto>(token, UiMessages.Authorized);
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequestDto requestDto)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id)
                                      && x.SiteId.Equals(_currentSiteId));

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

    public async Task<Result> ChangeEmailAsync(ChangeEmailRequestDto requestDto)
    {
        var checkEmailExists = await _dbContext.Users
            .AnyAsync(x => x.Email.Equals(requestDto.Email)
                           && !x.Id.Equals(requestDto.Id)
                           && x.SiteId.Equals(_currentSiteId));

        if (checkEmailExists)
        {
            return new ErrorResult(UiMessages.UserWithEmailAlreadyExist);
        }

        var site = await _dbContext.Sites
            .FirstOrDefaultAsync(x => x.Id.Equals(_currentSiteId));

        if (site is null)
        {
            return new ErrorResult(UiMessages.UnselectedSite);
        }

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id)
                                      && x.SiteId.Equals(_currentSiteId));

        if (user is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        var userName = site.Id + "_" + requestDto.Email;

        await _userManager.SetEmailAsync(user, requestDto.Email);
        await _userManager.SetUserNameAsync(user, userName);

        return new SuccessResult(UiMessages.Success);
    }
}