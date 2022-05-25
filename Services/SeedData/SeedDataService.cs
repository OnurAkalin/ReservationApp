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

    public async Task<Result> SeedBaseData()
    {
        if (await _dbContext.Sites.AnyAsync())
        {
            return new ErrorResult(UiMessages.Error);
        }

        var site = new Site
        {
            CreateDate = DateTime.Now,
            Code = "ADMIN",
            PhoneNumber = "ADMIN",
            Email = "ADMIN",
            Description = "ADMIN",
            Address = "ADMIN",
        };

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();

        var siteService = new Domain.Entities.SiteService
        {
            SiteId = site.Id,
            CreateDate = DateTime.Now,
            Name = "Test Hizmeti",
            Description = "Test Hizmeti Açıklama",
            Duration = 30,
            BreakAfter = true,
            BreakAfterDuration = 10,
            Price = 10
        };
        
        await _dbContext.SiteServices.AddAsync(siteService);
        await _dbContext.SaveChangesAsync();

        var user = new User
        {
            UserName = site.Code + "_" + "admin@admin.com",
            Email = "admin@admin.com",
            PhoneNumber = "0000000000",
            FirstName = "Admin",
            LastName = "Admin",
            SiteId = site.Id,
            RegisterDate = DateTime.Now,
            CreateDate = DateTime.Now
        };

        await _roleManager.CreateAsync(new Role {Name = UserRole.Admin.ToString()});
        await _roleManager.CreateAsync(new Role {Name = UserRole.BusinessOwner.ToString()});
        await _roleManager.CreateAsync(new Role {Name = UserRole.Employee.ToString()});
        await _roleManager.CreateAsync(new Role {Name = UserRole.Customer.ToString()});

        await _userManager.CreateAsync(user, "qwe123");
        await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

        return new SuccessResult(UiMessages.Success);
    }
}