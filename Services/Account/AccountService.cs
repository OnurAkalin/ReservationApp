using Core.Jwt;
using Core.Utilities.Results;
using Domain.Enumerations;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;

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
        ApplicationDbContext dbContext, ITokenService tokenService)
        : base(logger, mapper, dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }


    public async Task<Result> RegisterAsync(RegisterRequestDto requestDto)
    {
        var user = new User
        {
            Id = default,
            UserName = requestDto.Email + "_00000000-0000-0000-0000-000000000000",
            PhoneNumber = requestDto.PhoneNumber,
            FirstName = requestDto.Name,
            LastName = requestDto.Surname,
            Email = requestDto.Email
        };

        var createUserResult = await _userManager.CreateAsync(user, requestDto.Password);

        if (createUserResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
        }

        //await _dbContext.SaveChangesAsync();

        return new SuccessResult("İşlem başarılı");
    }

    public async Task<DataResult<AccessToken>> LoginAsync(LoginRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Email) || string.IsNullOrWhiteSpace(requestDto.Password))
        {
            return new ErrorDataResult<AccessToken>("Boş istek");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == requestDto.Email);

        if (user == null)
        {
            return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı");
        }
        
        var passwordIsCorrect = await _signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);

        if (!passwordIsCorrect.Succeeded)
        {
            return new ErrorDataResult<AccessToken>("Şifre hatalı.");
        }

        var token = await _tokenService.GenerateToken(user);

        return new SuccessDataResult<AccessToken>(token);
    }
    
    public async Task<Result> CreateRoleAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new Role {Name = roleName});

        if (result.Succeeded)
        {
            return new SuccessResult("İşlem başarılı");
        }

        return new ErrorResult("İşlem başarısız..");
    }
    
    public async Task<Result> UpdateRoleAsync(Guid id, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        
        if (role == null)
        {
            return new ErrorResult("Rol bulunamadı.");
        }

        role.Name = roleName;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return new SuccessResult("İşlem başarılı");
        }

        return new ErrorResult("İşlem başarısız..");
    }

    public async Task<DataResult<List<RoleResponseDto>>> GetRolesAsync()
    {
        var roles = await _dbContext.Roles.Select(x => new RoleResponseDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();

        var result = new SuccessDataResult<List<RoleResponseDto>>(roles);

        return result;
    }
}