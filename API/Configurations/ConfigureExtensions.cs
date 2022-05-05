namespace API.Configurations;

public static class ConfigureExtensions
{
    public static void ConfigureAllExtensions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.ConfigureDatabase(configuration);
        serviceCollection.ConfigureIdentity();
        serviceCollection.ConfigureAutoMapper();
        serviceCollection.ConfigureRedis(configuration);
        serviceCollection.ConfigureAuthentication(configuration);
        serviceCollection.ConfigureSwagger();
        serviceCollection.ConfigureLogger();
    }

    private static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            //options.OperationFilter<AddRequiredHeaderParameter>();
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Please enter token"
            };
            
            options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    private static void ConfigureIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        serviceCollection.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // Password settings.
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
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

    private static void ConfigureLogger(this IServiceCollection serviceCollection)
    {
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        serviceCollection.AddSingleton(logger);
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

    private static void ConfigureRedis(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string redis = "Redis";
        var connectionString = configuration.GetConnectionString(redis);

        var redisConnection = ConnectionMultiplexer.Connect(connectionString);
        serviceCollection.AddSingleton<IConnectionMultiplexer>(redisConnection);

        var redisDb = redisConnection.GetDatabase(0);
        serviceCollection.AddSingleton(redisDb);
    }
}