using Domain.Constants;

namespace Services;

public class SeedDataService : ISeedDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public SeedDataService
    (
        ApplicationDbContext dbContext,
        UserManager<User> userManager,
        RoleManager<Role> roleManager
    )
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result> SeedAdminSiteAndUser()
    {
        if (await _dbContext.Sites.AnyAsync())
        {
            return new ErrorResult(UiMessages.Error);
        }

        var site = new Site
        {
            CreateDate = DateTime.Now,
            Code = "ADMIN",
            PhoneNumber = "123456789",
            Email = "Admin",
            Description = "Admin",
            Address = "Admin",

        };

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();

        var user = new User
        {
            UserName = site.Code + "_" + "admin@admin.com",
            Email = "admin@admin.com",
            PhoneNumber = "123456789",
            FirstName = "Admin",
            LastName = "Admin"
        };
        
        await _userManager.CreateAsync(user, "qwe123");


        await _roleManager.CreateAsync(new Role {Name = "Admin"});
        await _roleManager.CreateAsync(new Role {Name = "BusinessOwner"});
        await _roleManager.CreateAsync(new Role {Name = "Employee"});
        await _roleManager.CreateAsync(new Role {Name = "Customer"});

        await _userManager.AddToRoleAsync(user, "Admin");

        return new SuccessResult(UiMessages.Success);
    }
}