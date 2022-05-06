namespace API.AppServices;

public static class ServiceRegistration
{
    public static void InjectApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
    }
}