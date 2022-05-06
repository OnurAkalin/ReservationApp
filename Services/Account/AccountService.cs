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
            return new ErrorResult(UIMessages.UserAlreadyExist);
        }

        var user = new User
        {
            UserName = userName,
            PhoneNumber = requestDto.PhoneNumber,
            FirstName = requestDto.Name,
            LastName = requestDto.Surname,
            Email = requestDto.Email
        };

        var createUserResult = await _userManager.CreateAsync(user, requestDto.Password);

        if (!createUserResult.Succeeded)
        {
            return new ErrorResult(UIMessages.InvalidCredentials);
        }

        return new SuccessResult(UIMessages.Success);
    }

    public async Task<DataResult<TokenResponseDto>> LoginAsync(LoginRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Email) || string.IsNullOrWhiteSpace(requestDto.Password))
        {
            return new ErrorDataResult<TokenResponseDto>(UIMessages.EmptyRequest);
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == requestDto.Email);

        if (user == null)
        {
            return new ErrorDataResult<TokenResponseDto>(UIMessages.UserNotFound);
        }

        var passwordIsCorrect = await _signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);

        if (!passwordIsCorrect.Succeeded)
        {
            return new ErrorDataResult<TokenResponseDto>(UIMessages.InvalidCredentials);
        }

        var token = await _tokenService.GenerateTokenAsync(user);

        return new SuccessDataResult<TokenResponseDto>(token, UIMessages.Authorized);
    }

    public async Task<Result> CreateRoleAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new Role {Name = roleName});

        if (result.Succeeded)
        {
            return new SuccessResult(UIMessages.Success);
        }

        return new ErrorResult(UIMessages.Fail);
    }

    public async Task<Result> UpdateRoleAsync(Guid id, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            return new ErrorResult(UIMessages.NotFoundData);
        }

        role.Name = roleName;

        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            return new ErrorResult(UIMessages.Fail);
        }

        return new SuccessResult(UIMessages.Success);
    }

    public async Task<DataResult<List<RoleResponseDto>>> GetRolesAsync()
    {
        var roles = await _dbContext.Roles.Select(x => new RoleResponseDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();

        var result = new SuccessDataResult<List<RoleResponseDto>>(roles, UIMessages.Success);

        return result;
    }
}