using SiteService = Services.SiteService;

namespace API.AppServices;

public static class ServiceRegistration
{
    public static void InjectApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        serviceCollection.AddScoped<ISeedDataService, SeedDataService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
        serviceCollection.AddScoped<ISiteService, SiteService>();
        serviceCollection.AddScoped<IComponentService, ComponentService>();
        serviceCollection.AddScoped<IReservationService, ReservationService>();
        serviceCollection.AddScoped<IDashboardService, DashboardService>();
        serviceCollection.AddScoped<IImageService, ImageService>();
    }
}