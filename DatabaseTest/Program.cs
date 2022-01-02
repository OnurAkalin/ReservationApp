using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enumerations;

namespace DatabaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initial DB Create For Admin Users
            SeedAdminSiteAndUsers();
        }

        private static void SeedAdminSiteAndUsers()
        {
            using var dbContext = new ApplicationDbContext();

            if (dbContext.ReservationSites.Any()) return;

            var adminSite = new ReservationSite
            {
                Code = "ADMIN",
                CreateDate = DateTime.Now
            };

            dbContext.ReservationSites.Add(adminSite);
            dbContext.SaveChanges();

            var userList = new List<User>
            {
                new()
                {
                    SiteId = adminSite.Id,
                    CreateDate = DateTime.Now,
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
                    SiteId = adminSite.Id,
                    CreateDate = DateTime.Now,
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

            dbContext.AddRange(userList);
            dbContext.SaveChanges();
        }
    }
}