using Services.AutoMapper;
using StackExchange.Redis;

namespace API.Configurations;

public static class ConfigureExtensions
{
    public static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.OperationFilter<AddRequiredHeaderParameter>();
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {jwtSecurityScheme, Array.Empty<string>()}
            });
        });
    }

    public static void ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });
    }
    
    public static void ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string database = "Sql";
        var connectionString = configuration.GetConnectionString(database);
        
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
    }

    public static void ConfigureAutoMapper(this IServiceCollection serviceCollection)
    {
        var mapperConfig = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapperInitializer());
        });

        var mapper = mapperConfig.CreateMapper();
        serviceCollection.AddSingleton(mapper);
    }

    public static void ConfigureRedis(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string redis = "Redis";
        var connectionString = configuration.GetConnectionString(redis);
        
        var redisConnection = ConnectionMultiplexer.Connect(connectionString);
        serviceCollection.AddSingleton<IConnectionMultiplexer>(redisConnection);

        var redisDb = redisConnection.GetDatabase(0);
        serviceCollection.AddSingleton(redisDb);
    }
}