using Newtonsoft.Json;

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
        var adminSite = (await SeedAdminSite()).Data;
        await SeedRoles();
        await SeedAdminUser(adminSite);
        await SeedLoginComponent();


        return new SuccessResult(UiMessages.Success);
    }

    private async Task<DataResult<Site>> SeedAdminSite()
    {
        if (await _dbContext.Sites.AnyAsync())
        {
            return new ErrorDataResult<Site>(UiMessages.SeedDataError);
        }

        var adminSite = new Site
        {
            CreateDate = DateTime.Now,
            Code = "Admin",
            PhoneNumber = "Admin",
            Email = "Admin",
            Description = "Admin",
            Address = "Admin",
        };

        await _dbContext.Sites.AddAsync(adminSite);
        await _dbContext.SaveChangesAsync();

        return new SuccessDataResult<Site>(adminSite);
    }

    private async Task SeedRoles()
    {
        await _roleManager.CreateAsync(new Role {Name = UserRoles.Admin});
        await _roleManager.CreateAsync(new Role {Name = UserRoles.BusinessOwner});
        await _roleManager.CreateAsync(new Role {Name = UserRoles.Employee});
        await _roleManager.CreateAsync(new Role {Name = UserRoles.Customer});
    }

    private async Task SeedAdminUser(Site adminSite)
    {
        var adminUserList = new List<User>
        {
            new()
            {
                UserName = adminSite.Id + "_" + "admin@admin.com",
                Email = "admin@admin.com",
                PhoneNumber = "0000000000",
                FirstName = "Admin",
                LastName = "Admin",
                SiteId = adminSite.Id,
                CreateDate = DateTime.Now
            },
            new()
            {
                UserName = adminSite.Id + "_" + "onur@admin.com",
                Email = "onur@admin.com",
                PhoneNumber = "0000000000",
                FirstName = "Onur",
                LastName = "Akalın",
                SiteId = adminSite.Id,
                CreateDate = DateTime.Now
            },
            new()
            {
                UserName = adminSite.Id + "_" + "arif@admin.com",
                Email = "arif@admin.com",
                PhoneNumber = "0000000000",
                FirstName = "Ahmet Arif",
                LastName = "Özçelik",
                SiteId = adminSite.Id,
                CreateDate = DateTime.Now
            }
        };

        foreach (var adminUser in adminUserList)
        {
            await _userManager.CreateAsync(adminUser, "qwe123");
            await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
        }
        
    }

    private async Task SeedLoginComponent()
    {
        var componentList = new List<LoginComponentDto>
        {
            new()
            {
                Id = "1",
                Name = "Login Container",
                BackgroundColor = "",
                Font = "",
                Margin = "",
                Padding = "",
                Width = "",
                Height = "",
                BackgroundImage = "",
                Color = "",
                BorderColor = ""
            },
            new()
            {
                Id = "2",
                Name = "Login Button",
                BackgroundColor = "",
                Font = "",
                Margin = "",
                Padding = "",
                Width = "",
                Height = "",
                BackgroundImage = "",
                Color = "",
                BorderColor = ""
            },
            new()
            {
                Id = "3",
                Name = "Login Forms",
                BackgroundColor = "",
                Font = "",
                Margin = "",
                Padding = "",
                Width = "",
                Height = "",
                BackgroundImage = "",
                Color = "",
                BorderColor = ""
            }
        };

        var loginComponent = new Component
        {
            Type = ComponentType.Login,
            Value = JsonConvert.SerializeObject(componentList)
        };

        await _dbContext.Components.AddAsync(loginComponent);
        await _dbContext.SaveChangesAsync();
    }
}