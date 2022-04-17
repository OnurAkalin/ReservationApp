using Domain.Entities;

namespace Infrastructure.SeedData;

public static partial class ApplicationDbInitializer
{
    private static void SeedAdminSite(ApplicationDbContext dbContext)
    {
        if (dbContext.Sites.Any())
        {
            return;
        }
        
        var adminSite = new Site
        {
            Code = "ADMIN",
            CreateDate = DateTime.Now
        };

        dbContext.Sites.Add(adminSite);
        dbContext.SaveChanges();
    }
    
    private static void SeedTestSites(ApplicationDbContext dbContext)
    {
        if (dbContext.Sites.Any()) // Another site control. (Admin)
        {
            return;
        }

        var siteList = new List<Site>
        {
            new()
            {
                CreateDate = DateTime.Now,
                Code = "A-BRB",
                PhoneNumber = "5051111111",
                Email = "berbera@gmail.com",
                Description = "A Berber",
                Address = "Kartal İstanbul"
            },
            new()
            {
                CreateDate = DateTime.Now,
                Code = "B-BRB",
                PhoneNumber = "5052222222",
                Email = "berberb@gmail.com",
                Description = "B Berber",
                Address = "Maltepe İstanbul"
            },
            new()
            {
                CreateDate = DateTime.Now,
                Code = "C-BRB",
                PhoneNumber = "5053333333",
                Email = "berberc@gmail.com",
                Description = "C Berber",
                Address = "Bostancı İstanbul"
            }
        };
        

        dbContext.Sites.AddRange(siteList);
        dbContext.SaveChanges();
    }
}