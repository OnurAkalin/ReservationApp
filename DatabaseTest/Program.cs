using Infrastructure;
using Infrastructure.SeedData;

using var dbContext = new ApplicationDbContext(); 
ApplicationDbInitializer.SeedData(dbContext);