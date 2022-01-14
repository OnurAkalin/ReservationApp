using Domain.Entities;
using Domain.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData;

public static partial class ApplicationDbInitializer
{
    private static void SeedAdminUsers(ApplicationDbContext dbContext)
    {
        var adminSiteId = dbContext.Sites
            .AsNoTracking()
            .Where(x => x.Code == "ADMIN")
            .Select(x => x.Id)
            .FirstOrDefault();

        var userList = new List<User>
        {
            new()
            {
                SiteId = adminSiteId,
                CreateDate = DateTime.Now,
                UserName = $"{adminSiteId}_5072128027",
                Name = "Onur",
                Surname = "Akalın",
                PhoneNumber = "5072128027",
                Email = "onur@gmail.com",
                PasswordHash = "11223344",
                UserRoles = new List<UserRole>
                {
                    new()
                    {
                        Role = Role.Admin
                    }
                }
            },
            new()
            {
                SiteId = adminSiteId,
                CreateDate = DateTime.Now,
                UserName = $"{adminSiteId}_123456789",
                Name = "Ahmet Arif",
                Surname = "Özçelik",
                PhoneNumber = "123456789",
                Email = "arif@gmail.com",
                PasswordHash = "11223344",
                UserRoles = new List<UserRole>
                {
                    new()
                    {
                        Role = Role.Admin
                    }
                }
            }
        };

        dbContext.Users.AddRange(userList);
        dbContext.SaveChanges();
    }
}