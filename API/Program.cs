var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureAllExtensions(builder.Configuration);
builder.Services.InjectApplicationServices();

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy",
        policyBuilder =>
        {
            policyBuilder.AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials();
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseMiddleware<ExceptionMiddleware>();
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.SeedData();
app.UseStaticFiles(new StaticFileOptions()
{
    RequestPath = "/Images",
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images"))
});
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();