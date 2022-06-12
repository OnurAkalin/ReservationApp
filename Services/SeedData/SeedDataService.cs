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
        await SeedTestCompany1();
        await SeedTestCompany2();
        await SeedTestCompany3();
        await SeedTestCompany4();
        
        Console.WriteLine("Seed ended... Date:" + DateTime.Now);
    }

    private async Task<DataResult<int>> SeedAdminSite()
    {
        if (await _dbContext.Sites.AnyAsync())
        {
            return new ErrorDataResult<int>();
        }
        
        Console.WriteLine("Seed started... Date: " + DateTime.Now);

        var adminSite = new Site
        {
            CreateDate = DateTime.Now,
            Code = "Admin",
            PhoneNumber = "Admin",
            Email = "Admin",
            Description = "Admin",
            Address = "Admin"
        };

        await _dbContext.Sites.AddAsync(adminSite);
        await _dbContext.SaveChangesAsync();
        
        var image = new Image
        {
            Title = "admin.jpg",
            Path = "/Images/admin.jpg"
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        await _dbContext.SiteImages.AddAsync(new SiteImage
        {
            SiteId = adminSite.Id,
            ImageId = image.Id
        });
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

        var adminUser = new User
        {
            UserName = siteId + "_" + "admin@admin.com",
            Email = "admin@admin.com",
            PhoneNumber = "0000000000",
            FirstName = "Admin",
            LastName = "Admin",
            SiteId = siteId,
            Gender = Gender.Male,
            CreateDate = DateTime.Now
        };
        
        await _userManager.CreateAsync(adminUser, "qwe123");
        await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
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

    private async Task SeedTestCustomers(int siteId)
    {
        var customers = new List<User>
        {
            new()
            {
                UserName = siteId + "_" + "customer1@gmail.com",
                Email = "customer1@gmail.com",
                PhoneNumber = "0000000001",
                FirstName = "Customer 1",
                LastName = "Test",
                CreateDate = DateTime.Now,
                Gender = Gender.Male,
                SiteId = siteId
            },
            new()
            {
                UserName = siteId + "_" + "customer2@gmail.com",
                Email = "customer2@gmail.com",
                PhoneNumber = "0000000002",
                FirstName = "Customer 2",
                LastName = "Test",
                CreateDate = DateTime.Now,
                Gender = Gender.Female,
                SiteId = siteId
            },
            new()
            {
                UserName = siteId + "_" + "customer3@gmail.com",
                Email = "customer3@gmail.com",
                PhoneNumber = "0000000003",
                FirstName = "Customer 3",
                LastName = "Test",
                CreateDate = DateTime.Now,
                Gender = Gender.Male,
                SiteId = siteId
            },
            new()
            {
                UserName = siteId + "_" + "customer4@gmail.com",
                Email = "customer4@gmail.com",
                PhoneNumber = "0000000004",
                FirstName = "Customer 4",
                LastName = "Test",
                CreateDate = DateTime.Now,
                Gender = Gender.Female,
                SiteId = siteId
            },
            new()
            {
                UserName = siteId + "_" + "customer5@gmail.com",
                Email = "customer5@gmail.com",
                PhoneNumber = "0000000005",
                FirstName = "Customer 5",
                LastName = "Test",
                CreateDate = DateTime.Now,
                Gender = Gender.Male,
                SiteId = siteId
            },
            new()
            {
                UserName = siteId + "_" + "customer6@gmail.com",
                Email = "customer6@gmail.com",
                PhoneNumber = "0000000006",
                FirstName = "Customer 6",
                LastName = "Test",
                CreateDate = DateTime.Now,
                Gender = Gender.Female,
                SiteId = siteId
            }
        };

        foreach (var customer in customers)
        {
            await _userManager.CreateAsync(customer, "123456");
            await _userManager.AddToRoleAsync(customer, UserRoles.Customer);
        }
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

        var image = new Image
        {
            Title = "1.jpg",
            Path = "/Images/1.jpg"
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        await _dbContext.SiteImages.AddAsync(new SiteImage
        {
            SiteId = site.Id,
            ImageId = image.Id
        });
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
            Gender = Gender.Male,
            SiteId = site.Id
        };

        await _userManager.CreateAsync(businessOwner, "123456");
        await _userManager.AddToRoleAsync(businessOwner, UserRoles.BusinessOwner);

        await SeedTestCustomers(site.Id);
        
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
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Sakal Kesimi",
                Description = "Her türlü sakal kesim işlemi.",
                Duration = 15,
                BreakAfter = false,
                Price = 20,
                Currency = Currency.Tl,
                Color = ""
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
                Color = ""
            }
        };

        await _dbContext.SiteServices.AddRangeAsync(siteServices);
        await _dbContext.SaveChangesAsync();

        foreach (var siteService in siteServices)
        {
            var siteServiceDays = new List<SiteServiceDay>
            {
                new()
                {
                    Day = DayOfWeek.Monday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Tuesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Wednesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Thursday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Friday,
                    SiteServiceId = siteService.Id,
                }
            };

            await _dbContext.SiteServiceDays.AddRangeAsync(siteServiceDays);
        }

        await _dbContext.SaveChangesAsync();

        var siteOffTimes = new List<SiteOffTime>
        {
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Saturday,
                IsFullDay = true,
            },
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Sunday,
                IsFullDay = true,
            }
        };

        await _dbContext.SiteOfTimes.AddRangeAsync(siteOffTimes);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task SeedTestCompany2()
    {
        var site = new Site
            {
                CreateDate = DateTime.Now,
                Code = "YBERBER",
                PhoneNumber = "5302222222",
                Email = "berbery@gmail.com",
                Description = "Y Berber Açıklama",
                Address = "İstanbul/Kadıköy"
            };

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();
        
        var image = new Image
        {
            Title = "2.jpg",
            Path = "/Images/2.jpg"
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        await _dbContext.SiteImages.AddAsync(new SiteImage
        {
            SiteId = site.Id,
            ImageId = image.Id
        });
        await _dbContext.SaveChangesAsync();

        await SeedAdminUser(site.Id);
        await SeedLoginComponent(site.Id);

        var businessOwner = new User
        {
            UserName = site.Id + "_" + "berbery@gmail.com",
            Email = "berbery@gmail.com",
            PhoneNumber = "5302222222",
            FirstName = "Berber Y",
            LastName = "Business Owner",
            CreateDate = DateTime.Now,
            Gender = Gender.Male,
            SiteId = site.Id
        };

        await _userManager.CreateAsync(businessOwner, "123456");
        await _userManager.AddToRoleAsync(businessOwner, UserRoles.BusinessOwner);

        await SeedTestCustomers(site.Id);

        var siteServices = new List<Domain.Entities.SiteService>
        {
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Saç Kesimi",
                Description = "",
                Duration = 40,
                BreakAfter = true,
                BreakAfterDuration = 15,
                Price = 30,
                Currency = Currency.Tl,
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Sakal Kesimi",
                Description = "",
                Duration = 20,
                BreakAfter = false,
                Price = 30,
                Currency = Currency.Tl,
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Yüz Bakımı",
                Description = "",
                Duration = 50,
                BreakAfter = true,
                BreakAfterDuration = 35,
                Price = 70,
                Currency = Currency.Tl,
                Color = ""
            }
        };

        await _dbContext.SiteServices.AddRangeAsync(siteServices);
        await _dbContext.SaveChangesAsync();

        foreach (var siteService in siteServices)
        {
            var siteServiceDays = new List<SiteServiceDay>
            {
                new()
                {
                    Day = DayOfWeek.Monday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Tuesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Wednesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Thursday,
                    SiteServiceId = siteService.Id,
                }
            };

            await _dbContext.SiteServiceDays.AddRangeAsync(siteServiceDays);
        }

        await _dbContext.SaveChangesAsync();

        var siteOffTimes = new List<SiteOffTime>
        {
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Friday,
                IsFullDay = true,
            },
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Saturday,
                IsFullDay = true,
            },
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Sunday,
                IsFullDay = true,
            }
        };

        await _dbContext.SiteOfTimes.AddRangeAsync(siteOffTimes);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task SeedTestCompany3()
    {
        var site = new Site
            {
                CreateDate = DateTime.Now,
                Code = "XDISCI",
                PhoneNumber = "5303333333",
                Email = "discix@gmail.com",
                Description = "X Dişçi Açıklama",
                Address = "İstanbul/Maltepe"
            };

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();
        
        var image = new Image
        {
            Title = "3.jpg",
            Path = "/Images/3.jpg"
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        await _dbContext.SiteImages.AddAsync(new SiteImage
        {
            SiteId = site.Id,
            ImageId = image.Id
        });
        await _dbContext.SaveChangesAsync();

        await SeedAdminUser(site.Id);
        await SeedLoginComponent(site.Id);

        var businessOwner = new User
        {
            UserName = site.Id + "_" + "discix@gmail.com",
            Email = "discix@gmail.com",
            PhoneNumber = "5303333333",
            FirstName = "Dişçi X",
            LastName = "Business Owner",
            CreateDate = DateTime.Now,
            Gender = Gender.Male,
            SiteId = site.Id
        };

        await _userManager.CreateAsync(businessOwner, "123456");
        await _userManager.AddToRoleAsync(businessOwner, UserRoles.BusinessOwner);

        await SeedTestCustomers(site.Id);

        var siteServices = new List<Domain.Entities.SiteService>
        {
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Diş Çekimi",
                Description = "",
                Duration = 120,
                BreakAfter = true,
                BreakAfterDuration = 15,
                Price = 500,
                Currency = Currency.Tl,
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Kanal Tedavisi",
                Description = "",
                Duration = 150,
                BreakAfter = true,
                BreakAfterDuration = 30,
                Price = 750,
                Currency = Currency.Tl,
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "İmplant",
                Description = "",
                Duration = 180,
                BreakAfter = true,
                BreakAfterDuration = 45,
                Price = 1000,
                Currency = Currency.Tl,
                Color = ""
            }
        };

        await _dbContext.SiteServices.AddRangeAsync(siteServices);
        await _dbContext.SaveChangesAsync();

        foreach (var siteService in siteServices)
        {
            var siteServiceDays = new List<SiteServiceDay>
            {
                new()
                {
                    Day = DayOfWeek.Monday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Tuesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Wednesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Thursday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Friday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Saturday,
                    SiteServiceId = siteService.Id,
                }
            };

            await _dbContext.SiteServiceDays.AddRangeAsync(siteServiceDays);
        }

        await _dbContext.SaveChangesAsync();

        var siteOffTimes = new List<SiteOffTime>
        {
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Sunday,
                IsFullDay = true,
            }
        };

        await _dbContext.SiteOfTimes.AddRangeAsync(siteOffTimes);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task SeedTestCompany4()
    {
        var site = new Site
            {
                CreateDate = DateTime.Now,
                Code = "YDISCI",
                PhoneNumber = "5304444444",
                Email = "disciy@gmail.com",
                Description = "Y Dişçi Açıklama",
                Address = "İstanbul/Kadıköy"
            };

        await _dbContext.Sites.AddAsync(site);
        await _dbContext.SaveChangesAsync();
        
        var image = new Image
        {
            Title = "4.jpg",
            Path = "/Images/4.jpg"
        };

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        await _dbContext.SiteImages.AddAsync(new SiteImage
        {
            SiteId = site.Id,
            ImageId = image.Id
        });
        await _dbContext.SaveChangesAsync();

        await SeedAdminUser(site.Id);
        await SeedLoginComponent(site.Id);

        var businessOwner = new User
        {
            UserName = site.Id + "_" + "disciy@gmail.com",
            Email = "disciy@gmail.com",
            PhoneNumber = "5304444444",
            FirstName = "Dişçi Y",
            LastName = "Business Owner",
            CreateDate = DateTime.Now,
            Gender = Gender.Male,
            SiteId = site.Id
        };

        await _userManager.CreateAsync(businessOwner, "123456");
        await _userManager.AddToRoleAsync(businessOwner, UserRoles.BusinessOwner);

        await SeedTestCustomers(site.Id);

        var siteServices = new List<Domain.Entities.SiteService>
        {
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Diş Çekimi",
                Description = "",
                Duration = 150,
                BreakAfter = true,
                BreakAfterDuration = 20,
                Price = 750,
                Currency = Currency.Tl,
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "Kanal Tedavisi",
                Description = "",
                Duration = 180,
                BreakAfter = true,
                BreakAfterDuration = 45,
                Price = 1200,
                Currency = Currency.Tl,
                Color = ""
            },
            new()
            {
                SiteId = site.Id,
                CreateDate = DateTime.Now,
                Name = "İmplant",
                Description = "",
                Duration = 200,
                BreakAfter = true,
                BreakAfterDuration = 30,
                Price = 1500,
                Currency = Currency.Tl,
                Color = ""
            }
        };

        await _dbContext.SiteServices.AddRangeAsync(siteServices);
        await _dbContext.SaveChangesAsync();

        foreach (var siteService in siteServices)
        {
            var siteServiceDays = new List<SiteServiceDay>
            {
                new()
                {
                    Day = DayOfWeek.Monday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Tuesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Wednesday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Thursday,
                    SiteServiceId = siteService.Id,
                },
                new()
                {
                    Day = DayOfWeek.Friday,
                    SiteServiceId = siteService.Id,
                }
            };

            await _dbContext.SiteServiceDays.AddRangeAsync(siteServiceDays);
        }

        await _dbContext.SaveChangesAsync();

        var siteOffTimes = new List<SiteOffTime>
        {
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Saturday,
                IsFullDay = true,
            },
            new()
            {
                SiteId = site.Id,
                Day = DayOfWeek.Sunday,
                IsFullDay = true,
            }
        };

        await _dbContext.SiteOfTimes.AddRangeAsync(siteOffTimes);
        await _dbContext.SaveChangesAsync();
    }
}