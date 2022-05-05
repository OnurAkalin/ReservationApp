﻿namespace API;

public static class Configurations
{
    public static void InjectApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
    }

    public static void ConfigureAllExtensions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.ConfigureDatabase(configuration);
        serviceCollection.ConfigureIdentity();
        serviceCollection.ConfigureRedis(configuration);
        serviceCollection.ConfigureAutoMapper();
        serviceCollection.ConfigureLogger();
        serviceCollection.ConfigureAuthentication(configuration);
        serviceCollection.ConfigureSwagger();
    }

    private static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Put **ONLY** your JWT token on text box below, **Sample:** eyJhbGci....",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {jwtSecurityScheme, Array.Empty<string>()}
            });
        });
    }

    private static void ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            
        }).AddJwtBearer(options =>
        {
            options.Audience = tokenOptions.Audience;
            options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });
    }

    private static void ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string database = "Sql";
        var connectionString = configuration.GetConnectionString(database);

        serviceCollection.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connectionString); });
    }

    private static void ConfigureIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        serviceCollection.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // Password settings.
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        });
    }

    private static void ConfigureRedis(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string redis = "Redis";
        var connectionString = configuration.GetConnectionString(redis);

        var redisConnection = ConnectionMultiplexer.Connect(connectionString);
        serviceCollection.AddSingleton<IConnectionMultiplexer>(redisConnection);

        var redisDb = redisConnection.GetDatabase(0);
        serviceCollection.AddSingleton(redisDb);
    }

    private static void ConfigureAutoMapper(this IServiceCollection serviceCollection)
    {
        var mapperConfig = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapperInitializer());
        });

        var mapper = mapperConfig.CreateMapper();
        serviceCollection.AddSingleton(mapper);
    }

    private static void ConfigureLogger(this IServiceCollection serviceCollection)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        serviceCollection.AddSingleton(logger);
    }
}