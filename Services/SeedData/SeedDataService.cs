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

    public async Task SeedBaseData()
    {
        var result = await SeedAdminSite();
        if (!result.Success) return;
        await SeedRoles();
        await SeedAdminUser(result.Data);
        await SeedLoginComponent(result.Data);
    }

    private async Task<DataResult<int>> SeedAdminSite()
    {
        if (await _dbContext.Sites.AnyAsync())
        {
            return new ErrorDataResult<int>(UiMessages.SeedDataError);
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

        return new SuccessDataResult<int>(adminSite.Id);
    }

    private async Task SeedRoles()
    {
        await _roleManager.CreateAsync(new Role {Name = UserRoles.Admin});
        await _roleManager.CreateAsync(new Role {Name = UserRoles.BusinessOwner});
        await _roleManager.CreateAsync(new Role {Name = UserRoles.Employee});
        await _roleManager.CreateAsync(new Role {Name = UserRoles.Customer});
    }

    private async Task SeedAdminUser(int siteId)
    {
        var adminUserList = new List<User>
        {
            new()
            {
                UserName = siteId + "_" + "admin@admin.com",
                Email = "admin@admin.com",
                PhoneNumber = "0000000000",
                FirstName = "Admin",
                LastName = "Admin",
                SiteId = siteId,
                CreateDate = DateTime.Now
            },
            new()
            {
                UserName = siteId + "_" + "onur@admin.com",
                Email = "onur@admin.com",
                PhoneNumber = "0000000000",
                FirstName = "Onur",
                LastName = "Akalın",
                SiteId = siteId,
                CreateDate = DateTime.Now
            },
            new()
            {
                UserName = siteId + "_" + "arif@admin.com",
                Email = "arif@admin.com",
                PhoneNumber = "0000000000",
                FirstName = "Ahmet Arif",
                LastName = "Özçelik",
                SiteId = siteId,
                CreateDate = DateTime.Now
            }
        };

        foreach (var adminUser in adminUserList)
        {
            await _userManager.CreateAsync(adminUser, "qwe123");
            await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
        }
    }

    private async Task SeedLoginComponent(int siteId)
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
            Value = JsonConvert.SerializeObject(componentList),
            SiteId = siteId
        };

        await _dbContext.Components.AddAsync(loginComponent);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task SeedTestCompany1()
    {
        var site = new Site
            {
                CreateDate = DateTime.Now,
                Code = "XBERBER",
                PhoneNumber = "5301111111",
                Email = "berberx@gmail.com",
                Description = "X Berber Açıklama",
                Address = "İstanbul/Maltepe"
            };

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();
        
        await SeedAdminUser(site.Id);
        await SeedLoginComponent(site.Id);

        var businessOwner = new User
        {
            UserName = site.Id + "_" + "berberx@gmail.com",
            Email = "berberx@gmail.com",
            PhoneNumber = "5301111111",
            FirstName = "Berber X",
            LastName = "Business Owner",
            CreateDate = DateTime.Now,
            SiteId = site.Id,
        };

        await _userManager.CreateAsync(businessOwner, "123456");
        await _userManager.AddToRoleAsync(businessOwner, UserRoles.BusinessOwner);


        var siteServices = new List<Domain.Entities.SiteService>
        {
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Saç Kesimi",
                Description = "Her türlü saç kesim işlemi.",
                Duration = 30,
                BreakAfter = true,
                BreakAfterDuration = 10,
                Price = 25,
                Currency = Currency.Tl,
                Color = null
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Sakalı Kesimi",
                Description = "Her türlü sakal kesim işlemi.",
                Duration = 15,
                BreakAfter = false,
                BreakAfterDuration = null,
                Price = 20,
                Currency = Currency.Tl,
                Color = null
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Yüz Bakımı",
                Description = "Tüm yüz bakımı içerir",
                Duration = 45,
                BreakAfter = true,
                BreakAfterDuration = 30,
                Price = 50,
                Currency = Currency.Tl,
                Color = null
            }
        };

        await _dbContext.SiteServices.AddRangeAsync(siteServices);
        await _dbContext.SaveChangesAsync();
        
        
    }
}